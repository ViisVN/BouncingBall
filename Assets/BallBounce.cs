using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{

    private Rigidbody2D _rb;
    public float speed = 10f;

private void Start()
{
    _rb = GetComponent<Rigidbody2D>();
}

private void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        var thisspeed =speed;
        // Get the mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 ballPos = new Vector2(transform.position.x, transform.position.y);

        // Calculate the direction from the ball to the mouse position
        Vector2 direction = (mousePosition - ballPos).normalized;
        Debug.Log(direction);
        // Set the velocity of the ball based on the normalized direction and speed
        _rb.velocity = direction * thisspeed;
    }
}
}
