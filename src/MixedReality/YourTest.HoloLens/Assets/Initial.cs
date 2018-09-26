using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initial : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        var audioManager = GameObject.Find("Audio Manager");
        TextToSpeechHelper.Instance.PlayText(audioManager, "Put Dynamos in the right place ");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
