using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score = 0;
    private float time = 0;
    // Start is called before the first frame update
    void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }
    }

    public int gm_score
    {
        get { return score; }
        set { score += value; }
    }
    public float gm_time
    {
        get { return time; }
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
}
