using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public GameObject fireball;
    SpriteRenderer sprRend;

    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();

        SetStats(0, 10, 75);
    }

    public override void Idle()
    {
        GetComponent<Animator>().SetBool("inRange", false);

    }

    public override void Attack()
    {
        if (transform.position.x > nearestPlayer.transform.position.x) { directionLooking = 1; }
        else { directionLooking = -1; }

        GetComponent<Animator>().SetBool("inRange", true);
        AudioManager.instance.PlaySFX("Dragon Fireball");
        GameObject bal = Instantiate(fireball, transform.position, Quaternion.identity);
        bal.gameObject.name = "Fireball";
    }
}
