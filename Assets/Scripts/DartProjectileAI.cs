using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartProjectileAI : MonoBehaviour
{

    public Vector3 velocity = new Vector3(0,0,0);
    private float lifespan = 10;
    private float deltaCounter;

    // Start is called before the first frame update
    void Start()
    {
        print("Ruun! This Dart projectile is coming for you!!");
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        float xv = velocity.x;
        float yv = velocity.y;
        float zv = velocity.z;
        transform.position = new Vector3(x + xv, y + yv, z + zv);
    }

    private void LateUpdate()
    { 
        deltaCounter += Time.deltaTime;
        if (deltaCounter >= lifespan)
        {
            Destroy(this.gameObject);
        }
    }
}
