using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    public Vector3 movementSpeed;
    public int detectionRadius;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start(){
        startPos = transform.position;
        print("Henlo. Am a Monster! I am here: " +startPos);
    }

    // Update is called once per frame
    void Update(){
        float dt = Time.deltaTime;
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3((float)(x + 0.01), y, z);
    }
}
