using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

    bool doorState;
    public bool interactinon;
    GameObject childDoor;
    Quaternion closedPos, openPos;
    InteractionManager interactionManager;

    public void Awake()
    {
        closedPos = Quaternion.Euler(transform.rotation.eulerAngles);
        openPos = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, -90, 0));
        interactionManager = GetComponent<InteractionManager>();//TODO set up error checking
    }

    public void OnEnable()
    {
        interactionManager.OnInteract += OpenDoor;
    }

    public void OnDisable()
    {
        interactionManager.OnInteract -= OpenDoor;
    }

    public bool OpenDoor(int i)
    {    
        if(transform.rotation == closedPos)
        {
            transform.rotation = openPos;
        }
        else
        {
            transform.rotation = closedPos;
        }

        return true;
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        interactinon = true;
    //        other.gameObject.SendMessage("DoorInteraction", this.gameObject);
    //    }
    //    else
    //    {
    //        interactinon = false;
    //    }
    //}
}
