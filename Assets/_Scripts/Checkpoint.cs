using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private float spawnPointX = 0;
    private float spawnPointY;
    private PlayerStatus player;
    private void Start()
    {
        spawnPointY = transform.position.y;
        player = FindObjectOfType<PlayerStatus>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ding");
            player.SetRespawnPoint(new Vector3(spawnPointX,spawnPointY,1));
        }
    }

    public void RespawnPlayer()
    {
        transform.position = new Vector3(spawnPointX,spawnPointY,1);
    }
}
