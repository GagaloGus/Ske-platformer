using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int points;
    public string pickupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>())
        {
            GameManager.instance.gm_score += points;
            AudioManager.instance.PlaySFX(pickupSFX);
            Destroy(gameObject);
        }
    }
}
