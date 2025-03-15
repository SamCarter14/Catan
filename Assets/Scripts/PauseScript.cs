using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public Button exitPauseButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        exitPauseButton = GameObject.Find("ExitPause").GetComponent<Button>();
        exitPauseButton.onClick.AddListener(exitPauseButtonClick);

        exitPauseButton = GameObject.Find("ReturnHome").GetComponent<Button>();
        exitPauseButton.onClick.AddListener(returnHomeButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitPauseButtonClick()
    {
        gameManager.exitPauseScreen();
    }

    public void returnHomeButtonClick()
    {
        gameManager.goToPlayerSetupScreen();
    }
}
