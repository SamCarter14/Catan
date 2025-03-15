using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerSetupScript : MonoBehaviour
{

    public Button goToMapButton;
    private string[] colorOfPersonStrings;
    public Button[] colorOfPersonButtons;
    public Color buttonColors;
    public Button addPlayerButton;
    public Button removePlayerButton;
    public Button currentSelectedPlayerColor;
    private string[] personNumber;
    public Button[] personNumberButton;
    public TMP_InputField inputNameField;
    private string soonToBeAddedPlayerName = "";
    private Button currentSelectedPlayerToDelete;
    //private Button[] playersGameplayButtons = new Button[4];
    //private string[] playersGameplayStrings;
    private string[] playersColors = { "default", "default", "default", "default" };
    public GameManager gameManager;
    public CreateMapScript createMapScript;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        goToMapButton = GameObject.Find("GoToMap").GetComponent<Button>();
        goToMapButton.onClick.AddListener(GoToMapButtonClick);
        buttonColors = goToMapButton.GetComponentInChildren<Image>().color;

        addPlayerButton = GameObject.Find("AddPlayer").GetComponent<Button>();
        addPlayerButton.onClick.AddListener(addPlayerButtonClick);

        removePlayerButton = GameObject.Find("DeletePlayer").GetComponent<Button>();
        removePlayerButton.onClick.AddListener(deletePlayerButtonClick);


        inputNameField.onValueChanged.AddListener(inputFieldTyping);
        Debug.Log("PlayerSetupScript Start");
        personNumber = new string[] { "Player1", "Player2", "Player3", "Player4" };
        personNumberButton = new Button[personNumber.Length];
        for (int i = 0; i < personNumber.Length; i++)
        {
            Button currentButton = GameObject.Find(personNumber[i]).GetComponent<Button>();
            personNumberButton[i] = currentButton;
            currentButton.onClick.AddListener(() => SetPersonButtonClick(currentButton));
        }

        colorOfPersonStrings = new string[] { "RedCharacter", "BlueCharacter", "GreenCharacter", "YellowCharacter", "PinkCharacter", "WhiteCharacter" };
        colorOfPersonButtons = new Button[colorOfPersonStrings.Length];


        for (int i = 0; i < colorOfPersonStrings.Length; i++)
        {
            Button currentButton = GameObject.Find(colorOfPersonStrings[i]).GetComponent<Button>();
            colorOfPersonButtons[i] = currentButton;
            currentButton.onClick.AddListener(() => ButtonClickOfColorPerson(currentButton));
        }
        
        gameManager.goToWelcomePanelScreen();
        Debug.Log("PlayerSetupScript end");
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void SetPersonButtonClick(Button currentButton)
    {
        for (int i = 0; i < personNumberButton.Length; i++)
        {
            personNumberButton[i].interactable = true;
        }

        currentButton.interactable = false;
        currentSelectedPlayerToDelete = currentButton;
    }

    public void currentPlayerButtons(Button currentButton)
    {

    }

    public void inputFieldTyping(string text)
    {
        soonToBeAddedPlayerName = soonToBeAddedPlayerName + text;
    }

    private void deletePlayerButtonClick()
    {
        Transform imageTransform = currentSelectedPlayerToDelete.transform.Find("ImagePerson");
        Image buttonImage = imageTransform.GetComponent<Image>();

        if (buttonImage.sprite != createSpriteOffColorPerson("default")) //check that the image to be changed (selected made player) is not default already
        {

            if (currentSelectedPlayerToDelete != null)
            {
                gameManager.SubtractFromNumberOfPlayers();
                if (gameManager.getNumberOfPlayers() < 2)
                {
                    goToMapButton.interactable = false;
                }

                currentSelectedPlayerToDelete.interactable = true;
                buttonImage.sprite = createSpriteOffColorPerson("default");
                string characterNumber = currentSelectedPlayerToDelete.name;
                currentSelectedPlayerToDelete.GetComponentInChildren<Text>().text = characterNumber.Replace("Player", "Player ");
                characterNumber = characterNumber.Replace("Player", "");
                currentSelectedPlayerToDelete = null;
                int numPlayer = int.Parse(characterNumber) - 1;


                if (playersColors[numPlayer] == "red")
                {
                    colorOfPersonButtons[0].interactable = true;
                    colorOfPersonButtons[0].GetComponentInChildren<Image>().color = buttonColors;
                }
                else if (playersColors[numPlayer] == "blue")
                {
                    colorOfPersonButtons[1].interactable = true;
                    colorOfPersonButtons[1].GetComponentInChildren<Image>().color = buttonColors;
                }
                else if (playersColors[numPlayer] == "green")
                {
                    colorOfPersonButtons[2].interactable = true;
                    colorOfPersonButtons[2].GetComponentInChildren<Image>().color = buttonColors;
                }
                else if (playersColors[numPlayer] == "yellow")
                {
                    colorOfPersonButtons[3].interactable = true;
                    colorOfPersonButtons[3].GetComponentInChildren<Image>().color = buttonColors;
                }
                else if (playersColors[numPlayer] == "pink")
                {
                    colorOfPersonButtons[4].interactable = true;
                    colorOfPersonButtons[4].GetComponentInChildren<Image>().color = buttonColors;
                }
                else if (playersColors[numPlayer] == "white")
                {
                    colorOfPersonButtons[5].interactable = true;
                    colorOfPersonButtons[5].GetComponentInChildren<Image>().color = buttonColors;
                }

                playersColors[numPlayer] = "default";
            }
        }
    }

    public void GoToMapButtonClick()
    {
        if (gameManager.getNumberOfPlayers() >= 2)
        {
            
            gameManager.setPlayerArrayCount();
            gameManager.goToMapSetupScreen();
            int playerNum = 0;
            for (int i = 0; i < gameManager.getNumberPotentialPlayers(); i++)
            {
                if (playersColors[i] != "default")
                {
                    gameManager.addPlayer(playerNum, personNumberButton[i].GetComponentInChildren<Text>().text, playersColors[i]);
                    playerNum++;
                    
                }
            }
            createMapScript.setMap();
        }
        

    }

    public Sprite createSpriteOffColorPerson(string color)
    {
        string imagePath = "Assets/Images/People/" + color + "Person.png";
        Image buttonImage = currentSelectedPlayerToDelete.GetComponentInChildren<Image>();
        Texture2D texture = LoadTextureFromFile(imagePath);
        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        return newSprite;
    }


    private void addPlayerButtonClick()
    {

        Transform imageTransform;
        Image buttonImage;
        Text buttonTextComponent;
        string color = "white";

        string imagePath = "Assets/Images/People/whitePerson.png";

        if (currentSelectedPlayerColor == colorOfPersonButtons[0])
        {
            imagePath = "Assets/Images/People/redPerson.png";
            color = "red";
        }
        else if (currentSelectedPlayerColor == colorOfPersonButtons[1])
        {
            imagePath = "Assets/Images/People/bluePerson.png";
            color = "blue";
        }
        else if (currentSelectedPlayerColor == colorOfPersonButtons[2])
        {
            imagePath = "Assets/Images/People/greenPerson.png";
            color = "green";
        }
        else if (currentSelectedPlayerColor == colorOfPersonButtons[3])
        {
            imagePath = "Assets/Images/People/yellowPerson.png";
            color = "yellow";
        }
        else if (currentSelectedPlayerColor == colorOfPersonButtons[4])
        {
            imagePath = "Assets/Images/People/pinkPerson.png";
            color = "pink";
        }


        for (int i = 0; i < gameManager.getNumberPotentialPlayers(); i++)
        {
            imageTransform = personNumberButton[i].transform.Find("ImagePerson");
            buttonImage = imageTransform.GetComponent<Image>();
            buttonTextComponent = personNumberButton[i].GetComponentInChildren<Text>();
            string buttonString = buttonTextComponent.text;
            if ((buttonString == "Player 1" || buttonString == "Player 2" || buttonString == "Player 3" || buttonString == "Player 4") && soonToBeAddedPlayerName != "" && currentSelectedPlayerColor.GetComponentInChildren<Image>().color != Color.grey)
            {
                gameManager.AddToNumberOfPlayers();
                if (gameManager.getNumberOfPlayers() >= 2)
                {
                    goToMapButton.interactable = true;
                }
                Texture2D texture = LoadTextureFromFile(imagePath);
                Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                buttonImage.sprite = newSprite;
                buttonTextComponent.text = getNameTextField();
                currentSelectedPlayerColor.GetComponentInChildren<Image>().color = Color.grey;
                playersColors[i] = color;
                break;
            }

        }

        clearPersonNameTextField();
    }


    private void ButtonClickOfColorPerson(Button currentButton)
    {
        for (int i = 0; i < colorOfPersonButtons.Length; i++)
        {
            if (colorOfPersonButtons[i].GetComponentInChildren<Image>().color != Color.grey)
            {
                colorOfPersonButtons[i].interactable = true;
            }
        }
        currentButton.interactable = !currentButton.interactable;
        currentSelectedPlayerColor = currentButton;
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


    public void clearPersonNameTextField()
    {
        inputNameField.text = "";
        soonToBeAddedPlayerName = "";
    }

    public string getNameTextField()
    {
        return inputNameField.text;
    }

    

}






