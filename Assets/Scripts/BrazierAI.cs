using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazierAI : ButtonListener
{
    public bool burning = true;
    public float lightRadius = 15f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
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
            animator.Play("BrazierAnim");
            GetComponentInChildren<Light>().enabled = true;
        }
        else
        {
            animator.Play("BrazierIdle");
            GetComponentInChildren<Light>().enabled = false;
        }
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
    }

    public override void OnButtonTriggerExit(GameObject source)
    {
    }
}
