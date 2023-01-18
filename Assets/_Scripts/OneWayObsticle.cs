using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObsticle : MonoBehaviour
{
    private Collider2D oneWayCollider;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public bool isOpaque;
    [SerializeField] private GameObject player;
    [SerializeField] private Color hoverColor;
    private void Start()
    {
        oneWayCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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

        if (!isOpaque)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                spriteRenderer.color = hoverColor;
                isOpaque = true;
            }
        }
    }
}



