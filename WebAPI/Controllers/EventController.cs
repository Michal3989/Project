
using BL;
using Common.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace WebAPI.Controllers
{
   // [RoutePrefix("api/Event")]
    public class EventController : ApiController
    {

      
        [HttpGet]
        [Route("api/Event/GetSelectedEvent/{eventId}")]
        public EventDto GetSelectedEvent(int eventId)
        {
            return EventBL.GetSelectedEvent(eventId);
        }

        [HttpGet]
        [Route("api/Event/GetEventTypes")]
        public List<EventTypeDto> GetEventTypes()
        {
            List<EventTypeDto> l = EventBL.GetEventTypes();
            return l;
        }

        [Route("api/Event/GetDefaultEvent/{ownerId}")]
        [HttpGet, HttpPost]
        public EventDto GetDefaultEvent(int ownerId)//להצגה בהתחברות
        {
            EventDto e = EventBL.GetDefaultEvent(ownerId);

            return e;
        }

        [HttpGet]
        [Route("api/Event/GetEvents/{ownerId}")]
        public List<EventDto> GetEvents(int ownerId)//רשימת אירועים לדשבורד
        {
            List<EventDto> g= EventBL.GetAllEvents(ownerId);
            return g;
        }

        //[HttpGet]
        //[Route("api/Event/GetEventDetails")]
        [Route("api/Event/GetEventDetails/{eventId}")]
        [HttpGet, HttpPost]
        public DashboardDto GetEventDetails(int eventId)//פרטי אירוע לדשבורד
        {
            DashboardDto f= EventBL.GetEventDetails(eventId);
            return f;
        }

        [HttpPost]
       // [Route("api/Event/AddEvent/{stringEvent}")]

        public int PostEvent(string stringEvent)//הוספת אירוע חדש
        {
            var file = HttpContext.Current.Request.Files[0];
            EventDto newEvent = JsonConvert.DeserializeObject<EventDto>(stringEvent);
            int ans = EventBL.AddEvent(newEvent);
            newEvent.Id = ans;
            newEvent.Picture=ans+""+ file.FileName.Substring(file.FileName.IndexOf("."));
            file.SaveAs(@"C:\d\המסמכים שליD\אפרת\הנדסאים יד\M\הנדסאים יד\פרויקט\ProjectExample\WebAPI\Images\" + newEvent.Picture);
            bool ansUpdate=EventBL.UpdateEvent(newEvent);
            return ansUpdate ? ans : -1;
        }
        /// <summary>
        /// כששומרים פעם נוספת הוא לא שומר בגלל שזה יומצא אותו שם של תמונה ויש לו כבר כזה, מה לעשות?
        /// </summary>
        /// <param name="stringEvent"></param>
        /// <returns></returns>
        [HttpPut]
        //[Route("api/Event/UpdataEvent")]
        public bool PutEvent(string stringEvent)//עדכון אירוע קיים
        {
            EventDto updateEvent = JsonConvert.DeserializeObject<EventDto>(stringEvent);
            bool isFile=true;
            HttpPostedFile file;
            EventDto oldEvent = EventBL.GetSelectedEvent(updateEvent.Id);
          
            try
            {
                 file = HttpContext.Current.Request.Files[0];
            }
            catch (Exception)
            {
               isFile = false;
             }
            if(isFile==true)
            {
               file = HttpContext.Current.Request.Files[0];
               //אם היה תמונה
                if (oldEvent.Picture != null)
                {
                    File.Delete(@"C:\d\המסמכים שליD\אפרת\הנדסאים יד\M\הנדסאים יד\פרויקט\ProjectExample\WebAPI\Images\" + oldEvent.Picture);
                }
              
               updateEvent.Picture = updateEvent.Id + "" + file.FileName.Substring(file.FileName.IndexOf("."));
               file.SaveAs(@"C:\d\המסמכים שליD\אפרת\הנדסאים יד\M\הנדסאים יד\פרויקט\ProjectExample\WebAPI\Images\" + updateEvent.Picture);
            }
            bool res=EventBL.UpdateEvent(updateEvent);
            return isFile ? true : res;
        }

        [HttpGet]
        [Route("api/Event/Login")]
        public OwnerDto Login(string email, string password)
        {
            return OwnerBL.Login(email, password);
        }


        [HttpPost]
        [Route("api/Event/AddOwner")]
        public int AddOwner(OwnerDto newOwner)
        {
            return OwnerBL.AddOwner(newOwner);
        }

    }
}
