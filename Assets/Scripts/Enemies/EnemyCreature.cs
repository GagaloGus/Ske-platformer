using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCreature : Enemy
{
    GameObject excl;

    [HideInInspector]
    public int killScore;


    protected override void Start()
    {
        base.Start();
        excl = new GameObject("exclamation");
        excl.transform.parent = gameObject.transform;
        SpriteRenderer rendExcl = excl.AddComponent<SpriteRenderer>();
        rendExcl.sprite = Resources.Load<Sprite>("Sprites/exclamationEnemy");
        rendExcl.sortingLayerName = "1";
        excl.transform.position = transform.position + Vector3.up * (GetComponent<SpriteRenderer>().bounds.size.y / 2 + 1);
    }

    protected override void Update()
    {
        base.Update();
        RaycastHit2D playerNotVisible = Physics2D.Raycast(transform.position, nearestPlayer.transform.position - transform.position, detectPlayerRange, groundLayerMask);

        if (!playerNotVisible && Vector2.Distance(nearestPlayer.transform.position, transform.position) <= detectPlayerRange)
        {
            Attack();
        }
        else
        {
            Idle();
        }
    }

    void OnEnable()
    {
        Enemy[] otherObjects = FindObjectsOfType<Enemy>();
        foreach (Enemy obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public virtual void Attack() 
    {
        excl.SetActive(true);

    }
    public virtual void Idle() 
    {
        excl.SetActive(false);
    }


    public void SetStats(float baseSpeed, float detectPlayerRange, int killPoints)
    {
        this.baseSpeed = baseSpeed;
        currentSpeed = baseSpeed;

        base.detectPlayerRange = detectPlayerRange;
        killScore = killPoints;
    }

    public void WallChangeDirection(LayerMask ground, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(directionLooking, 0), Mathf.Infinity, ground.value);

        //si encuentra el suelo se da la vuelta
        if (hit.distance < distance)
        {
            directionLooking *= -1;
        }
    }
}
