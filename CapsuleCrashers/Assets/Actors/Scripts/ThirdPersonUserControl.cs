using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum PlayerNumber { Player_1, Player_2, Player_3, Player_4}
[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    public PlayerNumber playerNum;
    public string playerPrefix;
    private ThirdPersonCharacter m_Character;           // A reference to the ThirdPersonCharacter on the object
    private Rigidbody m_Rigidbody;                      // A reference to the Rigidbody on the object
    private ActorEquipment actorEquipment;              // A reference to the ActorEquipment on the object
    private ActorInteraction actorInteraction;          // A reference to the ActorInteractionManager on this object
    private Transform m_Cam;                            // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;                       // The current forward direction of the camera
    private Vector3 m_Move;                             // The direction of movement as given by the input
    private bool m_Jump;                                // the world-relative desired move direction, calculated from the camForward and user input.
    public int playerPos;

        
    private void Start()
    {
        switch (playerNum)
        {
            case PlayerNumber.Player_1:
                playerPrefix = "p1";
                playerPos = 1;
                break;

            case PlayerNumber.Player_2:
                playerPrefix = "p2";
                playerPos = 2;
                break;
            case PlayerNumber.Player_3:
                playerPrefix = "p3";
                playerPos = 3;
                break;
            case PlayerNumber.Player_4:
                playerPrefix = "p4";
                playerPos = 4;
                break;
            default:
                break;
        }

        // get the transform of the main camera
        m_Cam = GameObject.FindWithTag("MainCamera").transform;
        m_CamForward = m_Cam.forward;

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_Rigidbody = GetComponent<Rigidbody>();
        actorEquipment = GetComponent<ActorEquipment>();
        actorInteraction = GetComponent<ActorInteraction>();
    }

    private void Update()
    {
        m_Character.enabled = false;

        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown(playerPrefix + "Jump");
        }

        if (Input.GetButtonDown(playerPrefix + "Grab"))
        {
            actorEquipment.GrabItem();
        }

        //interactionManager.BuildInput(Input.GetButton(playerPrefix + "Build"));
        actorInteraction.RaycastInteraction(Input.GetButtonDown(playerPrefix + "Build"));            
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs

        float h = CrossPlatformInputManager.GetAxis(playerPrefix + "Horizontal");
        float v = CrossPlatformInputManager.GetAxis(playerPrefix + "Vertical");

        Vector3 direction = new Vector3(Input.GetAxis(playerPrefix + "RightStickX"),0, Input.GetAxis(playerPrefix + "RightStickY"));

        float primary = Input.GetAxis(playerPrefix + "Fire1");
        float secondary = Input.GetAxis(playerPrefix + "Fire2");

        bool crouch = Input.GetButton(playerPrefix + "Crouch");

        m_Move = new Vector3 (h, 0,v);

        // pass all parameters to the character control script
        if (direction.normalized != Vector3.zero)
        {
            m_Character.Turning(direction);
        }
        else if(m_Rigidbody.velocity.x != 0 || m_Rigidbody.velocity.z != 0)
        {
            Vector3 lookVelocity = new Vector3(m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
            lookVelocity = m_Cam.InverseTransformDirection(lookVelocity);      
            m_Character.Turning(lookVelocity);
        }

        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;

        m_Character.UseEquipment(primary, secondary);

    }
}

