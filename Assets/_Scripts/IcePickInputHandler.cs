using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePickInputHandler : MonoBehaviour
{
    [SerializeField]private IcePick leftPick; // the left ice pick
    [SerializeField]private IcePick rightPick; // the right ice pick
    private void Update()
    {
        HandlePickInput();
    }
    private void HandlePickInput()
    {
        if(GameMananger.Instance.GetGameState()!=GameState.Climbing)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            leftPick.Plant();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            leftPick.Release();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            rightPick.Plant();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            rightPick.Release();
        }
    }
}
