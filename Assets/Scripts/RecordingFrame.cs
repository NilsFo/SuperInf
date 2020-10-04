using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingFrame {
    GameObject recordedObject;
    public Vector3 pos;
    public float timestamp;
    // Possible animations?
    public RecordingFrame(GameObject recordedObject) {
        this.recordedObject = recordedObject;
    }
    public RecordingFrame Capture(float timestamp, Transform referenceFrame) {
        Vector3 p = recordedObject.transform.position;
        this.pos = referenceFrame.worldToLocalMatrix.MultiplyPoint(p);
        this.timestamp = timestamp;
        return this;
    }

    public override string ToString() {
        return recordedObject + " at time " + timestamp + "; " + pos;
    }

    public Vector3 GetPosition() {
        return pos;
    }
}