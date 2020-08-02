using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script controls the source objects from which players will gather resources from.
/// </summary>
public class SourceObject : MonoBehaviour {

    public int hitPoints;     //the objects Hit points
    public int maxHitPoints;
    public GameObject yieldedRes;          //the resource object that is droped 

    public void Update()
    {
        if (hitPoints <= 0)
            YieldAndDie();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tool")
        {
            //TODO add is swinging criteria
            if(other.gameObject.GetComponent<Tool>().isAttacking)
            {
                TakeDamage(1);
            }
        }
    }

    void TakeDamage(int damage)
    {
        hitPoints -= damage;
    }

    void YieldAndDie()
    {
        GameObject yieldClone = Instantiate(yieldedRes, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
