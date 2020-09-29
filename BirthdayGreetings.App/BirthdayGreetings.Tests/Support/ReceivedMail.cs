using System;
using System.Linq;
using netDumbster.smtp;

namespace BirthdayGreetings.Tests.Support
{
    public class ReceivedMail
    {
        public String FromAddress { get; }
        public String ToAddress { get; }
        public String Subject { get; }
        public String Body { get; }

        public ReceivedMail(String fromAddress, String toAddress, String subject, String body)
        {
            this.FromAddress = fromAddress;
            this.ToAddress = toAddress;
            this.Subject = subject;
            this.Body = body;
        }

        static ReceivedMail From(SmtpMessage smtpMessage)
        {
            var fromAddress = smtpMessage.FromAddress.Address;
            var toAddress = smtpMessage.ToAddresses.Single().Address;
            var subject = smtpMessage.Headers["Subject"];
            var messagePart = smtpMessage.MessageParts.Single();
            var body = messagePart.BodyData;
            return new ReceivedMail(fromAddress, toAddress, subject, body);
        }

        public static ReceivedMail[] FromAll(SimpleSmtpServer smtpServer) =>
            smtpServer.ReceivedEmail
                .Select(From)
                .OrderBy(x => x.ToAddress)
                .ToArray();
    }
}