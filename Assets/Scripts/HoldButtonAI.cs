using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ButtonInteractable;

public class HoldButtonAI : MonoBehaviour
{

    public Sprite offSprite;
    public Sprite pressedSprite;
    public Weight requiredWight;
    public ButtonListener[] myListener;
    public float pressedTime = 1.0f;

    private int standingCount = 0;
    private float pressedAnimationCountDown = 0f;
    private bool currentlyPressed = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    { 
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        GameObject source = other.gameObject;
        ButtonInteractable bti = source.GetComponent<ButtonInteractable>();
        if (bti)
        {
            Weight sourceWeight = bti.myWeight;
            if (sourceWeight >= requiredWight)
            {
                standingCount++;
                if (standingCount == 1)
                {
                    NotifyTriggersOn();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject source = other.gameObject;
        ButtonInteractable bti = source.GetComponent<ButtonInteractable>();
        if (bti)
        {
            Weight sourceWeight = bti.myWeight;
            if (sourceWeight >= requiredWight)
            {
                standingCount--;
                if (standingCount == 0)
                {
                    NotifyTriggersOff();
                }
            }
        }
    }

    private void NotifyTriggersOn()
    {
        pressedAnimationCountDown = pressedTime;
        currentlyPressed = true;
        spriteRenderer.sprite = pressedSprite;

        for (int i = 0; i < myListener.Length; i++)
        {
            ButtonListener thisListerner = myListener[i];
            thisListerner.OnButtonTrigger(gameObject);
            thisListerner.OnButtonTriggerEnter(gameObject);
        }
    }

    private void NotifyTriggersOff()
    {
        currentlyPressed = false;
        spriteRenderer.sprite = offSprite;

        for (int i = 0; i < myListener.Length; i++)
        {
            ButtonListener thisListerner = myListener[i];
            thisListerner.OnButtonTrigger(gameObject);
            thisListerner.OnButtonTriggerEnter(gameObject);
        }
    }

    public bool isPressed()
    {
        return currentlyPressed;
    }
}


