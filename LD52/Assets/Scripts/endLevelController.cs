using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class endLevelController : MonoBehaviour
{
    
    public Sprite redAppleSprite;
    public Sprite greenAppleSprite;

    public GameObject duckGO;

    public AudioSource audioSource;

    public AudioClip collectClip;


    private GameObject redAppleScorePanel;
    private GameObject greenAppleScorePanel;

    private TextMeshProUGUI timeText;
    private TextMeshProUGUI titleText;

    private Level finsihedLevel;

    private int redAppleCount;
    private int greenAppleCount;

    private int redApplesDisplayed;
    private int greenApplesDisplayed;

    private int panelSize = 65;

    private int totalAppleCount =0;
    private int totalApplesDisplayed = 0;

    private float displayCount; 

    private bool spawnDucks;
    private int duckCount;

    private float duckTimer;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        titleText = GameObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
        redAppleScorePanel = GameObject.Find("RedAppleScorePanel");
        greenAppleScorePanel = GameObject.Find("GreenAppleScorePanel");

        displayCount = 0f;
        totalAppleCount = 0;
        totalApplesDisplayed = 0;
        greenApplesDisplayed = 0;
        redApplesDisplayed = 0;
        spawnDucks = false;
        duckCount = 0;
        duckTimer = 0f;
        if (Singleton.Instance.levelLookup.ContainsKey(Singleton.Instance.currentLevel))
        {
            
            Singleton.Instance.levelLookup.TryGetValue(Singleton.Instance.currentLevel, out finsihedLevel);
            titleText.text = "Level " + finsihedLevel.levelNumber + " Completed";
            int displayTimeElapsed = (int)Singleton.Instance.lastLevelTimeTaken;
            timeText.text = "" + (displayTimeElapsed / 60).ToString("D2") + ":" + (displayTimeElapsed % 60).ToString("D2");
            redAppleCount = finsihedLevel.redApples;
            greenAppleCount = finsihedLevel.greenApples;
            totalAppleCount = redAppleCount + greenAppleCount;

            redAppleScorePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, redAppleCount * panelSize);
            //redAppleScorePanel.GetComponent<RectTransform>().position = new Vector2(0f, 0f);
            //for (int i = 0; i < redAppleCount; i++)
            //{
            //    GameObject appleUiIGO = new GameObject();
            //    Image appleImage = appleUiIGO.AddComponent<Image>();
            //    //i.color = Color.red;
            //    appleImage.sprite = redAppleSprite;
            //    //scoreRedApples.Add(appleImage);
            //    appleUiIGO.transform.SetParent(redAppleScorePanel.transform);
            //    appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
            //    appleUiIGO.SetActive(true);
            //}

            if (!(redAppleCount > 0))
            {
                redAppleScorePanel.SetActive(false);
            }

            greenAppleScorePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, greenAppleCount * panelSize);


            //for (int i = 0; i < greenAppleCount; i++)
            //{
            //    GameObject appleUiIGO = new GameObject();
            //    Image appleImage = appleUiIGO.AddComponent<Image>();
            //    //i.color = Color.red;
            //    appleImage.sprite = greenAppleSprite;
            //    //scoreGreenApples.Add(appleImage);
            //    appleUiIGO.transform.SetParent(greenAppleScorePanel.transform);
            //    appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
            //    appleUiIGO.SetActive(true);
            //}
            if (!(greenAppleCount > 0))
            {
                greenAppleScorePanel.SetActive(false);
            }


        }

        Singleton.Instance.currentLevel++;

        

        if (!Singleton.Instance.levelLookup.ContainsKey(Singleton.Instance.currentLevel))
        {
            titleText.text = titleText.text + "\n You finished all the levels, maybe. Have some ducks.";
            spawnDucks = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        displayCount += Time.deltaTime;
        duckTimer += Time.deltaTime;

        if (displayCount > 0.5f)
        {
            displayCount = 0f;
            if (totalApplesDisplayed < totalAppleCount)
            {
                if (redApplesDisplayed < redAppleCount)
                {
                    playAudio(collectClip, audioSource, false);
                    GameObject appleUiIGO = new GameObject();
                    Image appleImage = appleUiIGO.AddComponent<Image>();
                    //i.color = Color.red;
                    appleImage.sprite = redAppleSprite;
                    //scoreRedApples.Add(appleImage);
                    appleUiIGO.transform.SetParent(redAppleScorePanel.transform);
                    appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
                    appleUiIGO.SetActive(true);
                    redApplesDisplayed++;
                }
                else
                {
                    if (greenApplesDisplayed < greenAppleCount)
                    {
                        playAudio(collectClip, audioSource, false);
                        GameObject appleUiIGO = new GameObject();
                        Image appleImage = appleUiIGO.AddComponent<Image>();
                        //i.color = Color.red;
                        appleImage.sprite = greenAppleSprite;
                        //scoreGreenApples.Add(appleImage);
                        appleUiIGO.transform.SetParent(greenAppleScorePanel.transform);
                        appleUiIGO.transform.localScale = new Vector2(0.5f, 0.5f);
                        appleUiIGO.SetActive(true);
                        greenApplesDisplayed++;
                    }
                }

                totalApplesDisplayed++;
            }
        }


        if (spawnDucks)
        {
            if (duckTimer > 2f)
            {
                duckTimer = 0f;
                if (duckGO != null)
                {
                    GameObject dGO = Instantiate(duckGO, new Vector3(0, -3, 0), Quaternion.identity);
                    if ((duckCount % 2) > 0)
                    {
                        dGO.GetComponent<Rigidbody2D>().velocity = new Vector2(dGO.GetComponent<Rigidbody2D>().velocity.x * -1, dGO.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    duckCount++;
                }
            }
        }
        
    }

    public void goNextLevel()
    {
        if (Singleton.Instance.levelLookup.ContainsKey(Singleton.Instance.currentLevel))
        {
            Level nextLevel ;
            Singleton.Instance.levelLookup.TryGetValue(Singleton.Instance.currentLevel, out nextLevel);
            if ((nextLevel != null) && (nextLevel.levelName.Length > 0))
            {
                SceneManager.LoadScene(nextLevel.levelName);
            }
        }

    }

    private void playAudio(AudioClip clip, AudioSource audioSource, bool contPlay)
    {
        if ((contPlay) && (audioSource.isPlaying))
        {
            return;
        }
        int volumeSet = PlayerPrefs.GetInt("FXvolumeSet");
        float vol = 1f;
        if (volumeSet > 0)
        {
            int volume = PlayerPrefs.GetInt("FXVolume");
            vol = 1f;
            vol = (float)volume / 100f;
        }

        audioSource.PlayOneShot(clip, vol);
    }
}
