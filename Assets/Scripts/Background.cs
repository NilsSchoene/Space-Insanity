using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private GameObject playerGO;
    private Vector2 startPos;
    private Vector2 playerPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        playerPos = playerGO.transform.position;

        if (playerPos.x <= startPos.x - 19.2 || playerPos.x >= startPos.x + 19.2)
        {
            transform.position = new Vector2(playerPos.x, transform.position.y);
            startPos = transform.position;
        }

        if (playerPos.y <= startPos.y - 10.8 || playerPos.y >= startPos.y + 10.8)
        {
            transform.position = new Vector2(transform.position.x, playerPos.y);
            startPos = transform.position;
        }
    }
}
