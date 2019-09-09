using DAL;
using Entities;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


namespace BL
{
   public static class EventBL
    {

      

        /// <summary>
        /// הוספת אירוע חדש
        /// </summary>
        /// <param name="newEvent"></param>
        /// <returns></returns>
        public static int AddEvent(EventDto newEvent)
        {
           return EventDAL.AddEvent(newEvent);
        }

        /// <summary>
        /// שליפת פרטי אירוע לדשבורד
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static DashboardDto GetEventDetails(int eventId)
        {
            return EventDAL.FillDashboardDetails(eventId);
        }

        public static bool UpdateEvent(EventDto eventToUpdate)
        {
           return EventDAL.UpdateEvent(eventToUpdate);
        }

        public static List<EventDto> GetAllEvents(int ownerId)
        {
            return EventDAL.GetAllEvents(ownerId);
        }

        public static EventDto GetSelectedEvent(int id)
        {
            return EventDAL.GetSelectedEvent(id);
        }

        public static EventDto GetDefaultEvent(int ownerId)
        {
            List<EventDto> eventsList = EventDAL.GetAllEvents(ownerId)/*.GetDefaultEvent(ownerId)*/;
            if (eventsList == null)
                return new EventDto();
            int minimum = int.MaxValue;
            EventDto defaultEvent = new EventDto();
            foreach (var e in eventsList)//שליפת האירוע הקרוב ביותר
            {
                int difference = (int)((DateTime.Today - e.Date).TotalDays);
                if(difference < 0)
                {
                    difference = difference * -1;
                }
                if (difference < minimum)
                {
                    minimum = difference;
                    defaultEvent = e;
                }
            }
            return defaultEvent;
        }

        /// <summary>
        /// שליפת כל סוגי האירועים
        /// </summary>
        /// <returns></returns>
        public static List<EventTypeDto> GetEventTypes()
        {
            return EventDAL.GetEventTypes();
        }
    }
}
