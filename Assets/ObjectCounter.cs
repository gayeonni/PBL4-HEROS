using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    public Plane countingPlane; // Unity �����Ϳ��� ������ ���

    public int GetObjectsOnPlaneCount()
    {
        // ��� ���� ��� ������Ʈ ��������
        GameObject[] objectsInScene = GameObject.FindGameObjectsWithTag("Player");

        // ��� ���� �ִ� ������Ʈ �� ����
        int objectsOnPlane = 0;

        foreach (GameObject obj in objectsInScene)
        {
            if (IsObjectOnPlane(obj.transform.position))
            {
                objectsOnPlane++;
            }
        }

        // ��� ��� �Ǵ� Ȱ��
        return objectsOnPlane;
    }

    // ��� ���� �ִ��� ���θ� Ȯ���ϴ� �Լ�
    private bool IsObjectOnPlane(Vector3 position)
    {
        return countingPlane.GetDistanceToPoint(position) == 0f;
    }
}


