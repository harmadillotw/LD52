using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

  
    public Dictionary<int, Level> levelLookup = new Dictionary<int, Level>();

    public int currentLevel = 1;

    public int objectsCollected = 0;
    public int totalObjects = 0;

    //level scores
    public int levelCompleted;
    public float levelTime;
    public int redApplesCollected;
    public int greenApplesCollected;
    public int pearsCollected;
    public int othersCollected;

    public float lastLevelTimeTaken;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
