using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using static Sharp;

public class DartProjectileAI : MonoBehaviour
{

    public Vector3 velocity = new Vector3(0, 0, 0);
    private new Rigidbody2D rigidbody2D;
    public GameObject mySpawner;
    public BoxCollider2D horizontalColider;
    public BoxCollider2D verticalColider;

    private float lifespan = 10;
    private float deltaCounter;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //print("Ruun! This Dart projectile is coming for you!!");
        anim = GetComponent<Animator>();
        anim.SetBool("burning", false);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rigidbody2D.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        /*float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        float xv = velocity.x;
        float yv = velocity.y;
        float zv = velocity.z;
        transform.position = new Vector3(x + xv, y + yv, z + zv);*/
    }

    private void OnDartHit(GameObject target)
    {
        //print("The dart projectile has connected with "+ target + "! Haha, die, Trash!");
        Destroy(this.gameObject);

        //Collision with Player:
        PlayerController pc = target.GetComponent<PlayerController>();
        if (pc)
        {
            pc.OnDartHit();
        }

        //Collission with Camera:
        Recorder rc = target.GetComponent<Recorder>();
        if (rc)
        {
            rc.OnDartHit();
        }
    }

    private void LateUpdate()
    {
        deltaCounter += Time.deltaTime;
        if (deltaCounter >= lifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject source = other.gameObject;
        //print("The dart projectile has connected with "+ source + "!");

        // Check if colliding with a Brazier
        BrazierAI bzai = source.GetComponent<BrazierAI>();
        if (bzai && bzai.burning)
        {
            Sharp sh = GetComponent<Sharp>();
            sh.sharpness = Sharpness.Burning;
            anim.SetBool("burning", true);
            return;
        }

        // Check if colliding with the original spawning Dart Trap.
        if(source == mySpawner)
        {
            return;
        }

        // Check if colliding with another dart.
        DartTrapAI dtai = source.GetComponent<DartTrapAI>();
        if (dtai)
        {
            return;
        }


        // Check if colliding with pitfalls.
        PitfallAI pfai = source.GetComponent<PitfallAI>();
        if (pfai)
        {
            return;
        }

        // Collision with anything else. Let's hit.
        OnDartHit(source);
    }
}