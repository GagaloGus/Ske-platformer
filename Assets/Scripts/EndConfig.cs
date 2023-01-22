using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndConfig : MonoBehaviour
{
    public GameObject player, lightEnd;
    // Start is called before the first frame update
    void Start()
    {
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
        player.GetComponent<Animator>().SetBool("endDance", true);
        GetComponent<Animator>().SetBool("endLevelTriggered", false);
    }

    public void lightUp()
    {
        lightEnd.GetComponent<Animator>().SetBool("active", true);
    }
}
