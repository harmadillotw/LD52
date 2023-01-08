using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractorController : MonoBehaviour
{
    public float speed = 3f;

    public AudioSource audioSource;

    public AudioClip tractorClip;

    float direction;
    private Rigidbody2D body;
    float changeDirCounter;

    float lastPositionCheck;
    Vector2 lastPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPositionCheck = 0f;
        direction = 0.5f;
        body = GetComponent<Rigidbody2D>();
        changeDirCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        lastPositionCheck += Time.deltaTime;
        if (direction > 0f)
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
            playAudio(tractorClip, audioSource, true);
        }
        else if (direction < 0f)
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
            playAudio(tractorClip, audioSource, true);
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
