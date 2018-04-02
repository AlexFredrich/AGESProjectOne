using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Depletion : MonoBehaviour
{

    //Private variables
    private int numberOfPlayersInTrigger;
    private int scoreValue = 100;
    private bool Done;
    private GameObject m_Player;
    //Serialize fields
    [SerializeField]
    private Slider resourceCounter;
    [SerializeField]
    private Image Fill;
    //Public variables
    public static int player1Score;
    public static int player2Score;
    public static int player3Score;
    public static int player4Score;
    public static int deadResources;

    //After the resources have been disabled and then reenabled, reinstating the number of players in triggers to 0 and Done to false.
    private void OnEnable()
    {
        Done = false;
        numberOfPlayersInTrigger = 0;
    }
    //Once a player has entered the trigger, connect it to an object and take note of how many are in the trigger box
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Player = other.gameObject;
            numberOfPlayersInTrigger++;
        }
    }
    //If a player remains within the trigger box, begin the coroutine that harvests the resource but only if there's one player within the trigger.
    //If there is more than one player, stop the coroutine and restart the counter
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (numberOfPlayersInTrigger == 1)
            {
                if (m_Player.GetComponent<PlayerHarvesting>().EnteredTrigger == true)
                {
                    StartCoroutine("HarvestingResource");
                }

            }
            else
            {
                StopCoroutine("HarvestingResource");
                resourceCounter.value = 0;
            }
        }
    }
    //If the player leaves the trigger, subtract the number of players within and stop the coroutine and restart the counter
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            numberOfPlayersInTrigger--;
            StopCoroutine("HarvestingResource");
            resourceCounter.value = 0;
        }
    }


    //Coroutine for harvesting
    private IEnumerator HarvestingResource()
    {
        
        do
        {
            //Changes the fill color depending on the player that enters the trigger
            if (m_Player.name == "Player1")
                Fill.color = Color.blue;
            else if (m_Player.name == "Player2")
                Fill.color = Color.green;
            else if (m_Player.name == "Player3")
                Fill.color = Color.yellow;
            else if (m_Player.name == "Player4")
                Fill.color = Color.red;
            //Increases the slider counter
            resourceCounter.value++;
            yield return new WaitForSeconds(1);

            //Once the counter reaches the max, break the loop
            if (resourceCounter.value == resourceCounter.maxValue)
                Done = true;

            
        } while (!Done);

        //Depending on the name of the player, add the score to that player
        if (m_Player.name == "Player1")
            player1Score += scoreValue;
        else if (m_Player.name == "Player2")
            player2Score += scoreValue;
        else if (m_Player.name == "Player3")
            player3Score += scoreValue;
        else if (m_Player.name == "Player4")
            player4Score += scoreValue;

        //Add the deactivated resource to a list
        deadResources++;
        //Reset resourcecounter
        resourceCounter.value = 0;
        //Deactivate the object
        gameObject.SetActive(false);
    }



}
