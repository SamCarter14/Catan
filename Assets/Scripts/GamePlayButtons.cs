using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayButtons : MonoBehaviour
{
    public Button[] gameTiles = new Button[20];// Array to hold the buttons
   
    private Button[] playersGameplayButtons = new Button[4];
    private string[] playersGameplayStrings;
    public Button pauseButton;
    public GameManager gameManager;
    public CreateMapScript createMapScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pauseButton.onClick.AddListener(pauseButtonClick);
        playersGameplayStrings = new string[] { "GamePlayer1", "GamePlayer2", "GamePlayer3", "GamePlayer4" };
        for (int i = 0; i < playersGameplayStrings.Length; i++)
        {
            Button currentButton = GameObject.Find(playersGameplayStrings[i]).GetComponent<Button>();
            playersGameplayButtons[i] = currentButton;
            
        }
        setPermanentMapSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseButtonClick()
    {
        gameManager.goToPauseScreen();
        float distance = Vector3.Distance(pauseButton.transform.position, playersGameplayButtons[1].transform.position);

        // Log the distance
        Debug.Log("Distance between button1 and button2: " + distance);
    }

    public void setPermanentMapSettings()
    {
        Debug.Log("got here");
        for (int i = 0; i < gameTiles.Length; i++)
        {
            string buttonName = "GameTile " + i;
            GameObject obj = GameObject.Find(buttonName);
            if (obj != null)
            {
                gameTiles[i] = obj.GetComponent<Button>();
                gameTiles[i].GetComponentInChildren<Text>().text = createMapScript.returnTextOfTile(i).text;
                gameTiles[i].GetComponentInChildren<Text>().color = createMapScript.returnColorOfTile(i);
                gameTiles[i].GetComponentInChildren<Image>().sprite = createMapScript.returnImageOfTile(i).sprite;
            }
        }
        for (int i = 0; i < gameManager.getNumberOfPlayers(); i++)
        {
            if (gameManager.getNumberOfPlayers() == 2)
            {
                Debug.Log("2Players");
                playersGameplayButtons[0].gameObject.SetActive(true);
                playersGameplayButtons[1].gameObject.SetActive(true);
                playersGameplayButtons[2].gameObject.SetActive(false);
                playersGameplayButtons[3].gameObject.SetActive(false);
            }
            else if (gameManager.getNumberOfPlayers() == 3)
            {
                playersGameplayButtons[0].gameObject.SetActive(true);
                playersGameplayButtons[1].gameObject.SetActive(true);
                playersGameplayButtons[2].gameObject.SetActive(true);
                playersGameplayButtons[3].gameObject.SetActive(false);
            }
            else if (gameManager.getNumberOfPlayers() == 4)
            {
                playersGameplayButtons[0].gameObject.SetActive(true);
                playersGameplayButtons[1].gameObject.SetActive(true);
                playersGameplayButtons[2].gameObject.SetActive(true);
                playersGameplayButtons[3].gameObject.SetActive(true);
            }
            string imagePath = "Assets/Images/People/" + gameManager.getPlayerColor(i) + "Person.png";
            Debug.Log(imagePath);
            Transform imageTransform = playersGameplayButtons[i].transform.Find("ImagePerson");
            Image buttonImage = imageTransform.GetComponent<Image>();
            Texture2D texture = LoadTextureFromFile(imagePath);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            buttonImage.sprite = newSprite;
            playersGameplayButtons[i].GetComponentInChildren<Text>().text = gameManager.getPlayerName(i) + " VP: " + gameManager.getPlayerVictoryPoints(i);
        }
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
}
