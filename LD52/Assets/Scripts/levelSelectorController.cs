using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelectorController : MonoBehaviour
{
    
    void Start()
    {
        //if (!Singleton.Instance.levelLookup.ContainsKey(1))
        //{
        //    Level level = new Level(1, "Level1Scene", "", "Well that was easy.", 2, 0, 0, 0);
        //    Singleton.Instance.levelLookup.Add(1, level);
        //    //Singleton.Instance.currentLevel = 1;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(2))
        //{
        //    Level level = new Level(2, "Level2Scene", "", "I think your getting the hang of this.", 3, 0, 0, 0);
        //    Singleton.Instance.levelLookup.Add(2, level);
        //    //Singleton.Instance.currentLevel = 1;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(3))
        //{
        //    Level level = new Level(3, "Level3Scene", "", "\n\n\n\nNothing like a nice, sturdy apple crate", 12, 0, 0, 0);
        //    Singleton.Instance.levelLookup.Add(3, level);
        //    //Singleton.Instance.currentLevel = 3;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(4))
        //{
        //    Level level = new Level(4, "Level4Scene", "", "\n\n\n\nThat was sneaky.", 9, 0, 0, 0);
        //    Singleton.Instance.levelLookup.Add(4, level);
        //    //Singleton.Instance.currentLevel = 4;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(5))
        //{
        //    Level level = new Level(5, "Level5Scene", "", "\n\n\n\nI wonder what else can be stacked.", 11, 0, 0, 0);
        //    Singleton.Instance.levelLookup.Add(5, level);
        //    //Singleton.Instance.currentLevel = 5;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(6))
        //{
        //    Level level = new Level(6, "Level6Scene", "", "\n\n\n\nThe ducks didn't help.", 11, 11, 0, 0);
        //    Singleton.Instance.levelLookup.Add(6, level);
        //    //Singleton.Instance.currentLevel = 6;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(7))
        //{
        //    Level level = new Level(7, "Level7Scene", "", "\n\n\n\nThey should have a tractor for picking the apples", 11, 11, 0, 0);
        //    Singleton.Instance.levelLookup.Add(7, level);
        //    // Singleton.Instance.currentLevel = 7;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(8))
        //{
        //    Level level = new Level(8, "Level8Scene", "", "\n\n\n\nWell done. That tree was unnecessarily toall.", 3, 10, 0, 0);
        //    Singleton.Instance.levelLookup.Add(8, level);
        //    //Singleton.Instance.currentLevel = 8;
        //}
        // ****



    }
    public void startLevel(int levelNumber)
    {

        Level currentLevel;
        if (Singleton.Instance.levelLookup.ContainsKey(levelNumber))
        {
            Singleton.Instance.levelLookup.TryGetValue(levelNumber, out currentLevel);
            Singleton.Instance.currentLevel = levelNumber;
            SceneManager.LoadScene(currentLevel.levelName);
        }

    }

    public void goMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
