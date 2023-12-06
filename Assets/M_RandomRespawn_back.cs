using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_RandomRespawn_back : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;

    public GameObject capsul;

    public GameObject emptyGameObject;

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
            yield return new WaitForSeconds(1f);

            GameObject instantCapsul = Instantiate(capsul, Return_RandomPosition(), Quaternion.Euler(0, 270, 0));

            instantCapsul.transform.parent = emptyGameObject.transform;
        }
    }
}