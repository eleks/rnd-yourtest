using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net.Sockets;
#if !UNITY_EDITOR
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
#endif

public class MobileCommunicator
{
    private MobileCommunicator()
    {
    }

    public static MobileCommunicator Instance => _instance ?? (_instance = new MobileCommunicator());

    public async Task ConnectAsync(String ipAddress, String port)
    {
#if !UNITY_EDITOR
        _soket = new Windows.Networking.Sockets.StreamSocket();
        Windows.Networking.HostName serverHost = new Windows.Networking.HostName(ipAddress);
        await _soket.ConnectAsync(serverHost, port);
#endif
    }

    public async void SendMessage(String message)
    {
#if !UNITY_EDITOR
        Windows.Storage.Streams.DataWriter writer;
        
        using (writer = new DataWriter(_soket.OutputStream))
        {
            writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
            writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;
            writer.MeasureString(message);
            writer.WriteString(message);
            await writer.StoreAsync();

            await writer.FlushAsync();
            writer.DetachStream();
        }
#endif
    }


#if !UNITY_EDITOR
    private Windows.Networking.Sockets.StreamSocket _soket;
#endif
    private static MobileCommunicator _instance;
}