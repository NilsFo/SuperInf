using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public Recorder cameraInHand;
    public Projector projectorPrefab;
    private int _lookDirection;
    public int LookDirection { 
        get{return _lookDirection;}
        set{_lookDirection = value; 
        UpdateDirection();}
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public new Rigidbody2D rigidbody2D;

    private Vector2 velocity = new Vector2();
        

    private void FixedUpdate()
    {
        rigidbody2D.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0;
        float y = 0;
        if(Input.GetKey(KeyCode.W))
            y += 1;
        if(Input.GetKey(KeyCode.S))
            y -= 1;
        if(Input.GetKey(KeyCode.A))
            x -= 1;
        if(Input.GetKey(KeyCode.D))
            x += 1;
        velocity = new Vector2(x, y).normalized * speed;

        // Find out look direction
        if((cameraInHand?.recordingstatus ?? Recorder.Recordingstatus.NO_RECORDING) == Recorder.Recordingstatus.NO_RECORDING) {
            if(x > 0) {
                LookDirection = 1;
            }
            else if(x < 0) {
                LookDirection = 3;
            }
            else if(y < 0) {
                LookDirection = 2;
            }
            else if(y > 0) {
                LookDirection = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            if(cameraInHand) {
                if(!cameraInHand.gameObject.activeInHierarchy) {
                    // Try to pick up
                    List<Collider2D> colliders = new List<Collider2D>();
                    GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
                    var projector = colliders.Find(c => c.GetComponentInParent<Projector>() != null);
                    if(projector) {
                        Destroy(projector.transform.parent.gameObject);
                        cameraInHand.gameObject.SetActive(true);
                    }
                } else {
                    // Put it down and start
                    var p = Instantiate<Projector>(projectorPrefab, cameraInHand.transform.position, cameraInHand.transform.rotation);
                    p.StartProjection(cameraInHand.GetLastRecording());
                    cameraInHand.gameObject.SetActive(false);
                }
            }
        }
    }
    
    private void UpdateDirection()
    {
        // TODO: Update Sprite

        // set camera direction
        switch(_lookDirection) {
            case 0: // Up
                if(cameraInHand != null)
                    cameraInHand.transform.localPosition = new Vector3(0,0.326f,-0.017f);
                    cameraInHand.transform.rotation = new Quaternion(0,0,0.707106829f,0.707106829f);
                break;
            case 1: // Right
                if(cameraInHand != null)
                    cameraInHand.transform.localPosition = new Vector3(0.27f,-0.02f,-0.017f);
                    cameraInHand.transform.rotation = new Quaternion(0,0,0,1);
                break;
            case 2: // Down
                if(cameraInHand != null)
                    cameraInHand.transform.localPosition = new Vector3(0,-0.171f,-0.017f);
                    cameraInHand.transform.rotation = new Quaternion(0,0,-0.707106829f,0.707106829f);
                break;
            default: // Left
                if(cameraInHand != null)
                    cameraInHand.transform.localPosition = new Vector3(-0.27f,-0.02f,-0.017f);
                    cameraInHand.transform.rotation = new Quaternion(0,0,1,0);
                break;
        }
    }
}
