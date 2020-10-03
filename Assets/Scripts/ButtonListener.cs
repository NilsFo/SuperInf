using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonListener : MonoBehaviour
{

    public abstract void onButtonTrigger(buttonAI source);
}
