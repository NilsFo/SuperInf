using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    static bool played;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake ()
    {
        if(!played) {
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(this.gameObject);
            played = true;
        }

    }
}
