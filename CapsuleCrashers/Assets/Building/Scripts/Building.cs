using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public List<BaseWall> wallList = new List<BaseWall>();
    public List<BaseFloor> floorList = new List<BaseFloor>();

    private void Start()
    {
        BaseWall[] tempWall = transform.GetComponentsInChildren<BaseWall>();

        for (int i = 0; i < tempWall.Length; i++)
        {
            wallList.Add(tempWall[i]);
        }

        BaseFloor[] tempFloor = transform.GetComponentsInChildren<BaseFloor>();

        for (int i = 0; i < tempFloor.Length; i++)
        {
            floorList.Add(tempFloor[i]);
        }
    }
}
