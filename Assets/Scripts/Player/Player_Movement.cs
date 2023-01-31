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
        jumpTimeCounter,
        playerJumpPower = 15;
        

     bool
        isGrounded,
        isJumping,
        isSwimming,
        AbleToMove = false;

    Vector2 coordsBoxCol2d;
    LayerMask groundLayerMask;

     int falling;

    SpriteRenderer sprRend;
    Rigidbody2D rb;
    public BoxCollider2D boxCol2d;
    Animator animator;
    RaycastHit2D boxcasteo;

    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCol2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //determina la posicion del Boxcast segun el box collider
        coordsBoxCol2d = transform.position + new Vector3(boxCol2d.offset.x, boxCol2d.offset.y - 0.2f, 0);

        if (AbleToMove)
        {
            moveX = Input.GetAxis("Horizontal");
            Boxcasting();
        } else { falling = 0; }
    }

    void Boxcasting()
    {
        boxcasteo = Physics2D.BoxCast(coordsBoxCol2d, boxCol2d.size / 1.25f, 0, Vector2.down, 0.1f, groundLayerMask);
        if (boxcasteo.collider)
        {
            //si el boxcast toca suelo
            if (boxcasteo.collider.CompareTag("suelo"))
            {
                falling = 0;
                Walk();
            }
            //si el boxcast toca awa
            else if (boxcasteo.collider.CompareTag("water"))
            {
                falling = 3;
                Swim();
            }
            FlipPlayer();
            GetComponent<Player_Stats>().bonked_enemy = false;
        }
        //si el boxcast no toca nada
        else
        {
            Walk();
            //si ha botado en un enemigo 
            if (GetComponent<Player_Stats>().bonked_enemy) { falling = 2; }
            else
            {
                //sube o baja
                if (rb.velocity.y > 0.1f) { falling = 1; }
                else if (rb.velocity.y < -0.1f) { falling = -1; }
            }
        }
    }

    void Walk()
    {
        rb.gravityScale = 7; rb.drag = 0.4f;

        //si no esta agachado 
        if (!Input.GetKey(crouchKey) && !Input.GetKey(GetComponent<Player_Stats>().counterKey))
        {
            Jump();
            //correr
            if (Input.GetKey(sprintKey) && Mathf.Abs(moveX) > 0.1f)
            { moveX *= 1.5f; }
            rb.velocity = new Vector2(moveX * playerSpeed, rb.velocity.y);
        }
    }

    void Swim()
    {
        rb.gravityScale = 1; rb.drag = 4;
        //reduce el movimiento en el agua
        rb.velocity = new Vector2(moveX * playerSpeed/1.5f, rb.velocity.y);
        //nada
        if (Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(moveX * playerSpeed/3, playerJumpPower/1.5f);
        }
    }
   void Jump()
    {
        //salta si esta en el suelo y le doy al espacio
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpPower);
            isJumping = true;
            jumpTimeCounter = 0.2f;
        }

        //permite que salte mas si sigo presionando espacio
        if (Input.GetKey(jumpKey) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, playerJumpPower);
                jumpTimeCounter -= Time.deltaTime;
            } else { isJumping = false; }
        }

        //si suelto el espacio no estoy saltando
        if (Input.GetKeyUp(jumpKey)) { isJumping = false; }
        
    }
    void FlipPlayer()
    {
        //doy la vuelta al personaje y a su box collider
            if (moveX > 0 && !sprRend.flipX) { sprRend.flipX = true; boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y); }
            else if (moveX < 0 && sprRend.flipX) { sprRend.flipX = false; boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y);}
    }
    public int falling_pm    
    {
        get { return falling; }
        set { falling = value; }
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
