using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sebas: Enemy
{
    Rigidbody2D rb;
    BoxCollider2D boxCol2d;

    // Start is called before the first frame update
    void Start()
    {
        //randomiza la direccion al que va
        do { directionLooking = Random.Range(-1, 2); } while (directionLooking == 0);

        rb = GetComponent<Rigidbody2D>();
        boxCol2d = GetComponent<BoxCollider2D>();

        SetStats(4, 6, 50);
    }

    public override void Idle()
    {
        currentSpeed = baseSpeed;
        //emite un raycast hacia la direccion a la que mira
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(directionLooking, 0), boxCol2d.size.x / 2 + 0.1f, groundLayerMask.value);
        rb.velocity = new Vector2(directionLooking * currentSpeed, rb.velocity.y);

        //si encuentra el suelo se da la vuelta
        if (hit.collider != null)
        {
            directionLooking *= -1;
        }
    }
    public override void Attack()
    {
        currentSpeed *= 1.5f;
    }


}
