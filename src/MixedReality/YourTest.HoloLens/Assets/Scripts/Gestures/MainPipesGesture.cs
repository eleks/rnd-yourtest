public class MainPipesGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsMainPipesRight; }
        set { Helper.IsMainPipesRight = value; }
    }

    protected override double NeededX => MainFrame.transform.position.x;

    protected override double NeededY => MainFrame.transform.position.y;

    protected override double NeededZ => MainFrame.transform.position.z;
}
