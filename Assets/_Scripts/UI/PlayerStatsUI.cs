using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField]private PlayerStatus player;
    [SerializeField]private TextMeshProUGUI textMeshX;
    [SerializeField]private TextMeshProUGUI textMeshY;
    [SerializeField]private TextMeshProUGUI ropeLength;
    [SerializeField]private TextMeshProUGUI playerVelocity;
    [SerializeField]private Image isGroundedImage;
    [SerializeField]private Image isFallingImage;
    [SerializeField]private Image isPlantedImage;
    [SerializeField]private AnchorSpawner anchorSpawner;
    [SerializeField]private Rigidbody2D playerRB;
    void Update()
    {
        textMeshX.text = player.transform.position.x.ToString();
        textMeshY.text = player.transform.position.y.ToString();
        ropeLength.text = anchorSpawner.GetDistanceJoint().distance.ToString();
        if(player.CheckIsGrounded()) isGroundedImage.gameObject.SetActive(true);
        else  isGroundedImage.gameObject.SetActive(false);
        if(player.GetIcePicksPlantState()==IcePicksPlantState.NonePlanted) isFallingImage.gameObject.SetActive(true);
        else  isFallingImage.gameObject.SetActive(false);
        if(player.GetIcePicksPlantState()!=IcePicksPlantState.NonePlanted) isPlantedImage.gameObject.SetActive(false);
        else  isPlantedImage.gameObject.SetActive(true);
        
        playerVelocity.text = playerRB.velocity.y.ToString();
    }
}
