using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IcePick : MonoBehaviour
{
    [SerializeField]private float armSpeed = 10.0f; // player's climbing speed
    [SerializeField]private float armLength = 1.0f; // player's arm length
    [SerializeField]private float armXOffset;
    [SerializeField]private float playerBodyIcePickCollisionZone = 0.8f;
    private bool isPlanted = false; // whether the pick is currently planted
    private SpriteRenderer pickSprite; // the pick's sprite renderer component
    private Rigidbody2D rb; // the player's rigidbody component
    [SerializeField] PlayerStatus playerBody;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pickSprite = GetComponent<SpriteRenderer>();
        pickSprite.color = Color.red;
    }
    private void Update()
    {
        if(!isPlanted)
        {
            ClampPickPosition();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void ClampPickPosition()
    {
        float minX = playerBody.transform.position.x + playerBodyIcePickCollisionZone;
        float maxX = playerBody.transform.position.x + armLength + armXOffset;
        if (playerBodyIcePickCollisionZone < 0)
        {
            minX = playerBody.transform.position.x - armLength - armXOffset;
            maxX = playerBody.transform.position.x + playerBodyIcePickCollisionZone;
        }
        float minY = playerBody.transform.position.y - armLength;
        float maxY = playerBody.transform.position.y + armLength;

        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );
        transform.position = clampedPosition;
    }
    public void MovePick(float xInput, float yInput)
    {
        rb.velocity = new Vector2(xInput*armSpeed,yInput*armSpeed);
    }
    public void MovePickGrounded(float xInput,float yInput)
    {
        float speed = 15f;
        transform.position = new Vector2(
                Mathf.Lerp(transform.position.x
                , playerBody.transform.position.x+playerBodyIcePickCollisionZone
                , Time.deltaTime * speed)
            ,transform.position.y
        );
        Vector2 newVelocity = new Vector2(xInput*armSpeed,yInput*armSpeed);
        if(Input.GetKeyDown(KeyCode.Space))newVelocity.y*=2f;
        rb.velocity = newVelocity;
    }
    public void Plant()
    {
        if (!isPlanted&&IsOverClimbable())
        {
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
    private bool IsOverClimbable()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.back, 20f, LayerMask.GetMask("Climbable"));
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}