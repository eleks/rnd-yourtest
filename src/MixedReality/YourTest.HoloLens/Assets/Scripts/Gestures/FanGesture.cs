using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanGesture : JetGesture
{
    public GameObject Engine;

    protected override bool IsRight
    {
        get { return Helper.IsFanRight; }
        set
        {
            Helper.IsFanRight = value;

            if (value)
            {
                if (!IsDone)
                {
                    TextToSpeechHelper.Instance.PlayText(AudioManager, "Put Main pipes in the right place!");
                    MobileCommunicator.Instance.SendMessage($"{name}:{value}");
                }
                IsDone = true;
            }

            var animator = Engine.GetComponent<Animator>();
            animator.enabled = value;
        }
    }

    protected override double NeededX => MainFrame.transform.position.x - 0.6419629 ;
    protected override double NeededY => MainFrame.transform.position.y + 2.384003E-18;
    protected override double NeededZ => MainFrame.transform.position.z - 0.641963;
}
