using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public GameObject player;
    public float XMin, XMax,YMin, YMax;
    public Vector2 endPosition;
    public bool levelEnded;

    // Start is called before the first frame update
    void Start()
    {
        levelEnded = false;
        //encuenta al jugador
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //restringe el seguir al jugador entre unos valores que ponemos en el inspector
        float x = Mathf.Clamp(player.transform.position.x, XMin, XMax);
        float y = Mathf.Clamp(player.transform.position.y, YMin, YMax);
        //si el nivel acaba, la camara se pone en la posicion indicada en el inspector para el cutscene
        if (!levelEnded) { transform.position = new Vector3(x, y, transform.position.z); } 
        else { transform.position = new Vector3(endPosition.x, y, transform.position.z); }
        
    }


}
