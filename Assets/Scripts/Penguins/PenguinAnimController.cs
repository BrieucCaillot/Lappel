using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAnimController : MonoBehaviour
{

    private Animator anim;

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float rotationRate = 360;
    public float moveSpeed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);
        
        ApplyInput(moveAxis, turnAxis);
    }

    private void ApplyInput(float moveInput, float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        anim.SetFloat("vertical", input);
        transform.Translate(Vector3.forward * input * moveSpeed);
    }
    
    private void Turn(float input)
    {
        anim.SetFloat("horizontal", input);
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    public void PinguinPlayAnim(string name)
    {
        anim.Play(name);
    }
}
