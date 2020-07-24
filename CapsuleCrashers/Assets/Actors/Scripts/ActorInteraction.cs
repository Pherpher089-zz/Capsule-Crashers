using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handels an actors interactions with the base and Items
/// </summary>
public class ActorInteraction : MonoBehaviour {

    //Base building interaction
    ActorEquipment actorEquipment;  
    bool buildPosition;
    bool buildInput;
    int matIndex;

    int interactLayer;              // The layer that all interactive objects lives
    int buildLayer;                 // The layer that all buildable objects live
    //Door interaction
    bool doorInput;

    public void Awake()
    {
        actorEquipment = GetComponent<ActorEquipment>();
    }

    public void Start()
    {
        interactLayer = LayerMask.GetMask("Interact");
        buildLayer = LayerMask.GetMask("Build");
    }

    public void DoorInput(bool input)
    {
        doorInput = input;
    }

    private void Update()
    {
       

        if(actorEquipment.hasItem)
        {
            matIndex = actorEquipment.equipedItem.GetComponent<Item>().matIndex;//TODO shouldnt be calling getcomponent ever frame
        }
        else
        {
            matIndex = -1;
        }
        
    }

    //public void BaseWallInteraction(GameObject wall)
    //{
    //    Vector3 direction = wall.transform.position - transform.position;
    //    float angle = Vector3.Angle(transform.forward, direction);

    //    if (angle < 70)
    //    {
    //        wall.layer = 10;
    //        wall.GetComponent<BaseWall>().interaction = true;
    //        buildPosition = true;
    //        Build(wall);
    //    }
    //    else
    //    {
    //        wall.GetComponent<BaseWall>().interaction = false;
    //        buildPosition = false;
    //    }
    //}

    public void RaycastInteraction(bool interact)
    {
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward * 4);

        RaycastHit hit;

        Debug.DrawRay(transform.position + Vector3.up, transform.forward * 4, Color.red);

        if (Physics.Raycast(ray, out hit, 4, buildLayer, QueryTriggerInteraction.Collide))
        {
            if(hit.collider.tag == "WallFrame")
            {
                bool built;
                if (interact)
                {
                    built = hit.collider.gameObject.GetComponent<InteractionManager>().Interact(matIndex);
                    if(built)
                    {
                        actorEquipment.UnequiptItem(true);
                    }
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 4, interactLayer, QueryTriggerInteraction.Collide))
        {
            if (interact)
            {
                hit.collider.gameObject.GetComponent<InteractionManager>().Interact(0);
            }
        }
    }

    /// <summary>
    /// A chck to see if the actor is holding the correct material needed to build. Returns false if the material is incorrect
    /// </summary>
    /// <param name="wall"></param>
    /// <returns> True if the material is correct for the build action that is being attempted</returns>
    //private bool CheckBuildingMaterial(GameObject wall)
    //{

    //    BaseWall baseWall = wall.GetComponent<BaseWall>();

    //    if(baseWall.wallLevel == 0)
    //    {
    //        if(actorEquipment.equipedItem.gameObject.tag == "Logs")
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if(baseWall.wallLevel == 1)
    //    {
    //        if (actorEquipment.equipedItem.gameObject.tag == "Logs")
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        if (actorEquipment.equipedItem.gameObject.tag == "Logs")
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}
}
