using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesRearGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsPipesRearRight; }
        set { Helper.IsPipesRearRight = value; }
    }

    protected override double NeededX => MainFrame.transform.position.x + 5.016274E-39;
    protected override double NeededY => MainFrame.transform.position.y + 0.4584139;
    protected override double NeededZ => MainFrame.transform.position.z + 0.6178892;
}
