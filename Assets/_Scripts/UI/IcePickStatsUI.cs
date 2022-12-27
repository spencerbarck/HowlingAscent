using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IcePickStatsUI : MonoBehaviour
{
    [SerializeField]private PlayerStatus player;
    [SerializeField]private IcePick icePick;
    [SerializeField]private IcePick icePick2;
    [SerializeField]private TextMeshProUGUI textMesh;

    void Update()
    {
        textMesh.transform.position = new Vector2(textMesh.transform.position.x,icePick.transform.position.y);

        float distance = icePick.transform.position.x;
        //float distance = Mathf.Abs(player.transform.position.x - icePick.transform.position.x);
        //float distance = Vector2.Distance(icePick.transform.position,icePick2.transform.position);

        textMesh.text = "X Position: " + distance.ToString("F2");
    }
}