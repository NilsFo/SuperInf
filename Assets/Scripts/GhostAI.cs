using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostAI : MonoBehaviour
{
    public float killDistance = 10f;
    public UnityEvent onKillEvent;

    private GameObject _player;
    
    
    private void FixedUpdate()
    {
        if (_player)
        {
            Vector3 otherPos3d = _player.transform.position;
            Vector3 myPos3d = transform.position;
            Vector2 otherPos = new Vector2(otherPos3d.x, otherPos3d.y);
            Vector2 myPos = new Vector2(myPos3d.x, myPos3d.y);
            float currentDistance = (myPos - otherPos).magnitude;

            if (currentDistance <= killDistance)
            {
                onKillEvent.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player = null;
        }
    }
}
