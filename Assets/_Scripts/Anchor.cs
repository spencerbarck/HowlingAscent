using UnityEngine;

public class Anchor : MonoBehaviour
{
    // The length of the rope
    [SerializeField] private float ropeLength = 2f;
    [SerializeField] private LayerMask ropeCollisionMask;
    [SerializeField] private Transform followTransform;
    private Rigidbody2D rb;
    private bool dropped;
    private Vector2 groundTransform = new Vector2();
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
        lineRenderer.sortingOrder = 40;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(2, followTransform.position);
    }

    void Update()
    {
        if(!dropped)
        {
            transform.position = followTransform.position;
            groundTransform = followTransform.position;
        }

        if (Input.GetKeyDown(KeyCode.E)&&!dropped)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, ropeLength, ropeCollisionMask);
            if (hit.collider != null)
            {
                groundTransform = hit.point;
                dropped = true;
            }
        }  
        lineRenderer.SetPosition(0, followTransform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(2, groundTransform);
    }
}