using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMover : MonoBehaviour
{
    public GameObject title;
    private float timer = 0.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = title.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float s = Mathf.Sin(timer) * .5f;

        float x = startPos.x;
        float y = startPos.y;
        float z = startPos.z;

        Vector3 newPos = new Vector3(x,y+s,z);
        title.transform.position = newPos;
    }
}
