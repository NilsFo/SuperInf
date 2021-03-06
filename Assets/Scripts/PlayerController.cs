﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public Recorder cameraInHand;
    private Animator anim;
    Camera cam;
    Vector3 camPos = new Vector3(0, 0, -10);
    public Projector projectorPrefab;
    public UnityEngine.Tilemaps.Tilemap floorTilemap;
    private int _lookDirection;
    public string mainMenu = "MainMenü";

    public int LookDirection { 
        get{return _lookDirection;}
        set{_lookDirection = value; 
        UpdateDirection();}
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        var newVelocity = new Vector2(x, y).normalized * speed;
        if(newVelocity.sqrMagnitude == 0 ^ velocity.sqrMagnitude > 0)
            UpdateAnimation();
        velocity = newVelocity;

        // Find out look direction
        if ((cameraInHand?.recordingstatus ?? Recorder.Recordingstatus.NO_RECORDING) == Recorder.Recordingstatus.NO_RECORDING) {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
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

        // Handling Input to pick up Camera
        if(Input.GetKeyDown(KeyCode.E)) {
            if(cameraInHand) {
                if(!cameraInHand.gameObject.activeInHierarchy) {
                    // Try to pick up
                    List<Collider2D> colliders = new List<Collider2D>();
                    GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
                    var projector = colliders.Find(c => c.GetComponentInParent<Projector>() != null);
                    if(projector) {
                        cameraInHand.SetLastRecording(projector.GetComponentInParent<Projector>().GetRecording());
                        Destroy(projector.transform.parent.gameObject);
                        cameraInHand.gameObject.SetActive(true);
                        cameraInHand.ShowLastRecordingStillframe();
                    }
                } else if(cameraInHand.GetComponent<Recorder>().GetLastRecording()?.Finished ?? true) {
                    if((!floorTilemap?.GetTile(floorTilemap.WorldToCell(this.transform.position))?.name.Equals("gitter")) ?? true) {
                        // Put it down and start
                        var p = Instantiate<Projector>(projectorPrefab, cameraInHand.transform.position, cameraInHand.transform.rotation);
                        p.StartProjection(cameraInHand.GetLastRecording());
                        cameraInHand.gameObject.SetActive(false);
                        cameraInHand.SetLastRecording(null);
                    }
                }
            }
        }

        // Handling Input to get back to Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }

        // Updating UI to inform about pickup
        LevelUIBehaviour behav = cameraInHand.levelUIManagerObj.GetComponent<LevelUIBehaviour>();
        behav.DisplayPickupInfo(true);
        List<Collider2D> colliders2 = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders2);
        var projector2 = colliders2.Find(c => c.GetComponentInParent<Projector>() != null);
        if (projector2)
        {
            behav.DisplayPickupInfo(true);
        }
        else
        {
            behav.DisplayPickupInfo(false);
        }
    }
    
    private void UpdateDirection()
    {
        UpdateAnimation();

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
    }

    private void UpdateAnimation()
    {
        bool hasCam = cameraInHand.gameObject.activeInHierarchy;
        switch (_lookDirection)
        {
            case 0: // Up
                if(velocity.sqrMagnitude == 0)
                    if(hasCam){anim.Play("PlayerCamUpIdle");} else { anim.Play("Player_Idle_Up");}
                else
                    if(hasCam){anim.Play("PlayerCamUp");} else { anim.Play("Player_WalkUp");}
                break;
            case 1: // Right
                if(velocity.sqrMagnitude == 0)
                    if(hasCam){anim.Play("PlayerCamRightIdle");} else { anim.Play("Player_Idle_Right");}
                else
                    if(hasCam){anim.Play("PlayerCamRight");} else { anim.Play("Player_WalkRight");}
                break;
            case 2: // Down
                if(velocity.sqrMagnitude == 0)
                    if(hasCam){anim.Play("PlayerCamDownIdle");} else { anim.Play("Player_Idle_Down");}
                else
                    if(hasCam){anim.Play("PlayerCamDown");} else { anim.Play("Player_WalkDown");}
                break;
            default: // Left
                if(velocity.sqrMagnitude == 0)
                    if(hasCam){anim.Play("PlayerCamLeftIdle");} else { anim.Play("Player_Idle_Left");}
                else
                    if(hasCam){anim.Play("PlayerCamLeft");} else { anim.Play("Player_WalkLeft");}
                break;
        }
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnDartHit()
    {
        PlayerDeath();
    }

}
