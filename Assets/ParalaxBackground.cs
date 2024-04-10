using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    private float lenght, startPosition;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * (parallaxEffect));
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if(temp > startPosition + lenght)
        {
            startPosition += lenght;
        }else if(temp < startPosition - lenght)
        {
            startPosition -= lenght;
        }
    }
}
