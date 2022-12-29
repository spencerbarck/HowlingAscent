using UnityEngine;
using System.Collections.Generic;

public class AnchorSpawner : MonoBehaviour
{
    [SerializeField]private Anchor anchorPrefab;// List to store the last two anchors spawned
    private PlayerStatus playerStatus;
    private List<Anchor> anchors = new List<Anchor>();
    private Rigidbody2D rb; // the player's rigidbody component
    private DistanceJoint2D distanceJoint;
    private float ropeDistance = 10f;
    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
    }
    private void Update()
    {
        if(!playerStatus.CheckIsPlanted()&&Input.GetKey(KeyCode.Q))
        {
            ropeDistance = Mathf.Max(ropeDistance - (2 * Time.deltaTime), 0f);
            distanceJoint.distance = ropeDistance;
            if(Vector2.Distance(distanceJoint.connectedBody.transform.position,transform.position)>0.1f)
            {
                Vector2 forceDirection = (distanceJoint.connectedBody.transform.position - transform.position).normalized;
                rb.AddForce(forceDirection * 1);
            }
        }
        else
        {
            ropeDistance = 10f;
            distanceJoint.distance = ropeDistance;
        }
        if (Input.GetKeyDown(KeyCode.E)&&IsOverClimbable())
        {
            SpawnAchor();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            CutLine();
        }
    }
    private void SpawnAchor()
    {
        Anchor anchor = Instantiate(anchorPrefab, transform.position, Quaternion.identity);
        anchor.InitFollow(transform);

        distanceJoint.enabled = true;
        distanceJoint.connectedBody = anchor.GetRigidBody();
        anchors.Add(anchor);
        while (anchors.Count > 2)
        {
            anchors.RemoveAt(0);
        }
        if(anchors.Count > 1)
        {
            anchors[0].ChangeFollow(anchors[1].transform);
        }
    }
    private void CutLine()
    {
        anchors[anchors.Count-1].ChangeFollow(anchors[anchors.Count-1].transform);
        if(anchors.Count>1)anchors.RemoveAt(1);
        if(anchors[0]!=null)anchors.RemoveAt(0);
        distanceJoint.enabled = false;
    }
    public DistanceJoint2D GetDistanceJoint()
    {
        return distanceJoint;
    }
    private bool IsOverClimbable()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.back, 20f, LayerMask.GetMask("Climbable"));
        return hit.collider != null ? true : false;
    }
}