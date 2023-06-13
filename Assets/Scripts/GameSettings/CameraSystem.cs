using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    GameObject player, casetafuera;
    public float XMin, XMax,YMin, YMax;
    public Vector2 endPosition;
    public bool levelEnded, followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        levelEnded = false; followPlayer = true ;
        //encuenta al jugador
        player = FindObjectOfType<Player_Controller>().gameObject;
        casetafuera = FindObjectOfType<EndConfig>().gameObject;
        endPosition = casetafuera.transform.position + new Vector3(-4.9f, 0, -127);
    }

    // Update is called once per frame
    void Update()
    {

        //restringe el seguir al jugador entre unos valores que ponemos en el inspector
        float x = Mathf.Clamp(player.transform.position.x, XMin, XMax);
        float y = Mathf.Clamp(player.transform.position.y, YMin, YMax);
        //si el nivel acaba, la camara se pone en la posicion indicada en el inspector para el cutscene
        if (!levelEnded && followPlayer) { transform.position = new Vector3(x, y, transform.position.z); } 
        else if (levelEnded) { transform.position = new Vector3(endPosition.x, endPosition.y, transform.position.z); }
        else { }
        
    }


}
