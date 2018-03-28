using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public int m_PlayerNumber = 1;

    [SerializeField]
    private Rigidbody m_Shell;
    [SerializeField]
    private Transform m_FireTransform;
    [SerializeField]
    private float m_MinLaunchForce = 15f;
    [SerializeField]
    private float m_MaxLaunchForce = 30f;
    [SerializeField]
    private float m_MaxChargeTime = 0.75f;

    private string m_FireButton;
    private float m_CurrentLaunchForce;
    private float m_ChargeSpeed;
    private bool m_Fired;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
    }
    // Use this for initialization
    void Start ()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
        m_Fired = false;
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        else if(Input.GetButtonDown(m_FireButton))
        {
            
            m_CurrentLaunchForce = m_MinLaunchForce;

        }
        else if(Input.GetButton(m_FireButton) && !m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
        }
        else if(Input.GetButtonUp (m_FireButton) && !m_Fired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        m_Fired = true;
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

        m_CurrentLaunchForce = m_MinLaunchForce;

        StartCoroutine(CoolDown());

    }

    public IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(3);
        m_Fired = false;
    }
}
