using UnityEngine;

public class PlayerMovementToggler : MonoBehaviour
{
    private PlayerClimbingMovement climbingMovement; // Reference to the climbing movement script
    private PlayerGroundedMovement groundedMovement; // Reference to the walking movement script
    private PlayerFallingMovement fallingMovement; // Reference to the falling movement script
    private PlayerStatus playerStatus;
    public enum MovementState
    {
        Grounded,
        Climbing,
        Falling
    }
    public MovementState currentState = MovementState.Falling; // The current movement state
    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        
        climbingMovement = GetComponent<PlayerClimbingMovement>();
        groundedMovement = GetComponent<PlayerGroundedMovement>();
        fallingMovement = GetComponent<PlayerFallingMovement>();
    }
    void Update()
    {
        if(GameMananger.Instance.GetGameState() == GameState.Death)
        {
            DisableAllMovement();
            return;
        }
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
    void DisableAllMovement()
    {
        climbingMovement.enabled = false;
        groundedMovement.enabled = false;
        fallingMovement.enabled = false;
    }
}