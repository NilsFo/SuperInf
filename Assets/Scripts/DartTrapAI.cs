using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrapAI : ButtonListener
{
    public enum Direction: UInt16
    {
        North,
        East,
        South,
        West
    } 

    public bool active = true;
    public float projectileCooldown = 1.5f;
    public float projectileVelocity = 4f;
    public GameObject projectilePrefab;
    public float projectileSpeed = .1f;
    public Direction direction = Direction.South;

    private float deltaCounter;

    // Start is called before the first frame update
    void Start()
    {
        deltaCounter = 0;
        print("Dart-Trap, reporting for duty.");
    }

    // Update is called once per frame
    void Update()
    {
        if (active && gameObject.layer != LayerMask.NameToLayer("ProjectionInvisible"))
        {
            //print("Getting ready to fire: "+ deltaCounter+"/"+ projectileCooldown);
            deltaCounter += Time.deltaTime;
            if (deltaCounter >= projectileCooldown)
            {
                SpawnProjectile();
            }
        }
    }

    public override void OnButtonTrigger(GameObject source)
    {
        active = !active;
        if (active)
        {
            SpawnProjectile();
        }
        deltaCounter = 0;
    }

    public void SpawnProjectile()
    {
        //print("Fire!!");
        deltaCounter = 0;

        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;
        Vector3 newPos = new Vector3(pos.x, pos.y, pos.z);

        //Roating based on sprite rotation
        Vector3 velocity = new Vector3(projectileSpeed,0f,0f);
        velocity = rot * velocity;

        //Checking for basic orientation
        Quaternion diretionQuat = Quaternion.Euler(0, 0, 0); ;
        switch (direction)
        {
            case Direction.North:
                diretionQuat = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.East:
                diretionQuat = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.South:
                diretionQuat = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.West:
                diretionQuat = Quaternion.Euler(0, 0, 180);
                break;
            default:
                new Exception("Unknown angle!");
                break;
        }
        velocity = diretionQuat * velocity;

        GameObject projectile = Instantiate(projectilePrefab,newPos, Quaternion.identity);
        projectile.transform.rotation = rot* diretionQuat;

        DartProjectileAI dpi = projectile.GetComponent<DartProjectileAI>();
        if (dpi)
        {
            dpi.velocity = velocity;
            dpi.mySpawner = this.gameObject;
        }
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
        
    }

    public override void OnButtonTriggerExit(GameObject source)
    {
        
    }
}
