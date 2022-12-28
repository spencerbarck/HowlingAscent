using UnityEngine;

public class PlayerFallingMovement : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStatus = GetComponent<PlayerStatus>();
    }
    void Update()
    {
        StartFalling();
    }
    private void StartFalling()
    {
        rb.isKinematic = false;
        rb.gravityScale = 3;
    }
}
