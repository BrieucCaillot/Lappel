using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    
    [Range(0, 10)]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float jumpRaycastDistance = 1.1f;
    [SerializeField] private float rotationRate = 360;

    private Animator playerAnim;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Jump();
    }
    
    private void FixedUpdate()
    {
        if (GameManager.Instance.isPlayable)
        {
            Move();
        }
        else
        {
            AutoMove();
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rigidBody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }
    
    private void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        
        playerAnim.SetFloat("horizontal", hAxis);
        playerAnim.SetFloat("vertical", vAxis);

        Vector3 movement = new Vector3(0,0, vAxis) * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        rigidBody.MovePosition(newPosition);
        
        Vector3 eulerAngleVelocity = new Vector3(0, hAxis * rotationRate * Time.deltaTime, 0);
        Quaternion newRotation = Quaternion.Euler(eulerAngleVelocity);
        rigidBody.MoveRotation(rigidBody.rotation * newRotation);
    }
    
    private void AutoMove()
    {
        playerAnim.SetFloat("vertical", 1);
        
        Vector3 movement = Vector3.forward * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        rigidBody.MovePosition(newPosition);
    }
    
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down,  jumpRaycastDistance);
    }

    private void Rotate360()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (collider.name == "Path Collider")
        {
            Debug.Log("PATH COLLIDER TOUCHED");
        }
        if (collider.tag == "Bosse")
        {
            print("OKKKKK");
        }
        if(collider.name == "defaultToOutroCollider"){
            CameraManagerTimeline.Instance.StartTimeline("defaultToOutro");
        }
    }
}
