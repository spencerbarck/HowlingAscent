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
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //leftPick.MovePickGrounded(horizontalInput,verticalInput);
        //rightPick.MovePickGrounded(horizontalInput,verticalInput);
    }
}
