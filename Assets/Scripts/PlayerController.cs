using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public Recorder cameraInHand;
    public Animator anim;
    Camera cam;
    Vector3 camPos = new Vector3(0, 0, -10);
    float vertical = 0f;
    float horizontal = 0f;
    [SerializeField] float viewField = 8f;
    bool moving = false;
    public Vector2 movingDirection = Vector2.zero;
    public Projector projectorPrefab;
    private int _lookDirection;
    public int LookDirection { 
        get{return _lookDirection;}
        set{_lookDirection = value; 
        UpdateDirection();}
    }

    //methoden für animationen
    #region methods for animations
    void SetFalse()
    {
        StandStill();
        vertical = 0f;
        horizontal = 0f;
        movingDirection = Vector2.zero;
    }

    void SetIdlesFalse()
    {
        anim.SetBool("idle", false);
        anim.SetBool("idleLeft", false);
        anim.SetBool("idleRight", false);
        anim.SetBool("idleUp", false);
    }

    public void StandStill()
    {
        moving = false;
        if (vertical > 0)
        {
            anim.SetBool("idleUp", true);
            vertical = 0f;
        }
        else if (vertical < 0)
        {
            anim.SetBool("idle", true);
            vertical = 0f;
        }

        if (horizontal > 0)
        {
            anim.SetBool("idleRight", true);
            horizontal = 0f;
        }

        else if (horizontal < 0)
        {
            anim.SetBool("idleLeft", true);
            horizontal = 0f;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("idle", true);
        cam = Camera.main;
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

        //determine if moving and direction
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            moving = true;
        else
        {
            StandStill();
        }

        // Find out look direction
        if ((cameraInHand?.recordingstatus ?? Recorder.Recordingstatus.NO_RECORDING) == Recorder.Recordingstatus.NO_RECORDING) {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = transform.worldToLocalMatrix.MultiplyPoint(mousePos);
            if(mousePos.x > mousePos.y) {
                if(-mousePos.x > mousePos.y)
                    LookDirection = 2;
                else
                    LookDirection = 1;
            } else {
                if(-mousePos.x > mousePos.y)
                    LookDirection = 3;
                else
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
        switch (_lookDirection)
        {
            case 0: // Up
                SetIdlesFalse();
                anim.SetFloat("walkVertical", vertical);
                anim.SetFloat("walkHorizontal", 0f);
                break;
            case 1: // Right
                SetIdlesFalse();
                anim.SetFloat("walkHorizontal", horizontal);
                anim.SetFloat("walkVertical", 0f);
                break;
            case 2: // Down
                SetIdlesFalse();
                anim.SetFloat("walkVertical", vertical);
                anim.SetFloat("walkHorizontal", 0f);
                break;
            default: // Left
                SetIdlesFalse();
                anim.SetFloat("walkHorizontal", horizontal);
                anim.SetFloat("walkVertical", 0f);
                break;
        }
        if (horizontal == 0 && vertical == 0)
        {
            anim.SetFloat("walkHorizontal", 0f);
            anim.SetFloat("walkVertical", 0f);
        }
        movingDirection = new Vector2(horizontal, vertical);

        // set camera direction
        switch (_lookDirection) {
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
        SetFalse();
    }

    private void LateUpdate()
    {
        cam.transform.position = transform.position + camPos;
        cam.orthographicSize = viewField;
    }
}
