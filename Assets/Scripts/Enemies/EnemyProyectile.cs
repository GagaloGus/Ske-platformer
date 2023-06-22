using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectile : Enemy
{
    public int despawnTime;
    public float speed;



    private new void Start()
    {
        base.Start();
        if (transform.position.x > nearestPlayer.transform.position.x) { directionLooking = -1; }
        else { directionLooking = 1; }

        base.baseSpeed = speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(baseSpeed * directionLooking, GetComponent<Rigidbody2D>().velocity.y);

        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
