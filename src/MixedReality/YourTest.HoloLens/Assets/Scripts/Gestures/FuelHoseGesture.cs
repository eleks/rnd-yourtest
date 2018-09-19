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
                TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Fan in the right place!");
            }
        }
    }

    protected override double NeededX => MainFrame.transform.position.x + 0.01398035;
    protected override double NeededY => MainFrame.transform.position.y + 0.01626544;
    protected override double NeededZ => MainFrame.transform.position.z + 0.01398039;
}
