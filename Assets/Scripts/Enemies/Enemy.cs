using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float currentSpeed;
    protected float baseSpeed { get; private set; }
    protected bool able_to_move = true;
    protected int directionLooking = -1;
    protected GameObject nearestPlayer;

    private float detectPlayerRange;
    [HideInInspector]
    public int killScore;

    protected LayerMask groundLayerMask;
    SpriteRenderer sprRend;
    private void Start()
    {
        FindNearestPlayer();
        groundLayerMask = LayerMask.GetMask("Ground");
        sprRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D playerVisible = Physics2D.Raycast(transform.position, nearestPlayer.transform.position - transform.position, detectPlayerRange);

        if (playerVisible.collider.GetComponent<Player_Controller>())
        {
            Attack();
        }
        else
        {
            Idle();
        }

        if (directionLooking == 1) { sprRend.flipX = true; }
        else { sprRend.flipX = false; }
    }

    void OnEnable()
    {
        Enemy[] otherObjects = FindObjectsOfType<Enemy>();
        foreach (Enemy obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public virtual void Attack() { }
    public virtual void Idle() { }

    public void FindNearestPlayer()
    {
        nearestPlayer = FindObjectOfType<Player_Controller>().gameObject;
    }

    public void SetStats(float baseSpeed, float detectPlayerRange, int killPoints)
    {
        this.baseSpeed = baseSpeed;
        currentSpeed = baseSpeed;

        this.detectPlayerRange = detectPlayerRange;
        killScore = killPoints;
    }


}
