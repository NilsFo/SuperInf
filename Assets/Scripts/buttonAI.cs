using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static ButtonInteractable;

public class buttonAI : MonoBehaviour

{
    public weight requiredWight;
    public ButtonListener myListener;

    // Start is called before the first frame update
    void Start()
    {
        print("I am a button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject source = other.gameObject;
        ButtonInteractable bti = source.GetComponent<ButtonInteractable>();
        print("This button triggered by: " + source.GetType());
        if (bti)
        {
            print("I have a correct button");
            weight sourceWeight = bti.myWeight;
            if (sourceWeight == requiredWight)
            {
                print("Weight requirement done. Notifying Target.");
                myListener.onButtonTrigger(this);
            }
        }
    }

}
