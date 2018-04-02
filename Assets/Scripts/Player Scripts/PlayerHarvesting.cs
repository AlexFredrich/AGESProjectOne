using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour {

    //Public variable
    public bool EnteredTrigger;
    //When enabled, resets the boolean that states whether a player is within a trigger
    private void OnEnable()
    {
        EnteredTrigger = false;
    }

    //If a player has this set to true, it will allow the harvesting to begin
    private void OnTriggerStay(Collider other)
    {
       if(other.tag == "Resource")
        {
            EnteredTrigger = true;

        }
        
    }
    //Exiting will stop the harvesting
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Resource")
        {
            EnteredTrigger = false;
        }
    }


}
