using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelHoseGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsFuelHoseRight; }
        set
        {
            Helper.IsFuelHoseRight = value;
            if (value)
            {
                if (!IsDone)
                {
                    TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Pipes Rear in the right place!");
                    MobileCommunicator.Instance.SendMessage($"{name}:{value}");
                }
                IsDone = true;
            }
        }
    }

    protected override double NeededX => MainFrame.transform.position.x + 0.01398035;
    protected override double NeededY => MainFrame.transform.position.y + 0.01626544;
    protected override double NeededZ => MainFrame.transform.position.z + 0.01398039;
}
