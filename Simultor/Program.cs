namespace Simultor
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            Console.WriteLine("Hello, World!");
            int timeUntilChek = 1;
            var i = 1;
            var t = 1;
            // client.GetAsync($"https://localhost:7072/api/Flights/land/F{i++}").Wait();
            while (true)
            {
                try
                {
                    if (Environment.TickCount % 2 == 0)
                        client.GetAsync($"https://localhost:7072/api/Flights/land/f{i++}").Wait();
                    else
                       client.GetAsync($"https://localhost:7072/api/Flights/departure/f{t++}").Wait();
                }
                catch (Exception x)
                {

                    Console.WriteLine($"serever is sleeping, exception:{x.Message}");
                    Thread.Sleep(timeUntilChek++ * 1000);

                }
                Thread.Sleep(1000);

            }
        }
    }
}