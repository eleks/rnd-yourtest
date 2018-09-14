using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsFanRight; }
        set { Helper.IsFanRight = value; }
    }

    protected override double NeededX => MainFrame.transform.position.x;
    protected override double NeededY => MainFrame.transform.position.y - 3.973339E-18;
    protected override double NeededZ => MainFrame.transform.position.z - 1.513121;
}
