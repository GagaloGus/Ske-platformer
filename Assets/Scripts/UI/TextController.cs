using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    TMP_Text timeText, scoreText;
    // Start is called before the first frame update
    void Start()
    {
        timeText = transform.Find("tiempo").gameObject.GetComponent<TMP_Text>();
        scoreText = transform.Find("score").gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateScore();
    }
    
    void UpdateText()
    {
        //si el personaje no esta en una cinematica o muerto el tiempo fluye
        if (!GameManager.instance.isCinematic && !GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().has_died)
            timeText.text = "Time: " + string.Format("{0:0.##}", GameManager.instance.gm_time);
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.instance.gm_score;
    }
}
