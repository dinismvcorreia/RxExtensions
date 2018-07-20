namespace RxExtensions
{
    using System;

    public class MessageSentEventArgs : EventArgs
    {
        public MessageSentEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}