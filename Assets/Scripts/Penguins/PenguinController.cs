using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{

    private void Start()
    {
        RandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        AutoMove();
    }

    private void AutoMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 10f);
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
