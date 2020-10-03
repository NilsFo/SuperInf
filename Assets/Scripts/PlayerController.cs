using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public Recorder cameraInHand;
    public Projector projectorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0, y = 0;
        if(Input.GetKey(KeyCode.W))
            y += speed*Time.deltaTime;
        if(Input.GetKey(KeyCode.S))
            y -= speed*Time.deltaTime;
        if(Input.GetKey(KeyCode.A))
            x -= speed*Time.deltaTime;
        if(Input.GetKey(KeyCode.D))
            x += speed*Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);


        if(Input.GetKeyDown(KeyCode.E)) {
            if(cameraInHand != null) {
                if(!cameraInHand.gameObject.activeInHierarchy) {
                    // Try to pick up
                    List<Collider2D> colliders = new List<Collider2D>();
                    GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
                    var projector = colliders.Find(c => c.GetComponentInParent<Projector>() != null);
                    if(projector != null) {
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
