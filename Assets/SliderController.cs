using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;                   // 슬라이더 UI 요소
    public GameObject objectToDuplicate;    // 복제할 게임 오브젝트
    public Vector3 duplicationPosition;     // 복제 위치

    public float positionVariation = 0.1f;  // 위치 변화 정도

    private float previousSliderValue;      // 이전 슬라이더 값

    private void Start()
    {
        // 초기에 이전 슬라이더 값 설정
        previousSliderValue = slider.value;
    }

    private void Update()
    {
        // 슬라이더 값이 변경되면
        if (slider.value != previousSliderValue)
        {
            // 슬라이더 값에 따라 객체를 복제하고 위치를 랜덤하게 변화시킴
            if (slider.value > previousSliderValue)
            {
                // y값은 1로 고정하고 x와 z값은 랜덤한 값으로 설정
                Vector3 randomOffset = new Vector3(Random.Range(-positionVariation, positionVariation),
                                                   1f,
                                                   Random.Range(-positionVariation, positionVariation));
                Vector3 finalPosition = duplicationPosition + randomOffset;
                // 게임 오브젝트를 복제하고 랜덤 위치에 배치
                Instantiate(objectToDuplicate, finalPosition, Quaternion.identity);
            }
            else
            {
                // y값은 1로 고정하고 x와 z값은 랜덤한 값으로 설정
                Vector3 randomOffset = new Vector3(Random.Range(-positionVariation, positionVariation),
                                                   1f,
                                                   Random.Range(-positionVariation, positionVariation));
                Vector3 finalPosition = duplicationPosition + randomOffset;
                // 게임 오브젝트를 복제하고 랜덤 위치에 배치
                Instantiate(objectToDuplicate, finalPosition, Quaternion.identity);
            }

            // 이전 슬라이더 값 업데이트
            previousSliderValue = slider.value;
        }
    }
}