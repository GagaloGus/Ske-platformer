using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sebas : EnemyCreature
{
    Rigidbody2D rb;
    BoxCollider2D boxCol2d;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //randomiza la direccion al que va
        do { directionLooking = Random.Range(-1, 2); } while (directionLooking == 0);

        rb = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();

        SetStats(4, 5, 50);
    }

    public override void Idle()
    {
        base.Idle();
        currentSpeed = baseSpeed;
        Move();
    }
    public override void Attack()
    {
        base.Attack();
        currentSpeed = baseSpeed * 1.5f;
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(directionLooking * currentSpeed, rb.velocity.y);
        WallChangeDirection(groundLayerMask, 1);
    }
}
