using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerCount : MonoBehaviour {

    [SerializeField]
    List<Text> joinText;
    [SerializeField]
    AudioSource joinSound;
    [SerializeField]
    AudioClip join;

    public static int NumberOfPlayers
    {
        get
        {
            if (joinedPlayers == null)
                return 0;
            return joinedPlayers.Count(c => c);

            
        }
    }

    private string joinButtonName = "Fire";
    public const int MaxPlayers = 4;
    private static bool[] joinedPlayers;

    private void Start()
    {
        GettingPlayerList();
    }

    private void Update()
    {
        CheckingJoinedPlayers();
        
    }

    private void CheckingJoinedPlayers()
    {
        for (int i = 0; i < MaxPlayers; i++)
        {
            if (joinedPlayers[i] == true)
                continue;
            if(Input.GetButtonDown(joinButtonName + (i+1).ToString()))
            {
                joinSound.PlayOneShot(join);
                joinText[i].text = "Player " + (i + 1).ToString() + " has joined.";
                joinedPlayers[i] = true;
            }
        }
    }

    private void GettingPlayerList()
    {
        joinedPlayers = new bool[MaxPlayers];

        for(int i = 0; i < MaxPlayers; i++)
        {
            joinedPlayers[i] = false;
        }
    }
}
