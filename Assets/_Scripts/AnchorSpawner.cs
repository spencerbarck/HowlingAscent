using UnityEngine;
using System.Collections.Generic;

public class AnchorSpawner : MonoBehaviour
{
    [SerializeField]private Anchor anchorPrefab;// List to store the last two anchors spawned
    private PlayerStatus playerStatus;
    private List<Anchor> anchors = new List<Anchor>();
    private Rigidbody2D rb; // the player's rigidbody component
    public DistanceJoint2D distanceJoint;
    private float ropeDistance = 10f;
    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }
    private void Update()
    {
        if(!playerStatus.CheckIsPlanted()&&Input.GetKey(KeyCode.Q))
        {
            ropeDistance = Mathf.Max(ropeDistance - (2 * Time.deltaTime), 0f);
            distanceJoint.distance = ropeDistance;
            Vector2 forceDirection = (distanceJoint.connectedBody.transform.position - transform.position).normalized;
            rb.AddForce(forceDirection * 2);

        }
        else
        {
            ropeDistance = 10f;
            distanceJoint.distance = ropeDistance;
        }
        if (Input.GetKeyDown(KeyCode.E)&&IsOverClimbable())
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
    }
    private bool IsOverClimbable()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.back, 20f, LayerMask.GetMask("Climbable"));
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}