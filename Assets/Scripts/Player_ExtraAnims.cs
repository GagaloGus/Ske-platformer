using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ExtraAnims : MonoBehaviour
{
    public float enterLevelTimer;
    
    Rigidbody2D rb;
    Animator animator;
    Player_Movement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enterLevelTimer = 3.5f;
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
            Swimming();
        }
    }
    void Falling()
    {
        if (!playerMovementScript.is_grounded && !playerMovementScript.is_swimming)
        {
            if (!GetComponent<Player_Stats>().bonked_enemy) {
                if (rb.velocity.y > 0.1) { animator.SetInteger("falling", 1); }
                else if (rb.velocity.y < -0.1) { animator.SetInteger("falling", -1); }
            } else { animator.SetInteger("falling", 2); }  
        }
        else if (playerMovementScript.is_swimming)
        {
            animator.SetInteger("falling", 3);
        }
        else 
        { 
            animator.SetInteger("falling", 0); 
            GetComponent<Player_Stats>().bonked_enemy = false;
        }
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

    void Swimming()
    {
        if (playerMovementScript.is_swimming) { animator.SetInteger("falling", 3); }
    }

    public void EnterLevel()
    {
        playerMovementScript.able_to_move = true;
    }
}
