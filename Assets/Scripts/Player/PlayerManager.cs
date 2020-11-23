using System;
using DG.Tweening;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {

    [NonSerialized]
    public bool canMove = false;
    [Range(0, 50)]
    public float speed = 6f;

    [SerializeField] private GameObject player = null;
    private float rotationRate = 360;

    private Animator anim;
    private static Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start() {
        anim = player.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        if (GameManager.Instance.DebugMode) {
            canMove = true;
            speed = 20;
        }
    }

    private void FixedUpdate() {
        if (!GameManager.Instance.enteredGame || !canMove) return;
        Move();
    }

    public Rigidbody GetRigidbody() {
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
        var rotation = rigidBody.rotation * newRotation;
        rigidBody.MoveRotation(rotation);
    }

    private void AutoMove() {
        anim.SetFloat("vertical", 1);

        Vector3 movement = Vector3.forward * speed * Time.fixedDeltaTime;
        Vector3 newPosition = rigidBody.position + rigidBody.transform.TransformDirection(movement);
        rigidBody.MovePosition(newPosition);
    }

    public void RotateIntro() {
        transform.DORotate(new Vector3(0, 180, 0), 2f)
            .OnPlay(() => canMove = true);
    }

    public void ResetPosition() {
        SetPosition(new Vector3(0, 0, 0));
    }

    public Vector3 GetPosition() {
        return rigidBody.position;
    }

    public void SetPosition(Vector3 position) {
        rigidBody.MovePosition(position);
    }

    public void SetRotation(Vector3 rotation) {
        rigidBody.MoveRotation(Quaternion.Euler(rotation));
    }

    private void OnTriggerEnter(Collider collider) {
        Debug.Log("OnTriggerEnter " + collider.name);
        switch (collider.name) {
            case "INTERACTION ZONE CREVASSE":
                collider.transform.parent.GetComponent<InteractionCrevasseManager>().PlayerInInteractionZone();
                break;
            case "INTERACTION ZONE CASCADE":
                collider.transform.parent.GetComponent<InteractionCascadeManager>().PlayerInInteractionZone();
                break;
            case "TRIGGER SCENE MOUNTAIN":
                UnderwaterSceneManager.NextScene();
                break;
            default:
                break;
        }

        CameraManager.Instance.StartTimeline(collider.name);
    }

    private void OnTriggerExit(Collider collider) {
        Debug.Log("OnTriggerExit " + collider.name);
        switch (collider.name) {
            case "INTERACTION ZONE CREVASSE":
                collider.transform.parent.GetComponent<InteractionCrevasseManager>().PlayerOutInteractionZone();
                break;
            case "INTERACTION ZONE CASCADE":
                collider.transform.parent.GetComponent<InteractionCascadeManager>().PlayerOutInteractionZone();
                break;
            default:
                break;
        }
    }
}