using UnityEngine.Events;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int maxHealth = 5;
    public int health = 5;
    GameObject shotEffectPrefab;
    GameObject bleedingEffectPrefab;
    Collider col;
    public UnityEvent m_onDeath;

    public void Awake()
    {
        shotEffectPrefab = Resources.Load("ShotEffect") as GameObject;
        bleedingEffectPrefab = Resources.Load("BleedingEffect") as GameObject;
        col = GetComponent<Collider>();
    }

    public void Start()
    {
        health = maxHealth;
        if (m_onDeath == null)
        {
            m_onDeath = new UnityEvent();
        }
    }

    public void TakeDamage(int damage)
    {

        Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        Instantiate(bleedingEffectPrefab, transform.position, transform.rotation, transform);
        health -= damage;

        if (health < 0)
        {
            health = 0;
            m_onDeath.Invoke();
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
