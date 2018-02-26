using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depletion : MonoBehaviour
{

    [SerializeField]
    private int resourceAmount = 100;
    [SerializeField]
    private int delayTime = 5;

    private void Update()
    {
        if (resourceAmount == 0)
            Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        resourceAmount -= 5;


    }

}
