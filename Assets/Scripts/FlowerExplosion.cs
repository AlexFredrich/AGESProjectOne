using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerExplosion : MonoBehaviour {

    [SerializeField]
    private LayerMask m_ButterflyMask;
    [SerializeField]
    private ParticleSystem m_ImpactParticles;
    [SerializeField]
    private float m_ImpactForce = 1000f;
    [SerializeField]
    private float m_MaxLifeTime = 2f;
    [SerializeField]
    private float m_ImpactRadius = 5f;

	// Use this for initialization
	void Start ()
    {

        Destroy(gameObject, m_MaxLifeTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ImpactRadius, m_ButterflyMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(m_ImpactForce, transform.position, m_ImpactRadius);
        }

        m_ImpactParticles.transform.parent = null;

        m_ImpactParticles.Play();

        Destroy(m_ImpactParticles.gameObject, m_ImpactParticles.main.duration);

        Destroy(gameObject);
    }

}
