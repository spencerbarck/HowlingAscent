using UnityEngine;
using System.Collections.Generic;

public class AnchorSpawner : MonoBehaviour
{
    [SerializeField]private Anchor anchorPrefab;// List to store the last two anchors spawned
    private List<Anchor> anchors = new List<Anchor>();
    private Rigidbody2D rb; // the player's rigidbody component
    private DistanceJoint2D distanceJoint;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anchor anchor = Instantiate(anchorPrefab, transform.position, Quaternion.identity);
            anchor.InitFollow(transform,rb);

            distanceJoint.enabled = true;
            distanceJoint.connectedBody = anchor.GetRigidBody();
            distanceJoint.distance = 10f;

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
}