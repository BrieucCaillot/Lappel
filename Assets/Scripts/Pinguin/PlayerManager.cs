using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PenguinsManager>
{
    private Animator anim;

    private string moveInputAxis = "Vertical";
    private string turnInputAxis = "Horizontal";

    public float rotationRate = 360;
    public float moveSpeed = 10;

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        _rigidbody.AddForce(transform.forward * input * moveSpeed, ForceMode.Force);
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