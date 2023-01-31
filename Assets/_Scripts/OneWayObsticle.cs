using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObsticle : MonoBehaviour
{
    private Collider2D oneWayCollider;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject leftPick;
    [SerializeField] private GameObject rightPick;
    private void Start()
    {
        oneWayCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Climbable"), true);
    }
    private void FixedUpdate()
    {
        bool ignore = Input.GetMouseButton(0) || Input.GetMouseButton(1) || player.transform.position.y - 1f < transform.position.y ||
                    (player.transform.position.y > transform.position.y - 1 && player.transform.position.y < transform.position.y + 1);

        Physics2D.IgnoreCollision(oneWayCollider, player.GetComponent<Collider2D>(), ignore);
        Physics2D.IgnoreCollision(oneWayCollider, leftPick.GetComponent<Collider2D>(), ignore);
        Physics2D.IgnoreCollision(oneWayCollider, rightPick.GetComponent<Collider2D>(), ignore);
        spriteRenderer.color = ignore ? hoverColor : originalColor;
    }
}



