using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static bool IsMainPipesRight { get; set; }
    public static bool IsDynamosRight { get; set; }
    public static bool IsFanRight { get; set; }
    public static bool IsFuelHoseRight { get; set; }
    public static bool IsPipesRearRight { get; set; }
}

public class SpeechHandler : MonoBehaviour , ISpeechHandler
{
    void ISpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        if (eventData.RecognizedText.ToLower().Equals("finish"))
        {
            MobileCommunicator.Instance.SendMessage(Helper.IsMainPipesRight ? "turbine" : "false");
        }
        else
        {
            return;
        }

        eventData.Use();
    }
}
