using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float currentSpeed, detectPlayerRange;
    public float baseSpeed;
    protected bool able_to_move = true;
    public int directionLooking = -1;
    protected GameObject nearestPlayer;

    protected LayerMask groundLayerMask;
    protected SpriteRenderer sprRend;
    protected virtual void Start()
    {
        FindNearestPlayer();
        currentSpeed = baseSpeed;
        sprRend = GetComponent<SpriteRenderer>();
        groundLayerMask = LayerMask.GetMask("Ground");

    }

    protected virtual void Update()
    {
        if (directionLooking == 1) { sprRend.flipX = true; }
        else { sprRend.flipX = false; }
    }

    public void FindNearestPlayer()
    {
        nearestPlayer = FindObjectOfType<Player_Controller>().gameObject;
    }

    public void SetSprite(SpriteRenderer sprRend)
    {
        this.sprRend = sprRend;
    }
}
