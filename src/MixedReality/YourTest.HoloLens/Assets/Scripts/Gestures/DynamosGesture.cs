using UnityEngine;

public class DynamosGesture : JetGesture
{
    protected override bool IsRight
    {
        get { return Helper.IsDynamosRight; }
        set
        {
            Helper.IsDynamosRight = value;

            if (value)
            {
                TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Main Pipes in the right place!");
            }
        }
    }

    protected override double NeededX => MainFrame.transform.position.x - 0.1726437;
    protected override double NeededY => MainFrame.transform.position.y + 0.2346903;
    protected override double NeededZ => MainFrame.transform.position.z - 0.4103022;
}
