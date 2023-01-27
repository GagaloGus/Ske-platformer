using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gumba_Move : MonoBehaviour
{
    int xMoveDirection;
    float sizeXRatio;
    Rigidbody2D rb;
    SpriteRenderer sprRend;
    BoxCollider2D boxCol2d;

    LayerMask groundLayerMask;

    Enemy_Stats stats;
    //ignorar los otros enemigos
    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //randomiza la direccion al que va
        do { xMoveDirection = Random.Range(-1, 2); } while (xMoveDirection == 0);

        rb = GetComponent<Rigidbody2D>();
        sprRend = GetComponent<SpriteRenderer>();
        boxCol2d = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        //mantiene la escala del bicho para q no se deforme
        sizeXRatio = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        stats = GetComponent<Enemy_Stats>();
        //se da la vuelta
        transform.localScale = new Vector2(xMoveDirection * -1 * sizeXRatio, transform.localScale.y);
        //emite un raycast hacia la direccion a la que mira
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0), (boxCol2d.size.x / 2 + 0.1f) * sizeXRatio, groundLayerMask.value);
        rb.velocity = new Vector2(xMoveDirection * stats.speed, rb.velocity.y);

        //si encuentra el suelo se da la vuelta
        if(hit.collider != null)
        {
            xMoveDirection *= -1;
        }
    }


}
