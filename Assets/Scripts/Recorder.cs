using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Collider2D recordingArea;
    private float recordingStartTime;
    public enum Recordingstatus: ushort {
        NO_RECORDING,
        START_RECORDING,
        RECORDING_ACTIVE,
        STOP_RECORDING
    }
    public Recordingstatus recordingstatus = Recordingstatus.NO_RECORDING;
    Recording lastRecording = null;

    public Recording GetLastRecording() {
        return lastRecording;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recordingstatus == Recordingstatus.NO_RECORDING && Input.GetKeyDown(KeyCode.Mouse0)) {
            recordingstatus = Recordingstatus.START_RECORDING;
        }
        if(recordingstatus == Recordingstatus.RECORDING_ACTIVE && Input.GetKeyDown(KeyCode.Mouse0)) {
            recordingstatus = Recordingstatus.STOP_RECORDING;
        }
    }

    void FixedUpdate() 
    {
        if(recordingstatus == Recordingstatus.START_RECORDING) {
            StartRecording();
        }
        else if(recordingstatus == Recordingstatus.RECORDING_ACTIVE) {
            RecordFrame();
        }
        else if(recordingstatus == Recordingstatus.STOP_RECORDING) {
            StopRecording();
        }
    }

    private void StartRecording() {
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

        // Visual & Audio stuff
        GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,0.4f);
    }

    private void RecordFrame() {
        lastRecording.recordFrame(Time.time - recordingStartTime, this.transform);
    }

    private void StopRecording() {
        var t = Time.time - recordingStartTime;
        lastRecording.recordFrame(t, this.transform);
        lastRecording.FinishRecording(t);
        recordingstatus = Recordingstatus.NO_RECORDING;
        Debug.Log("Recording end");

        // Visual & Audio stuff
        GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,0.1f);
    }

    public List<GameObject> getObjectsToRecord() {
        //var c = new ContactFilter2D();
        //c.layerMask = LayerMask.NameToLayer("Default");
        

        /*List<Collider2D> results = new List<Collider2D>();
        int num = Physics2D.OverlapCollider(recordingArea, c, results);
        if(num == 0) {
            return null;
        }*/
        List<GameObject> objs = new List<GameObject>();
        foreach(var r in GameObject.FindGameObjectsWithTag("Recordable")) {
            objs.Add(r.gameObject);
        }
        if(objs.Count == 0)
            return null;
        return objs;
    }
}
