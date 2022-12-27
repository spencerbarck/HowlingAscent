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
        HandlePickInput();
        if(!rightPick.CheckPlanted()&&!leftPick.CheckPlanted())
        {
            if(!playerStatus.CheckIsFalling())StartFalling();
        }
        if(playerStatus.CheckGrounded())
        {
            return;
        }
        if (leftPick.CheckPlanted()&&!rightPick.CheckPlanted())
        {
            if(playerStatus.CheckIsFalling())StopFalling();
            rightPick.MovePick(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        }
        else if(rightPick.CheckPlanted()&&!leftPick.CheckPlanted())
        {
            if(playerStatus.CheckIsFalling())StopFalling();
            leftPick.MovePick(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        }
        else if(rightPick.CheckPlanted()&&leftPick.CheckPlanted())
        {
            if(playerStatus.CheckIsFalling())StopFalling();
        }
        if(!playerStatus.CheckIsFalling())
        {
            HandlePlayerBodyMovement();
        }
    }
    private void HandlePickInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            leftPick.Plant();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            leftPick.Release();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            rightPick.Plant();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            rightPick.Release();
        }
    }
    private void HandlePlayerBodyMovement()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector2.zero;
            return;
        }
        float threshold = 0.1f;
        rb.isKinematic = false;

        Vector2 targetPosition = GetMoveTargetPosition();
        if (Vector2.Distance(targetPosition, transform.position) <= threshold)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        if ((Mathf.Abs(rb.transform.position.x - targetPosition.x) <= playerBodyIcePickCollisionZone)
        &&(!(leftPick.CheckPlanted() && rightPick.CheckPlanted())))
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
        Vector2 midpoint = (leftPick.transform.position + rightPick.transform.position) / 2;
        return midpoint;
    }
    private void StartFalling()
    {
        playerStatus.SetIsFalling(true);
        rb.isKinematic = false;
        rb.gravityScale = 3;
    }
    private void StopFalling()
    {
        playerStatus.SetIsFalling(false);
        rb.isKinematic = true;
        rb.gravityScale = 0;
    }
}

