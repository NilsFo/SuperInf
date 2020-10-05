using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    public Collider2D projectorCollider;
    private Collider2D mycollider2D;
    public int direction;
    private bool firstframe;
    // Start is called before the first frame update
    void Start()
    {
        mycollider2D = GetComponent<Collider2D>();
        gameObject.layer = LayerMask.NameToLayer("ProjectionInvisible");
        var darttrap = GetComponent<DartTrapAI>();
        if(darttrap != null) {
            var dir = transform.up;
            if(dir.x > 0.1) { // right
                darttrap.direction=(DartTrapAI.Direction)((1+1-direction+(int)darttrap.direction)%4);
            } // 
            else if(dir.x < -0.1) {
                darttrap.direction=(DartTrapAI.Direction)((1+3-direction+(int)darttrap.direction)%4);
            }
            else if(dir.y < -0.1) {
                darttrap.direction=(DartTrapAI.Direction)((1+2-direction+(int)darttrap.direction)%4);
            }
            else if(dir.y > 0.1) {
                darttrap.direction=(DartTrapAI.Direction)((1+0-direction+(int)darttrap.direction)%4);
            }
        }
        firstframe = true;
        // gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        // Make sure it is facing upwards
        transform.rotation = Quaternion.identity;
        /*if(firstframe && !projectorCollider.IsTouching(mycollider2D)) {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            firstframe = false;
        }*/
    }


    void OnTriggerEnter2D(Collider2D col){
        if(col == projectorCollider) {
            Debug.Log("Enable projection "+ this);
            gameObject.layer = LayerMask.NameToLayer("Projection");
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider == projectorCollider) {
            Debug.Log("Disable projection " + this);
            gameObject.layer = LayerMask.NameToLayer("ProjectionInvisible");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
