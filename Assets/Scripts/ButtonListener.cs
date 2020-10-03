using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonListener : MonoBehaviour
{
    public abstract void OnButtonTrigger(GameObject source);
    
    public abstract void OnButtonTriggerEnter(GameObject source);
    
    public abstract void OnButtonTriggerExit(GameObject source);
}
