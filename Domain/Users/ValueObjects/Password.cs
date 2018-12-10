namespace WorkDayLog.Domain.Users.ValueObjects
{
    public class Password
    {
        public Password(string value)
        {
            Value = value?.Trim();
        }
        
        public string Value { get; private set; }
    }
}