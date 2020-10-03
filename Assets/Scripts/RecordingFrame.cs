using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingFrame {
    GameObject recordedObject;
    public float x, y, z;
    public float timestamp;
    // Possible animations?
    public RecordingFrame(GameObject recordedObject) {
        this.recordedObject = recordedObject;
    }
    public RecordingFrame Capture(float timestamp, Transform referenceFrame) {
        x = referenceFrame.transform.position.x - recordedObject.transform.position.x;
        y = referenceFrame.transform.position.y - recordedObject.transform.position.y;
        z = referenceFrame.transform.position.z - recordedObject.transform.position.z;
        this.timestamp = timestamp;
        return this;
    }

    public override string ToString() {
        return recordedObject + " at time " + timestamp + "; " + x + ", " + y + ", " + z;
    }

    public Vector3 GetPosition() {
        return new Vector3(x,y,z);
    }
}