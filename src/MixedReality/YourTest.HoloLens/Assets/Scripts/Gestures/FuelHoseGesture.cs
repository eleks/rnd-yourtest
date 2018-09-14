using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelHoseGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsFuelHoseRight; }
        set { Helper.IsFuelHoseRight = value; }
    }

    protected override double NeededX => MainFrame.transform.position.x;
    protected override double NeededY => MainFrame.transform.position.y - 0.02710906;
    protected override double NeededZ => MainFrame.transform.position.z + 0.032952;
}
