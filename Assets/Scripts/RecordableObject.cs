using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordableObject : MonoBehaviour
{
    public Sprite standardSprite;
    public Sprite outlineSprite;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Projection") || gameObject.layer == LayerMask.NameToLayer("ProjectionInvisible"))
            GetComponent<SpriteRenderer>().sprite = standardSprite;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableOutline() {
        if(gameObject.layer != LayerMask.NameToLayer("Projection") && gameObject.layer != LayerMask.NameToLayer("ProjectionInvisible"))
            GetComponent<SpriteRenderer>().sprite = outlineSprite;
    }
    
    public void DisableOutline() {
        if(gameObject.layer != LayerMask.NameToLayer("Projection") && gameObject.layer != LayerMask.NameToLayer("ProjectionInvisible"))
            GetComponent<SpriteRenderer>().sprite = standardSprite;
    }
}
