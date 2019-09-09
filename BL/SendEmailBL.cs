using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SendEmailBL
    {
        public static string SendEmail(EventDto myEvent, List<GuestDto> guestList, List<EventTypeDto> typesList)
        {
            bool statusOfSending=true;
            string _password = "Student@264";
            string _sender = "206996340@mby.co.il";
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;
            foreach (var guest in guestList)
            {
                try
                {
                MailMessage message = new MailMessage(_sender, guest.Email);
                message.Subject = "הזמנה ל"+typesList.Where(t=>t.Id==myEvent.EventTypeCode).Select(t=>t.Description).ToList()[0]+" של "+myEvent.Name;
                message.Body = "http://localhost:4200/guest-page?idGuest="+guest.Id; 
                client.Send(message);
                }
                catch (Exception e)
                {
                    statusOfSending = false;
                }
            }
            return statusOfSending ? "כל המיילים נשלחו בהצלחה":"לא כל המיילים נשלחו בהצלחה";            
        }
    }
}