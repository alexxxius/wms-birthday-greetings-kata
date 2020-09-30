using System;

namespace BirthdayGreetings.App
{
    public class SmtpConfiguration
    {
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public String Sender { get; set; }
    }
}