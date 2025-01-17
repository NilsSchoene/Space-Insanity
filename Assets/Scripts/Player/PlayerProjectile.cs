using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 0.1f;
    [SerializeField]
    private float projectileLifeTime = 1f;

    private Vector2 direction;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = this.transform.up;
        rb.AddForce(direction * projectileSpeed);
        StartCoroutine(DestroyAfterSeconds());
    }

    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            enemyHit.Die();
            Destroy(this.gameObject);
        }
    }
}
