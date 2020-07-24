using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public List<GameObject> buildList = new List<GameObject>();
    Object[] resourceObjList;

    private void Awake()
    {
        resourceObjList = Resources.LoadAll("BuildObj", typeof(GameObject));

        int l = resourceObjList.Length;

        for (int i = 0; i < l; i++)
        {
            buildList.Add(resourceObjList[i] as GameObject);
        }
    }

    public GameObject BuildObject(WallType type, int buildLevel, int matIndex)
    {
        GameObject target = null;
        
        foreach (GameObject item in buildList)
        {
            WallLayerManager wallLayerManager = item.GetComponent<WallLayerManager>();

            if (wallLayerManager.wallType == type && wallLayerManager.requiredWallLevel == buildLevel && wallLayerManager.requiredMaterial == matIndex)
            {
                target = item;
            }
        }

        return target;
    }
}
