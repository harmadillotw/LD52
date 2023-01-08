using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpSpeed = 10.0f;
    public GameObject gameController;

    public AudioSource audioSource;
    public AudioSource audioSourceShort;

    public AudioClip walkClip;
    public AudioClip jumpClip;
    public AudioClip collectClip;

    private Rigidbody2D body;
    private float horizontal;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");

        if (horizontal > 0f)
        {
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
            playAudio(walkClip, audioSource, true);
        }
        else if (horizontal < 0f)
        {
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
            playAudio(walkClip, audioSource, true);
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        if ((Input.GetKeyDown(KeyCode.Space)) && (isTouchingGround))
        {
            playAudio(jumpClip, audioSourceShort, false);
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(body.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);


    }
    //private void FixedUpdate()
    //{
    //Vector2 movement = new Vector2(horizontal * speed, body.velocity.y);



    //body.velocity = movement;
    //transform.up = direction;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RedApple")
        {
            playAudio(collectClip, audioSourceShort, false);
            gameController.GetComponent<gameController>().RedApplesCollected++;
            //Singleton.Instance.objectsCollected++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "GreenApple")
        {
            playAudio(collectClip, audioSourceShort, false);
            gameController.GetComponent<gameController>().GreenApplesCollected++;
            //Singleton.Instance.objectsCollected++;
            Destroy(collision.gameObject);
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
