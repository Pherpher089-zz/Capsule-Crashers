using UnityEngine;

public enum ItemOwner { Player, Enemy, Other, Null }//TODO - plug this in for each item

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class Item : MonoBehaviour {

    public ItemOwner itemOwner;
    public bool isEquiped;
    public int matIndex;
    public Rigidbody rigidbodyRef;
    public MeshCollider col;
    public Collider ignoredCollider;

    public void Awake()
    {
        col = GetComponent<MeshCollider>();
        rigidbodyRef = GetComponent<Rigidbody>();
        col.convex = true;
    }

    private void Start()
    {
        col.convex = true;
        col.inflateMesh = true;
        col.skinWidth = 0.1f;
    }

    public virtual void OnEquipt(GameObject character)
    {
       
        if (character.CompareTag("Player"))
        {
            itemOwner = ItemOwner.Player;
        }
        else if( character.CompareTag("Enemy"))
        {
            itemOwner = ItemOwner.Enemy;
        }
        else
        {
            itemOwner = ItemOwner.Other;
        }

        ignoredCollider = character.gameObject.GetComponent<Collider>();
        Physics.IgnoreCollision(col, ignoredCollider);
        rigidbodyRef.isKinematic = true;
        col.isTrigger = true;
        isEquiped = true;

    }

    public virtual void OnUnequipt()
    {
        Physics.IgnoreCollision(col, ignoredCollider, false);
        col.isTrigger = false;
        rigidbodyRef.isKinematic = false;
        isEquiped = false;
    }

    public virtual void PrimaryAction(float input)
    {

    }

    public virtual void SecondaryAction(float input)
    {

    }
}
