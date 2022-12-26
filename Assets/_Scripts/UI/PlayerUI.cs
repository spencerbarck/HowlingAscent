using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]private PlayerMovement player;
    [SerializeField]private TextMeshProUGUI textMeshX;
    [SerializeField]private TextMeshProUGUI textMeshY;

    void Update()
    {
        textMeshX.text = player.transform.position.x.ToString();
        textMeshY.text = player.transform.position.y.ToString();
    }
}
