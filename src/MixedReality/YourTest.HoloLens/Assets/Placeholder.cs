using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Placeholder : MonoBehaviour
{
    public Transform textMeshObject;

    private void Start()
    {
        this.textMesh = this.textMeshObject.GetComponent<TextMesh>();
        this.OnReset();
    }

    public void OnScan()
    {
        this.textMesh.text = "scanning...";
#if !UNITY_EDITOR
    MediaFrameQrProcessing.Wrappers.ZXingQrCodeScanner.ScanFirstCameraForQrCode(
        async (result) =>
        {
            UnityEngine.WSA.Application.InvokeOnAppThread(() =>
            {
                this.textMesh.text = "connecting...";
            },
          false);
            await MobileCommunicator.Instance.ConnectAsync(result, "8888");
          UnityEngine.WSA.Application.InvokeOnAppThread(() =>
          {
              SceneManager.LoadScene("ModelExplorer");
          }, 
          false);
        },
        TimeSpan.FromSeconds(30));
#endif
    }

    public void OnReset()
    {
        this.textMesh.text = "say scan to start";
    }

    TextMesh textMesh;
}
