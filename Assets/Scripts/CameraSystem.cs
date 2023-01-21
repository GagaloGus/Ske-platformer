using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public GameObject player;
    public float XMin, XMax,YMin, YMax;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, XMin, XMax);
        float y = Mathf.Clamp(player.transform.position.y, YMin, YMax);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
