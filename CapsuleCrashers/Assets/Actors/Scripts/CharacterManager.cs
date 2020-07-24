using UnityEngine;

public enum ActorState {Alive, Dead}

[RequireComponent (typeof (HealthManager))]
[RequireComponent (typeof (Collider))]
public class CharacterManager : MonoBehaviour {

    public ActorState actorState;
    HealthManager healthManager;
    GameObject deathEffectPrefab;
    ThirdPersonCharacter thirdPersonCharacter;
    Collider col;

    public bool inBuilding;
    public GameObject currentBuildingObj;

    int floorLayer;

    public void Awake()
    {
        col = GetComponent<Collider>();
        deathEffectPrefab = Resources.Load("DeathEffect") as GameObject;
        healthManager = GetComponent<HealthManager>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }

    public void Start()
    {
        floorLayer = LayerMask.NameToLayer("Floor");
        actorState = ActorState.Alive;
    }

    public void Update()
    {
        CharacterStateMachine();
    }

    public void CharacterStateMachine()
    {
        switch (actorState)
        {
            case ActorState.Alive:
                CheckCharacterHealth();
                break;

            case ActorState.Dead:
                KillCharacter();
                break;

            default:
                break;
        }
    }

    private void KillCharacter()
    {
        Instantiate(deathEffectPrefab, transform.position, transform.rotation);

        GameObject.Destroy(this.gameObject);
    }

    private void CheckCharacterHealth()
    {
        if (healthManager.health <= 0)
        {
            actorState = ActorState.Dead;
        }
    }



    private void OnCollisionStay(Collision collision)
    {
        if(thirdPersonCharacter.m_IsGrounded)
        {
            if (collision.collider.gameObject.layer == floorLayer)
            {
                inBuilding = true;

                if (currentBuildingObj != collision.collider.transform.parent.gameObject)
                {
                    currentBuildingObj = collision.collider.transform.parent.gameObject;
                }
            }
            else
            {
                inBuilding = false;
                currentBuildingObj = null;
            }
        }
    }

}
