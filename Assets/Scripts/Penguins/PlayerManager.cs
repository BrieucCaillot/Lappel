using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject Player;
    public float moveSpeed = 10f;
    public float rotationRate = 360;

    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayable)
        {
            PlayerMovement();
        }
        else
        {
            PlayerAutoMove();
        }
    }

    private void PlayerAutoMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void PlayerMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis( "Horizontal");
        
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input)
    {
        playerAnim.SetFloat("vertical", input);
        transform.Translate(Vector3.forward * input * (moveSpeed / 100));
    }
    
    private void Turn(float input)
    {
        playerAnim.SetFloat("horizontal", input);
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }
}
