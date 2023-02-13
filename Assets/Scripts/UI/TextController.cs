using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text timeText, scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
        UpdateScore();
    }
    
    void UpdateText()
    {
        if (!GameManager.instance.isCinematic && !GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>().has_died)
            timeText.text = "Time: " + Mathf.Round(GameManager.instance.gm_time * 100) * 0.01f;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.instance.gm_score;
    }
}
