using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int m_PlayerNumber = 1;
    [SerializeField]
    private float m_Speed = 10f, m_TurnSpeed = 180f;
    [SerializeField]
    private AudioSource m_MovementAudio;
    [SerializeField]
    private AudioClip m_ButterflyMovement;
    [SerializeField]
    private float m_PitchRange = 0.2f;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;

	// Use this for initialization
	void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();	
	}

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;

        m_MovementInputValue = 0;
        m_TurnInputValue = 0;
        
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }


    // Update is called once per frame
    void Update () {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        ButterflyAudio();
	}

    private void ButterflyAudio()
    {
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            m_MovementAudio.clip = m_ButterflyMovement;
            m_MovementAudio.Play();
        }
        else
        {
            m_MovementAudio.clip = m_ButterflyMovement;
            m_MovementAudio.pitch = UnityEngine.Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
            m_MovementAudio.Play();
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
