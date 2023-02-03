using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedMovement : MonoBehaviour
{
    [SerializeField]private float movementSpeed = 10.0f; // the speed at which the player moves
    [SerializeField]private float jumpForce = 10.0f; // the force with which the player jumps
    private Rigidbody2D rb; // the player's rigidbody component
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
{
    float horizontalInput = Input.GetAxis("Horizontal");
    float horizontalSpeed = 0.0f;
    if (horizontalInput != 0.0f)
    {
        horizontalSpeed = horizontalInput * movementSpeed;
    }
    Vector2 horizontalForce = new Vector2(horizontalSpeed - rb.velocity.x, 0);
    rb.AddForce(horizontalForce * 5);
    if (Input.GetKeyDown(KeyCode.Space))
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
}
