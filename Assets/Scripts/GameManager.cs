using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject WelcomePanel;
    public GameObject PlayerSetupPanel;
    public GameObject MapSetupPanel;
    public GameObject GameplayPanel;
    public GameObject PausePanel;
    public GameObject DicePanel;
    private int potentialPlayers = 4;
    public Players[] currentPlayers;
    private int numberOfActivePlayers = 0;
    public Die[] determinePositionDice;
    public int[] amountOfEachDiceValue = new int[7];

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Debug.Log("GameManager Awake");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void goToWelcomePanelScreen()
    {
        WelcomePanel.SetActive(true);
        PlayerSetupPanel.SetActive(false);
        MapSetupPanel.SetActive(false);
        GameplayPanel.SetActive(false);
        PausePanel.SetActive(false);
        DicePanel.SetActive(false);
        Debug.Log("hello");
    }

    public void goToGameScreen()
    {
        WelcomePanel.SetActive(false);
        PlayerSetupPanel.SetActive(false);
        MapSetupPanel.SetActive(false);
        GameplayPanel.SetActive(true);
        DicePanel.SetActive(true);
        DicePanel.GetComponent<DicePanelScript>().setDiceImages();
        StartCoroutine(InitializeGameplayPanel());
    }

    private IEnumerator InitializeGameplayPanel()
    {
        // Wait for one frame to ensure that the objects are properly initialized
        //This essentially was making sure that set permanent map settings comes after the panel is made.
        //It was trying to be called before?? 
        yield return null;

        // Call the function after the objects are initialized
        GameplayPanel.GetComponent<GamePlayButtons>().setPermanentMapSettings();
        
        
    }

    public void goToPlayerSetupScreen()
    {
        WelcomePanel.SetActive(false);
        PlayerSetupPanel.SetActive(true);
        MapSetupPanel.SetActive(false);
        PausePanel.SetActive(false);
        GameplayPanel.SetActive(false);
        clearPlayers();
    }

    public void turnOnDicePanel()
    {
        DicePanel.SetActive(true);
    }

    public void turnOffDicePanel()
    {
        DicePanel.SetActive(false);
    }

    public void goToPauseScreen()
    {
        if (!PausePanel.activeSelf)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }
    }

    public void exitPauseScreen()
    {
        PausePanel.SetActive(false);
    }

    public void goToMapSetupScreen()
    {
        PlayerSetupPanel.SetActive(false);
        MapSetupPanel.SetActive(true);
    }

    public int getNumberOfPlayers()
    {
        return numberOfActivePlayers;
    }

    public void AddToNumberOfPlayers()
    {
        numberOfActivePlayers++;
    }

    public void SubtractFromNumberOfPlayers()
    {
        numberOfActivePlayers--;
    }

    public void setPlayerArrayCount()
    {
        currentPlayers = new Players[numberOfActivePlayers];
    }

    public int getNumberPotentialPlayers()
    {
        return potentialPlayers;
    }

    public void addPlayer(int playerNum, string name, string color)
    {
       currentPlayers[playerNum] = new Players(name, color);
    }

    public string getPlayerColor(int player)
    {
        return currentPlayers[player].getColor();
    }

    public string getPlayerName(int player)
    {
        return currentPlayers[player].getName();
    }

    public int getPlayerVictoryPoints(int player)
    {
        return currentPlayers[player].getVictoryPoints();
    }

    public void clearPlayers()
    {
        if (currentPlayers != null)
        {
            Array.Clear(currentPlayers, 0, currentPlayers.Length);
        }
    }

    public void rollInitialDice()
    {
        Array.Clear(amountOfEachDiceValue, 0, amountOfEachDiceValue.Length);
        determinePositionDice = new Die[numberOfActivePlayers];
        for (int i = 0; i < numberOfActivePlayers; i++)
        {
            determinePositionDice[i] = new Die();
            determinePositionDice[i].rollDie();
            while(amountOfEachDiceValue[determinePositionDice[i].getSideUp()] == 1)
            {
                determinePositionDice[i].rollDie();
            }
            amountOfEachDiceValue[determinePositionDice[i].getSideUp()] += 1;
        }
        
    }

    public int[] initialDiceValues()
    {
        int order = 1;
        int[] numbersRolled = new int[numberOfActivePlayers];
        for(int i = 0; i < numberOfActivePlayers; i++)
        {
            numbersRolled[i] = determinePositionDice[i].getSideUp();
            Debug.Log(determinePositionDice[i].getSideUp());
        }
        for(int i = 6; i > 0; i--)
        {
            for(int j = 0; j < numberOfActivePlayers; j++)
            {
                if(determinePositionDice[j].getSideUp() == i)
                {
                    currentPlayers[j].setOrder(order);
                    order += 1;
                    Debug.Log(currentPlayers[j].getName() + currentPlayers[j].getOrder());
                }
            }
        }

     
        return numbersRolled;
    }


}





