namespace WorkDayLog.Domain.Users.ValueObjects
{
    public class Email 
    {
        public Email(string address)
        {
            Address = address?.Trim();
        }

        public string Address { get; private set; }
    }
}