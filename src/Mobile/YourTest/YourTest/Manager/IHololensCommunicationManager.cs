using System;

namespace YourTest.Manager
{
    public interface IHololensCommunicationManager
    {
        void StartListening(String address, Int32 port);

        void StopListening();

        event EventHandler<String> MessageReceived;
    }
}
