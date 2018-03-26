using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class PlayerManager
{

    public Color m_PlayerColor;
    public Transform m_SpawnPoints;
    public Text m_PlayerScore;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Score;
    [HideInInspector] public int m_Wins;

    private PlayerMovement m_Movement;
    private PlayerShooting m_Shooting;
    private Canvas m_CanvasGameObject;
    public Text PlayerName;

    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<PlayerMovement>();
        m_Shooting = m_Instance.GetComponent<PlayerShooting>();
       
        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>();
        PlayerName = m_Instance.GetComponentInChildren<Text>();
        
        
        m_Instance.name = "Player" + m_PlayerNumber;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
        
        m_PlayerScore.color = m_PlayerColor;
        
        
       
    }

    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.enabled = false;
    }

    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.enabled = true;
    }

    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoints.position;
        m_Instance.transform.rotation = m_SpawnPoints.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);

    }

}
