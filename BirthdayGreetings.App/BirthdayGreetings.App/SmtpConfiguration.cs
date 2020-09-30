using System;
using System.Collections.Specialized;

namespace BirthdayGreetings.App
{
    public class SmtpConfiguration
    {
        public String Host { get; set; }
        public Int32 Port { get; set; }
        public String Sender { get; set; }

        public static SmtpConfiguration From(NameValueCollection appSettings)
        {
            throw new NotImplementedException();
        }
    }
    
    /*
     * SmtpConfigurationTests
     *      - full config ok
     *
     * example based test
     * generative based test
     *     - property-based test
     */
}