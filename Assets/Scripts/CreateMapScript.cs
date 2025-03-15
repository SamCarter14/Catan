using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateMapScript : MonoBehaviour
{
    public Button[] tiles = new Button[20];
    public int[] typesOfTiles = { 1, 3, 3, 4, 4, 4 }; //Will be used to only allow one desert, three ore/brick, three wood/sheep/wheat
    public int[] typesOfTileNumbers =  {2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12}; //Amount of tiles with numbers 2, 3, 4, 5, 6, 8, 9, 10, 11, 12.
    public Button newMapButton;
    public Button backToPlayersButton;
    public GameManager gameManager;
    public Button startGameButton;
    
    

    void Start()
    {
       gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onNewMapButtonClick()
    {
        setMapImages();
        setMapNumbers();
    }


    // Callback function for the buttons
    private void ButtonCallBack(int index){
        // Do something when the button is clicked, using the index if needed
        Debug.Log("Button " + (index + 1) + " clicked!");
    }


    public void setMapNumbers(){
        int[] amountOfEachNumber = typesOfTileNumbers.ToArray();
        int selectedNumber;
        for (int i = 0; i < 19; i++)
        {
            string buttonName = "Tile " + i;
            GameObject obj = GameObject.Find(buttonName);
                tiles[i] = obj.GetComponent<Button>(); 
                //tiles[i].interactable = false;//Set all buttons to false for now.
                if (tiles[i] != null)
                {
                    Text buttonTextComponent = tiles[i].GetComponentInChildren<Text>();
                    if (tiles[i].GetComponentInChildren<Text>().text != "-1"){ //Don't want the desert to take up one of the numbers
                        do
                        {
                            selectedNumber = Random.Range(0, amountOfEachNumber.Length);
                        } while (amountOfEachNumber[selectedNumber] == 0);

                        if(amountOfEachNumber[selectedNumber] == 6 || amountOfEachNumber[selectedNumber] == 8)
                        {
                            tiles[i].GetComponentInChildren<Text>().color = Color.red;
                        }
                        else
                        {
                            tiles[i].GetComponentInChildren<Text>().color = Color.white;
                        }
                        buttonTextComponent.text = amountOfEachNumber[selectedNumber].ToString();
                        amountOfEachNumber[selectedNumber] = 0;
                    }
                    else{
                        buttonTextComponent.text = "";
                    }
                }
                else
                {
                    Debug.LogWarning("Button component not found for " + buttonName);
                }
        }
    }


    public Image returnImageOfTile(int num)
    {
        return tiles[num].GetComponentInChildren<Image>();
    }

    public Text returnTextOfTile(int num)
    {
        return tiles[num].GetComponentInChildren<Text>();
    }

    public Color returnColorOfTile(int num)
    {
        return tiles[num].GetComponentInChildren<Text>().color;
    }



    public void setMapImages(){
        int[] amountOfEachResource = typesOfTiles.ToArray();
        int tileType = -1;
        for (int i = 0; i < 19; i++)
        {
            string buttonName = "Tile " + i; // Construct the button name
            GameObject obj = GameObject.Find(buttonName); // Find the GameObject by name
            if (obj != null)
            {
                tiles[i] = obj.GetComponent<Button>(); // Get the Button component
                ColorBlock colors = tiles[i].colors; //Make it so that disabled buttons have same vibrant color.
                colors.disabledColor = colors.normalColor;
                tiles[i].colors = colors;

                if (tiles[i] != null)
                {
                    // int index = i; // Store the current index for the listener
                    //tiles[i].onClick.AddListener(() => ButtonCallBack(index)); // Add listener
                    Text buttonTextComponent = tiles[i].GetComponentInChildren<Text>();
                    do
                    {
                        tileType = Random.Range(0, amountOfEachResource.Length); // Randomly select a tile type
                    } while (amountOfEachResource[tileType] == 0);

                    ChangeImage(tiles[i], tileType);
                    amountOfEachResource[tileType]--; // Decrement the count of this tile type
                }


                else
                {
                    Debug.LogWarning("Button component not found for " + buttonName);
                }
            }
            else
            {
                Debug.LogWarning("GameObject not found for " + buttonName);
            }
        }
    }




    public void ChangeImage(Button buttonToChange, int pathToChange){
        string imagePath = "Assets/Images/Tiles/desert.png";
        if (pathToChange == 0)
        {
            Text buttonTextComponent = buttonToChange.GetComponentInChildren<Text>();
            buttonTextComponent.text = "-1";
        }
        else if(pathToChange == 1){
            imagePath = "Assets/Images/Tiles/ore.png";
        }
        else if(pathToChange == 2){
            imagePath = "Assets/Images/Tiles/brick.png";
        }
        else if(pathToChange == 3){
            imagePath = "Assets/Images/Tiles/wood.png";
        }
        else if(pathToChange == 4){
            imagePath = "Assets/Images/Tiles/wheat.png";
        }
        else if(pathToChange == 5){
            imagePath = "Assets/Images/Tiles/grass.png";
        }

        if (buttonToChange != null && !string.IsNullOrEmpty(imagePath)){
            Texture2D texture = LoadTextureFromFile(imagePath);
            if (texture != null){
                Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                Image buttonImage = buttonToChange.image;
                if (buttonImage != null){
                    buttonImage.sprite = newSprite;
                }
            }
        }
    }




    private Texture2D LoadTextureFromFile(string path){
        if (System.IO.File.Exists(path)){
            byte[] fileData = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
        return null;
    }

    public void backToStartButtonClick()
    {
        gameManager.clearPlayers();
        gameManager.goToPlayerSetupScreen();
    }


    public void startGameButtonClick()
    {
        gameManager.goToGameScreen();
        
    }

    public void setMap()
    {
        setMapImages();
        setMapNumbers();
        newMapButton = GameObject.Find("ChangeMap").GetComponent<Button>();
        newMapButton.onClick.AddListener(onNewMapButtonClick);
        backToPlayersButton = GameObject.Find("BackToSetUp").GetComponent<Button>();
        backToPlayersButton.onClick.AddListener(backToStartButtonClick);
        startGameButton = GameObject.Find("StartGame").GetComponent<Button>();
        startGameButton.onClick.AddListener(startGameButtonClick);
    }
}

