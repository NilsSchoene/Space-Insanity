using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private GameObject explosionGO;
    [SerializeField]
    private AudioClip explosionAudio;
    Player player;
    
    void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, moveSpeed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.Die();
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.IncreaseScore();
        EnemySpawner enemySpawner = player.GetComponentInChildren<EnemySpawner>();
        enemySpawner.RemoveEnemyFromList(this.gameObject);
        Instantiate(explosionGO, this.transform.position, quaternion.identity);
        AudioManager.Instance.PlaySound(explosionAudio);
        Destroy(this.gameObject);
    }
}
