using System;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace BirthdayGreetings.Smtp
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

        public static SmtpConfiguration From(IConfiguration configuration)
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
     *
     * generateInvalid = NameValueCollection(invalidHost, invalidPort, invalidSender)
     * [InData
     * InvalidConfShouldThrow(NameValueCollection invalid)
     *   Assert.Throws<...>(() => SmtpConfiguration.From(invalid)
     */
}