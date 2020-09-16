using System;
using System.Linq;
using netDumbster.smtp;

namespace BirthdayGreetings.Tests.Support
{
    public class ReceivedMail
    {
        readonly String fromAddress;
        readonly String toAddress;
        readonly String subject;
        readonly String body;

        public ReceivedMail(String fromAddress, String toAddress, String subject, String body)
        {
            this.fromAddress = fromAddress;
            this.toAddress = toAddress;
            this.subject = subject;
            this.body = body;
        }

        bool Equals(ReceivedMail other)
        {
            return String.Equals(fromAddress, other.fromAddress) &&
                   String.Equals(toAddress, other.toAddress) &&
                   String.Equals(subject, other.subject) &&
                   String.Equals(body, other.body);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReceivedMail) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (fromAddress != null ? fromAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (toAddress != null ? toAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (subject != null ? subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (body != null ? body.GetHashCode() : 0);
                return hashCode;
            }
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
                .OrderBy(x => x.toAddress)
                .ToArray();
    }
}