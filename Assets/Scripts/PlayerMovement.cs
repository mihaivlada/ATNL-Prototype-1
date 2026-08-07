using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    private Vector2 input;
    private Rigidbody rb;

    [SerializeField] private float stepInterval = 0.4f;
    private float stepTimer;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //action.Enable();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        bool isMoving = input.sqrMagnitude > 0.01f;

        if (!isMoving)
        {
            stepTimer = 0;
            return;
        }

        stepTimer += Time.deltaTime;

        if (stepTimer >= stepInterval)
        {
            stepTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        // Aici punem codul care se executa la un interval fix de timp, indiferent de framerate (de exemplu, calculele fizice)
        // Default FixedUpdate interval is 0.02 seconds (50 times per second)

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized * Helper.playerSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aici e frame-ul in care s-a realizat coliziunea
        if (collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("Player collided with a tree!");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Aici sunt frame-urile in care ramane lipit de obiectul cu care a colizionat
    }

    private void OnCollisionExit(Collision collision)
    {
        // Aici e frame-ul in care s-a terminat coliziunea
    }
}
