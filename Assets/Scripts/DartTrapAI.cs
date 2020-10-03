using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrapAI : ButtonListener
{

    public bool active = true;
    public float projectileCooldown = 1.5f;
    public float projectileVelocity = 4f;
    public GameObject projectilePrefab;
    public Vector2 offset;
    public float projectileSpeed = .1f;

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

    public override void onButtonTrigger(buttonAI source)
    {
        active = !active;
        deltaCounter = 0;
    }

    public void SpawnProjectile()
    {
        //print("Fire!!");
        deltaCounter = 0;

        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;
        Vector3 newPos = new Vector3(pos.x, pos.y, pos.z);

        Vector3 velocity = new Vector3(projectileSpeed,0f,0f);
        velocity = rot * velocity;

        GameObject projectile = Instantiate(projectilePrefab,newPos, Quaternion.identity);
        projectile.transform.rotation = rot;

        DartProjectileAI dpi = projectile.GetComponent<DartProjectileAI>();
        if (dpi)
        {
            dpi.velocity = velocity;
        }
    }
}
