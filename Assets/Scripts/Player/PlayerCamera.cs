using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private float damping = 0.5f;
    private Vector3 vel = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z = mainCamera.gameObject.transform.position.z;
        mainCamera.gameObject.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref vel, damping);
    }
}
