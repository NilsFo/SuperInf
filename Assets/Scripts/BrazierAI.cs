using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazierAI : ButtonListener
{

    public Sprite burningSprite;
    public Sprite offSprite;
    public bool burning = true;
    public float lightRadius = 15f;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        updateState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnButtonTrigger(GameObject source)
    {
        burning = !burning;
        updateState();
    }

    private void updateState()
    {
        if (burning)
        {
            spriteRenderer.sprite = burningSprite;
        }
        else
        {
            spriteRenderer.sprite = offSprite;
        }
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
    }

    public override void OnButtonTriggerExit(GameObject source)
    {
    }
}
