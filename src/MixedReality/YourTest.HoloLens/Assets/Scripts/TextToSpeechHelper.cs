using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToSpeechHelper
{
    private TextToSpeechHelper()
    {
    }

    public static TextToSpeechHelper Instance => _instance ?? (_instance = new TextToSpeechHelper());

    public void PlayText(GameObject audioManager, String message)
    {
        TextToSpeech textToSpeech = audioManager.GetComponent<TextToSpeech>();
        textToSpeech.Voice = TextToSpeechVoice.Zira;
        textToSpeech.StartSpeaking(message);
    }

    private static TextToSpeechHelper _instance;
}
