# -*- encoding: euc-kr-*-
from pymongo import MongoClient
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

import pandas as pd 
from datetime import datetime, timezone, timedelta

import pymysql

track_history = defaultdict(lambda: [])

current_people = 0
korea_timezone = timezone(timedelta(hours=9))

#connection db information
conn = pymysql.connect(host='127.0.0.1', user='pbl_guest',passwd='ghkdlxld1042~!' , db='pbl4', charset='utf8')
cur = conn.cursor()


def is_inside_polygon(point, polygon):
    return polygon.contains(Point(point))

def save_data (current_people, now_time):
    
    check_time = now_time
    people = current_people
    
   
    sql_query = f"INSERT INTO count_people (NOW_TIME, COUNT ) VALUES ('{check_time}', '{people}')"
    cur.execute(sql_query)
    
    conn.commit()

    
    

def run(
    weights='yolov8n.pt',
    device='cpu',
    view_img=True,
    line_thickness=2,
    track_thickness=2,
    region_thickness=2,
):
    vid_frame_count = 0
    
    data_count = 0
    data_count_max = 100
   
    

    # Setup Model
    model = YOLO(f'{weights}')
    model.to('cuda') if device == '0' else model.to('cpu')

    # Video setup
    videocapture = cv2.VideoCapture(0)
    frame_width, frame_height = int(videocapture.get(3)), int(videocapture.get(4))
    fps, fourcc = int(videocapture.get(5)), cv2.VideoWriter_fourcc(*'mp4v')

    # Iterate over video frames
    while videocapture.isOpened():
        success, frame = videocapture.read()
        if not success:
            break
        vid_frame_count += 1

        
        people_count = 0 
        now_time = datetime.now(korea_timezone).strftime("%Y/%m/%d %H:%M:%S") 
        
        try: 
            results = model.track(frame, persist=True)
            boxes = results[0].boxes.xywh.cpu()
            track_ids = results[0].boxes.id.int().cpu().tolist()
            clss = results[0].boxes.cls.cpu().tolist()
            names = results[0].names
        except Exception as e:
            print('error:', e)
            pass

        annotator = Annotator(frame, line_width=line_thickness, example=str(names))

        for box, track_id, cls in zip(boxes, track_ids, clss):
            x, y, w, h = box
            label = str(names[cls])
            if label == 'person':
                people_count += 1 
                save_data(people_count, now_time)
                data_count +=1
                
            xyxy = (x - w / 2), (y - h / 2), (x + w / 2), (y + h / 2)

            
            bbox_color = colors(cls, True)
            annotator.box_label(xyxy, label, color=bbox_color)
            
        
 
        cv2.putText(frame, f"inside: {people_count}", (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 0, 255), 2)
        #print(people_count)
        
        if view_img:
            if vid_frame_count == 1:
                cv2.namedWindow('Ultralytics YOLOv8 Region Counter Movable')
            cv2.imshow('Ultralytics YOLOv8 Region Counter Movable', frame)

        if cv2.waitKey(1) & 0xFF == ord('q'):
            conn.close()
            break

    del vid_frame_count
    cv2.destroyAllWindows()
    

def main():
    """Main function."""
    run()
    
    
if __name__ == '__main__':
    
    #opt = parse_opt()
    main()

    
    
        



