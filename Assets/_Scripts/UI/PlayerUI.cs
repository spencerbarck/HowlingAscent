using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]private PlayerStatus player;
    [SerializeField]private TextMeshProUGUI textMeshX;
    [SerializeField]private TextMeshProUGUI textMeshY;
    [SerializeField]private Image isGroundedImage;
    [SerializeField]private Image isFallingImage;
    void Update()
    {
        textMeshX.text = player.transform.position.x.ToString();
        textMeshY.text = player.transform.position.y.ToString();
        if(player.CheckGrounded()) isGroundedImage.gameObject.SetActive(true);
        else  isGroundedImage.gameObject.SetActive(false);
        if(player.CheckIsFalling()) isFallingImage.gameObject.SetActive(true);
        else  isFallingImage.gameObject.SetActive(false);
    }
}
