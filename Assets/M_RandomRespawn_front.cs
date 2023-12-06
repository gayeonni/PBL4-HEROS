using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class M_RandomRespawn_front : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject capsul;
    public GameObject emptyGameObject;
    public string count;
    public int nowPeople = 0;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = UnityEngine.Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = UnityEngine.Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;

        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(respawnPosition, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            respawnPosition = hit.position;
        }

        return respawnPosition;
    }

    private void Start()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        while (true)
        {
            yield return StartCoroutine(GetMySQLData());

            //nowPeople = Convert.ToInt32(count);

            for (int i = 0; i < nowPeople; i++)
            {
                GameObject instantCapsul = Instantiate(capsul, Return_RandomPosition(), Quaternion.Euler(0, 90, 0));
                instantCapsul.transform.parent = emptyGameObject.transform;
                Debug.Log("Success to make people");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator GetMySQLData()
    {
        string serverPath = "http://localhost/PBLUnityDB/display.php";

        using (UnityWebRequest www = UnityWebRequest.Get(serverPath))
        {

            yield return www.SendWebRequest();

            // 응답 처리
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request successful!");
                count = www.downloadHandler.text;
                Debug.Log(count);

                if (int.TryParse(count, out nowPeople))
                {
                    // 변환에 성공한 경우 countAsInt 변수에 저장
                    Debug.Log("Converted to int: " + nowPeople);
                }
                else
                {
                    // 변환에 실패한 경우
                    Debug.LogError("Failed to convert to int: " + count);
                }
            }
            else
            {
                Debug.LogError("Request failed. Error: " + www.error);
            }
        }


    }

}