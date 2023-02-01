using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AnchorSpawner : MonoBehaviour
{
    [SerializeField]private Anchor anchorPrefab;
    private PlayerStatus playerStatus;
    private PlayerInventory playerInventory;
    private List<Anchor> anchors = new List<Anchor>();
    private Rigidbody2D rb; // the player's rigidbody component
    private DistanceJoint2D distanceJoint;
    private float ropeDistance = 10f;
    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        playerInventory = GetComponent<PlayerInventory>();
        rb = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
    }
    private void Update()
    {
        if(!playerStatus.CheckIsPlanted()&&Input.GetKey(KeyCode.Q))
        {
            PullBodyUpRope();
        }
        else
        {
            ropeDistance = 10f;
        }
        distanceJoint.distance = ropeDistance;
        if (Input.GetKeyDown(KeyCode.E)&&IsOverClimbable())
        {
            if(playerInventory.RemoveAnchor())
                SpawnAnchor();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            CutLine();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AttachToAnchor();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveAnchor();
        }
    }
    private void PullBodyUpRope()
    {
        ropeDistance = Mathf.Max(ropeDistance - (2 * Time.deltaTime), 0f);
        if(Vector2.Distance(distanceJoint.connectedBody.transform.position,transform.position)>0.1f)
        {
            Vector2 forceDirection = (distanceJoint.connectedBody.transform.position - transform.position).normalized;
            rb.AddForce(forceDirection * 1);
        }
    }
    private void SpawnAnchor()
    {
        Anchor anchor = Instantiate(anchorPrefab, transform.position, Quaternion.identity);
        distanceJoint.enabled = true;
        distanceJoint.connectedBody = anchor.GetRigidBody();
        anchors.Add(anchor);
        while (anchors.Count > 2)
        {
            anchors.RemoveAt(0);
        }
        anchor.InitFollow(transform,anchors.First());
        if(anchors.Count > 1)
        {
            anchors[0].ChangeFollow(anchors[1].transform);
        }
    }
    private void CutLine()
    {
        if(anchors.Count==0)
        {
            return;
        }
        anchors.Last().RemoveFollow();
        anchors.Clear();
        distanceJoint.enabled = false;
    }
    private void AttachToAnchor()
    {
        Anchor[] allAnchors = FindObjectsOfType<Anchor>();
        Anchor nearestAnchor = allAnchors.OrderBy(a => Vector3.Distance(a.transform.position, transform.position))
                                         .Where(a => Vector3.Distance(a.transform.position, transform.position) <= 1f)
                                         .FirstOrDefault();
        if(nearestAnchor!=false)
        {
            CutLine();
            distanceJoint.enabled = true;
            distanceJoint.connectedBody = nearestAnchor.GetRigidBody();
            anchors.Add(nearestAnchor);
            nearestAnchor.ChangeFollow(transform);
        }
    }
    private void RemoveAnchor()
    {
        Anchor[] allAnchors = FindObjectsOfType<Anchor>();
        Anchor nearestAnchor = allAnchors.OrderBy(a => Vector3.Distance(a.transform.position, transform.position))
                                         .Where(a => Vector3.Distance(a.transform.position, transform.position) <= 1f)
                                         .FirstOrDefault();
        if(nearestAnchor!=false)
        {
            if(anchors.Count>0)
            {
                if(nearestAnchor.Equals(anchors[0]))
                {
                    anchors.Remove(anchors[0]);
                }
            }
            if(anchors.Count>1)
            {
                if(nearestAnchor.Equals(anchors[1]))
                {
                    anchors.Remove(anchors[1]);
                }
            }
            playerInventory.AddAnchor();
            nearestAnchor.DestoryAnchor();
        }
    }
    private bool IsOverClimbable()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.back, 20f, LayerMask.GetMask("Climbable"));
        return hit.collider != null ? true : false;
    }
    public DistanceJoint2D GetDistanceJoint()
    {
        return distanceJoint;
    }
}