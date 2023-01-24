using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindZone : MonoBehaviour
{
    public float windForce = 10f;
    public float windZoneSpeed = 2f;
    public float windDuration = 5f;
    public bool moveRight = true;
    private float currentWindDuration;
    private Vector2 screenBounds;
    private Vector2 windDirection;
    private WindZone windZone;
    private Transform windTransform;

    void Start()
    {
        currentWindDuration = windDuration;
        windTransform = transform;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        windDirection = moveRight ? windTransform.right : -windTransform.right;
    }

    void Update()
    {
        currentWindDuration -= Time.deltaTime;

        windTransform.Translate(windDirection * windZoneSpeed * Time.deltaTime);

        if (currentWindDuration <= 0 || Mathf.Abs(windTransform.position.x) > screenBounds.x)
        {
            WindManager.instance.DisableWindIndicator();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<Rigidbody2D>().AddForce(windDirection * windForce);
        }
    }
}