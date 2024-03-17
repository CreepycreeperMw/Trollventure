using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxColl;
    private Animator anim;
    private SpriteRenderer rbSprite;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpHeight = 12f;
    private float xVel = 0f;
    private bool isSprinting = false;
    MovementState state;
    public static bool blockMovement = false;
    private static bool freezed = false;

    static PlayerMovement pl;

    private enum MovementState { idle, running, sprinting, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();
        pl = this;
    }

    // Update is called once per frame
    private void Update()
    {
        xVel = Input.GetAxisRaw("Horizontal");

        // Sprinting logic
        isSprinting = Input.GetButton("Sprint");
        float speedMultiplier = 1;
        if (isSprinting) speedMultiplier = 1.5f;

        // Movement Logic
        if(!PlayerLife.isDead() && !blockMovement && !freezed) rb.velocity = new Vector2(xVel * moveSpeed * speedMultiplier, rb.velocity.y);
        
        if(!freezed) UpdateAnimationState();

        // Jump
        if (Input.GetButton("Jump") && !blockMovement && isOnGround())
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpHeight * (speedMultiplier/2f +0.5f));
        }

    }
    private void UpdateAnimationState()
    {
        // Idle & Running & Sprinting
        if (xVel == 0 || blockMovement)
        {
            state = MovementState.idle;
        }
        else
        {
            if (xVel < 0) rbSprite.flipX = true;
            else rbSprite.flipX = false;

            if(isSprinting) { state = MovementState.sprinting; }
            else state = MovementState.running;
        }

        // Jumping & Falling
        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int) state);
    }
    private bool isOnGround()
    {
        return (state != MovementState.jumping && state != MovementState.falling) && Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    public static void freeze()
    {
        freezed = true;
        pl.rb.bodyType = RigidbodyType2D.Static;
    }

    public static void unfreeze()
    {
        freezed = false;
        pl.rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
