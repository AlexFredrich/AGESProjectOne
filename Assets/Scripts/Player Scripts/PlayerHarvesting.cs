using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour {


    public bool EnteredTrigger;

    private void OnEnable()
    {
        EnteredTrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
       if(other.tag == "Resource")
        {
            EnteredTrigger = true;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Resource")
        {
            EnteredTrigger = false;
        }
    }


}
