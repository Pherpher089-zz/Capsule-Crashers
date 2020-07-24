using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Tooltip ("In (Units/Sec), how fast will the camera position move to the target position. Lower numbers will slow this down and higher numbers speed it up.")]
    public float Smoothing;
    [Tooltip("The Game Object of focus. If nothing is assigned, it will focus on game object taged 'player'. Then if there is no player, it will log an error to the console.")]
    public Transform target;

    [HideInInspector]
    public CamShake camShake;
    GameObject camObj;
    Camera cam;
    
    public void Start()
    {
        if(target == null)
        {
            if(GameObject.FindWithTag("Player"))
           {
                target = GameObject.FindWithTag("Player").transform;
           }
        }
        else
        {
            Debug.Log(gameObject.name + " has no target object assigned");
        }

        camObj = transform.GetChild(0).gameObject;
        cam = camObj.GetComponent<Camera>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * Smoothing);
    }
}
