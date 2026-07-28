using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 6, -5);
    private Vector3 velocity = Vector3.zero;
    private float deadZoneWidth = 4f;
    private float deadZoneHeight = 2f;

    private float leftOffset;
    private float rightOffset;
    private float bottomOffset;
    private float topOffset;

    private float left;
    private float right;
    private float bottom;
    private float top;

    void Start()
    {
        // Optionally initialize player here if not assigned in Inspector or elsewhere.
        // player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        leftOffset = deadZoneWidth / 2f;
        rightOffset = deadZoneWidth / 2f;
        bottomOffset = deadZoneHeight / 4f;
        topOffset = deadZoneHeight;

        left = transform.position.x - leftOffset;
        right = transform.position.x + rightOffset;

        float cameraTargetZ = transform.position.z - offset.z;

        bottom = cameraTargetZ - bottomOffset;
        top = cameraTargetZ + topOffset;

        if (player == null)
        {
            Debug.LogError("Player object is not assigned.");
            return;
        }

        Vector3 newPosition = transform.position;

        if (player.transform.position.x < left)
        {
            newPosition.x = player.transform.position.x + leftOffset;
        }
        if (player.transform.position.x > right)
        {
            newPosition.x = player.transform.position.x - rightOffset;
        }
        if (player.transform.position.z < bottom)
        {
            newPosition.z = player.transform.position.z + offset.z + bottomOffset;
        }
        if (player.transform.position.z > top)
        {
            newPosition.z = player.transform.position.z + offset.z - topOffset;
        }

        newPosition.y = offset.y; // Keep the camera at a fixed height

        transform.position = Vector3.SmoothDamp(
            transform.position,
            newPosition,
            ref velocity,
            0.2f
        );
    }
}
