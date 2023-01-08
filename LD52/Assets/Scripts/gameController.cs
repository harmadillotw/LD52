using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class gameController : MonoBehaviour
{
    // Start is called before the first frame update


    public int panelSize = 65;
    public int totalRedApplesRequired = 0;
    public int totalGreenApplesRequired = 0;
    public int totalPearsRequired = 0;
    public int totalOthersRequired = 0;

    public int scoreRedApplesFound;   
    public int scoreGreenApplesFound;
    public int scorePearsFound;
    public int scoreOthersFound;

    public int RedApplesCollected;
    public int GreenApplesCollected;
    public int PearsCollected;
    public int OthersCollected;

    public Sprite redAppleSpriteNeeded;
    public Sprite redAppleSpriteFound;
    public Sprite greenAppleSpriteNeeded;
    public Sprite greenAppleSpriteFound;
    public Sprite pearSpriteNeeded;
    public Sprite pearSpriteFound;
    public Sprite otherSpriteNeeded;
    public Sprite otherSpriteFound;

    private TextMeshProUGUI timeText;
    private TextMeshProUGUI narratorText;

    private float exitTimer = 0f;
    private bool doExit = false;
    private float exitDelay = 3f;

    private List<Image> scoreRedApples = new List<Image>();
    private List<Image> scoreGreenApples = new List<Image>();
    private List<Image> scorePears = new List<Image>();
    private List<Image> scoreOthers = new List<Image>();




    private GameObject redAppleScorePanel;
    private GameObject greenAppleScorePanel;
    private GameObject pearScorePanel;
    private GameObject otherScorePanel;

    private float timeElapsed;
    private bool levelFinished;
    private Level currentLevel;

    private float textCounter;
    void Start()
    {

        textCounter = 0f;
        // This is a temporary singleton initialisation. Needs to be moved to the splash screen later.
        // ****
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
        //   // Singleton.Instance.currentLevel = 7;
        //}
        //if (!Singleton.Instance.levelLookup.ContainsKey(8))
        //{
        //    Level level = new Level(8, "Level8Scene", "", "\n\n\n\nWell done. That tree was unnecessarily toall.", 3, 10, 0, 0);
        //    Singleton.Instance.levelLookup.Add(8, level);
        //    //Singleton.Instance.currentLevel = 8;
        //}
        // ****



        if (Singleton.Instance.levelLookup.ContainsKey(Singleton.Instance.currentLevel))
        {
            Singleton.Instance.levelLookup.TryGetValue(Singleton.Instance.currentLevel, out currentLevel);
        }

        if (currentLevel != null)
        {
            totalRedApplesRequired = currentLevel.redApples;
            totalGreenApplesRequired = currentLevel.greenApples;
        }
        
        levelFinished = false;
        timeElapsed = 0f;
        scoreRedApplesFound = 0;
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        narratorText = GameObject.Find("NarratorText").GetComponent<TextMeshProUGUI>();
        redAppleScorePanel = GameObject.Find("RedAppleScorePanel");
        greenAppleScorePanel = GameObject.Find("GreenAppleScorePanel");
        pearScorePanel = GameObject.Find("PearScorePanel");
        otherScorePanel = GameObject.Find("OtherScorePanel");
        Singleton.Instance.totalObjects = 3;

        redAppleScorePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalRedApplesRequired * panelSize);
        //redAppleScorePanel.GetComponent<RectTransform>().position = new Vector2(0f, 0f);
        for (int i = 0; i < totalRedApplesRequired; i++)
        {
            GameObject appleUiIGO = new GameObject();
            Image appleImage = appleUiIGO.AddComponent<Image>();
            //i.color = Color.red;
            appleImage.sprite = redAppleSpriteNeeded;
            scoreRedApples.Add(appleImage);
            appleUiIGO.transform.SetParent(redAppleScorePanel.transform);
            appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
            appleUiIGO.SetActive(true);
        }

        if (!(totalRedApplesRequired > 0))
        {
            redAppleScorePanel.SetActive(false);
        }

        greenAppleScorePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalGreenApplesRequired * panelSize);
        //redAppleScorePanel.GetComponent<RectTransform>().position = new Vector2(0f, 0f);
        //for (int i = 0; i < totalGreenApplesRequired; i++)
        //{
        //    GameObject appleUiIGO = new GameObject();
        //    Image appleImage = appleUiIGO.AddComponent<Image>();
        //    //i.color = Color.red;
        //    appleImage.sprite = greenAppleSpriteNeeded;
        //    scoreGreenApples.Add(appleImage);
        //    appleUiIGO.transform.SetParent(greenAppleScorePanel.transform);
        //    appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
        //    appleUiIGO.SetActive(true);
        //}

        //if (!(totalGreenApplesRequired > 0))
        //{
        //    greenAppleScorePanel.SetActive(false);
        //}

        for (int i = 0; i < totalGreenApplesRequired; i++)
        {
            GameObject appleUiIGO = new GameObject();
            Image appleImage = appleUiIGO.AddComponent<Image>();
            //i.color = Color.red;
            appleImage.sprite = greenAppleSpriteNeeded;
            scoreGreenApples.Add(appleImage);
            appleUiIGO.transform.SetParent(greenAppleScorePanel.transform);
            appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
            appleUiIGO.SetActive(true);
        }
        if (!(totalGreenApplesRequired > 0))
        {
            greenAppleScorePanel.SetActive(false);
        }

        for (int i = 0; i < totalPearsRequired; i++)
        {
            GameObject appleUiIGO = new GameObject();
            Image appleImage = appleUiIGO.AddComponent<Image>();
            //i.color = Color.red;
            appleImage.sprite = redAppleSpriteNeeded;
            scorePears.Add(appleImage);
            appleUiIGO.transform.SetParent(pearScorePanel.transform);
            appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
            appleUiIGO.SetActive(true);
        }
        if (!(totalPearsRequired > 0))
        {
            pearScorePanel.SetActive(false);
        }

        for (int i = 0; i < totalOthersRequired; i++)
        {
            GameObject appleUiIGO = new GameObject();
            Image appleImage = appleUiIGO.AddComponent<Image>();
            //i.color = Color.red;
            appleImage.sprite = otherSpriteNeeded;
            scoreOthers.Add(appleImage);
            appleUiIGO.transform.SetParent(otherScorePanel.transform);
            appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
            appleUiIGO.SetActive(true);
        }
        if (!(totalOthersRequired > 0))
        {
            otherScorePanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textCounter += Time.deltaTime;
        if (textCounter > 20f)
        {
            narratorText.text = "";
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene("MainMenuScene");          
        }

        if (!levelFinished)
        {
            timeElapsed += Time.deltaTime;
        }
        int displayTimeElapsed = (int)timeElapsed;
        timeText.text = "" + (displayTimeElapsed / 60).ToString("D2") + ":" + (displayTimeElapsed % 60).ToString("D2");

        if (RedApplesCollected > scoreRedApplesFound)
        {

            if ((scoreRedApples.Count > scoreRedApplesFound))
            {
                scoreRedApples[scoreRedApplesFound].sprite = redAppleSpriteFound;
            }
            scoreRedApplesFound++;
        }

        if (GreenApplesCollected > scoreGreenApplesFound)
        {

            if ((scoreGreenApples.Count > scoreGreenApplesFound))
            {
                scoreGreenApples[scoreGreenApplesFound].sprite = greenAppleSpriteFound;
            }
            scoreGreenApplesFound++;
        }

        if ((RedApplesCollected == totalRedApplesRequired) && (GreenApplesCollected == totalGreenApplesRequired))
        {
            levelFinished = true;
            if ((currentLevel != null) && (currentLevel.endingText != null ) &&(currentLevel.endingText.Length > 0))
            {
                narratorText.text = currentLevel.endingText;
            }

            
            exitTimer += Time.deltaTime;
        }

        if (exitTimer > exitDelay)
        {
            Singleton.Instance.lastLevelTimeTaken = timeElapsed;
            SceneManager.LoadScene("EndLevelScene");
        }
    }
    void FixedUpdate()
    {
        
    }
}
