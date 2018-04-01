using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Depletion : MonoBehaviour
{


    private int numberOfPlayersInTrigger;
    private GameObject m_Player;
    [SerializeField]
    private Slider resourceCounter;
    [SerializeField]
    private Image Fill;
    private int scoreValue = 100;
    private bool Done;

    public static int player1Score;
    public static int player2Score;
    public static int player3Score;
    public static int player4Score;

    public static int deadResources;

    private void OnEnable()
    {
        Done = false;
        numberOfPlayersInTrigger = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Player = other.gameObject;
            numberOfPlayersInTrigger++;
        }
    }

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
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            numberOfPlayersInTrigger--;
            StopCoroutine("HarvestingResource");
            resourceCounter.value = 0;
        }
    }



    private IEnumerator HarvestingResource()
    {
        
        do
        {
            if (m_Player.name == "Player1")
                Fill.color = Color.blue;
            else if (m_Player.name == "Player2")
                Fill.color = Color.green;
            else if (m_Player.name == "Player3")
                Fill.color = Color.yellow;
            else if (m_Player.name == "Player4")
                Fill.color = Color.red;
            
            resourceCounter.value++;
            yield return new WaitForSeconds(1);


            if (resourceCounter.value == resourceCounter.maxValue)
                Done = true;

            
        } while (!Done);

        
        if (m_Player.name == "Player1")
            player1Score += scoreValue;
        else if (m_Player.name == "Player2")
            player2Score += scoreValue;
        else if (m_Player.name == "Player3")
            player3Score += scoreValue;
        else if (m_Player.name == "Player4")
            player4Score += scoreValue;

        deadResources++;
        Debug.Log("Gained 100 points.");
        resourceCounter.value = 0;
        gameObject.SetActive(false);
    }



}
