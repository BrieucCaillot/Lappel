using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public Rigidbody rb;
    public GameObject cameraManager;
    private CameraManagerTimeline cameraManagerScript;

    void Start() {
        cameraManagerScript = cameraManager.GetComponent<CameraManagerTimeline>();
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("COLLIDER");
        switch (collider.name) {
            case "Collider":
                Debug.Log("call timeline1 cam1");
                cameraManagerScript.StartTimeline("cam1");
                break;
            case "Collider2":
                Debug.Log("call timeline2 cam2");
                cameraManagerScript.StartTimeline("cam2");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKey("d")) {
            rb.AddForce(500 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("q")) {
            rb.AddForce(-500 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("z")) {
            rb.AddForce(0, 0, 500 * Time.deltaTime);
        }

        if (Input.GetKey("s")) {
            rb.AddForce(0, 0, -500 * Time.deltaTime);
        }
    }
}