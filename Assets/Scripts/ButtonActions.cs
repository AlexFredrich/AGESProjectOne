using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;

public class ButtonActions : MonoBehaviour
{
    [SerializeField]
    GameObject createButton, creditsButton, exitButton, returnButton, startButton, controlsButton, secondReturnButton;
    [SerializeField]
    GameObject MainMenu, JoinMenu, CreditsScreen, ControlsScreen;
    [SerializeField]
    EventSystem eventSystem;
    private GameObject storeSelected;

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
    //All of the functions are used to control the menu and the panel visibility
    // Use this for initialization
    void Start()
    {
        CreditsScreen.SetActive(false);
        JoinMenu.SetActive(false);
        ControlsScreen.SetActive(false);
        storeSelected = eventSystem.firstSelectedGameObject;
    }
    //Making sure that if you click with the mouse you can still use the controller
    private void Update()
    {
        if(eventSystem.currentSelectedGameObject != storeSelected)
        {
            if (eventSystem.currentSelectedGameObject == null)
                eventSystem.SetSelectedGameObject(storeSelected);
            else
                storeSelected = eventSystem.currentSelectedGameObject;
            

        }
    }
    
    public void CreditOpen()
    {
        CreditsScreen.SetActive(true);
        MainMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(returnButton);
    }

    public void CreditClose()
    {
        CreditsScreen.SetActive(false);
        MainMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(createButton);
    }
    public void ControlsOpen()
    {
        ControlsScreen.SetActive(true);
        CreditsScreen.SetActive(false);
        MainMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(secondReturnButton);
    }
    public void ControlsClose()
    {
        ControlsScreen.SetActive(false);
        MainMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(createButton);
    }

    public void EndGame()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        //Making sure that the player can only move forward if there are at least 2 players.
        if(PlayerCount.NumberOfPlayers >= 2)
        {
            SceneManager.LoadScene(1);
        }
        
    }
	
    public void CreateAGame()
    {
        MainMenu.SetActive(false);
        JoinMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(startButton);
    }
}
