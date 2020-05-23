namespace Gofbd.TestConsole
{
    using Gofbd.TestConsole.Feature.Encoding;
    using Gofbd.TestConsole.Feature.LockObject;
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //AccountTest.Execute().Wait();

            var result = Base64EncodingTest.UnPwToBase64("abc@abc.com", "password11");
            var unPw = Base64EncodingTest.UnPwFromBase64(result);
        }
    }
}
