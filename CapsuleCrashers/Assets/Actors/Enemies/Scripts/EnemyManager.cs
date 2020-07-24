using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager {

    public void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.transform.tag == "Player")
        {
            //collision.collider.gameObject.GetComponent<HealthManager>().TakeDamage(3);
            
        }

    }
}
