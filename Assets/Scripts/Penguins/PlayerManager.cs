using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    public float MoveSpeed = 10f;
    [SerializeField] private float rotationRate = 360;
    [SerializeField] private bool isGrounded = false;
    private bool jumpKeyPress = false;
    private Animator playerAnim;
    private Rigidbody rigidBody;
    [SerializeField] private GameObject toto;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        if (GameManager.Instance.isPlayable)
        {
            PlayerMovement(moveInput, turnInput);
            if (Input.GetKeyDown("space")) jumpKeyPress = true;
        }
        else
        {
            PlayerAutoMove();
        }
    }

    private void FixedUpdate()
    {

        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //
        // if (isGrounded) return;
        
        // if (jumpKeyPress)
        // {
        //     rigidBody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
        //     jumpKeyPress = false;
        // }
    }

    private void PlayerAutoMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
    }
    
    private void PlayerMovement(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        playerAnim.SetFloat("vertical", input);
        rigidBody.MovePosition(new Vector3(input, rigidBody.position.y, rigidBody.position.z) * MoveSpeed * Time.deltaTime);
        // rigidBody.velocity = new Vector3(input, rigidBody.velocity.y, rigidBody.velocity.z);
        // transform.Translate(Vector3.forward * input * (MoveSpeed / 100));
    }
    
    private void Turn(float input)
    {
        playerAnim.SetFloat("horizontal", input);
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bosse")
        {
            print("OKKKKK");
        }
    }
}
