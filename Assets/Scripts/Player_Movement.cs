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
        

    public bool
        isGrounded,
        isJumping,
        isSwimming,
        AbleToMove = false;

    Vector2 coordsBoxCol2d;
    LayerMask groundLayerMask;

    public int falling;

    SpriteRenderer sprRend;
    Rigidbody2D rb;
    BoxCollider2D boxCol2d;
    Animator animator;
    RaycastHit2D boxcasteo;

    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCol2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        //playerDistanceToBottom = boxCol2d.bounds.size.y + 0.05f;
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        coordsBoxCol2d = transform.position + new Vector3(boxCol2d.offset.x, boxCol2d.offset.y, 0);

        if (AbleToMove)
        {
            moveX = Input.GetAxis("Horizontal");
            Boxcasting();
        }
    }

    void Boxcasting()
    {
        boxcasteo = Physics2D.BoxCast(coordsBoxCol2d, boxCol2d.size + boxCol2d.size/10, 0, Vector2.down, 0.1f, groundLayerMask);
        if (boxcasteo.collider)
        {
            if (boxcasteo.collider.CompareTag("suelo"))
            {
                falling = 0;
                Walk();
            }
            else if (boxcasteo.collider.CompareTag("water"))
            {
                falling = 3;
                Swim();
            }
            FlipPlayer();
            GetComponent<Player_Stats>().bonked_enemy = false;
        }
        else
        {
            Walk();
            if (GetComponent<Player_Stats>().bonked_enemy) { falling = 2; }
            else
            {
                if (rb.velocity.y > 0.1f) { falling = 1; }
                else if (rb.velocity.y < -0.1f) { falling = -1; }
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (boxcasteo)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(coordsBoxCol2d, Vector2.down * boxcasteo.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(coordsBoxCol2d, boxCol2d.size);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(coordsBoxCol2d, Vector2.down * 100);
        }
    }
    void Walk()
    {
        rb.gravityScale = 7; rb.drag = 0.4f;

        if (!Input.GetKey(crouchKey))
        {
            Jump();
            if (Input.GetKey(sprintKey) && Mathf.Abs(moveX) > 0.1f)
            { moveX *= 1.5f; }
            rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        }
    }

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
            if (moveX > 0 && !sprRend.flipX) { sprRend.flipX = true; boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y); }
            else if (moveX < 0 && sprRend.flipX) { sprRend.flipX = false; boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y);}
    }
    public int falling_pm    
    {
        get { return falling; }
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
    public bool is_swimming
    {
        get { return isSwimming; }
        set { isSwimming = value; }
    }
}
