using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class PlayerManager
{
    //Public variables
    public Color m_PlayerColor;
    public Transform m_SpawnPoints;
    public Text m_PlayerScore;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Score;
    [HideInInspector] public int m_Wins;
    //Private variables
    private PlayerMovement m_Movement;
    private PlayerShooting m_Shooting;
    private Canvas m_CanvasGameObject;
    public Text PlayerName;

    public void Setup()
    {
        //Setting movement for each instance of the player
        m_Movement = m_Instance.GetComponent<PlayerMovement>();
        m_Shooting = m_Instance.GetComponent<PlayerShooting>();
       
        //Setting the player number in each of the scripts
        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;
        //Getting the canvas
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>();
        PlayerName = m_Instance.GetComponentInChildren<Text>();
        
        //Coloring the canvas depending on the player
        m_Instance.name = "Player" + m_PlayerNumber;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
        
        m_PlayerScore.color = m_PlayerColor;
        
        
       
    }
    //Disable the control and firing capabilities
    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.enabled = false;
    }
    //Enabling the control and firing capabilities
    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.enabled = true;
    }
    //Funciton to reset the player
    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoints.position;
        m_Instance.transform.rotation = m_SpawnPoints.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);

    }

}
