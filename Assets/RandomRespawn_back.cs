using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomRespawn_back : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;

    // ��ȯ�� Object
    public GameObject capsul;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;

        // NavMesh �󿡼� ��ȿ�� ��ġ ã��
        NavMeshHit hit;
        if (NavMesh.SamplePosition(respawnPosition, out hit, 1.0f, NavMesh.AllAreas))
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
            yield return new WaitForSeconds(1f);

            GameObject instantCapsul = Instantiate(capsul, Return_RandomPosition(), Quaternion.Euler(0, 270, 0));
        }
    }
}
