using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioClip sound;

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

    private void Start()
    {
        AudioManager.instance.PlayMusic(SceneManager.GetActiveScene().name + " Theme");
    }

    public void ChangeScene (string name)
    {
        time = 0;
        SceneManager.LoadScene(name);
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic(name + " Theme");
        print(name + " Theme");
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
