using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterMoveDefault : MonoBehaviour
{
    float horizontal = 0, vertical = 0;

    bool moveBool = true;

    Rigidbody physic;
    Animator animator;

    public VirtualJoystick joystick;

    Transform cameraa;

    void Start()
    {
        animator = GetComponent<Animator>();

        physic = GetComponent<Rigidbody>();
        cameraa = Camera.main.transform;
    }

    void Update()
    {

        jump();

    }

    void FixedUpdate()
    {

        characterMove();

    }

    void characterMove()
    {
        if (moveBool)
        {
            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");

            horizontal = joystick.Horizontal();
            vertical = joystick.Vertical();

            Vector3 vec = new Vector3(horizontal, 0, vertical);
            physic.position += vec * Time.deltaTime * 4f;

            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
    }


    void JumpTime()
    {
        physic.AddForce(0, 200, 0);
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && moveBool)
        {

            animator.SetBool("Jump", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && moveBool)
        {
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "levelWall")
        {
            
        SceneManager.LoadScene(0);    

        }
    }
}
