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
        windWarning.SetActive(false);
        StartCoroutine(ShowWindWarning());
    }
    void Update()
    {
        playerHeight.text = (player.transform.position.y+3).ToString("0.0");
    }
    IEnumerator ShowWindWarning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 15f));
            windWarning.SetActive(true);
            yield return new WaitForSeconds(Random.Range(1f, 10f));
            windWarning.SetActive(false);
        }
    }
}
