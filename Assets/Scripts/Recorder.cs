using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public Transform objToRecord;
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
    }

    void FixedUpdate() 
    {
        if(recordingstatus == Recordingstatus.START_RECORDING) {
            Debug.Log("Recording Start");
            recordingstatus = Recordingstatus.RECORDING_ACTIVE;
            recordingStartTime = Time.time;
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
        List<GameObject> objs = new List<GameObject>(1);
        objs.Add(objToRecord.gameObject);
        return objs;
    }


}
