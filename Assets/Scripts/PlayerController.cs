using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public Recorder cameraInHand;
    public Projector projectorPrefab;
    public Rigidbody2D rigidbody2D;

    private float _horizontalSpeed = 0;
    private float _verticalSpeed = 0;
        

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalSpeed = 0;
        _verticalSpeed = 0;
        if(Input.GetKey(KeyCode.W))
            _verticalSpeed = speed;
        if(Input.GetKey(KeyCode.S))
            _verticalSpeed = -speed;
        if(Input.GetKey(KeyCode.A))
            _horizontalSpeed = -speed;
        if(Input.GetKey(KeyCode.D))
            _horizontalSpeed = speed;
        

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
                    var p = Instantiate<Projector>(projectorPrefab, cameraInHand.transform.position, this.transform.rotation);
                    p.StartProjection(cameraInHand.GetLastRecording());
                    cameraInHand.gameObject.SetActive(false);
                }
            }
        }
    }
}
