using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObsticle : MonoBehaviour
{
    private Collider2D oneWayCollider;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public bool isOpaque;
    [SerializeField] private Color hoverColor;
    [SerializeField] private GameObject player;
    private void Start()
    {
        oneWayCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Ground"), LayerMask.NameToLayer("Climbable"), true);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)||Input.GetMouseButton(1))
        {
            oneWayCollider.enabled = false;
        }
        else
        {
            oneWayCollider.enabled = true;
        }
        int layerMask = ~((1 << LayerMask.NameToLayer("Climbable")) | (1 << LayerMask.NameToLayer("Ground")));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, layerMask);
        if (hit.collider != null)
        {
            spriteRenderer.color = hoverColor;
            isOpaque = true;
        }
        else
        {
            spriteRenderer.color = originalColor;
            isOpaque = false;
        }
    }
}



