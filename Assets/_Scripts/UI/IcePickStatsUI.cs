using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IcePickStatsUI : MonoBehaviour
{
    [SerializeField]private PlayerStatus player;
    [SerializeField]private IcePick icePick;
    [SerializeField]private IcePick icePick2;
    [SerializeField]private TextMeshProUGUI xTextMesh;
    [SerializeField]private TextMeshProUGUI yTextMesh;

    void Update()
    {
        xTextMesh.text = "X Position: " + Mathf.Round(icePick.transform.position.x*10f)/10f;
        yTextMesh.text = "Y Position: " + Mathf.Round(icePick.transform.position.y*10f)/10f;
    }
}
