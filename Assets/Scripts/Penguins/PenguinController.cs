using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private float speed = 10f;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        RandomRotation();
    }

    void FixedUpdate()
    {
        AutoMove();
    }

    private void AutoMove()
    {
        Vector3 movement = Vector3.forward * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        rigidBody.MovePosition(newPosition);
        // transform.Translate(Vector3.forward * Time.deltaTime * 10f);
    }

    private void RandomRotation()
    {
        transform.Rotate(transform.rotation.x, UnityEngine.Random.Range(-10, 10), transform.rotation.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Penguin")
        {
            Debug.Log("Penguin touched another penguin");
        }
    }
}
