using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager : MonoBehaviour
{
    public static WindManager instance;
    [SerializeField]
    private GameObject windZonePrefab;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private GameObject windIndicator;
    [SerializeField]
    [Range(10f, 20f)]
    private float minSpawnInterval = 10f;
    [SerializeField]
    [Range(10f, 20f)]
    private float maxSpawnInterval = 20f;
    [SerializeField]
    [Range(1f, 10f)]
    private float windForce = 10f;
    [SerializeField]
    [Range(1f, 10f)]
    private float minWindZoneSpeed = 1f;
    [SerializeField]
    [Range(1f, 20f)]
    private float maxWindZoneSpeed = 3f;
    [SerializeField]
    [Range(0.2f, 10f)]
    private float minWindZoneSize = 0.5f;
    [SerializeField]
    [Range(0.2f, 10f)]
    private float maxWindZoneSize = 1.5f;
    [SerializeField]
    private float currentSpawnInterval;
    [SerializeField]
    private Vector3 spawnPosition;
    [SerializeField]
    [Range(10f, 50f)]
    private float spawnDistance = 20f;
    [SerializeField]
    [Range(1f, 10f)]
    private float spawnYDistance = 5f;
    void Awake()
    {
        instance = instance == null ? this : instance;
        if (instance != this)
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

            float randomWindZoneSize = Random.Range(minWindZoneSize, maxWindZoneSize);
            
            spawnPosition = playerTransform.position + (Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right) * (spawnDistance*randomWindZoneSize);
            spawnPosition.y = playerTransform.position.y + Random.Range(-spawnYDistance, spawnYDistance);
            spawnPosition.y = Mathf.Max(spawnPosition.y, -3f);

            windIndicator.SetActive(true);

            GameObject windZone = Instantiate(windZonePrefab, spawnPosition, Quaternion.identity);
            float randomWindZoneSpeed = Random.Range(minWindZoneSpeed, maxWindZoneSpeed);
            windZone.GetComponent<WindZone>().windForce = windForce;
            windZone.GetComponent<WindZone>().windZoneSpeed = randomWindZoneSpeed;
            windZone.GetComponent<WindZone>().moveRight = spawnPosition.x < playerTransform.position.x;
            windZone.transform.localScale = new Vector3(randomWindZoneSize, randomWindZoneSize, 1f);

            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
    public void DisableWindIndicator()
    {
        windIndicator.SetActive(false);
    }
}