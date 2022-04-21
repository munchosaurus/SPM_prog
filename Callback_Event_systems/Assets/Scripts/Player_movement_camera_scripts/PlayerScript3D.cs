using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;
//using Mirror;

public class PlayerScript3D : MonoBehaviour
{
    [SerializeField] private State[] states;
    private MyRigidbody3D myRigidbody;
    private StateMachine stateMachine;
    public float jumpForce = 10f;
    public float acceleration  = 10f;
    public Camera mainCamera;
    public bool firstPerson;
    [SerializeField] private LayerMask layerMask;
    
    void Start()
    {

        myRigidbody = GetComponent<MyRigidbody3D>();
        if(states.Length > 0)    
            stateMachine = new StateMachine(this,states);
        
            Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    { 
        
        if (states.Length > 0)      
            stateMachine.Update();
        RaycastHit hit;
        if(Physics.SphereCast(gameObject.transform.Find("Main Camera").transform.position,0.1f,gameObject.transform.Find("Main Camera").transform.forward,out hit,300,layerMask))
        {
            if(Input.GetMouseButtonDown(0))
            {
                hit.collider.GetComponent<Health>().Die();
            }

        }


        
    }
    
    
    //Returns myRigidbody
    public MyRigidbody3D MyRigidbody3D => myRigidbody;
}
