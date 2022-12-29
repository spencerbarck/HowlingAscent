using UnityEngine;

public class Anchor : MonoBehaviour
{
    private Transform followTransform;
    private LayerMask ropeCollisionMask;
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;
    private Rigidbody2D rb;
    private Anchor parentAnchor;
    public void InitFollow(Transform follow, Anchor anchor)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = 40;

        
        lineRenderer.positionCount = 3;
        followTransform = follow;
        parentAnchor = anchor;
        lineRenderer.SetPosition(0, parentAnchor.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(2, followTransform.position);

        if(FindObjectsOfType<Anchor>().Length>1)
        {
            return;
        }
        ropeCollisionMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,float.PositiveInfinity, ropeCollisionMask);
        if (hit.collider != null)
        {
            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(0, hit.point);
        }
    }
    public void ChangeFollow(Transform follow)
    {
        followTransform = follow;
    }
    public void RemoveFollow()
    {
        followTransform = null;
    }
    public Rigidbody2D GetRigidBody()
    {
        rb = GetComponent<Rigidbody2D>();
        return rb;
    }
    void Update()
    {
        lineRenderer.SetPosition(1, transform.position);
        if(followTransform!=null)
        {
            lineRenderer.SetPosition(2, followTransform.position);
        }
        else
        {
            lineRenderer.SetPosition(2, transform.position);
        }
    }
}