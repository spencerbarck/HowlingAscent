using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI playerHeight;
    [SerializeField]private TextMeshProUGUI playerAnchors;
    private PlayerStatus player;
    private PlayerInventory playerInventory;
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }
    void Update()
    {
        playerHeight.text = (player.transform.position.y+3).ToString("0.0");
        playerAnchors.text = "Anchors: "+playerInventory.numberOfAnchors;
    }
}
