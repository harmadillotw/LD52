using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propController : MonoBehaviour
{

    private float speed = 10f;
    private float startPosX;
    private float startPosY;
    private bool dragging = false;

    private Rigidbody2D body;

    private Vector3 offset;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 lastPosition;

    private float counter;
    public float checkCounter = 0.2f;

    private bool isPlayerOnProp = false;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            counter += Time.deltaTime;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            if (counter > checkCounter)
            {
                if (lastPosition == transform.position)
                {
                    startPosition = lastPosition;
                }
                lastPosition = transform.position;
                counter = 0f;
            }
            lastPosition = transform.position;
        }
    }

    private void OnMouseDown()
    {
        if (!isPlayerOnProp)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
            startPosition = transform.position;
            lastPosition = startPosition;
            counter = 0f;
        }
    }

    private void OnMouseUp()
    {
        if (!isPlayerOnProp)
        {
            dragging = false;
            endPosition = transform.position;
            Vector3 dir = (endPosition - startPosition).normalized;
            float dist = Vector3.Distance(startPosition, endPosition);
            float useSpeed = speed * (dist / 10f);
            if (useSpeed > speed)
            {
                useSpeed = speed;
            }
            body.velocity = new Vector2(dir.x * useSpeed, dir.y * useSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter collission " + collision.tag);
        //if (collision.tag == "Player")
        //{
        if (!collision.isTrigger)
        {
            isPlayerOnProp = true;
        }
       // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit collission " + collision.tag);
        //if (collision.tag == "Player")
        //{
        if (!collision.isTrigger)
        {
            isPlayerOnProp = false;
        }
        //}
    }
}
