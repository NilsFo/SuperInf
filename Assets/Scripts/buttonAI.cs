using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static ButtonInteractable;

public class ButtonAI : MonoBehaviour

{
    public Weight requiredWight;
    public ButtonListener[] myListener;
    public float pressedStateTime = 1f;

    private SpriteRenderer spriteRenderer;
    private int standingCountCurrent = 0;
    private int standingCountLastKnown = 0;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject source = other.gameObject;
        ButtonInteractable bti = source.GetComponent<ButtonInteractable>();
        if (bti)
        {
            Weight sourceWeight = bti.myWeight;
            if (sourceWeight >= requiredWight)
            {
                standingCountCurrent++;
                if(standingCountCurrent == 1)
                {
                    pressButton();
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
                standingCountCurrent--;
            }
        }
    }

    private void pressButton()
    {
        for (int i = 0; i < myListener.Length; i++)
        {
            ButtonListener thisListerner = myListener[i];
            thisListerner.onButtonTrigger(gameObject);
        }
    }

}
