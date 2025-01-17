using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Movement speed")]
    [SerializeField]
    private float moveSpeed = 5f;

    private float speedX;
    private float speedY;
    private Vector2 lookDirection;
    private Rigidbody2D rb;

    private bool blockMovement = true;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GameManager.Instance.OnPlayerDeath += OnGameplayInterrupt;
        GameManager.Instance.OnStartGameplay += OnGameplayStart;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(speedX, speedY);
        rb.transform.up = lookDirection;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(!blockMovement)
        {
            speedX = context.ReadValue<Vector2>().x * moveSpeed;
            speedY = context.ReadValue<Vector2>().y * moveSpeed;
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        if(!blockMovement)
        {
            Vector3 mousePos = context.ReadValue<Vector2>();
            mousePos.z = Camera.main.nearClipPlane;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            lookDirection = new Vector2(mousePos.x - rb.transform.position.x, mousePos.y - rb.transform.position.y);
        }
    }

    private void OnGameplayStart()
    {
        blockMovement = false;
        speedX = 0;
        speedY = 0;
    }

    private void OnGameplayInterrupt()
    {
        blockMovement = true;
        speedX = 0;
        speedY = 0;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDeath -= OnGameplayInterrupt;
        GameManager.Instance.OnStartGameplay -= OnGameplayStart;
    }
}
