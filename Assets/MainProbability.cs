using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainProbability : MonoBehaviour
{
    public theNumberOfPeople objectCounter; // Unity 에디터에서 설정할 ObjectCounter 컴포넌트
    public Text accidentProbabilityText;

    void Update()
    {
        if (objectCounter == null)
        {
            Debug.LogWarning("ObjectCounter 컴포넌트가 할당되지 않았습니다. Unity 에디터에서 ObjectCounter 컴포넌트를 할당해주세요.");
            return;
        }

        int objectsOnPlane = objectCounter.GetObjectsOnPlaneCount();

        // 사고 발생 확률 계산
        float accidentProbability = (float)objectsOnPlane / 960f * 100f;

        // 사고 발생 확률을 Text UI에 표시
        accidentProbabilityText.text = accidentProbability.ToString("F2");

    }
}