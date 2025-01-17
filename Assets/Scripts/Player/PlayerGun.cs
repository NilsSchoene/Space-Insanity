using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    private GameObject playerProjectileGO;
    [SerializeField]
    private AudioClip shootAudio;

    [SerializeField]
    private float cooldown = 0.1f;
    private bool onCooldown = false;
    private bool gunBlocked = true;

    void Start()
    {
        GameManager.Instance.OnPlayerDeath += OnGameplayInterrupt;
        GameManager.Instance.OnStartGameplay += OnGameplayStart;
    }

    public void Shoot()
    {
        if (!onCooldown && !gunBlocked)
        {
            Instantiate(playerProjectileGO, gameObject.transform.position, gameObject.transform.rotation);
            AudioManager.Instance.PlaySound(shootAudio);
            onCooldown = true;
            StartCoroutine(StartCooldown());
        }
    }

    private void OnGameplayInterrupt()
    {
        gunBlocked = true;
    }

    private void OnGameplayStart()
    {
        gunBlocked = false;
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDeath -= OnGameplayInterrupt;
        GameManager.Instance.OnStartGameplay -= OnGameplayStart;
    }
}
