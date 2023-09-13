namespace SampleSPAwithbackend.Models
{
    public class Tokens
    {

        private static bool set = false;
        private static string adminToken = Guid.NewGuid().ToString("n").Substring(0, 10);
        private static string userToken = Guid.NewGuid().ToString("n").Substring(0, 10);

        public Tokens()
        {

        }


    }
}
