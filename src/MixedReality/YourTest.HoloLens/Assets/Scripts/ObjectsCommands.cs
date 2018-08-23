using HoloToolkit.UX.Dialog;
using HoloToolkit.UX.Progress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectsCommands : MonoBehaviour
{

    //fill dialogPrefab in the inspector:
    private GameObject dialogPrefab;
    void OnSelect()
    {
        var selectedObjectName = this.gameObject.name;
        MobileCommunicator.Instance.SendMessage($"[Test] OnSelected object name: {selectedObjectName}");
        Debug.Log($"[Test] OnSelected object name: {selectedObjectName}");
    }

    private void ShowDialog()
    {
        dialogPrefab = Resources.Load("Dialog") as GameObject;

        Dialog dialog = Dialog.Open(dialogPrefab.gameObject, DialogButtonType.OK | DialogButtonType.Cancel, String.Empty, "Are you sure it is the left leg?");
        dialog.OnClosed += OnDialogClosed;
    }

    private async void OnDialogClosed(DialogResult result)
    {
        if (result.Result == DialogButtonType.OK)
        {
            ProgressIndicator.Instance.Open(IndicatorStyleEnum.AnimatedOrbs, ProgressStyleEnum.None, ProgressMessageStyleEnum.Visible, "Loading...");

            await Task.Delay(3000);

            ProgressIndicator.Instance.Close();
        }
    }
}
