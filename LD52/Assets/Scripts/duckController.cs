using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckController : MonoBehaviour
{
    public float speed = 3f;

    public AudioSource audioSource;

    public AudioClip quack1Clip;
    public AudioClip quack2Clip;
    public AudioClip quack3Clip;

    float direction;
    private Rigidbody2D body;
    float changeDirCounter;
    float lastPositionCheck;
    Vector2 lastPosition;
    private float quackTimer;
    private float quackLimit;
    // Start is called before the first frame update
    void Start()
    {
        quackLimit = 2f;
        quackTimer = 0f;
        lastPositionCheck = 0f;
        direction = 0.5f;
        body = GetComponent<Rigidbody2D>();
        changeDirCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if ((lastPosition == null) || (lastPosition.x != body.position.x))
        //if ((lastPosition == null) || (Mathf.Abs(lastPosition.x - body.position.x)) < 0.01f)
        // {
        //   lastPositionChange = Time.time;
        //    lastPosition = body.position;
        // }
        //if ((Time.time - lastPositionChange) > 3.0f)
        //if ((lastPosition != null) && (Mathf.Abs(lastPosition.x - body.position.x) < 0.001f))
        //{
        //    direction = direction * -1;
        //}
        //lastPosition = body.position;
        quackTimer += Time.deltaTime;
        lastPositionCheck += Time.deltaTime;
        changeDirCounter += Time.deltaTime;

        if (changeDirCounter > 5f)
        {
            changeDirCounter = 0f;
            int changeRoll = Random.Range(0, 31);
            if (changeRoll == 30)
            {
                direction = direction * -1;
            }
        }
        if (direction > 0f)
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (direction < 0f)
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
        }
        if (lastPositionCheck > 1f)
        {
            lastPositionCheck = 0f;
            if ((lastPosition != null) && (Mathf.Abs(lastPosition.x - body.position.x) < 0.1f))
            {
                direction = direction * -1;
            }
            lastPosition = body.position;
        }
        if (quackTimer > quackLimit)
        {
            quackTimer = 0f;
            quackLimit = Random.Range(0f, 5f);
            int whichQuack = Random.Range(1, 4);
            if (whichQuack == 1)
            {
                playAudio(quack1Clip, audioSource, false);
            }
            if (whichQuack == 2)
            {
                playAudio(quack2Clip, audioSource, false);
            }
            if (whichQuack == 3)
            {
                playAudio(quack3Clip, audioSource, false);
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
