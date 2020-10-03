using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartProjectileAI : MonoBehaviour
{

    public Vector3 velocity = new Vector3(0, 0, 0);
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

    private void OnDartHit(GameObject target)
    {
        print("The dart projectile has connected with "+ target + "! Haha, die, Trash!");
        Destroy(this.gameObject);
    }

    private void LateUpdate()
    {
        deltaCounter += Time.deltaTime;
        if (deltaCounter >= lifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject source = other.gameObject;

        BrazierAI bzai = source.GetComponent<BrazierAI>();
        if (bzai && bzai.burning)
        {
            //TODO SET BURNING AT THIS POINT
            return;
        }

        DartTrapAI dtai = source.GetComponent<DartTrapAI>();
        if (!dtai)
        {
            OnDartHit(source);
        }
    }
}