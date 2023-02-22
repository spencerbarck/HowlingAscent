using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private float spawnPointY;
    private PlayerStatus player;
    private float spawnPointX;
    private void Start()
    {
        spawnPointY = transform.position.y;
        player = FindObjectOfType<PlayerStatus>();
        spawnPointX = transform.GetChild(0).transform.position.x;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(spawnPointX);
            player.SetRespawnPoint(new Vector3(spawnPointX,spawnPointY,1));
        }
    }
}
