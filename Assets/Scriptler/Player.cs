using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float accelerationForce;
    public float maxSpeed;
    public float timeZeroToMax;
    public Rigidbody2D GregRB;
    private float moveInput;
    private Vector3 scale;
    void Start()
    {
        scale = this.transform.position;
    }

    
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        GregRB.AddForce(Vector2.right * moveInput * accelerationForce);
        Vector2 clampedVelocity = GregRB.velocity;
        clampedVelocity.x = Mathf.Clamp(GregRB.velocity.x, -maxSpeed, maxSpeed);
        GregRB.velocity = clampedVelocity;

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
    }
}
