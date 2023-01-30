using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_shoot : MonoBehaviour
{
    public GameObject player, fireball;
    SpriteRenderer sprRend;
    BoxCollider2D boxCol2d;
    // Start is called before the first frame update
    void Start()
    {
        //encuentra al jugador
        player = GameObject.FindGameObjectWithTag("Player");
        sprRend = GetComponent<SpriteRenderer>();
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //si la posicion en x del jugador es menor a 10 respecto a la del dragon, dispara
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 10)
        {
            GetComponent<Animator>().SetBool("inRange", true);
            //se da la vuelta para mirar al jugador
            if (transform.position.x > player.transform.position.x) { sprRend.flipX = true; }
            else { sprRend.flipX = false; }
        } else { GetComponent<Animator>().SetBool("inRange", false); }
        
    }
    public void Shoot()
    {
        //dispara
        Instantiate(fireball, transform.position, Quaternion.identity);
    }
}
