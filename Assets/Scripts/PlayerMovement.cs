using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float speed = 5.0f;
    public InputAction action;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(input.x, 0, input.y) * speed * Time.deltaTime;
        player.transform.Translate(movement, Space.World);
        
        
    }
}
