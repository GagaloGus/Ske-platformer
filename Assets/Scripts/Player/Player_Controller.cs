using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    KeyCode
        jumpKey = KeyCode.Space,
        sprintKey = KeyCode.LeftShift,
        crouchKey = KeyCode.LeftControl;

    public int maxwells, controlState;
    public bool isGrounded, 
        isSwimming, 
        ableToMove = false, 
        isJumping;
    
    public float moveX, 
        speed = 8,
        jumpPower = 15, jumpTimeCounter;
    Vector2 coordsBoxCol2d;
    LayerMask groundLayerMask;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprRend;
    public BoxCollider2D boxCol2d;
    GameObject cameraGO;
    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");

        sprRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCol2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        cameraGO = GameObject.FindGameObjectWithTag("MainCamera");

        //determina la posicion del Boxcast segun el box collider
        
    }

    // Update is called once per frame
    void Update()
    {
        coordsBoxCol2d = transform.position + new Vector3(boxCol2d.offset.x, boxCol2d.offset.y - 0.2f, 0);
        BoxCasting();

        if (ableToMove)
        {
            moveX = Input.GetAxis("Horizontal");

            if (isGrounded) { Ground(); }
            else if (isSwimming) { Swim(); }
            else { Air(); }

            Jump();
            animator.SetInteger("falling", controlState);
        }
    }

    void Ground()
    {
        rb.gravityScale = 7; rb.drag = 0.4f;
        if (!Input.GetKey(crouchKey))
        {
            if (Input.GetKey(sprintKey)) { moveX *= 1.5f; }
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

            if (Mathf.Abs(moveX) < 0.1f) { controlState = 0; }
            else { controlState = 2; }
        }
    }
    void Swim()
    {
        rb.gravityScale = 1; rb.drag = 4;
        //reduce el movimiento en el agua
        rb.velocity = new Vector2(moveX * speed / 1.5f, rb.velocity.y);
        //nada
        if (Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(moveX * speed / 3, jumpPower / 1.5f);
        }
    }
    void Air()
    {
        rb.gravityScale = 7; rb.drag = 0.4f;
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if(rb.velocity.y < -0.1) { controlState = -1; }
        else if (rb.velocity.y > 0.1) { controlState = 1; }
    }

    void Jump()
    {
        //salta si esta en el suelo y le doy al espacio
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpTimeCounter = 0.2f;
            //AudioManager.instance.PlayAudio(jumpSound, jumpSoundVol);
        }

        //permite que salte mas si sigo presionando espacio
        if (Input.GetKey(jumpKey) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
            }
            else { isJumping = false; }
        }

        //si suelto el espacio no estoy saltando
        if (Input.GetKeyUp(jumpKey)) { isJumping = false; }

    }
    void BoxCasting()
    {
        RaycastHit2D boxcasteo = Physics2D.BoxCast(coordsBoxCol2d, boxCol2d.size / 1.25f, 0, Vector2.down, 0.1f, groundLayerMask);
       
        if (boxcasteo.collider)
        {
            print(boxcasteo.collider.tag);
            //si el boxcast toca suelo
            if (boxcasteo.collider.CompareTag("suelo"))
            {
                isGrounded = true; isSwimming = false;
            }
            //si el boxcast toca awa
            else if (boxcasteo.collider.CompareTag("water"))
            {
                isSwimming = true; isGrounded = false;
            }
            FlipPlayer();
        }
        //si el boxcast no toca nada
        else
        {
            isGrounded = false; isSwimming = false;
        } 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(coordsBoxCol2d, boxCol2d.size / 1.25f);
    }

    void FlipPlayer()
    {
        //doy la vuelta al personaje y a su box collider
        if (moveX > 0 && !sprRend.flipX) { sprRend.flipX = true; boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y); }
        else if (moveX < 0 && sprRend.flipX) { sprRend.flipX = false; boxCol2d.offset = new Vector2(boxCol2d.offset.x * -1, boxCol2d.offset.y); }
    }
    public void EnterLevel()
    {
        ableToMove = true;
    }
}
