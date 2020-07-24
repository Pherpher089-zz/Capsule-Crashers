using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum WallType {Basic, Door, Window}

public class BaseWall : MonoBehaviour {

    [Header("Wall Atributes")]
    [Tooltip("The type of frame the wall will be. This can be a basic wall, a wall with a door or, a door with a window")]
    public WallType wallType;
    [Tooltip("The level at which the wall will be built at the begining of the game.")]
    [Range(0,3)]public int startLevel = 0;

    BuildingManager buildingManager;                // A ref to the building manager
    InteractionManager interactionManager;          // A ref to this interactionManager
    int wallLevel = 0;                              // The current stage of the wall. 0 = base, 1 = frame, 2 = panels (not nessisaraly all pannels)
    bool isBuilding;                                // This is true untill the layer is built or the player releases the button;
    float buildValue;                               // The progress of the build from 0 to 1;

    [HideInInspector] public Transform target;

    [Header("Wall Durability")]
    List<WallLayerManager> wallLayerList = new List<WallLayerManager>();

    //Gizmo Vars
    public Mesh arowGiz;
    private BoxCollider boxCol;

    public void Awake()
    {
        buildingManager = GameObject.FindWithTag("GameController").GetComponent<BuildingManager>();
        interactionManager = GetComponent<InteractionManager>();
        boxCol = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        target = transform.Find("Target");
        InitializeBuildLevel();
    }

    private void Update()
    {
        foreach (WallLayerManager wall in wallLayerList)
        {
            if(wall.wallState == WallState.Destroyed)
            {
                DestroyWall(wall);
            }
        }
    }

    private void InitializeBuildLevel()
    {
        if (startLevel != 0)
        {
            if (wallType == WallType.Basic)
            {
                if (startLevel > 2)
                {
                    startLevel = 2;
                }
            }
            for (int i = 0; i < startLevel; i++)
            {
                BuildWall(1);
            }
        }
    }

    public void OnEnable()
    {
        interactionManager.OnInteract += BuildWall;
    }

    public void OnDisable()
    {
        interactionManager.OnInteract -= BuildWall;
    }

    public bool BuildWall(int materiaLIndex)
    {
        GameObject buildObj = null;

        switch (wallLevel) 
        { 
            case 0://BaseFrame

                if (materiaLIndex == 1)
                {
                     buildObj = Instantiate(buildingManager.BuildObject(wallType, wallLevel, 1), transform);
                }

                break;

            case 1://SubFrame

                if (materiaLIndex == 1)
                {
                     buildObj = Instantiate(buildingManager.BuildObject(wallType, wallLevel, 1), transform);
                }

                break;

            case 2://Panels

                if (materiaLIndex == 1)
                {
                     buildObj = Instantiate(buildingManager.BuildObject(wallType, wallLevel, 1), transform);
                }
                break;

            default:
                break;
        }

        if(buildObj != null)
        {
            wallLevel += 1;
            wallLayerList.Add(buildObj.GetComponent<WallLayerManager>());
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyWall(WallLayerManager wallManager)
    {
        wallLevel -= 1;
        wallLayerList.Remove(wallManager);
        GameObject.Destroy(wallManager.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 drawPos = transform.position + (transform.forward * 2) + (transform.right * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(arowGiz, drawPos, transform.rotation);
    }
}
