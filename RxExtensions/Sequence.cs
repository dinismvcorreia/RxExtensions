namespace RxExtensions
{
    using System;

    public class Sequence
    {
        public Sequence(string message, TimeSpan waitSpan)
        {
            Message = message;
            WaitSpan = waitSpan;
        }

        public string Message { get; }

        public TimeSpan WaitSpan { get; }
    }
}
