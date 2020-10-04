using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallAI : ButtonListener
{
    private GameObject _playerObject;
    
    public bool isOpen = false;
    public bool needSignal = true;
    
    public Sprite openSprite;
    public Sprite closedSprite;
    public SpriteRenderer spriteRenderer;
    public Transform tpPos;
    
    private bool _renderState;

    private void OnEnable()
    {
        if (isOpen)
        {
            spriteRenderer.sprite = openSprite;
        } 
        else
        {
            spriteRenderer.sprite = closedSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_renderState != isOpen)
        {
            if (isOpen)
            {
                spriteRenderer.sprite = openSprite;
            } 
            else
            {
                spriteRenderer.sprite = closedSprite;
            }
            _renderState = isOpen;
        }
    }

    public override void OnButtonTrigger(GameObject source)
    {
    }

    public override void OnButtonTriggerEnter(GameObject source)
    {
        isOpen = !isOpen;
        if (_playerObject && isOpen)
        {
            _playerObject.transform.position = tpPos.position;
        }
    }

    public override void OnButtonTriggerExit(GameObject source)
    {
        if (needSignal)
        {
            isOpen = !isOpen;
        }
        if (_playerObject && isOpen)
        {
            _playerObject.transform.position = tpPos.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _playerObject = other.gameObject;
            if (isOpen)
            {
                _playerObject.transform.position = tpPos.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerObject = null;
    }
}
