using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour {

    public static int resources = 0;
    [SerializeField]
    private int delayTime = 5;

    private void OnTriggerStay(Collider other)
    {
        resources += 5;


        Debug.Log("Resources = " + resources);
    }

}
