namespace Codere.BetRadar.ServiceEventsBetRadar.Helpers
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fullName => $"{name} {surname}";
        public string surname { get; set; }
        public string birthdate { get; set; }

        public int roleId { get; set; }
        public string roleName { get; set; }
    }
}
