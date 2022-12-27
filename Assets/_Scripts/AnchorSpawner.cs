using UnityEngine;
using System.Collections.Generic;

public class AnchorSpawner : MonoBehaviour
{
    public Anchor anchorPrefab;// List to store the last two anchors spawned
    private List<Anchor> anchors = new List<Anchor>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anchor anchor = Instantiate(anchorPrefab, transform.position, Quaternion.identity);
            anchor.InitFollow(transform);
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