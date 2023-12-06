using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Transform target; // agent가 찾아갈 타깃 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target is not assigned! Please assign a target in the Inspector.");
        }
        else
        {
            // 스크립트가 실행될 때 바로 목적지로 이동
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (target != null) // 'target'이 할당되었는지 체크
        {
            // agent에게 목적지를 알려주는 함수
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("Target is not assigned! Please assign a target in the Inspector.");
        }
    }
}
