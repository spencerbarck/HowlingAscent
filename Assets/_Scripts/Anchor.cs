using UnityEngine;

public class Anchor : MonoBehaviour
{
    private Transform followTransform;
    private LayerMask ropeCollisionMask;
    private LineRenderer lineRenderer;
    public void InitFollow(Transform follow)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.sortingOrder = 40;

        followTransform = follow;
        lineRenderer.SetPosition(0, followTransform.position);
        lineRenderer.SetPosition(1, transform.position);

        if(FindObjectsOfType<Anchor>().Length>1)
        {
            return;
        }
        ropeCollisionMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,float.PositiveInfinity, ropeCollisionMask);
        if (hit.collider != null)
        {
            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(2, hit.point);
        }
    }
    public void ChangeFollow(Transform follow)
    {
        followTransform = follow;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, followTransform.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}