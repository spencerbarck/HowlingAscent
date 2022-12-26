using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float bodySpeed = 10.0f; // player's climbing speed
    [SerializeField]private IcePick leftPick; // the left ice pick
    [SerializeField]private IcePick rightPick; // the right ice pick
    private bool isFalling;
    private float playerBodyIcePickCollisionZone = 0.8f;
    private Rigidbody2D rb; // the player's rigidbody component
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        HandlePickInput();
        if (leftPick.CheckPlanted()&&!rightPick.CheckPlanted())
        {
            if(isFalling)StopFalling();
            rightPick.MovePick();
        }
        else if(rightPick.CheckPlanted()&&!leftPick.CheckPlanted())
        {
            if(isFalling)StopFalling();
            leftPick.MovePick();
        }
        else if(rightPick.CheckPlanted()&&leftPick.CheckPlanted())
        {
            if(isFalling)StopFalling();
        }
        else if(!rightPick.CheckPlanted()&&!leftPick.CheckPlanted())
        {
            if(!isFalling)StartFalling();
        }
        if(!isFalling)
        {
            HandlePlayerBodyMovement();
        }
    }
    private void HandlePickInput()
    {
        float leftPickInput = Input.GetAxis("Fire1");
        if (leftPickInput > 0)
        {
            leftPick.Plant();
        }
        else if (leftPickInput == 0)
        {
            leftPick.Release();
        }
        float rightPickInput = Input.GetAxis("Fire2");
        if (rightPickInput > 0)
        {
            rightPick.Plant();
        }
        else if (rightPickInput == 0)
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
        rb.velocity = direction * bodySpeed;
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
        isFalling = true;
        rb.isKinematic = false;
        rb.gravityScale = 3;
    }
    private void StopFalling()
    {
        isFalling = false;
        rb.isKinematic = true;
        rb.gravityScale = 0;
    }
}

