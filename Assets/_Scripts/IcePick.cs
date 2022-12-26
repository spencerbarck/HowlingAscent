using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IcePick : MonoBehaviour
{
    [SerializeField]private float armSpeed = 10.0f; // player's climbing speed
    [SerializeField]private float armLength = 1.0f; // player's arm length
    [SerializeField]private float armXOffset;
    private bool isPlanted = false; // whether the pick is currently planted
    private SpriteRenderer pickSprite; // the pick's sprite renderer component
    private Rigidbody2D rb; // the player's rigidbody component
    private FixedJoint2D joint;
    [SerializeField] PlayerMovement playerBody;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pickSprite = GetComponent<SpriteRenderer>();
        joint = GetComponent<FixedJoint2D>();
        pickSprite.color = Color.red;
    }
    public void LockJoint()
    {
        joint.enabled = true;
    }
    public void UnlockJoint()
    {
        joint.enabled = false;
    }
    public void MovePick()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(xInput*armSpeed,yInput*armSpeed);
        Vector2 newPosition = new Vector2(
            Mathf.Clamp(transform.position.x, playerBody.transform.position.x - armLength - armXOffset, playerBody.transform.position.x + armLength + armXOffset),
            Mathf.Clamp(transform.position.y, playerBody.transform.position.y - armLength, playerBody.transform.position.y + armLength)
        );
        transform.position = newPosition;
    }
    public void Plant()
    {
        if (!isPlanted)
        {
            rb.velocity = Vector2.zero;
            isPlanted = true;
            pickSprite.color = Color.green;
        }
    }
    public void Release()
    {
        if (isPlanted)
        {
            isPlanted = false;
            pickSprite.color = Color.red;
        }
    }
    public bool CheckPlanted()
    {
        return isPlanted;
    }
}