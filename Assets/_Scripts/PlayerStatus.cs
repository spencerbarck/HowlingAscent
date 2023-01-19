using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]private IcePick leftPick;
    [SerializeField]private IcePick rightPick;
    private IcePicksPlantState currentPicksState = IcePicksPlantState.NonePlanted;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public bool CheckHasClimbedWall(int wallSummitHeight)
    {
        if(transform.position.y>=wallSummitHeight)
        {
            return true;
        }
        return false;
    }
    public bool CheckIsGrounded()
    {
        if (currentPicksState == IcePicksPlantState.NonePlanted)
        {
            float distanceToGround = 1.5f;
            RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y-1f), Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y-1f), Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
            
            if (hit1.collider != null)
            {
                if (hit1.point.y > transform.position.y - 1.25f&&!Physics2D.GetIgnoreCollision(hit1.collider, GetComponent<Collider2D>()))
                {
                    return true;
                }
            }
            if (hit2.collider != null)
            {
                if (hit2.point.y > transform.position.y - 1.25f&&!Physics2D.GetIgnoreCollision(hit2.collider, GetComponent<Collider2D>())) 
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsFallingToDeath()
    {
        if(CheckIsGrounded())
        {
            if(rb.velocity.y <= -15f)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    private void Update()
    {
        currentPicksState = (leftPick.CheckPlanted() && rightPick.CheckPlanted()) ? IcePicksPlantState.BothPlanted : 
            (!leftPick.CheckPlanted() && rightPick.CheckPlanted() || leftPick.CheckPlanted() && !rightPick.CheckPlanted()) ? IcePicksPlantState.OnePlanted : 
            IcePicksPlantState.NonePlanted;

        
            float distanceToGround = 1.5f;

        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y-2f), Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y-2f), Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
            if (hit1.collider != null)
            {
                // Draw the ray in the scene view
                Debug.DrawRay(hit1.point, Vector2.down * hit1.distance, Color.green);
            }
            
            if (hit1.collider != null)
        {
            // Draw the ray in the scene view
            Debug.DrawRay(hit1.point, Vector2.down * hit1.distance, Color.green);
        }
        else
        {
            // Draw the ray in the scene view
            Debug.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y-2f), Vector2.down * distanceToGround, Color.red);
        }
        if (hit2.collider != null)
        {
            // Draw the ray in the scene view
            Debug.DrawRay(hit2.point, Vector2.down * hit2.distance, Color.green);
        }
        else
        {
            // Draw the ray in the scene view
            Debug.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y-2f), Vector2.down * distanceToGround, Color.red);
        }
    }
    public IcePicksPlantState GetIcePicksPlantState()
    {
        return currentPicksState;
    }
    public bool CheckIsPlanted()
    {
        return currentPicksState != IcePicksPlantState.NonePlanted;
    }
    public bool CheckIsFalling()
    {
        return currentPicksState == IcePicksPlantState.NonePlanted;
    }
}
public enum IcePicksPlantState
{
    OnePlanted,
    BothPlanted,
    NonePlanted
}