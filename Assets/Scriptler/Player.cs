using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float accelerationForce;
    public float maxSpeed;
    public Rigidbody2D GregRB;
    private float moveInput;
    private Vector3 scale;
    private Animator animator;

    public float jumpVelocity;
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplayer = 1.5f;

    void Start()
    {
        scale = this.transform.position;
        animator = GetComponent<Animator>();
        GregRB = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        //movement in x axis
        moveInput = Input.GetAxisRaw("Horizontal");
        GregRB.AddForce(Vector2.right * moveInput * accelerationForce);
        //Vector2 clampedVelocity = GregRB.velocity;
        //clampedVelocity.x = Mathf.Clamp(GregRB.velocity.x, -maxSpeed, maxSpeed);
        //GregRB.velocity = clampedVelocity.x;

        //animation
        animator.SetBool("Running", true);

        if (moveInput == 0)
        {
            animator.SetBool("Running", false);
        }

        if (moveInput > 0)
        {
            scale.x = 1;
            scale.y = 1;
            this.transform.localScale = scale;
        }
        else if(moveInput < 0)
        {
            scale.x = -1;
            scale.y = 1;
            this.transform.localScale = scale;
        }

        //jump
        if(Input.GetKeyDown("w"))
        {
            GregRB.velocity = Vector2.up * jumpVelocity;
        }
        if(GregRB.velocity.y <0)
        {
            GregRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplayer - 1)* Time.deltaTime;
        }
        else if (GregRB.velocity.y >0 && !Input.GetKey("w"))
        {
            GregRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplayer - 1)* Time.deltaTime;
        }
    }
}
