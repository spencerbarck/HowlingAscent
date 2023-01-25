using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;
    public GameObject windZonePrefab;
    public Transform playerTransform;
    public GameObject windIndicator;
    public float minSpawnInterval = 10f;
    public float maxSpawnInterval = 20f;
    public float windForce = 10f;
    public float windZoneSpeed = 2f;
    private float currentSpawnInterval;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private float spawnDistance = 20f;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        currentSpawnInterval -= Time.deltaTime;

        if (currentSpawnInterval <= 0)
        {
            // Generate random spawn position and rotation
            spawnPosition = playerTransform.position + (Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right) * spawnDistance;
            spawnPosition.y = playerTransform.position.y;
            spawnRotation = Quaternion.Euler(0f, 180f, 0f);

            windIndicator.SetActive(true);

            // Spawn wind zone at random position and rotation
            GameObject windZone = Instantiate(windZonePrefab, spawnPosition, spawnRotation);
            windZone.GetComponent<WindZone>().windForce = windForce;
            windZone.GetComponent<WindZone>().windZoneSpeed = windZoneSpeed;
            if(spawnPosition.x < playerTransform.position.x)
            {
                windZone.GetComponent<WindZone>().moveRight = true;
            }
            else
            {
                windZone.GetComponent<WindZone>().moveRight = false;
            }

            // Reset spawn interval
            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
    public void DisableWindIndicator()
    {
        windIndicator.SetActive(false);
    }
}