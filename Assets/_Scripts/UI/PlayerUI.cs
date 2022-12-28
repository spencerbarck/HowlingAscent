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
    [SerializeField]private TextMeshProUGUI ropeLength;
    [SerializeField]private Image isGroundedImage;
    [SerializeField]private Image isFallingImage;
    [SerializeField]private Image isPlantedImage;
    [SerializeField]private AnchorSpawner anchorSpawner;
    void Update()
    {
        textMeshX.text = player.transform.position.x.ToString();
        textMeshY.text = player.transform.position.y.ToString();
        ropeLength.text = anchorSpawner.distanceJoint.distance.ToString();
        if(player.CheckGrounded()) isGroundedImage.gameObject.SetActive(true);
        else  isGroundedImage.gameObject.SetActive(false);
        if(player.CheckIsFalling()) isFallingImage.gameObject.SetActive(true);
        else  isFallingImage.gameObject.SetActive(false);
        if(player.CheckIsPlanted()) isPlantedImage.gameObject.SetActive(true);
        else  isPlantedImage.gameObject.SetActive(false);
    }
}
