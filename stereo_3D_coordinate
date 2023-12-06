import argparse
from collections import defaultdict
from pathlib import Path

import cv2
import numpy as np
from shapely.geometry import Polygon
from shapely.geometry.point import Point

from ultralytics import YOLO
from ultralytics.utils.files import increment_path
from ultralytics.utils.plotting import Annotator, colors

# 내부 매개변수 및 카메라 행렬추가
fx, fy, cx, cy = 1000.0, 1000.0, 320.0, 240.0
camera_matrix = np.array([[fx, 0, cx], [0, fy, cy], [0, 0, 1]])

# 3D 좌표 계산 함수
def get_3d_coordinates(image_points, depth):
    # 객체의 3D 모델 정의
    object_points = np.array([[0, 0, 0], [1, 0, 0], [0, 1, 0], [1, 1, 0]], dtype=np.float32)
    # 영상 내 바운딩 박스의 네 꼭짓점 좌표
    image_points = np.array(image_points, dtype=np.float32).reshape(-1, 1, 2)

    if len(image_points) >= 4:
        # 카메라의 위치, 방향을 추정해 회전 벡터, 이동 벡터 획득
        _, rvec, tvec = cv2.solvePnP(objectPoints=object_points, imagePoints=image_points, cameraMatrix=camera_matrix, distCoeffs=None)
        # 카메라 좌표계로 반환
        rotation_matrix, _ = cv2.Rodrigues(rvec)
        translation_vector = tvec.flatten()
        translation_vector *= depth
        camera_coordinates = np.dot(rotation_matrix.T, -translation_vector)

        # X, Y, Z 좌표 반환
        X, Y, Z = camera_coordinates
        return X, Y, Z
    else:
        print("Not enough points to calculate 3D coordinates.")
        return None

def run(weights='yolov8x.pt', device='cpu', view_img=True, line_thickness=2):
    vid_frame_count = 0

    model = YOLO(f'{weights}')
    model.to('cuda') if device == '0' else model.to('cpu')

    videocapture = cv2.VideoCapture(0)

    while videocapture.isOpened():
        success, frame = videocapture.read()
        if not success:
            break
        vid_frame_count += 1

        people_count = 0

        results = model.track(frame, persist=True)

        if results is not None and results[0].boxes is not None:
            boxes = results[0].boxes.xywh.cpu()
            track_ids = results[0].boxes.id.int().cpu().tolist()
            clss = results[0].boxes.cls.cpu().tolist()
            names = results[0].names

            annotator = Annotator(frame, line_width=line_thickness, example=str(names))

            for box, track_id, cls in zip(boxes, track_ids, clss):
                x, y, w, h = box
                label = str(names[cls])
                if label == 'person':
                    people_count += 1
                    xyxy = (x - w / 2), (y - h / 2), (x + w / 2), (y + h / 2)
                    bbox_color = colors(cls, True)
                    annotator.box_label(xyxy, label, color=bbox_color)

                    # 수정된 부분: 3D 좌표 출력
                    depth = 10.0  # 예제 깊이 값
                    # 클릭할 필요 없음. 이미지 좌표 직접 사용
                    clicked_points = [(x, y), (x + w, y), (x, y + h), (x + w, y + h)]
                    # 프레임에서 감지된 객체의 3D 좌표 출력
                    X, Y, Z = get_3d_coordinates(clicked_points, depth)
                    if X is not None:
                        print(f"3D Coordinates for person {track_id}: X={X}, Y={Y}, Z={Z}")

            cv2.putText(frame, f"inside: {people_count}", (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 0, 255), 2)

        if view_img:
            if vid_frame_count == 1:
                cv2.namedWindow('Ultralytics YOLOv8 Region Counter Movable')

            cv2.imshow('Ultralytics YOLOv8 Region Counter Movable', frame)

        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    del vid_frame_count
    cv2.destroyAllWindows()

def main():
    run()

if __name__ == '__main__':
    main()
