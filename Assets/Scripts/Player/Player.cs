using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerCamera))]

public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    public bool controllable = false;

    public bool alive = true;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        playerMovement = GetComponent<PlayerMovement>();
        alive = true;
    }

    public void Die()
    {
        if(alive)
        {
            alive = false;
            controllable = false;
            Debug.Log("YOU DIED! Final Score: " + GameManager.Instance.score);
            GameManager.Instance.EndGame();
        }
        
    }
}
