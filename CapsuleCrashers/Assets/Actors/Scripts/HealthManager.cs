using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public int maxHealth = 5;
    public int health = 5;
    GameObject shotEffectPrefab;
    GameObject bleedingEffectPrefab;
    Collider col;

    public void Awake()
    {
        shotEffectPrefab = Resources.Load("ShotEffect") as GameObject;
        bleedingEffectPrefab = Resources.Load("BleedingEffect") as GameObject;
        col = GetComponent<Collider>();
    }

    public void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {

        Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        Instantiate(bleedingEffectPrefab,transform.position,transform.rotation, transform);
        health -= damage;

        if (health < 0)
        {
            health = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool")
        {
            if (other.gameObject.GetComponent<Tool>().isAttacking)
            {
                TakeDamage(1);
            }
        }

        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }

    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.collider.gameObject.tag == "Tool")
    //    {
    //        TakeDamage(1);
    //    }

    //    if (other.collider.gameObject.tag == "Bullet")
    //    {
    //        TakeDamage(1);
    //    }

    //}
}
