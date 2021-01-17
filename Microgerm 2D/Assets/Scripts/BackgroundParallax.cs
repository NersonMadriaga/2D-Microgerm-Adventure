using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
     private float length;
     private float startPos;
     private GameObject cam;
    [SerializeField] private float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));

        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        startPos = temp > startPos + length ? startPos += length : temp < startPos - length ? startPos -= length : startPos;
    }
    
}
