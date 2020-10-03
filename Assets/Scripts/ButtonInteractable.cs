using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour
{
    public enum Weight
    {
        Light = 0, 
        Medium = 1, 
        Heavy = 2
    };

    public Weight myWeight;
}
