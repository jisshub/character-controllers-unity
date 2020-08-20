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
        // call GetInput
        GetInput();
    }

    void Movement()
    {
        // if our character is in the ground
        if (charController.isGrounded)
        {
            // On pressing  W, player walks in z axis
            if (Input.GetKey(KeyCode.W))
            {

                // check player is attacking / not
                if (anim.GetBool("attacking") == true)
                {
                    return;
                } else if (anim.GetBool("attacking") == false)
                {
                    // if no attacking,
                    // start running
                    anim.SetBool("running", true);
                    // run animation when v press W.
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    // move the character in diff directions too.
                    moveDir = transform.TransformDirection(moveDir);
                }
               
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                // stop the runnning
                anim.SetBool("running", false);
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

    void GetInput()
    {
        // if the controller is grounded, 
        if (charController.isGrounded)
        {
            // check v press the left mouse button, 0 means left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("running") == true)
                {
                    // if character running, then set it to false
                    anim.SetBool("running", false);
                    // set to idle
                    anim.SetInteger("condition", 0);
                }
                if (anim.GetBool("running") == false)
                {
                    // do attacking
                    Attacking();
                }
                // call Attacking function
                Attacking();
            }
        }
    }
    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        // v play the animation
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        // wait for 1 second
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);

    }

}
