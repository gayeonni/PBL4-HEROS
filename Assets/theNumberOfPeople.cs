using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theNumberOfPeople : MonoBehaviour
{
    public UnityEngine.Plane countingPlane;
    public Text objectCountText;

    void Update()
    {
        UpdateObjectCountText();
    }

    void UpdateObjectCountText()
    {
        int objectsOnPlane = GetObjectsOnPlaneCount();
        objectCountText.text = objectsOnPlane.ToString();
    }

    public int GetObjectsOnPlaneCount()
    {
        GameObject[] objectsInScene = GameObject.FindGameObjectsWithTag("Player");
        int objectsOnPlane = 0;

        foreach (GameObject obj in objectsInScene)
        {
            if (IsObjectOnPlane(obj.transform.position))
            {
                objectsOnPlane++;
            }
        }

        return objectsOnPlane;
    }

    private bool IsObjectOnPlane(Vector3 position)
    {
        return countingPlane.GetDistanceToPoint(position) == 0f;
    }
}
