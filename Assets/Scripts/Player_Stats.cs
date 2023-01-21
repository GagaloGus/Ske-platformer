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

        if(playerMovementScript.falling_pm == 0) { playerMovementScript.is_grounded = true; playerMovementScript.is_swimming = false; }
        else if (playerMovementScript.falling_pm == 3) { playerMovementScript.is_grounded = false; playerMovementScript.is_swimming = true; }
        else { playerMovementScript.is_grounded = false; playerMovementScript.is_swimming = false; }
    }

    public void DieFall()
    {
        SceneManager.LoadScene("level1");
    }

    public void DieEnemy()
    {
        SceneManager.LoadScene("level1");
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
            rb.velocity = new Vector2(rb.velocity.x, playerMovementScript.player_jump_power * 1.5f);
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
    }
    public bool bonked_enemy
    {
        get { return bonkedEnemy; }
        set { bonkedEnemy = value; }
    }
}
