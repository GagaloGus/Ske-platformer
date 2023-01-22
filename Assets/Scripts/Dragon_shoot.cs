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
        player = GameObject.FindGameObjectWithTag("Player");
        sprRend = GetComponent<SpriteRenderer>();
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 10)
        {
            GetComponent<Animator>().SetBool("inRange", true);
            if (transform.position.x > player.transform.position.x) { sprRend.flipX = true; }
            else { sprRend.flipX = false; }
        } else { GetComponent<Animator>().SetBool("inRange", false); }
        
    }
    public void Shoot()
    {
        Instantiate(fireball, transform.position, Quaternion.identity);
    }
}
