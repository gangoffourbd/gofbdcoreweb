namespace Gofbd.TestConsole
{
    using Gofbd.TestConsole.Feature.LockObject;
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            AccountTest.Execute().Wait();
        }
    }
}
