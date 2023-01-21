using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    KeyCode
        jumpKey = KeyCode.Space,
        sprintKey = KeyCode.LeftShift,
        crouchKey = KeyCode.LeftControl;

    float
        playerSpeed = 8,
        moveX, 
        playerDistanceToBottom, 
        jumpTimeCounter,
        playerJumpPower = 15;
        
<<<<<<< HEAD
    public bool
=======
    bool
        facingRight,
>>>>>>> parent of 5d62601 (o my gah)
        isGrounded,
        isJumping,
        AbleToMove = false;

<<<<<<< HEAD

=======
    LayerMask groundLayerMask;
>>>>>>> parent of 5d62601 (o my gah)

   

    SpriteRenderer sprRend;
    Rigidbody2D rb;
    public BoxCollider2D boxCol2d;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCol2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        playerDistanceToBottom = boxCol2d.bounds.size.y + 0.1f;
=======
        playerDistanceToBottom = boxCol2d.bounds.size.y + 0.05f;
        groundLayerMask = LayerMask.GetMask("Ground");
        

        //pone al bichejo mirando a la derecha
        FlipPlayer();
>>>>>>> parent of 5d62601 (o my gah)
    }

    

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        moveX = Input.GetAxis("Horizontal");
=======
        if (!Input.GetKey(crouchKey) && AbleToMove) { PlayerMove(); }
>>>>>>> parent of 5d62601 (o my gah)
        Raycasting();

        if (AbleToMove && !Input.GetKey(crouchKey)) 
        {
            if (!isSwimming) { Move(); }
            else { Swim(); }
            FlipPlayer();
        }
    }
    void Raycasting()
    {
        RaycastHit2D groundRaycastR = Physics2D.Raycast(
            new Vector2(transform.position.x + boxCol2d.bounds.size.x / 2 + boxCol2d.offset.x, transform.position.y), 
            Vector2.down, playerDistanceToBottom);
        RaycastHit2D groundRaycastL = Physics2D.Raycast(
            new Vector2(transform.position.x - boxCol2d.bounds.size.x / 2 + boxCol2d.offset.x, transform.position.y), 
            Vector2.down, playerDistanceToBottom);
        bool RaysHitSomething = (groundRaycastR.collider != null) || (groundRaycastL.collider != null);

        print(groundRaycastR.collider.name);
        if (RaysHitSomething)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }
    }

    void PlayerMove()
    {
<<<<<<< HEAD
        rb.gravityScale = 7;
        rb.drag = 0.4f;
        //controles
        Jump();
        
=======
        //controles
        PlayerJump();
        moveX = Input.GetAxis("Horizontal");


>>>>>>> parent of 5d62601 (o my gah)
        if (Input.GetKey(sprintKey) && moveX != 0)
        { moveX *= 1.5f; }

        //animacion
        
        
        //fisicas
        rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
    }
<<<<<<< HEAD
    void Swim()
    {
        rb.gravityScale = 1; rb.drag = 4;
        rb.velocity = new Vector2(moveX * playerSpeed/1.5f, rb.velocity.y);
        if (Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(moveX * playerSpeed/3, playerJumpPower/1.5f);
        }
    }
    void Jump()
=======

    void PlayerJump()
>>>>>>> parent of 5d62601 (o my gah)
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpPower);
            isJumping = true;
            jumpTimeCounter = 0.2f;
        }

        if (Input.GetKey(jumpKey) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, playerJumpPower);
                jumpTimeCounter -= Time.deltaTime;
            } else { isJumping = false; }
        }

        if (Input.GetKeyUp(jumpKey)) { isJumping = false; }
        
    }

    void FlipPlayer()
    {
        if (isGrounded || isSwimming) {

            if (moveX > 0 && !sprRend.flipX)
            {
                boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y);
                sprRend.flipX = true;
            }
            else if (moveX < 0 && sprRend.flipX)
            {
                boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y);
                sprRend.flipX = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Vector2(transform.position.x + boxCol2d.bounds.size.x / 2 + boxCol2d.offset.x, transform.position.y), Vector2.down);
        Gizmos.DrawRay(new Vector2(transform.position.x - boxCol2d.bounds.size.x / 2 + boxCol2d.offset.x, transform.position.y), Vector2.down);
    }
    public float player_speed
    {
        get { return playerSpeed; }
    }
    public float player_jump_power
    {
        get { return playerJumpPower; }
    }
    public KeyCode jump_key
    {
        get { return jumpKey; }
    }
    public KeyCode crouch_key
    {
        get { return crouchKey; }
    }
<<<<<<< HEAD
    public bool is_swimming
    {
        get { return isSwimming; }
        set { isSwimming = value; }
=======
    public bool facing_Right
    {
        get { return facingRight; }
        set { facingRight = value; }
>>>>>>> parent of 5d62601 (o my gah)
    }
    public bool is_grounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }
    public bool is_jumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }
    public bool able_to_move
    {
        get { return AbleToMove; }
        set { AbleToMove = value; }
    }
}
