using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdating : MonoBehaviour {

    [SerializeField]
    private Text p1Score;

    [SerializeField]
    private Text p2Score;

    [SerializeField]
    private Text p3Score;

    [SerializeField]
    private Text p4Score;

    private void FixedUpdate()
    {
        UpdatingScores();
    }

    private void UpdatingScores()
    {
        if(Depletion.player1Score != 0)
        {
            p1Score.text = "Player 1: " + Depletion.player1Score.ToString();
        }
        else
        {
            p1Score.text = string.Empty;
        }
        if(Depletion.player2Score != 0)
        {
            p2Score.text = "Player 2: " + Depletion.player2Score.ToString();
        }
        else
        {
            p2Score.text = string.Empty;
        }
        if (Depletion.player3Score != 0)
        {
            p3Score.text = "Player 3: " + Depletion.player3Score.ToString();
        }
        else
        {
            p3Score.text = string.Empty;
        }
        if(Depletion.player4Score != 0)
        {
            p4Score.text = "Player 4: " + Depletion.player4Score.ToString();
        }
        else
        {
            p4Score.text = string.Empty;
        }


    }
}
