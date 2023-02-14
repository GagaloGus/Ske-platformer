using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isCinematic = false;

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
        //como el juego empieza en el menu, que reproduzca la cancion del menu
        AudioManager.instance.PlayMusic(SceneManager.GetActiveScene().name + " Theme");
    }

    public void ChangeScene (string name) //cambio de escena
    {
        isCinematic = false;
        time = 0; score = 0;
        SceneManager.LoadScene(name);

        //para todas las canciones
        AudioManager.instance.musicSource.Stop();
        //reproduce el tema del nivel
        AudioManager.instance.PlayMusic(name + " Theme");
        //desmutea los efectos de sonido
        AudioManager.instance.sfxSource.mute = false;
    }
    public int gm_score
    {
        get { return score; }
        //suma los valores directamente al score
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
