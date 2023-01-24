using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndConfig : MonoBehaviour
{
    public GameObject player, lightEnd;
    // Start is called before the first frame update
    void Start()
    {
        //encuentra al jugador y al totem del final
        player = GameObject.FindGameObjectWithTag("Player");
        lightEnd = GameObject.FindGameObjectWithTag("end light");
        lightEnd.GetComponent<Animator>().SetBool("active", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void skeDance()
    {
        //que el jugador baile
        player.GetComponent<Animator>().SetBool("endDance", true);
    }

    public void lightUp()
    {
        //se ilumine el totem
        lightEnd.GetComponent<Animator>().SetBool("active", true);
    }
}
