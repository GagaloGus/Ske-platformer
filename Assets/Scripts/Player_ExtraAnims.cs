using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ExtraAnims : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Player_Movement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovementScript = GetComponent<Player_Movement>();
        if (playerMovementScript.able_to_move)
        {
            Falling();
            Crouching();
            Walk();
        }  else { animator.SetInteger("falling", 0); }

    }
 
    void Falling()
    {
        animator.SetInteger("falling", playerMovementScript.falling_pm);
    }
    void Walk()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 ) { animator.SetBool("isMoving", true); }
        else { animator.SetBool("isMoving", false); }
    }
    void Crouching()
    { 
        animator.SetBool("isCrouching", Input.GetKey(playerMovementScript.crouch_key));
    }
    public void EnterLevel()
    {
        playerMovementScript.able_to_move = true;
    }


}
