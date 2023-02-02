using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObsticle : MonoBehaviour
{
    private Collider2D oneWayCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer ledgeSpriteRenderer;
    [SerializeField] private Color hoverColor;
    private Color originalColor;
    [SerializeField] private Color hoverColor2;
    private Color originalColor2;
    private GameObject player;
    private GameObject leftPick;
    private GameObject rightPick;
    private void Start()
    {
        oneWayCollider = GetComponent<Collider2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<PlayerStatus>().gameObject;
        leftPick = GameObject.Find("LeftPick");
        rightPick = GameObject.Find("RightPick");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Climbable"), true);
        originalColor = spriteRenderer.color;
        originalColor2 = ledgeSpriteRenderer.color;
    }
    private void FixedUpdate()
    {
        bool ignore = Input.GetMouseButton(0) || Input.GetMouseButton(1) || player.transform.position.y - 1f < transform.position.y ||
                    (player.transform.position.y > transform.position.y - 1 && player.transform.position.y < transform.position.y + 1);

        Physics2D.IgnoreCollision(oneWayCollider, player.GetComponent<Collider2D>(), ignore);
        Physics2D.IgnoreCollision(oneWayCollider, leftPick.GetComponent<Collider2D>(), ignore);
        Physics2D.IgnoreCollision(oneWayCollider, rightPick.GetComponent<Collider2D>(), ignore);
        
        bool ignore2 = (player.transform.position.y - 1f < transform.position.y && (Input.GetMouseButton(0) || Input.GetMouseButton(1))) ||
                    (player.transform.position.y > transform.position.y - 1 && player.transform.position.y < transform.position.y + 1);
        spriteRenderer.color = ignore2 ? hoverColor : originalColor;
        ledgeSpriteRenderer.color = ignore2 ? hoverColor2 : originalColor2;
    }
}



