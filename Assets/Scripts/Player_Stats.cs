using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    public int maxwells = 0;
    bool bonkedEnemy = false;
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
        if (transform.position.y < -7) {  DieFall(); }

        //if (GetComponent<Player_Movement>().is_grounded) 
        //{ animator.SetBool("airSpin", false); } 
    }

    public void DieFall()
    {
        SceneManager.LoadScene("LevelTest");
    }

    public void DieEnemy()
    {
        SceneManager.LoadScene("LevelTest");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            DieEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy bonk box"))
        {
            Destroy(collision.transform.parent.gameObject);
            bonkedEnemy = true;
            rb.velocity = new Vector2(rb.velocity.x, playerMovementScript.player_jump_power*1.5f);
        } 

        if (collision.gameObject.CompareTag("key level item"))
        {
            maxwells++;
            print("mrrowww " + maxwells);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("active", true);
        }
<<<<<<< HEAD

        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            playerMovementScript.is_swimming = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            playerMovementScript.is_swimming = false;
        }
    }
=======
    } 

>>>>>>> parent of 5d62601 (o my gah)
    public bool bonked_enemy
    {
        get { return bonkedEnemy; }
        set { bonkedEnemy = value; }
    }
}
