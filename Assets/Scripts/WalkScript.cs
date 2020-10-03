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
    
    private float _distanceTraveled;
    private Vector3 _pointToWalk;

    private WalkState _currentWalkState;

    // Start is called before the first frame update
    void Start()
    {
        _currentWalkState = WalkState.OffPath;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentWalkState == WalkState.OffPath)
        {
            float maxDistance = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _pointToWalk, maxDistance);
        } 
        else if (_currentWalkState == WalkState.OnPath)
        {
            _distanceTraveled += speed * Time.deltaTime;
            transform.position = myPathCreator.path.GetPointAtDistance(_distanceTraveled);
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            _currentWalkState = WalkState.OffPath;
            _pointToWalk = target.position;
        }
        else
        {
            if (_currentWalkState == WalkState.OffPath)
            {
                _distanceTraveled = myPathCreator.path.GetClosestDistanceAlongPath(transform.position);
                _pointToWalk = myPathCreator.path.GetPointAtDistance(_distanceTraveled);
                float currentDistance = Vector3.Distance(transform.position, _pointToWalk);
                if (currentDistance < 0.2f) //Done
                {
                    _currentWalkState = WalkState.OnPath;
                }
            }
        }
    }
}
