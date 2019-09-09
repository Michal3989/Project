using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.DTO;

namespace WebAPI.Controllers
{
    public class GuestController : ApiController
    {
        
        // GET: api/Guest/5
        [HttpGet]
        [Route("api/Guest/GetGuestsDetails/{eventId}")]
        public List<GuestsForViewDto> GetGuestsDetails(int eventId)
        {
            return GuestBL.GetGuestsDetails(eventId);
        }

        [HttpGet]
        [Route("api/Guest/GetEventPictureByGuestId/{guestId}")]
        public string GetEventIdByGuestId(int guestId)
        {
            return GuestBL.GetEventIdByGuestId(guestId);
        }

        
        [HttpPost]
        [Route("api/Guest/SendInvitationsToGuests")]
        public string SendInvitationsToGuests(EventDto myEvent)
        {
            return GuestBL.SendInvitationsToGuests(myEvent);
        }
        [HttpPost]
        [Route("api/Guest/PostGuest")]
        public List<GuestsForViewDto> PostGuest(GuestDto guestDetails)
        {
            return GuestBL.PostGuest(guestDetails);
        }

        // POST: api/Guest

        [HttpGet]
        [Route("api/Guest/Confirm")]
        public int Confirm(string guestId, string numOfMale, string numOfFemale)
        {
            return GuestBL.Confirm(Int32.Parse(guestId),Int32.Parse(numOfMale),Int32.Parse(numOfFemale));
        }
        [HttpPost]
        [Route("api/Guest/UploadGuestFile")]
        public bool UploadGuestFile()
        {
            return true;
        }


        [HttpGet]
        [Route("api/Guest/GetCategories/{eventTypeId}")]
        public List<CategoryDto> GetCategories(byte eventTypeId)
        {
            return GuestBL.GetCategories(eventTypeId);
        }


        
        [HttpPost]
        [Route("api/Guest/UpdateGuests")]
        public int UpdateGuests(List<GuestsForViewDto> guestDetails)
        {
            return GuestBL.UpdateGuests(guestDetails);
        }

    }
}
