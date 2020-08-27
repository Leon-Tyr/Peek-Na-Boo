using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rg;
    private bool m_Grounded;
    private bool m_isCrouched;

    public Animator animator;

    public float speed = 8f;
    public float jump = 10f;
    public float gravity = 12f;

    public float moveSpeed = 0f;
    private bool facingRight;

    public Joystick joystick;



    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {   
        animator.SetFloat("Speed", Mathf.Abs(moveSpeed));
        moveSpeed = joystick.Horizontal * speed;
        float verticalMove = joystick.Vertical;

        if (m_Grounded)
        {
            landing();
            if (verticalMove >= 0.5f)
            {
                rg.AddForce(new Vector2(0f, jump));
                animator.SetBool("isJump", true);
            }

            if (verticalMove <= -0.5f)
            {
                animator.SetBool("isCrouch", true);
              
            }
            else
            {
                notCrouching();
            }

        }
        Flip(moveSpeed);
    }

    private void Flip(float moveSpeed)
    {
        if (moveSpeed > 0 && !facingRight || moveSpeed < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;

            transform.localScale = playerScale;
        }
    }

    private void landing()
    {
        animator.SetBool("isJump", false);
    }

    private void notCrouching()
    {
        animator.SetBool("isCrouch", false);
    }
}
