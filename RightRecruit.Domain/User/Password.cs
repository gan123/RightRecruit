namespace RightRecruit.Domain.User
{
    public class Password
    {
        public byte[] Text { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }

        public Password(byte[] text, byte[] salt, byte[] hash)
        {
            Text = text;
            Salt = salt;
            Hash = hash;
        }
    }
}