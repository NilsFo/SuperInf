﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static ButtonInteractable;

public class ButtonAI : MonoBehaviour

{
    public Weight requiredWight;
    public ButtonListener[] myListener;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject source = other.gameObject;
        ButtonInteractable bti = source.GetComponent<ButtonInteractable>();
        if (bti)
        {
            Weight sourceWeight = bti.myWeight;
            if (sourceWeight >= requiredWight)
            {
                for (int i = 0; i < myListener.Length; i++)
                {
                    ButtonListener thisListerner = myListener[i];
                    thisListerner.onButtonTrigger(this);
                }
                
            }
        }
    }

}