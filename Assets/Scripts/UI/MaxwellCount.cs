using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxwellCount : MonoBehaviour
{
    GameObject player;
    public Sprite[] maxwellSprites;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int maxwellCount = Mathf.Clamp(player.GetComponent<Player_Controller>().maxwell_count, 0, 3);
        GetComponent<Image>().sprite = maxwellSprites[maxwellCount];
    }
}
