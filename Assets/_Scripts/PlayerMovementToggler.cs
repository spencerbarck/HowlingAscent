using UnityEngine;

public class PlayerMovementToggler : MonoBehaviour
{
    private PlayerClimbingMovement climbingMovement; // Reference to the climbing movement script
    private PlayerGroundedMovement groundedMovement; // Reference to the walking movement script
    private PlayerFallingMovement fallingMovement; // Reference to the falling movement script
    private PlayerStatus playerStatus;
    private enum MovementState
    {
        Grounded,
        Climbing,
        Falling
    }
    private MovementState currentState = MovementState.Falling; // The current movement state
    private void Start()
    {
        climbingMovement = GetComponent<PlayerClimbingMovement>();
        groundedMovement = GetComponent<PlayerGroundedMovement>();
        fallingMovement = GetComponent<PlayerFallingMovement>();
        playerStatus = GetComponent<PlayerStatus>();
    }
    void Update()
    {
        if((currentState == MovementState.Falling)&&(playerStatus.CheckIsFallingToDeath()))
        {
            GameMananger.Instance.ChangeGameState(GameState.Death);
            currentState = MovementState.Grounded;
        }
        else
        {
            currentState = playerStatus.CheckIsGrounded() ? MovementState.Grounded : (playerStatus.CheckIsFalling()
             ? MovementState.Falling : MovementState.Climbing);
        }
        switch (currentState)
        {
            case MovementState.Grounded:
                climbingMovement.enabled = false;
                groundedMovement.enabled = true;
                fallingMovement.enabled = false;
                break;
            case MovementState.Climbing:
                climbingMovement.enabled = true;
                groundedMovement.enabled = false;
                fallingMovement.enabled = false;
                break;
            case MovementState.Falling:
                climbingMovement.enabled = false;
                groundedMovement.enabled = false;
                fallingMovement.enabled = true;
                break;
        }
    }
}