using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]private IcePick leftPick;
    [SerializeField]private IcePick rightPick;
    private IcePicksPlantState currentPicksState = IcePicksPlantState.NonePlanted;
    public bool CheckIsGrounded()
    {
        if (currentPicksState == IcePicksPlantState.NonePlanted)
        {
            float distanceToGround = 1.5f;
            RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), Vector2.down, distanceToGround, LayerMask.GetMask("Ground"));
            return hit1.collider != null || hit2.collider != null;
        }
        else
        {
            return false;
        }
    }
    private void Update()
    {
        currentPicksState = (leftPick.CheckPlanted() && rightPick.CheckPlanted()) ? IcePicksPlantState.BothPlanted : 
            (!leftPick.CheckPlanted() && rightPick.CheckPlanted() || leftPick.CheckPlanted() && !rightPick.CheckPlanted()) ? IcePicksPlantState.OnePlanted : 
            IcePicksPlantState.NonePlanted;
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