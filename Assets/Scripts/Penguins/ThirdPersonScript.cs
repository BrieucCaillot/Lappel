using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonScript : MonoBehaviour
{
    public Transform camera;
    public Animator playerAnim;
    public CharacterController controller;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    private bool canMove = false;
    private float turnSmoothVelocity;

    private void Start()
    {
        PlayerAutoMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerMove();
        }
        else
        {
            PlayerAutoMove();
        }
    }

    private void PlayerMove()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        playerAnim.SetFloat("vertical", vertical);
        playerAnim.SetFloat("horizontal", horizontal);

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
    }
    
    private void PlayerAutoMove()
    {
        controller.Move(Vector3.forward * speed * Time.deltaTime);
    }

}

