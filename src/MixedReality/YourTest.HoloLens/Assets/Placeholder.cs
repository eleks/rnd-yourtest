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
        this.textMesh.text = "scanning for 30s";
#if !UNITY_EDITOR
    MediaFrameQrProcessing.Wrappers.ZXingQrCodeScanner.ScanFirstCameraForQrCode(
        result =>
        {
          UnityEngine.WSA.Application.InvokeOnAppThread(async () =>
          {
              await MobileCommunicator.Instance.ConnectAsync(result, "8888");
              SceneManager.LoadScene("ModelExplorer", LoadSceneMode.Additive);
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
