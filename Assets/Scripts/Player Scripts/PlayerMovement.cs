using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Public variables
    public int m_PlayerNumber = 1;
    //Serialize fields
    [SerializeField]
    private float m_Speed = 10f, m_TurnSpeed = 180f;
    //Private variables
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    

	// Use this for initialization
	void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();	
	}

    //Setting the inputs and rigidbody once it's enabled
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;

        m_MovementInputValue = 0;
        m_TurnInputValue = 0;
        
    }
    //Making the player kinematic once it is disabled
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    //Setting the movement and turn axis's for each player
    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

    }

    // Update is called once per frame
    void Update () {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

	}

    private void FixedUpdate()
    {
        Move();
        Turn();
    }
    //Movement function
    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }
    //Turn funciton
    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
