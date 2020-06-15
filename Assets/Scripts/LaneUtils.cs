using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneUtils : MonoBehaviour
{
    public static LaneUtils Instance;
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public GameObject[] laneList;
    public int laneStartPosition;
    public int laneEndPosition;


    public int getNearestLaneIndex(Vector3 position)
    {
        for (int i = 0; i < laneList.Length; i++)
        {
            if (Mathf.Abs(laneList[i].transform.localPosition.y - position.y) < 80)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 getLaneStartPosition(GameObject lane)
    {
        Vector3 lanePosition = lane.transform.localPosition;
        return new Vector3(laneStartPosition, lanePosition.y, 0);
    }

    public Vector3 getLaneStartPosition(int laneIdx)
    {
        return getLaneStartPosition(laneList[laneIdx]);
    }
    public Vector3 getLaneEndPosition(GameObject lane)
    {
        Vector3 lanePosition = lane.transform.localPosition;
        return new Vector3(laneEndPosition, lanePosition.y, 0);
    }

    public Vector3 getLaneEndPosition(int laneIdx)
    {
        return getLaneEndPosition(laneList[laneIdx]);
    }
}
