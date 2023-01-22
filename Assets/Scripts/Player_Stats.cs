using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    int maxwells = 0;
    bool bonkedEnemy = false;
    Rigidbody2D rb;
    Animator animator;
    Player_Movement playerMovementScript;
    GameObject cameraGO;

    public float heightDeathzone;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraGO = GameObject.FindGameObjectWithTag("MainCamera");


    }

    // Update is called once per frame
    void Update()
    {
        playerMovementScript = GetComponent<Player_Movement>();
        if (transform.position.y < heightDeathzone) {  DieFall(); }

        if(playerMovementScript.falling_pm == 0) { playerMovementScript.is_grounded = true; playerMovementScript.is_swimming = false; }
        else if (playerMovementScript.falling_pm == 3) { playerMovementScript.is_grounded = false; playerMovementScript.is_swimming = true; }
        else { playerMovementScript.is_grounded = false; playerMovementScript.is_swimming = false; }
    }

    public void DieFall()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DieEnemy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        if (collision.gameObject.CompareTag("enemy ball"))
        {
            DieEnemy();
        }

        if (collision.gameObject.CompareTag("key level item"))
        {
            maxwells++;
            print("mrrowww " + maxwells);
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.CompareTag("maxwell end trigger"))
        {
            if(maxwells >= 3)
            {
                playerMovementScript.able_to_move = false;
                GetComponent<SpriteRenderer>().flipX = true;
                animator.SetBool("isMoving", false);
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.Play("idle-anim");
                cameraGO.GetComponent<CameraSystem>().levelEnded = true;
                collision.transform.parent.GetComponent<Animator>().SetBool("endLevelTriggered", true);
                collision.transform.parent.GetComponent<Animator>().SetBool("endLevelNot", false);
            }
            else
            {
                collision.transform.parent.GetComponent<Animator>().SetBool("endLevelNot", true);
            }

        }
    }
    public bool bonked_enemy
    {
        get { return bonkedEnemy; }
        set { bonkedEnemy = value; }
    }
    public int maxwell_count
    {
        get { return maxwells; }
        set { maxwells = value; }
    }
}
