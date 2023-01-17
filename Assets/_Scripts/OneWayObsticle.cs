using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObsticle : MonoBehaviour
{
    private Collider2D oneWayCollider;
    private void Start()
    {
        oneWayCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)||Input.GetMouseButton(1))
        {
            oneWayCollider.enabled = false;
        }
        else
        {
            oneWayCollider.enabled = true;
        }
    }
}



