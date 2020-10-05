using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording {
    public float length;
    public int frameCount = 0;
    public bool Finished;
    public List<GameObject> recordedObjects;
    public Dictionary<GameObject,List<RecordingFrame>> frames;
    public int direction;
    public Recording(List<GameObject> objectsToRecord, int direction) {
        this.direction = direction;
        recordedObjects = objectsToRecord;
        frames = new Dictionary<GameObject, List<RecordingFrame>>(objectsToRecord.Count);
        foreach(GameObject gameObject in objectsToRecord) {
            frames.Add(gameObject, new List<RecordingFrame>());
        }
    }

    public void recordFrame(float timestamp, Transform referenceFrame) {
        // Record a single frame for all watched objects
        foreach(GameObject gameObject in recordedObjects) {
            frames[gameObject].Add(new RecordingFrame(gameObject).Capture(timestamp, referenceFrame));
        }
        frameCount++;
    }

    public void FinishRecording(float timestamp) {
        Finished = true;
        length = timestamp;
    }
}