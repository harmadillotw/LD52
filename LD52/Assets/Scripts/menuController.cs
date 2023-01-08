using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class menuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        Singleton.Instance.currentLevel = 1;
        SceneManager.LoadScene("Level1Scene");
    }

    public void goInstructions()
    {
        SceneManager.LoadScene("InstructionsScene");
    }
    public void goOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void goCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void goLevelSelector()
    {
        SceneManager.LoadScene("LevelSelectorScene");
    }
    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
