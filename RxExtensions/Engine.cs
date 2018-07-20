namespace RxExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Engine
    {
        public event EventHandler<MessageSentEventArgs> MessageSent;

        public void Start()
        {
            foreach (var sequence in GetMessageSequence())
            {
                MessageSent?.Invoke(this, new MessageSentEventArgs(sequence.Message));

                Thread.Sleep(sequence.WaitSpan);
            }
        }

        private static IEnumerable<Sequence> GetMessageSequence()
        {
            return new[]
            {
                new Sequence("LLP_IsEnabled", TimeSpan.FromMilliseconds(300)),
                new Sequence("LLP_IsAttached", TimeSpan.FromMilliseconds(100)),
                new Sequence("LLP_IsEnabled", TimeSpan.FromMilliseconds(100)),
                new Sequence("LLP_ColorEnabled", TimeSpan.FromMilliseconds(100)),
                new Sequence("LLP_GrayScaleEnabled", TimeSpan.FromMilliseconds(100)),
            };
        }
    }
}