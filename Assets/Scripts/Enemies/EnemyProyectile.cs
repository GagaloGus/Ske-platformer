using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectile : Enemy
{
    private float despawnTimer;
    protected int despawnMaxTime;
    public float speed;

    private void Start()
    {
        if (transform.position.x > nearestPlayer.transform.position.x) { directionLooking = -1; }
        else { directionLooking = 1; }

        speed = baseSpeed;
    }

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(baseSpeed * directionLooking, GetComponent<Rigidbody2D>().velocity.y);

        DespawnProyectile();
    }

    public void DespawnProyectile()
    {
        despawnTimer -= Time.deltaTime;
        if (despawnTimer <= 0)
        {
            despawnTimer = despawnMaxTime;
            Destroy(gameObject);
        }
    }
}
