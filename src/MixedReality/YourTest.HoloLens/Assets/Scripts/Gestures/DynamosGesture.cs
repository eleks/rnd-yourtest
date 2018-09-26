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
                if (!IsDone)
                {
                    TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Fuell House in the right place ");
                    MobileCommunicator.Instance.SendMessage($"{name}:{value}");
                }
                IsDone = true;
            }
        }
    }

    protected override double NeededX => MainFrame.transform.position.x - 0.1726437;
    protected override double NeededY => MainFrame.transform.position.y + 0.2346903;
    protected override double NeededZ => MainFrame.transform.position.z - 0.4103022;
}
