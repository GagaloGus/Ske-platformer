using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject player, dragon;
    SpriteRenderer sprRend;
    public float despawnTimer = 5, speed;
    // Start is called before the first frame update
    void Start()
    {
        //encuentra el personaje
        player = GameObject.FindGameObjectWithTag("Player");
        sprRend = GetComponent<SpriteRenderer>();

        //se da la vuelta segun  la posicion del personaje
        if(transform.position.x > player.transform.position.x) { sprRend.flipX = true; GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0); }
        else { sprRend.flipX = false; GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0); }
    }

    // Update is called once per frame
    void Update()
    {
        //despawnea
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            despawnTimer = 5;
            Destroy(gameObject);
        }
    }
}
