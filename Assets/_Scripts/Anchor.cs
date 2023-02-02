using UnityEngine;
using System.Collections.Generic;

public class Anchor : MonoBehaviour
{
    private Transform followTransform;
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;
    private Rigidbody2D rb;
    private List<Anchor> anchoresAttached = new List<Anchor>();
    public void InitFollow(Transform follow, Anchor anchor)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = 40;
        lineRenderer.positionCount = 3;
        followTransform = follow;
        Anchor parentAnchor = anchor;
        lineRenderer.SetPositions(new Vector3[] { parentAnchor.transform.position, transform.position, followTransform.position });
        if(FindObjectsOfType<Anchor>().Length==1)
        {
            //SendLineToGround();
        }
    }
    private void SendLineToGround()
    {
        LayerMask ropeCollisionMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,float.PositiveInfinity, ropeCollisionMask);
        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, hit.point);
        }
    }
    public void ChangeFollow(Transform follow)
    {
        if(follow.gameObject.GetComponent<Anchor>()!=null)
        {
            anchoresAttached.Add(follow.gameObject.GetComponent<Anchor>());
        }
        followTransform = follow;
    }
    public void RemoveFollow()
    {
        followTransform = null;
    }
    public void RemoveParent()
    {
        lineRenderer.SetPosition(0, transform.position);
    }
    public void DestoryAnchor()
    {
        foreach(Anchor anchor in anchoresAttached)
        {
            anchor.RemoveParent();
        }
        Destroy(gameObject);
    }
    public Rigidbody2D GetRigidBody()
    {
        rb = GetComponent<Rigidbody2D>();
        return rb;
    }
    void Update()
    {
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(2, followTransform != null ? followTransform.position : transform.position);
    }
}