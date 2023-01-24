using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI playerHeight;
    [SerializeField]private GameObject windWarning;
    private PlayerStatus player;
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
    }
    void Update()
    {
        playerHeight.text = (player.transform.position.y+3).ToString("0.0");
    }
}
