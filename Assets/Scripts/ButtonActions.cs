using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonActions : MonoBehaviour
{
    [SerializeField]
    Button createButton, creditsButton, exitButton, returnButton, startButton, controlsButton, secondReturnButton;
    [SerializeField]
    GameObject MainMenu, JoinMenu, CreditsScreen, ControlsScreen;

    private void Awake()
    {
        Button CreateButton = createButton.GetComponent<Button>();
        CreateButton.onClick.AddListener(CreateAGame);
        Button CreditsButton = creditsButton.GetComponent<Button>();
        CreditsButton.onClick.AddListener(CreditOpen);
        Button ReturnButton = returnButton.GetComponent<Button>();
        ReturnButton.onClick.AddListener(CreditClose);
        Button ExitGame = exitButton.GetComponent<Button>();
        ExitGame.onClick.AddListener(EndGame);
        Button StartButton = startButton.GetComponent<Button>();
        StartButton.onClick.AddListener(StartGame);
        Button ControlsButton = controlsButton.GetComponent<Button>();
        ControlsButton.onClick.AddListener(ControlsOpen);
        Button SecondReturnButton = secondReturnButton.GetComponent<Button>();
        SecondReturnButton.onClick.AddListener(ControlsClose);

    }
    // Use this for initialization
    void Start()
    {
        CreditsScreen.SetActive(false);
        JoinMenu.SetActive(false);
        ControlsScreen.SetActive(false);
    }

    public void CreditOpen()
    {
        CreditsScreen.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void CreditClose()
    {
        CreditsScreen.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ControlsOpen()
    {
        ControlsScreen.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void ControlsClose()
    {
        ControlsScreen.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if(PlayerCount.NumberOfPlayers >= 2)
        {
            SceneManager.LoadScene(1);
        }
        
    }
	
    public void CreateAGame()
    {
        MainMenu.SetActive(false);
        JoinMenu.SetActive(true);
    }
}
