using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorEquipment : MonoBehaviour {
    public GameObject equipedItem;
    public bool hasItem;
    private GameObject newItem;
    private Transform socket;
    private List<GameObject> grabableItems = new List<GameObject>();

    public void Start()
    {
        hasItem = false;
        socket = transform.Find("HandSocket");

        if(equipedItem)
            EquipItem(equipedItem); 
    }

    public void EquipItem(GameObject item)
    {
        hasItem = true;
        //equipedItem = item;
        equipedItem = Instantiate(item, socket.position, socket.rotation, socket);
        equipedItem.GetComponent<Item>().OnEquipt(this.gameObject);
        //TODO set up equipment object swap on the player
    }

    public void UnequiptItem()
    {
        hasItem = false;
        equipedItem.GetComponent<Item>().OnUnequipt();
        equipedItem.transform.parent = null;
    }

    public void UnequiptItem(bool spendItem)
    {
        hasItem = false;
        equipedItem.GetComponent<Item>().OnUnequipt();
        Destroy(equipedItem.gameObject);
    }

    public void SpendItem()
    {
        UnequiptItem(true);
    }

    /// <summary>
    /// Finds all not equiped items in the sceene that are close enough to the player to grab and
    /// adds them to the grabableItems list. This funtion also returns the closest
    /// </summary>
    GameObject GatherAllItemsInScene()
    {
        Item[] allItems = GameObject.FindObjectsOfType<Item>();
        GameObject closestItem = null;
        float closestDist = 5;
        foreach (Item item in allItems)
        {
            if(!item.isEquiped)
            {
                float currentItemDist =  Vector3.Distance(transform.position, item.gameObject.transform.position);

                if (currentItemDist < 3)
                {
                    if(currentItemDist < closestDist)
                    {
                        //TODO check for player direction as well to stop players from picking up unintended items

                        closestDist = currentItemDist;
                        closestItem = item.gameObject;
                    }

                    grabableItems.Add(item.gameObject);//TODO a list?
                }
            }
        }

        if(grabableItems.Count <=0)
            return null;
        else
            return closestItem;
    }

    public bool GrabItem()
    {
        newItem = GatherAllItemsInScene();

        if (hasItem)
            UnequiptItem();

        if (newItem != null)
        {
            EquipItem(newItem);
            Destroy(newItem);
            return true;
        }
        else
        {
            return false;
        }
    }
}
