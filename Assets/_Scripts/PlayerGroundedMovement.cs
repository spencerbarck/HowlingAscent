using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedMovement : MonoBehaviour
{
    [SerializeField]private float movementSpeed = 10.0f; // the speed at which the player moves
    [SerializeField]private float jumpForce = 10.0f; // the force with which the player jumps
    [SerializeField]private IcePick leftPick;
    [SerializeField]private IcePick rightPick;
    private PlayerStatus playerStatus; // the player's PlayerStatus script
    private Rigidbody2D rb; // the player's rigidbody component
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStatus = GetComponent<PlayerStatus>();
    }
    private void Update()
    {
        if(!playerStatus.CheckGrounded())
        {
            return;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        leftPick.MovePickGrounded(horizontalInput,verticalInput);
        rightPick.MovePickGrounded(horizontalInput,verticalInput);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
