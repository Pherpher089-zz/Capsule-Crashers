using UnityEngine;
using System.Collections;

public class WallLayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int health;
    GameObject hitEffectPrefab;

    void Awake()
    {
        hitEffectPrefab = Resources.Load("ShotEffect") as GameObject;
    }

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {

        Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        health -= damage;

        if (health < 0)
        {
            health = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tool")
        {
            if (other.gameObject.GetComponent<Tool>().isAttacking)
            {
                TakeDamage(1);
            }
        }
    }
}
