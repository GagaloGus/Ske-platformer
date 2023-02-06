using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    int maxwells = 0;
    bool bonkedEnemy = false;
    public KeyCode counterKey = KeyCode.V;
    Rigidbody2D rb;
    Animator animator;
    Player_Movement playerMovementScript;
    GameObject cameraGO;
    GameObject counteredEnemy;

    //public bool isCountering, counterSucess;
    //public float counterTimer;

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

        //determina si esta en el suelo, nadando o en el aire
        if(playerMovementScript.falling_pm == 0) { playerMovementScript.is_grounded = true; playerMovementScript.is_swimming = false; }
        else if (playerMovementScript.falling_pm == 3) { playerMovementScript.is_grounded = false; playerMovementScript.is_swimming = true; }
        else { playerMovementScript.is_grounded = false; playerMovementScript.is_swimming = false; }


        //CounterDetection();
    }
    /*void CounterDetection()
    {
        if (!counterSucess)
        {
            if (Input.GetKeyDown(counterKey) && playerMovementScript.is_grounded && !isCountering)
            {
                counterTimer = 1;
                isCountering = true;
                playerMovementScript.able_to_move = false;
            }
            if (isCountering)
            {
                counterTimer -= Time.deltaTime;
                animator.SetInteger("countering", 1);
            }

            if ((Input.GetKeyUp(counterKey) || counterTimer <= 0) && isCountering)
            {
                isCountering = false;
                playerMovementScript.able_to_move = true;
                animator.SetInteger("countering", 0);
            }
        }
    }
    public void endOfCounter()
    {
        animator.SetInteger("countering", 0);
        isCountering = false;
        counterSucess = false;
        playerMovementScript.able_to_move = true;
        Destroy(counteredEnemy);

        Enemy_Stats[] components = GameObject.FindObjectsOfType<Enemy_Stats>();
        foreach (Enemy_Stats comp in components) { comp.GetComponent<Enemy_Stats>().speed *= 3; }
    }*/
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
        if(SceneManager.GetActiveScene().name == "Level 1") { SceneManager.LoadScene("Level 2"); }
        else if (SceneManager.GetActiveScene().name == "Level 2") { SceneManager.LoadScene("Level 1"); }

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
        //salta en el enemigo
        if (collision.gameObject.CompareTag("enemy bonk box"))
        {
            Destroy(collision.transform.parent.gameObject);
            bonkedEnemy = true;
            //salta tras botar en el enemigo
            rb.velocity = new Vector2(rb.velocity.x, playerMovementScript.player_jump_power * 1.5f);
        }

        //le pega un proyectil
        if (collision.gameObject.CompareTag("enemy ball"))
        {
            DieEnemy();
        }

        //coje un maxwell
        if (collision.gameObject.CompareTag("key level item"))
        {
            maxwells++;
            print("mrrowww " + maxwells);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("maxwell end trigger"))
        {
            //si tiene los 3 maxwells necesarios
            if (maxwells >= 3)
            {
                //el personaje no se mueva
                playerMovementScript.able_to_move = false;

                //pone al gato mirando a la casa
                GetComponent<SpriteRenderer>().flipX = true;

                //animacion de idle
                animator.SetBool("isMoving", false);
                animator.Play("idle-anim");

                //no se mueva en el eje X
                rb.velocity = new Vector2(0, rb.velocity.y);

                //la camara se ponga en la posicion de la cutscene
                cameraGO.GetComponent<CameraSystem>().levelEnded = true;

                //reproduce las animaciones del cutsscene
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
