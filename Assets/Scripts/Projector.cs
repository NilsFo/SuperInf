using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : MonoBehaviour
{
    public Collider2D recordingArea;
    public SpriteRenderer cameraSpriteRight;
    public SpriteRenderer cameraSpriteLeft;
    public SpriteRenderer cameraSpriteUp;
    public SpriteRenderer cameraSpriteDown;

    private Recording recording;
    private float playbackTime;
    private int playbackFrame;
    private List<Projection> projections = new List<Projection>();
    private Dictionary<Projection, List<RecordingFrame>> frameIterators = new Dictionary<Projection, List<RecordingFrame>>();
    // Start is called before the first frame update
    void Start()
    {
        // Check direction
        var dir = transform.right;
        if(dir.x > 0.1) {
            cameraSpriteRight.gameObject.SetActive(true);
            cameraSpriteLeft.gameObject.SetActive(false);
            cameraSpriteDown.gameObject.SetActive(false);
            cameraSpriteUp.gameObject.SetActive(false);
        }
        else if(dir.x < -0.1) {
            cameraSpriteRight.gameObject.SetActive(false);
            cameraSpriteLeft.gameObject.SetActive(true);
            cameraSpriteDown.gameObject.SetActive(false);
            cameraSpriteUp.gameObject.SetActive(false);
        }
        else if(dir.y < -0.1) {
            cameraSpriteRight.gameObject.SetActive(false);
            cameraSpriteLeft.gameObject.SetActive(false);
            cameraSpriteDown.gameObject.SetActive(true);
            cameraSpriteUp.gameObject.SetActive(false);
        }
        else if(dir.y > 0.1) {
            cameraSpriteRight.gameObject.SetActive(false);
            cameraSpriteLeft.gameObject.SetActive(false);
            cameraSpriteDown.gameObject.SetActive(false);
            cameraSpriteUp.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(recording == null) {
            return;
        }
        playbackTime += Time.deltaTime;
        if(playbackTime >= recording.length) {
            playbackTime -= recording.length;
            playbackFrame = 1;
        }
        foreach(Projection projection in projections) {
            // Get the current transforms for the timestamp
            List<RecordingFrame> frames = frameIterators[projection];
            while(frames[playbackFrame].timestamp < playbackTime) {
                playbackFrame++;
            }
            var frame1 = frames[playbackFrame-1];
            var frame2 = frames[playbackFrame];
            // Interpolate between frames
            float t = (playbackTime - frame1.timestamp)/(frame2.timestamp - frame1.timestamp);
            if(t < 0 | t > 1) {
                Debug.Log("Time fuckery " + t);
            }
            var pos = Vector3.Lerp(frame1.GetPosition(), frame2.GetPosition(), t);
            projection.transform.localPosition = pos;
        }

    }

    public void StartProjection(Recording r) {
        if(r == null) {
            //Debug.Log("Nothing to play back");
            this.recording = null;
            return;
        }
        this.recording = r;
        foreach(var g in recording.recordedObjects) {
            var projection = Instantiate(g, recording.frames[g][0].GetPosition(), Quaternion.identity, this.transform);
            var p = projection.AddComponent<Projection>();
            p.projectorCollider = recordingArea;
            var sprite = p.GetComponent<SpriteRenderer>();
            sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            sprite.color = new Color(1,1,1,0.5f);
            projections.Add(p);
            frameIterators.Add(p, recording.frames[g]);
            playbackFrame = 1;

            WalkScript ws = p.GetComponent<WalkScript>();
            if (ws)
            {
                ws.enabled = false;
            }
        }

    }
}
