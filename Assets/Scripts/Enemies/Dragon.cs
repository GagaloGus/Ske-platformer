using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyCreature
{
    public GameObject fireball;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        SetStats(0, 8, 75);
    }

    public override void Idle()
    {
        base.Idle();
        GetComponent<Animator>().SetBool("inRange", false);
    }

    public override void Attack()
    {
        base.Attack();

        if (transform.position.x > nearestPlayer.transform.position.x) { directionLooking = 1; }
        else { directionLooking = -1; }

        GetComponent<Animator>().SetBool("inRange", true);
    }

    public void Shoot()
    {
        AudioManager.instance.PlaySFX("Dragon Fireball");
        GameObject bal = Instantiate(fireball, transform.position, Quaternion.identity);
        bal.gameObject.name = "Fireball";
    }
}
