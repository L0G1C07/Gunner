using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player's Transform in the Inspector
    public float smoothSpeed = 5f; // Adjust for smoother movement
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Adjust offset as needed

    private Camera cam;
    public float cameraSize = 5f; // Set the camera size

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.orthographicSize = cameraSize; // Set the camera size
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
