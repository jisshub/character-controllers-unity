using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    float speed = 4; 
    float rotSpeed = 80;
    float gravity = 8;
    float rotChar = 0f;

    // direction to move
    Vector3 moveDir = Vector3.zero;

    // Vector3.zero same as Vector3(0, 0, 0)

    // variable for character controller and animator
    CharacterController charController;
    Animator anim;

    void Start()
    {
        // initiliaze variables at start.
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        // if our character is in the ground
        if (charController.isGrounded)
        {
            // On pressing W, player walks in z axis
            if (Input.GetKey(KeyCode.W))
            {
                // run animation when v press W.
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                // move the character in diff directions too.
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                // halt animation when stop pressing W.
                anim.SetInteger("condition", 0);
                // once v stop pressing W, stop mocing character
                // else character continues walking.
                moveDir = new Vector3(0, 0, 0);
            }
        }

        // next rotate the character horizontally
        rotChar += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotChar, 0);

        // if not in ground, move in y axis, i.e. up and down direction
        moveDir.y -= gravity * Time.deltaTime;
        charController.Move(moveDir * Time.deltaTime);
        // key a - rotate left, d - rotate right
    }

}
