using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]private IcePick leftPick;
    [SerializeField]private IcePick rightPick;
    private bool isFalling;
    public bool CheckGrounded()
    {
        if (leftPick.CheckPlanted() || rightPick.CheckPlanted())
        {
            return false;
        }
        float distanceToGround = 2f;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x+1,transform.position.y), Vector2.down, distanceToGround,LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return true;
        }
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x-1,transform.position.y), Vector2.down, distanceToGround,LayerMask.GetMask("Ground"));
        if (hit2.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetIsFalling(bool falling)
    {
        isFalling = falling;
    }
    public bool CheckIsFalling()
    {
        return isFalling;
    }
}
