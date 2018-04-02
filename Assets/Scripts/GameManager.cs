using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //Serialize fields
    [SerializeField]
    private int m_NumRoundsToWin = 1;
    [SerializeField]
    private float m_StartDelay = 3f;
    [SerializeField]
    private float m_EndDelay = 3f;
    //Public variables
    public CameraControl m_CameraControl;
    public Text m_MessageText;
    public GameObject m_ButterflyPrefab;
    public PlayerManager[] m_Players;
    public GameObject[] m_Resources;
    //Private variables
    private int deadResources;
    private int[] Scores = new int[4];
    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private PlayerManager m_RoundWinner;
    private PlayerManager m_GameWinner;
    private int numberOfPlayers;

    //UI Elements
    [SerializeField]
    private GameObject endMessagePanel;
    [SerializeField]
    private EventSystem ES;
    [SerializeField]
    private GameObject button;
    private GameObject storeSelected;

	// Use this for initialization
	private void Start ()
    {
        numberOfPlayers = PlayerCount.NumberOfPlayers;
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        endMessagePanel.SetActive(false);
        SettingScores();
        SpawnAllPlayers();
        SetCameraTargets();

        storeSelected = ES.firstSelectedGameObject;
        StartCoroutine(GameLoop());
	}

    //Spawning all the players depending on the number of people that joined in the join screen
    private void SpawnAllPlayers()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            m_Players[i].m_Instance = Instantiate(m_ButterflyPrefab, m_Players[i].m_SpawnPoints.position, m_Players[i].m_SpawnPoints.rotation);
            m_Players[i].m_PlayerNumber = i + 1;
            m_Players[i].Setup();
            m_Players[i].PlayerName.text = m_Players[i].m_ColoredPlayerText;
            
        }
    }
    //Setting the cameras depending on the number of players
    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[numberOfPlayers];

        for(int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Players[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }
    //Setting the scores and making sure to keep track of them
    private void SettingScores()
    {
        Scores[0] = Depletion.player1Score;
        Scores[1] = Depletion.player2Score;
        Scores[2] = Depletion.player3Score;
        Scores[3] = Depletion.player4Score;
    }
    //The main loop for the game
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());

        if(m_GameWinner != null)
        {
            //SceneManager.LoadScene("Level");
            endMessagePanel.SetActive(true);
            ES.SetSelectedGameObject(button);

        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }
    //Beginning of the game, setting round number
    private IEnumerator RoundStarting()
    {
        ResetAllPlayers();
        DisablePlayerControls();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "Round " + m_RoundNumber;

        yield return m_StartWait;
    }
    //Loop during the game
    private IEnumerator RoundPlaying()
    {
        //Enabling the players control
        EnablePlayerControl();
        m_MessageText.text = string.Empty;
        //Checking if all the resources are gone and if they aren't continue with the loop
        while(!AllResourcesGone())
        {
            yield return null;
        }

    }
    //For the end of the game and checking for round winners and if there's a game winner
    private IEnumerator RoundEnding()
    {
        DisablePlayerControls();

        m_RoundWinner = null;

        m_RoundWinner = GetRoundWinner();

        if (m_RoundWinner != null)
        {
            m_RoundWinner.m_Wins++;
        }

        m_GameWinner = GetGameWinner();

        string message = EndMessage();
        m_MessageText.text = message;

        yield return m_EndWait;
    }
    //Checking the variable to see if all the resources are gone
    private bool AllResourcesGone()
    {
        if (Depletion.deadResources == 6)
            return true;
        else
            return false;
       

    }
    //Going through all of the player scores to see which is the highest, or if there's a tie
    private PlayerManager GetRoundWinner()
    {
        bool tie = false;
        int winningNumber = 0;
        SettingScores();
        for (int i = 0; i < Scores.Length; i++)
        {
            if (Scores[winningNumber] == Scores[i] && i != 0)
                tie = true;
            else if (Scores[i] > Scores[winningNumber])
            {
                winningNumber = i;
            }

        }
        if (!tie)
            return m_Players[winningNumber];
        else
            return null;

    }
    //Checking the number of wins for each player to see if a player has the number to win
    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {

            if (m_Players[i].m_Wins == m_NumRoundsToWin)
                return m_Players[i];
        }

        return null;
    }
    //Sending back the right message depending on the player winning
    private string EndMessage()
    {
        string message = "Draw";

        if(m_RoundWinner != null)
        {
            message = m_RoundWinner.m_ColoredPlayerText + "WINS THE ROUND!";
        }

        message += "\n\n\n\n";

        for(int i = 0; i < numberOfPlayers; i++)
        {
            message += m_Players[i].m_ColoredPlayerText + ": " + m_Players[i].m_Wins + " WINS\n";
        } 

        if(m_GameWinner != null)
        {
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";
        }

        return message;

    }
    //reseting the players, the resources, and the scores for the players
    private void ResetAllPlayers()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            m_Players[i].Reset();
            
        }
        for(int i = 0; i < m_Resources.Length; i++)
        {
            m_Resources[i].SetActive(true);
        }

        Depletion.deadResources = 0;
        Depletion.player1Score = 0;
        Depletion.player2Score = 0;
        Depletion.player3Score = 0;
        Depletion.player4Score = 0;
        
    }

    private void EnablePlayerControl()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            m_Players[i].EnableControl();
        }
    }

    private void DisablePlayerControls()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            m_Players[i].DisableControl();
        }

    }
    //Buttons on the end of the game screen
    public void Replay()
    {
        SceneManager.LoadScene("Level");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
