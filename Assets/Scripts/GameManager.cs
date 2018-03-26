using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int m_NumRoundsToWin = 5;
    [SerializeField]
    private float m_StartDelay = 3f;
    [SerializeField]
    private float m_EndDelay = 3f;
    public CameraControl m_CameraControl;
    public Text m_MessageText;
    public GameObject m_ButterflyPrefab;
    public PlayerManager[] m_Players;
    public GameObject[] m_Resources;
    private int deadResources;
    private int[] Scores = new int[4];

    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private PlayerManager m_RoundWinner;
    private PlayerManager m_GameWinner;
    private int numberOfPlayers;

    [SerializeField]
    private GameObject endMessagePanel;

	// Use this for initialization
	private void Start ()
    {
        numberOfPlayers = PlayerCount.NumberOfPlayers;
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        endMessagePanel.SetActive(false);
        SettingScores();
        SpawnAllTanks();
        SetCameraTargets();


        StartCoroutine(GameLoop());
	}

    private void SpawnAllTanks()
    {
        for(int i = 0; i < numberOfPlayers; i++)
        {
            m_Players[i].m_Instance = Instantiate(m_ButterflyPrefab, m_Players[i].m_SpawnPoints.position, m_Players[i].m_SpawnPoints.rotation);
            m_Players[i].m_PlayerNumber = i + 1;
            m_Players[i].Setup();
            m_Players[i].PlayerName.text = m_Players[i].m_ColoredPlayerText;
            //Scores[i].enabled = true;
            //Scores[i].color = m_Players[i].m_PlayerColor;
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[numberOfPlayers];

        for(int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Players[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }

    private void SettingScores()
    {
        Scores[0] = Depletion.player1Score;
        Scores[1] = Depletion.player2Score;
        Scores[2] = Depletion.player3Score;
        Scores[3] = Depletion.player4Score;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());

        if(m_GameWinner != null)
        {
            //SceneManager.LoadScene("Level");
            endMessagePanel.SetActive(true);
            if (Input.GetButton("Fire1"))
                SceneManager.LoadScene("Level");
            if (Input.GetButton("Submit1"))
                SceneManager.LoadScene("Menu");

        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }

    private IEnumerator RoundStarting()
    {
        ResetAllPlayers();
        DisablePlayerControls();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = "Round " + m_RoundNumber;

        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();
        //UpdatePlayerScores();
        m_MessageText.text = string.Empty;

        while(!AllResourcesGone())
        {
            yield return null;
        }

    }

    //private void UpdatePlayerScores()
    //{
    //    for(int i = 0; i < numberOfPlayers; i++)
    //    {
    //        m_Players[i].m_PlayerScore.text = "Player " + (i + 1).ToString() + ": " + m_Players[i].m_Score.ToString();
    //    }
    //}

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

    private bool AllResourcesGone()
    {
        for(int i = 0; i < m_Resources.Length; i++)
        {
            if (m_Resources[i].activeSelf)
                deadResources++;
        }

        return deadResources <= 1;

    }

    private PlayerManager GetRoundWinner()
    {
        int maxValue = 0;
        int winningIndex = -1;
        for (int i = 0; i < Scores[i]; i++)
        {
            if (Scores[i] > maxValue)
            {
                maxValue = Scores[i];
                winningIndex = i;
            }

        }
        return m_Players[winningIndex];
    }

    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {

            if (m_Players[i].m_Wins == m_NumRoundsToWin)
                return m_Players[i];
        }

        return null;
    }

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

    private void ResetAllPlayers()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            m_Players[i].Reset();
            
        }

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

}
