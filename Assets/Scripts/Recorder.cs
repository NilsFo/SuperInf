﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Transform objToRecord;
    public Projector projector;
    public Collider2D recordingArea;
    private float recordingStartTime;
    private enum Recordingstatus: ushort {
        NO_RECORDING,
        START_RECORDING,
        RECORDING_ACTIVE,
        STOP_RECORDING
    }
    private Recordingstatus recordingstatus = Recordingstatus.NO_RECORDING;
    Recording lastRecording;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recordingstatus == Recordingstatus.NO_RECORDING && Input.GetKeyDown(KeyCode.Space)) {
            recordingstatus = Recordingstatus.START_RECORDING;
        }
        if(recordingstatus == Recordingstatus.RECORDING_ACTIVE && Input.GetKeyDown(KeyCode.Space)) {
            recordingstatus = Recordingstatus.STOP_RECORDING;
            foreach(var v in lastRecording.frames.Values)
                Debug.Log(v);
            
        }
        if(Input.GetKeyDown(KeyCode.X)) {
            projector.StartRecording(lastRecording);
        }
    }

    void FixedUpdate() 
    {
        if(recordingstatus == Recordingstatus.START_RECORDING) {
            Debug.Log("Recording Start");
            recordingstatus = Recordingstatus.RECORDING_ACTIVE;
            recordingStartTime = Time.time;
            var objrec = getObjectsToRecord();
            if(objrec == null) {
                Debug.Log("Nothing to record");
                recordingstatus = Recordingstatus.NO_RECORDING;
                return;
            }
            lastRecording = new Recording(getObjectsToRecord());
            lastRecording.recordFrame(0, this.transform);
        }
        else if(recordingstatus == Recordingstatus.RECORDING_ACTIVE) {
            lastRecording.recordFrame(Time.time - recordingStartTime, this.transform);
        }
        else if(recordingstatus == Recordingstatus.STOP_RECORDING) {
            var t = Time.time - recordingStartTime;
            lastRecording.recordFrame(t, this.transform);
            lastRecording.FinishRecording(t);
            recordingstatus = Recordingstatus.NO_RECORDING;
        }
    }

    public List<GameObject> getObjectsToRecord() {
        var c = new ContactFilter2D();
        c.layerMask = LayerMask.NameToLayer("Default");
        

        List<Collider2D> results = new List<Collider2D>();
        int num = Physics2D.OverlapCollider(recordingArea, c, results);
        if(num == 0) {
            return null;
        }
        List<GameObject> objs = new List<GameObject>();
        foreach(var r in results) {
            if(r.gameObject.tag.Contains("Recordable"))
                objs.Add(r.gameObject);
        }
        if(objs.Count == 0)
            return null;
        return objs;
    }
}