using Entities;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using AutoMapper;

namespace DAL
{
    public static class EventDAL
    {
        /// <summary>
        /// לתשובות של השרת
        /// </summary>
        static int result;

        /// <summary>
        /// פרטים לדשבורד בצד
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dashboardDetails"></param>
        /// 
        public static DashboardDto FillDashboardDetails(int eventId)
        {
            DashboardDto dashboardDetails = new DashboardDto();
            using (var db = new DBContext())
            {
                try
                {
                    //ownerName:
                    var ownerName = db.Tbl_event.Where(e => e.id == eventId).Join(db.Tbl_event_owner_,
                                 (e => e.id_event_owner),
                                 (o => o.id),
                                 ((e, o) => new { firstName = o.first_name, lastName = o.last_name })).FirstOrDefault();

                    dashboardDetails.OwnerFirstName = ownerName.firstName;
                    dashboardDetails.OwnerLastName = ownerName.lastName;
                    //EventType:
                    dashboardDetails.EventType = db.Tbl_event.Where(e => e.id == eventId).Join(db.Tbl_event_type,
                                                e => e.event_type_code,
                                                t => t.id,
                                                ((e, t) => t.description)).FirstOrDefault();

                    //CelebratorsName:
                    dashboardDetails.CelebratorsName = db.Tbl_event.FirstOrDefault(e => e.id == eventId)?.name;
                    //NumDaysForEvent:
                    dashboardDetails.NumDaysForEvent = (int)((DateTime.Today - db.Tbl_event.FirstOrDefault(e => e.id == eventId)?.date)?.TotalDays);
                    //StatusApprovals:
                    //dashboardDetails.StatusApprovals = FillStatusApprovals(eventId);
                }
                catch (Exception ex)
                {
                    dashboardDetails = null;
                }

            }
            return dashboardDetails;
        }

        /// <summary>
        /// שליפת מצב אישורי השתתפות לדשבורד
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="dashboardDetails"></param>
        //public static StatusApprovalsDto FillStatusApprovals(int eventId)
        //{
        //    StatusApprovalsDto statusApprovals = new StatusApprovalsDto();
        //    using (var db = new DBContext())
        //    {
        //        try
        //        {
        //            statusApprovals.NumOfComing = db.Tbl_guests.Count(g => g.status_code == (int)Statuses.isComing);
        //            statusApprovals.NumOfNotComing = db.Tbl_guests.Count(g => g.status_code == (int)Statuses.notComing);
        //            statusApprovals.NoAnswer = db.Tbl_guests.Count(g => g.status_code == (int)Statuses.noAnswer);
        //        }
        //        catch (Exception ex)
        //        {
        //            statusApprovals = null;
        //        }

        //    }
        //    return statusApprovals;
        //}

        /// <summary>
        /// הוספת אירוע
        /// </summary>
        /// <param name="newEvent"></param>
        /// <returns></returns>
        public static int AddEvent(EventDto newEvent)
        {
            using (var db = new DBContext())
            {
                Tbl_event eventToAdd = Converter.ConvertEventDtoToTbl(newEvent);
                try
                {
                    db.Tbl_event.Add(eventToAdd);

                    result = db.SaveChanges();
                    if (result == 1)
                    { return db.Tbl_event.Max(e => e.id); }

                    else
                    { return -1;
                }

                }
                catch (Exception ex)
                {

                    return -1;
                }
            }
        }

        /// <summary>
        /// עדכון פרטי אירוע
        /// </summary>
        /// <param name="eventToUpdate"></param>
        /// <returns></returns>
        public static bool UpdateEvent(EventDto eventToUpdate)
        {
            using (var db = new DBContext())
            {
                Tbl_event tbl_Event;
                try
                {
                    tbl_Event = db.Tbl_event.FirstOrDefault(currentEvent => currentEvent.id == eventToUpdate.Id);
                }
                catch (Exception ex)
                {
                    return false;
                }
                if (tbl_Event == null)
                    return false;
                tbl_Event.date = eventToUpdate.Date;
                tbl_Event.name = eventToUpdate.Name;
                tbl_Event.picture = eventToUpdate.Picture;
                 
                tbl_Event.free_text = eventToUpdate.FreeText;
                result = db.SaveChanges();
            }
            return (result > 0 ? true : false);
        }

        /// <summary>
        /// שליפת כל האירועים לדשבורד
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static List<EventDto> GetAllEvents(int ownerId)
        {
            List<EventDto> convertedEvents = new List<EventDto>();
            using (var db = new DBContext())
            {
                try
                {
                    List<Tbl_event> eventsList = db.Tbl_event.Where(currentEvent => currentEvent.id_event_owner == ownerId).ToList();
                    foreach (var currentEvent in eventsList)
                    {
                        var eventToAdd = Converter.ConvertEventTblToDto(currentEvent);
                        convertedEvents.Add(eventToAdd);
                    }
                }
                catch (Exception ex)
                {
                    convertedEvents = null;
                }             
            }
            return convertedEvents;
        }

        /// <summary>
        /// שליפת פרטי אירוע ע"פ בחירה ברשימה בדשבורד
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static EventDto GetSelectedEvent(int eventId)
        {
            using (var db = new DBContext())
            {
                EventDto selectedEvent;
                try
                {
                    Tbl_event eventById = db.Tbl_event.First(currentEvent => currentEvent.id == eventId);
                    selectedEvent = Converter.ConvertEventTblToDto(eventById);
                }
                catch (Exception ex)
                {
                    selectedEvent = null;
                }
                return selectedEvent;
            }

        }

        /// <summary>
        /// שליפת כל סוגי האירועים
        /// </summary>
        /// <returns></returns>
        public static List<EventTypeDto> GetEventTypes()
        {
            List<EventTypeDto> typesList = new List<EventTypeDto>();
            using (var db = new DBContext())
            {
                try
                {
                    List<Tbl_event_type> types = db.Tbl_event_type.ToList();
                    foreach (var type in types)
                    {
                        typesList.Add(Converter.ConvertEventTypeTblToDto(type));
                    }
                }
                catch (Exception ex)
                {
                    typesList = null;
                }
            }
            return typesList;
        }
    }
}
