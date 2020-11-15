using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : Singleton<PlayerManager> {
    [SerializeField] private GameObject player = null;

    [Range(0, 50)]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationRate = 360;
    
    private Animator anim;
    private static Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (GameManager.Instance.DebugMode) {
            Move();
            return;
        }

        if (GameManager.Instance.pressedSpaceIntro) {
            if (GameManager.Instance.canRotateIntro) Rotate180();
            if (GameManager.Instance.canMove) Move();
        }
    }
    
    public Animator GetAnim()
    {
        return anim;
    }
    
    public Rigidbody GetRigidbody()
    {
        return rigidBody;
    }

    private void Move() {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        anim.SetFloat("horizontal", hAxis);
        anim.SetFloat("vertical", vAxis);

        Vector3 movement = new Vector3(0, 0, vAxis) * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        SetPosition(newPosition);

        Vector3 eulerAngleVelocity = new Vector3(0, hAxis * rotationRate * Time.deltaTime, 0);
        Quaternion newRotation = Quaternion.Euler(eulerAngleVelocity);
        rigidBody.MoveRotation(rigidBody.rotation * newRotation);
    }

    private void AutoMove() {
        anim.SetFloat("vertical", 1);

        Vector3 movement = Vector3.forward * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        rigidBody.MovePosition(newPosition);
    }

    private bool isGrounded()
    {
        float jumpRaycastDistance = 1.1f;
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
    }

    private void Rotate180() {
        Vector3 eulerAngle180 = new Vector3(0, 180, 0);
        Quaternion newRotation = Quaternion.Euler(eulerAngle180 * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * newRotation);
        if (rigidBody.rotation.y == 1) {
            GameManager.Instance.canRotateIntro = false;
            GameManager.Instance.canMove = true;
        }
    }

    public void ResetPosition()
    {
        SetPosition(new Vector3(0, 0, 0));
    }

    public Vector3 GetPosition() {
        return rigidBody.position;
    }

    public void SetPosition(Vector3 position)
    {
        rigidBody.MovePosition(position);
    }
    
    public void SetRotation(Vector3 rotation)
    {
        rigidBody.MoveRotation(Quaternion.Euler(rotation));
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.name);
        switch (collider.name) {
            case "Interaction Zone":
                InteractionManager.PlayerInInteractionZone();
                break;
        }

        switch (collider.name) {
            case "defaultToOutroCollider":
                CameraManagerTimeline.Instance.StartTimeline("defaultToOutro");
                break;
            case "defaultToSidesCollider":
                CameraManagerTimeline.Instance.StartTimeline("defaultToSides");
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        switch (collider.name)
        {
            case "Interaction Zone":
                InteractionManager.PlayerOutInteractionZone();
                break;
            default:
                break;
        }
    }
}