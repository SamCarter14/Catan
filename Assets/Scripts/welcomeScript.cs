using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class welcomeScript : MonoBehaviour
{
    public Button continueToSetupButton;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        continueToSetupButton.onClick.AddListener(continueToSetupButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void continueToSetupButtonClick()
    {
        gameManager.goToPlayerSetupScreen();
    }
}
