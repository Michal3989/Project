using Common.DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GuestBL
    {

        /// <summary>
        ///בעל האירוע id שליפת המוזמנים של בעל אירוע לפי 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<GuestsForViewDto> GetGuestsDetails(int eventId)
        {
            return GuestDAL.GetGuestsDetails(eventId);              
        }

        public static string SendInvitationsToGuests(EventDto myEvent)
        {
            //שליפת המוזמנים של האירוע
            List<GuestDto> guestList= GuestDAL.getGuestListToSendEmails(myEvent.Id);
            List<EventTypeDto> typesList = EventDAL.GetEventTypes();
            return SendEmailBL.SendEmail(myEvent, guestList,typesList);

        }

        public static string GetEventIdByGuestId(int guestId)
        {
            return GuestDAL.GetEventIdByGuestId(guestId);
        }

        public static int Confirm(int guestId, int numOfMale, int numOfFemale)
        {
           return GuestDAL.Confirm(guestId, numOfMale, numOfFemale);
        }

        public static List<GuestsForViewDto> PostGuest(GuestDto guestDetails)
        {
            return GuestDAL.PostGuest(guestDetails);
        }
        public static List<CategoryDto> GetCategories(byte eventTypeId)
        {
            return GuestDAL.GetCategories(eventTypeId);
        }

        public static int UpdateGuests(List<GuestsForViewDto> guestDetails)
        {
            return GuestDAL.UpdateGuests(guestDetails);
        }
    }
}
