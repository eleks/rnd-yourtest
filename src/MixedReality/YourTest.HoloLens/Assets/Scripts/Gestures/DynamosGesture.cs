public class DynamosGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsDynamosRight; }
        set { Helper.IsDynamosRight = value; }
    }

    protected override double NeededX => MainFrame.transform.position.x + 0.2800832;
    protected override double NeededY => MainFrame.transform.position.y - 0.3911505;
    protected override double NeededZ => MainFrame.transform.position.z -0.6870083;
}
