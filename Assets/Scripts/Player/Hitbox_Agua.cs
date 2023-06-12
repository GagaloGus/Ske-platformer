using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Agua : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("water")) { AudioManager.instance.PlaySFX("Enter water"); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water")) { AudioManager.instance.PlaySFX("Leave water"); }  
    }

}
