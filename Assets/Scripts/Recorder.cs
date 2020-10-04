using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Recorder : MonoBehaviour
{
    public Collider2D recordingArea;
    public GameObject levelUIManagerObj;

    private float recordingStartTime;
    private LevelUIBehaviour levelUI;

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
        levelUI = levelUIManagerObj.GetComponent<LevelUIBehaviour>();
        print("Recorder here. This is my UI: " + levelUI);
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
        RemoveLastRecordingStillframes();

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
        levelUI.StartRecording();
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
        ShowLastRecordingStillframe();
        levelUI.StopRecording();
    }

    public void ShowLastRecordingStillframe() {
        if(lastRecording == null) {
            return;
        }
        
        foreach(var g in lastRecording.recordedObjects) {
            Debug.Log(lastRecording.frames[g][0].GetPosition());
            var projection = Instantiate(g, this.transform);
            projection.transform.localPosition = lastRecording.frames[g][0].GetPosition();
            var p = projection.AddComponent<Projection>();
            p.projectorCollider = recordingArea;
            var sprite = p.GetComponent<SpriteRenderer>();
            sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            sprite.color = new Color(1,1,1,0.5f);
            p.gameObject.layer = LayerMask.NameToLayer("ProjectionInvisible");
            p.gameObject.tag = "ProjectionInvisible";
        }
    }

    private void RemoveLastRecordingStillframes()
    {
        foreach(var c in GetComponentsInChildren<Projection>()) {
            Destroy(c.gameObject);
        }
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
