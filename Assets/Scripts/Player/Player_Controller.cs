using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    KeyCode
        jumpKey = KeyCode.Space,
        sprintKey = KeyCode.LeftShift,
        crouchKey = KeyCode.LeftControl;

     int maxwells;

    enum playerState { Idle, Walking, GoingUp, GoingDown, Crouching, Swimming, AirSpin };
       playerState controlState;

     bool isGrounded, 
        isSwimming, 
        ableToMove = false, 
        isJumping,
        bonkedEnemy,
        hasDied;

    float moveX,
        speed = 8,
        jumpPower = 15, jumpTimeCounter,
        speedRed, groundDrag = 1;

    Vector2 coordsBoxCol2d;
    LayerMask groundLayerMask;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprRend;
    public BoxCollider2D boxCol2d;
    GameObject cameraGO, deathMenu;

    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");

        sprRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCol2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        cameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        deathMenu = FindObjectOfType<Canvas>().gameObject.transform.Find("DeathScreen").gameObject;

        sprRend.sortingLayerName = "2";
        animator.SetBool("killed", false);
        boxCol2d.enabled = true;
        hasDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        coordsBoxCol2d = transform.position + new Vector3(boxCol2d.offset.x, boxCol2d.offset.y - 0.2f, 0);
        BoxCasting();

        //si se puede mover y el juego no esta pausado
        if (ableToMove && !PauseMenu.isPaused)
        {
            moveX = Input.GetAxis("Horizontal");

            //segun lo que detecte la caja
            if (isGrounded) { Ground(); }
            else if (isSwimming) { Swim(); }
            else { Air(); }

            //tenga friccion en el suelo
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && controlState != playerState.Crouching) 
            { rb.velocity = new Vector2(moveX * speed / speedRed, rb.velocity.y); }
            else { rb.velocity = new Vector2(rb.velocity.x / groundDrag, rb.velocity.y); }
            
            Jump();
            //cambia los parametros del animator
            animator.SetInteger("controlState", (int)controlState);
        }
    }

    void Ground()
    {
        GroundGravSettings();
        //si no esta agachado
        if (!Input.GetKey(crouchKey))
        {
            //correr
            if (Input.GetKey(sprintKey)) { moveX *= 1.5f; }
            //esta quieto o andando
            if (Mathf.Abs(moveX) < 0.15f) { controlState = playerState.Idle; }
            else { controlState = playerState.Walking; }
        }
        else { controlState = playerState.Crouching;}
    }
    void Swim()
    {
        WaterGravSettings();
        //reduce el movimiento en el agua

        //nada
        if (Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(moveX * speed / 3, jumpPower / 1.5f);
        }
        controlState = playerState.Swimming;
    }
    void Air()
    {
        GroundGravSettings();
        if (Input.GetKey(sprintKey)) { moveX *= 1.5f; }

        //si le ha pegado a un enemigo
        if (bonkedEnemy) { controlState = playerState.AirSpin; }
        else
        {
            //subiendo o bajando
            if (rb.velocity.y < -0.1) { controlState = playerState.GoingDown; }
            else if (rb.velocity.y > 0.1) { controlState = playerState.GoingUp; }
        }
    }

    void Jump()
    {
        //salta si esta en el suelo y le doy al espacio
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpTimeCounter = 0.21f;
            AudioManager.instance.PlaySFX("Jump");
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
            //si el boxcast toca suelo
            if (boxcasteo.collider.CompareTag("suelo"))
            {
                isGrounded = true; isSwimming = false;
                groundDrag = 1.1f;
            }
            //si el boxcast toca awa
            else if (boxcasteo.collider.CompareTag("water"))
            {
                isSwimming = true; isGrounded = false;
                groundDrag = 1;
            }
            else if (boxcasteo.collider.CompareTag("ice"))
            {
                isGrounded = true; isSwimming = false;
                groundDrag = 1.005f;
            }
            FlipPlayer();
            bonkedEnemy = false;
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

    //Stats
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //le mata un enemigo
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Death(collision.gameObject.name);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //entro en el agua
        if (collision.gameObject.CompareTag("water")) { AudioManager.instance.PlaySFX("Enter water"); }

        //salta en el enemigo
        if (collision.gameObject.CompareTag("enemy bonk box"))
        {
            Destroy(collision.transform.parent.gameObject);
            //salta tras botar en el enemigo
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 1.5f);
            bonkedEnemy = true;

            //añade puntos
            GameManager.instance.gm_score += collision.transform.parent.GetComponent<Enemy>().killScore;

            AudioManager.instance.PlaySFX("Stomp enemy");
        }

        //le pega un proyectil o se cae al vacio
        if (collision.gameObject.GetComponent<Enemy>() || collision.gameObject.CompareTag("deathZone"))
        {
            Death(collision.gameObject.name);
        }

        //coje una gema
        if (collision.gameObject.CompareTag("item"))
        {
            GameManager.instance.gm_score += 10;
            AudioManager.instance.PlaySFX("Gem");
            Destroy(collision.gameObject);
        }

        //coje un maxwell
        if (collision.gameObject.CompareTag("key level item"))
        {
            maxwells++;
            AudioManager.instance.PlaySFX("Pickup maxwell");
            GameManager.instance.gm_score += 125;
            Destroy(collision.gameObject);
        }

        //fin del nivel
        if (collision.gameObject.CompareTag("maxwell end trigger"))
        {
            //si tiene los 3 maxwells necesarios
            if (maxwells >= 3)
            {
                //el personaje no se mueva
                ableToMove = false;

                //pone al gato mirando a la casa
                sprRend.flipX = true;

                //animacion de idle
                animator.SetInteger("controlState", 0);

                //no se mueva en el eje X
                rb.velocity = new Vector2(0, rb.velocity.y);

                //la camara se ponga en la posicion de la cutscene
                cameraGO.GetComponent<CameraSystem>().levelEnded = true;

                //reproduce las animaciones del cutsscene
                collision.transform.parent.GetComponent<Animator>().SetBool("endLevelTriggered", true);
                collision.transform.parent.GetComponent<Animator>().SetBool("endLevelNot", false);

                //pare la musica de fondo
                AudioManager.instance.musicSource.Stop();

                //para el tiempo en la cinematica
                GameManager.instance.isCinematic = true;
                
            }
            else
            {
                collision.transform.parent.GetComponent<Animator>().SetBool("endLevelNot", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water")) { AudioManager.instance.PlaySFX("Leave water"); }
    }
    public void Death(string enemyKiller)
    {
        //se muere :(
        hasDied = true; ableToMove = false;
        //le pasa al script del menu de muerte que le mató
        DeathMenu.entityKiller = enemyKiller;
        deathMenu.SetActive(true);

        //activa la animacion de muerte del menu
        deathMenu.GetComponent<Animator>().Play("deathMenuEnter"); 
        //tiempo y puntuacion restantes
        deathMenu.GetComponent<DeathMenu>().timeRemaining = Mathf.Round(GameManager.instance.gm_time * 100) * 0.01f;
        deathMenu.GetComponent<DeathMenu>().scoreRemaining = GameManager.instance.gm_score;

        //pone al personaje delante de la interfaz
        sprRend.sortingLayerName = "Delante UI";
        //se quede quieto y desactiva su collider
        rb.gravityScale = 0; rb.velocity = Vector2.zero; boxCol2d.enabled = false;

        //animacion de muerte
        animator.Play("enemyDeath");
        animator.SetInteger("controlState", 0);

        //para todos los sonidos y reproduce el sonido de morir
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.sfxSource.Stop();
        AudioManager.instance.PlaySFX("Death");
    }

    public void ChangeLevel() //cambio de escenas al pasarse el nivel
    {
        //para los sonidos
        AudioManager.instance.musicSource.Stop();

        //string que sera el nombre del siguiente nivel respecto al nivel actual
        string[] allScenes = { "Menu", "Level 1", "Level 2" };
        int nextScene = SceneManager.GetActiveScene().buildIndex;

        if (nextScene == allScenes.Length - 1) { nextScene = 0; }
        else { nextScene++; }
        GameManager.instance.ChangeScene(allScenes[nextScene]);

    }
    public void EnterLevel()
    {
        ableToMove = true;
    }

    void GroundGravSettings()
    {
        rb.gravityScale = 7; rb.drag = 0.4f;
        speedRed = 1;
    }

    void WaterGravSettings()
    {
        rb.gravityScale = 1; rb.drag = 4;
        speedRed = 1.5f;
    }

    public bool has_died
    {
        get { return hasDied; }
        set { hasDied = value; }
    }

    public int maxwell_count
    {
        get { return maxwells; }
    }
}
