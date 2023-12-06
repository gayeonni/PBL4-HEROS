// ColorChanger.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public theNumberOfPeople objectCounter; // Unity 에디터에서 설정할 ObjectCounter 컴포넌트
    public Image color1Image; // Unity 에디터에서 설정할 Image 컴포넌트
    public Image color2Image;
    public Image color3Image;
    public Image color4Image;

    private void Update()
    {
        if (objectCounter == null)
        {
            Debug.LogWarning("ObjectCounter 컴포넌트가 할당되지 않았습니다. Unity 에디터에서 ObjectCounter 컴포넌트를 할당해주세요.");
            return;
        }

        int objectsOnPlane = objectCounter.GetObjectsOnPlaneCount();

        // 사람 수에 따라 Canvas의 Image UI의 색상 변경
        if (objectsOnPlane >= 150)
        {
            ChangeColor(color1Image, Color.red); // 빨간색 (Red)
            ChangeColor(color2Image, Color.red);
            ChangeColor(color3Image, Color.red);
            ChangeColor(color4Image, Color.red);
        }
        else if (objectsOnPlane >= 100)
        {
            ChangeColor(color1Image, new Color(1f, 0.5f, 0f)); // 주황색 (Orange)
            ChangeColor(color2Image, new Color(1f, 0.5f, 0f));
            ChangeColor(color3Image, new Color(1f, 0.5f, 0f));
            ChangeColor(color4Image, new Color(1f, 0.5f, 0f));
        }
        else if (objectsOnPlane >= 50)
        {
            ChangeColor(color1Image, Color.yellow); // 노란색 (Yellow)
            ChangeColor(color2Image, Color.yellow);
            ChangeColor(color3Image, Color.yellow);
            ChangeColor(color4Image, Color.yellow);
        }
        else
        {
            ChangeColor(color1Image, Color.green); // 초록색 (Green)
            ChangeColor(color2Image, Color.green);
            ChangeColor(color3Image, Color.green);
            ChangeColor(color4Image, Color.green);
        }
    }

    // Image의 색상을 변경하는 함수
    private void ChangeColor(Image image, Color newColor)
    {
        if (image != null)
        {
            image.color = newColor;
        }
        else
        {
            Debug.LogWarning("Image 컴포넌트가 할당되지 않았습니다. Unity 에디터에서 Image 컴포넌트를 할당해주세요.");
        }
    }
}
