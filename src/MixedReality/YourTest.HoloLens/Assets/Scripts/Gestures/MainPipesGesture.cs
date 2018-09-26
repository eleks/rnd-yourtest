using UnityEngine;

public class MainPipesGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsMainPipesRight; }
        set
        {
            Helper.IsMainPipesRight = value;
            if (value)
            {
                TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Rear Pipes in the right place!");
            }
        }
    }

    protected override double NeededX => MainFrame.transform.position.x;

    protected override double NeededY => MainFrame.transform.position.y;

    protected override double NeededZ => MainFrame.transform.position.z;
}
