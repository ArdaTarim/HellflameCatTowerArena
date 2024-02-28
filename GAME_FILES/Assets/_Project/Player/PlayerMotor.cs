using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{   
    private CharacterController controller;
    private UnityEngine.Vector3 playerVelocity;
    private bool isGrounded;
    private bool sprinting = false;
    public float gravity = -9.81f;
    private float speed = 3f;
    public float sprintSpeed = 6f;
    public float walkSpeed = 3f;
    public float jumpHeight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(UnityEngine.Vector2 input)
    {
        UnityEngine.Vector3 moveDirection = UnityEngine.Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        if (isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2;
        }
        playerVelocity.y += gravity*Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump(){
        if (isGrounded){
            playerVelocity.y = (float)Math.Sqrt(jumpHeight * -3.0f *gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;

        if (sprinting){
            speed = sprintSpeed;
        } else {
            speed = walkSpeed;
        }
    }

}
