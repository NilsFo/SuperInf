using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0, y = 0;
        if(Input.GetKey(KeyCode.W))
            y += speed*Time.deltaTime;
        if(Input.GetKey(KeyCode.S))
            y -= speed*Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
            x -= speed*Time.deltaTime;
        if(Input.GetKey(KeyCode.D))
            x += speed*Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);
    }
}
