namespace Project_FitnessApp.Server.Models
{
    public class Subscription
    {

        public int Subscription_id { get;  }
        public string Type { get; set; }
        public DateTime Time_sub { get; set; }
        public int User_id { get; set; }

    }
}
