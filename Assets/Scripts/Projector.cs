using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    private Recording recording;
    private float playbackTime;
    private int playbackFrame;
    private List<Projection> projections = new List<Projection>();
    private Dictionary<Projection, List<RecordingFrame>> frameIterators = new Dictionary<Projection, List<RecordingFrame>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recording == null) {
            return;
        }
        playbackTime += Time.deltaTime;
        foreach(Projection projection in projections) {
            // Get the current transforms for the timestamp
            List<RecordingFrame> frames = frameIterators[projection];
            while(frames[playbackFrame].timestamp > playbackTime) {
                playbackFrame++;
            }
            var frame1 = frames[playbackFrame-1];
            var frame2 = frames[playbackFrame];
            // Interpolate between frames
            var pos = Vector3.Lerp(frame1.GetPosition(), frame2.GetPosition(), (playbackTime - frame1.timestamp)/(frame2.timestamp - frame1.timestamp));
            projection.transform.position = pos;
        }

    }

    public void StartRecording(Recording r) {
        this.recording = r;
        foreach(var g in recording.recordedObjects) {
            var projection = Instantiate(g, recording.frames[g][0].GetPosition(), Quaternion.identity, this.transform);
            var p = projection.AddComponent<Projection>();
            projections.Add(p);
            frameIterators.Add(p, recording.frames[g]);
            playbackFrame = 1;
        }

    }
}
