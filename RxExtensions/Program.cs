namespace RxExtensions
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            //StartEventBasedScenario();

            StartRxBasedScenario();
        }

        private static void StartRxBasedScenario()
        {
            var engine = new Engine();

            var observable = Observable
                .FromEventPattern<MessageSentEventArgs>(
                    h => engine.MessageSent += h,
                    h => engine.MessageSent -= h);

            var set = new[] {"LLP_IsEnabled", "LLP_ColorEnabled", "LLP_GrayScaleEnabled"};

            var isEnabled = observable
                .Throttle(TimeSpan.FromMilliseconds(160))
                .Where(ep => set.Contains(ep.EventArgs.Message))
                .Subscribe(e => Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: SET CHANGED {e.EventArgs.Message}"));

            var isAttached = observable
                .Where(ep => ep.EventArgs.Message == "LLP_IsAttached")
                .Subscribe(e => Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: {e.EventArgs.Message}"));

            engine.Start();

            Console.ReadKey();

            isEnabled.Dispose();
            isAttached.Dispose();
        }

        private static void StartEventBasedScenario()
        {
            var engine = new Engine();

            engine.MessageSent += OnMessageSent;

            engine.Start();

            Console.ReadKey();

            engine.MessageSent -= OnMessageSent;
        }

        private static void OnMessageSent(object sender, MessageSentEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: {e.Message}");
        }
    }
}
