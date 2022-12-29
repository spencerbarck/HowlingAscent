using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerClimbingMovement : MonoBehaviour
{
    [SerializeField]private float bodyClimbSpeed = 10.0f; // player's climbing speed
    [SerializeField]private IcePick leftPick; // the left ice pick
    [SerializeField]private IcePick rightPick; // the right ice pick
    private PlayerStatus playerStatus;
    private float playerBodyIcePickCollisionZone = 0.8f;
    private Rigidbody2D rb; // the player's rigidbody component
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStatus = GetComponent<PlayerStatus>();
    }
    private void Update()
    {
        StopFalling();
        HandlePlayerBodyMovement();
    }
    private void HandlePlayerBodyMovement()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            return;
        }
        rb.isKinematic = false;
        
        float threshold = 0.1f;
        Vector2 targetPosition = GetMoveTargetPosition();

        rb.velocity = (Vector2.Distance(targetPosition, transform.position) <= threshold) ? Vector2.zero : rb.velocity;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        if ((Mathf.Abs(rb.transform.position.x - targetPosition.x) <= playerBodyIcePickCollisionZone)
        &&(playerStatus.GetIcePicksPlantState()!=IcePicksPlantState.BothPlanted))
        {
            direction.x = 0;
        }
        rb.velocity = direction * bodyClimbSpeed;
    }
    private Vector2 GetMoveTargetPosition()
    {
        if (leftPick.CheckPlanted() && rightPick.CheckPlanted())
        {
            return GetMiddleOfTwoPlantedPicks();
        }
        else
        {
            return GetPositionOfSinglePlantedPick();
        }
    }
    private Vector2 GetPositionOfSinglePlantedPick()
    {
        IcePick[] picks = { leftPick, rightPick };
        IcePick closestPick = picks.FirstOrDefault(pick => pick.CheckPlanted());
        return closestPick != null ? closestPick.transform.position : new Vector2(float.MaxValue, float.MaxValue);
    }
    private Vector2 GetMiddleOfTwoPlantedPicks()
    {
        return (leftPick.transform.position + rightPick.transform.position) / 2;
    }
    private void StopFalling()
    {
        rb.gravityScale = 0;
    }
}

