using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalBoundary;
    public float horizontalSpeed;
    public float maxSpeed;

    private Vector3 endTouch;
    private Rigidbody2D m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        endTouch = new Vector3();
        m_rigidBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        float direction = 0.0f;

        
        //simple touch input
        foreach(var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0.0f));

            //touch input support
            if (worldTouch.x > transform.position.x)
            {
                //direction is positive
                direction = 1.0f;

            }
            if (worldTouch.x < transform.position.x)
            {
                //direction is negative
                direction = -1.0f;
            }

            endTouch = worldTouch;
        }

       
        
        //keyboard input
        if(Input.GetAxis("Horizontal") >= 0.1f)
        {
            //direction is positive
            direction = 1.0f;

        }
        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            //direction is negative
            direction = -1.0f;
        }

        

        if (endTouch.x != 0.0f)
        {
            Debug.Log("lerping");
            transform.position = new Vector2( Mathf.Lerp(transform.position.x, endTouch.x, 0.1), transform.position.y);
        }
        else
        {
            var newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);

            m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            m_rigidBody.velocity *= 0.99f;//to prevent it from moving forever
        }

    }

    private void _CheckBounds()
    {
        //check right bounds
        if(transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0.0f);
        }

        //check left bounds
        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0.0f);
        }
    }

}
