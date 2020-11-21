using UnityEngine;

public class PenguinController : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private float speed = 10f;
    [Range(0, 10)]
    [SerializeField] private float maxYRot = 8f;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        InvokeRepeating("RandomRotation", 2f, 10);
    }

    void FixedUpdate()
    {
        // AutoMove();
    }

    private void AutoMove()
    {
        Vector3 movement = Vector3.forward * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        rigidBody.MovePosition(newPosition);
    }

    private void RandomRotation()
    {
        float rotationY = rigidBody.rotation.y > 0
            ? UnityEngine.Random.Range(-maxYRot, 0)
            : UnityEngine.Random.Range(0, maxYRot);
        
        transform.Rotate(transform.rotation.x, rotationY, transform.rotation.z);
        Debug.Log("RANDOM ROTATION " + transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Penguin")
        {
            Debug.Log("Penguin touched another penguin");
        }
    }
}
