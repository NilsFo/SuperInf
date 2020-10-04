﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    public Collider2D projectorCollider;
    private Collider2D mycollider2D;
    // Start is called before the first frame update
    void Start()
    {
        mycollider2D = GetComponent<Collider2D>();
        gameObject.layer = LayerMask.NameToLayer("ProjectionInvisible");
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure it is facing upwards
        transform.rotation = Quaternion.identity;
    }


    void OnTriggerEnter2D(Collider2D col){
        if(col == projectorCollider) {
            //Debug.Log("Enable projection "+ this);
            gameObject.layer = LayerMask.NameToLayer("Projection");
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider == projectorCollider) {
            //Debug.Log("Disable projection " + this);
            gameObject.layer = LayerMask.NameToLayer("ProjectionInvisible");
        }
    }
}
