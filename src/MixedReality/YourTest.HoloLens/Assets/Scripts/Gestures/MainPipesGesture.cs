﻿using UnityEngine;

public class MainPipesGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return EngineStateStorage.IsMainPipesRight; }
        set
        {
            EngineStateStorage.IsMainPipesRight = value;
            if (value)
            {
                if (!IsDone)
                {
                    TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Fan in the right place!");
                    MobileCommunicator.Instance.SendMessage($"{name}:{value}");
                }
                IsDone = true;
            }
        }
    }

    protected override double NeededX => MainFrame.transform.position.x;

    protected override double NeededY => MainFrame.transform.position.y;

    protected override double NeededZ => MainFrame.transform.position.z;
}
