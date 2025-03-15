using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePanelScript : MonoBehaviour
{

    private Button rollDiceButton;
    private Button continueToGameButton;
    public GameManager gameManager;
    private Image[] playersDiceImages;
    private Button[] playerDiceButtons;
    public int[] diceValuesRolled;
   
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        rollDiceButton = GameObject.Find("Roll").GetComponent<Button>();
        rollDiceButton.onClick.AddListener(rollDiceButtonClick);

        continueToGameButton = GameObject.Find("GoToGame").GetComponent<Button>();
        continueToGameButton.onClick.AddListener(continueToGameButtonClick);

        playersDiceImages = new Image[gameManager.getNumberPotentialPlayers()];
        playerDiceButtons = new Button[gameManager.getNumberPotentialPlayers()];

        for (int i = 0; i < gameManager.getNumberPotentialPlayers(); i++)
        {
            playersDiceImages[i] = GameObject.Find("Dice" + (i + 1)).GetComponent<Image>();
        }

        for (int i = 0; i < gameManager.getNumberPotentialPlayers(); i++)
        {
            playerDiceButtons[i] = GameObject.Find("DicePlayer" + (i + 1)).GetComponent<Button>();
        }

        for (int i = 0; i < gameManager.getNumberOfPlayers(); i++)
        {
            playersDiceImages[i].enabled = true;
        }

        for (int i = gameManager.getNumberOfPlayers(); i < gameManager.getNumberPotentialPlayers(); i++)
        {
            playersDiceImages[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continueToGameButtonClick()
    {
        gameManager.turnOffDicePanel();
    }

    public void rollDiceButtonClick()
    {
        gameManager.rollInitialDice();
        diceValuesRolled = gameManager.initialDiceValues();
        for (int i = 0; i < diceValuesRolled.Length; i++)
        {
            string imagePath = "Assets/Images/Dice/dice" + diceValuesRolled[i] + ".png";
            Texture2D texture = LoadTextureFromFile(imagePath);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            playersDiceImages[i].GetComponent<Image>().sprite = newSprite;
        }

        rollDiceButton.interactable = false;
        continueToGameButton.interactable = true;
    }

    private Texture2D LoadTextureFromFile(string path)
    {
        if (System.IO.File.Exists(path))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
        return null;
    }


    public void setDiceImages()
    {
        for (int i = 0; i < gameManager.getNumberOfPlayers(); i++)
        {
            playersDiceImages[i].enabled = true;
            playerDiceButtons[i].gameObject.SetActive(true);
            string imagePath = "Assets/Images/People/" + gameManager.getPlayerColor(i) + "Person.png";
            Debug.Log(imagePath);
            Transform imageTransform = playerDiceButtons[i].transform.Find("ImagePersonDice");
            Image buttonImage = imageTransform.GetComponent<Image>();
            Texture2D texture = LoadTextureFromFile(imagePath);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            buttonImage.sprite = newSprite;
            playerDiceButtons[i].GetComponentInChildren<Text>().text = gameManager.getPlayerName(i);
        }

        for (int i = gameManager.getNumberOfPlayers(); i < gameManager.getNumberPotentialPlayers(); i++)
        {
            playersDiceImages[i].enabled = false;
            playerDiceButtons[i].gameObject.SetActive(false);
        }


        rollDiceButton.interactable = true;
        continueToGameButton.interactable = false;
    }
}
