using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public enum WalkState
    {
        OffPath,
        OnPath
    }
    
    public PathCreator myPathCreator;
    public float speed = 2f;
    public Transform target;
    public List<GameObject> avoid = new List<GameObject>();

    private bool _forward = true;
    private float _distanceTraveled;
    private float _maxDistanceTraveled;
    private Vector3 _pointToWalk;

    private WalkState _currentWalkState;

    private Animator _anim;
    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _currentWalkState = WalkState.OffPath;
        if (myPathCreator)
        {
            _maxDistanceTraveled = myPathCreator.path.length;
        }
        else
        {
            _maxDistanceTraveled = 0;
        }
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var oldPosition = transform.position;
        if (_currentWalkState == WalkState.OffPath && _pointToWalk.magnitude != 0)
        {
            float maxDistance = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _pointToWalk, maxDistance);
        } 
        else if (_currentWalkState == WalkState.OnPath && myPathCreator)
        {
            if (!myPathCreator.path.isClosedLoop)
            {
                if (_distanceTraveled >= _maxDistanceTraveled)
                {
                    _forward = false;
                    _distanceTraveled = _maxDistanceTraveled;
                }
                else if(_distanceTraveled <= 0)
                {
                    _forward = true;
                    _distanceTraveled = 0;
                }
            }

            if (_forward)
            {
                _distanceTraveled += speed * Time.deltaTime;
            }
            else
            {
                _distanceTraveled -= speed * Time.deltaTime;
            }
            transform.position = myPathCreator.path.GetPointAtDistance(_distanceTraveled);
        }
        var deltaPos = transform.position - oldPosition;
        if(_anim != null) {
            if(deltaPos.x > deltaPos.y) {
                if(-deltaPos.x > deltaPos.y){
                    // Down
                    _anim.Play("cat_down");
                    _sprite.flipX = false;
                }
                else{
                    // Right
                    _anim.Play("cat_right");
                    _sprite.flipX = true;
                }
            } else {
                if(-deltaPos.x > deltaPos.y){
                    // Left
                    _anim.Play("cat_left");
                    _sprite.flipX = false;
                }
                else{
                    // Up
                    _anim.Play("cat_up");
                    _sprite.flipX = false;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 myPosition = transform.position;
        if (target || avoid.Count > 0)
        {
            _currentWalkState = WalkState.OffPath;
            if (avoid.Count > 0)
            {
                Vector2 myNewDirection = Vector2.zero;
                Vector2 myVector = new Vector2(myPosition.x, myPosition.y);;
                for (int i = 0; i < avoid.Count; i++)
                {
                    Vector2 direction = myVector - new Vector2(avoid[i].transform.position.x, avoid[i].transform.position.y);
                    myNewDirection += direction;
                }
                myNewDirection = (myNewDirection / avoid.Count);
                _pointToWalk = new Vector3(myPosition.x + myNewDirection.x, myPosition.y + myNewDirection.y, myPosition.z);
            }
            else
            {
                _pointToWalk = new Vector3(target.position.x, target.position.y, myPosition.z);
            }
        }
        else
        {
            if (_currentWalkState == WalkState.OffPath && myPathCreator)
            {
                _distanceTraveled = myPathCreator.path.GetClosestDistanceAlongPath(transform.position);
                _pointToWalk = myPathCreator.path.GetPointAtDistance(_distanceTraveled);
                _pointToWalk.z = myPosition.z;
                float currentDistance = Vector3.Distance(myPosition, _pointToWalk);
                if (currentDistance < 0.2f) //Done
                {
                    _currentWalkState = WalkState.OnPath;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Feared myFeared = other.gameObject.GetComponent<Feared>();
        if (myFeared)
        {
            for (int i = 0; i < myFeared.byName.Length; i++)
            {
                if (myFeared.byName[i] == gameObject.name)
                {
                    avoid.Add(other.gameObject);
                    i = myFeared.byName.Length;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        avoid.Remove(other.gameObject);
    }
}
