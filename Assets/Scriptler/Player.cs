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

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpforce;

    private float JumpTimeCounter;
    public float JumpTime;
    private bool isJumping;
    
    void Start()
    {
        scale = this.transform.position;
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        GregRB.AddForce(Vector2.right * moveInput * accelerationForce);
        Vector2 clampedVelocity = GregRB.velocity;
        clampedVelocity.x = Mathf.Clamp(GregRB.velocity.x, -maxSpeed, maxSpeed);
        GregRB.velocity = clampedVelocity;

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

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius,whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
            GregRB.velocity = Vector2.up * jumpforce;
            isJumping = true;
            JumpTimeCounter = JumpTime;
        }
        if(isGrounded == true && Input.GetKey(KeyCode.Space) && isJumping == true){
            if(JumpTimeCounter > 0)
            {
            GregRB.velocity = Vector2.up * jumpforce;
            JumpTimeCounter -= Time.deltaTime;
            }
            else{
                isJumping = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping= false;
        }
    }
}
