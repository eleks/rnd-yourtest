using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;

public class MobileCommunicator
{
    private MobileCommunicator()
    {
    }

    public static MobileCommunicator Instance => _instance ?? (_instance = new MobileCommunicator());

    public async Task ConnectAsync(String ipAddress, String port)
    {
#if !UNITY_EDITOR
        var socket = new Windows.Networking.Sockets.StreamSocket();
        Windows.Networking.HostName serverHost = new Windows.Networking.HostName(ipAddress);
        await socket.ConnectAsync(serverHost, port);

        Stream streamOut = socket.OutputStream.AsStreamForWrite();
        _writer = new StreamWriter(streamOut, Encoding.Unicode) { AutoFlush = true };
#endif
    }

    public void SendMessage(String message)
    {
        _writer.Write(message);
    }

    private StreamWriter _writer;
    private static MobileCommunicator _instance;
}