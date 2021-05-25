using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float playerSpeed = 10.0f;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private float gravity = -9.81f;
    private float groundRadius = 0.4f;
    private float jumpHeight = 3.0f;
    public Transform groundCheck;
    public LayerMask groundMask;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            controller.Move(move * 1.5f * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(scaleX, scaleY * 0.5f, scaleZ);
            controller.Move(move * 0.5f * playerSpeed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            controller.Move(move * playerSpeed * Time.deltaTime);
        }
    }
}
