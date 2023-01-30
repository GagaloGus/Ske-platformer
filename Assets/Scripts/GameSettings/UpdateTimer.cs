using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTimer : MonoBehaviour
{
    public TMPro.TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        timeText.text = "Tiempo" + Mathf.Round(GameManager.instance.gm_time * 10) * 0.1f;
    }
}
