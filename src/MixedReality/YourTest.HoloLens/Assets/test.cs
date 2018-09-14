using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour, ISpeechHandler
{
    public GameObject mainFrame;
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ISpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        System.Diagnostics.Debug.WriteLine($"{name} X delta: {mainFrame.transform.position.x - transform.position.x}");
        System.Diagnostics.Debug.WriteLine($"{name} Y delta: {mainFrame.transform.position.y - transform.position.y}");
        System.Diagnostics.Debug.WriteLine($"{name} Z delta: {mainFrame.transform.position.z - transform.position.z}");
    }
}
