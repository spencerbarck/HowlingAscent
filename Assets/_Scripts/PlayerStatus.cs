using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]private IcePick leftPick;
    [SerializeField]private IcePick rightPick;
    [SerializeField]private GameObject coldIndicator;
    private IcePicksPlantState currentPicksState = IcePicksPlantState.NonePlanted;
    private Rigidbody2D rb;
    private bool isCold;
    private float coldDuration = 2f;
    private float coldTimer;
    public Vector3 playerRespawnPoint;
    public UnityEvent screenShake;
    private void Start()
    {
        isCold = false;
        coldTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        playerRespawnPoint = transform.position;
    }
    public void SetRespawnPoint(Vector3 respawnPoint)
    {
        playerRespawnPoint = respawnPoint;
    }
    public void RespawnPlayer()
    {
        transform.position = playerRespawnPoint;
    }
    public bool CheckIfPlayerIsCold()
    {
        return isCold;
    }
    public bool CheckHasClimbedWall(float wallSummitHeight)
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
                screenShake.Invoke();
                return true;
            }
            return false;
        }
        return false;
    }
    private void Update()
    {
        if (isCold)
        {
            coldTimer += Time.deltaTime;
            if (coldTimer >= coldDuration)
            {
                coldIndicator.SetActive(false);
                isCold = false;
                coldTimer = 0f;
            }
        }
        currentPicksState = (leftPick.CheckPlanted() && rightPick.CheckPlanted()) ? IcePicksPlantState.BothPlanted : 
            (!leftPick.CheckPlanted() && rightPick.CheckPlanted() || leftPick.CheckPlanted() && !rightPick.CheckPlanted()) ? IcePicksPlantState.OnePlanted : 
            IcePicksPlantState.NonePlanted;
    }
    public void OnHitByWind()
    {
        if(currentPicksState == IcePicksPlantState.OnePlanted)
        {
            rightPick.Release();
            leftPick.Release();
        }
        if (!isCold)
        {
            coldIndicator.SetActive(true);
            isCold = true;
            coldTimer = 0f;
            screenShake.Invoke();
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