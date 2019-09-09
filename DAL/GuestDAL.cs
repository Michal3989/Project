using AutoMapper;
using Common.DTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GuestDAL
    {
        /// <summary>
        /// לתשובות של השרת
        /// </summary>
        static int result;

        /// <summary>
        /// שליפת פרטי מוזמנים לטבלה שבעל האירוע רואה 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<GuestsForViewDto> GetGuestsDetails(int eventId)
        {
            List<GuestsForViewDto> guestsList = new List<GuestsForViewDto>();
            using (var db = new DBContext())
            {
                List<Tbl_guests> guestsDetails;
                try//if it doesn't work it returns null:
                {
                    //guests according to the event
                    guestsDetails = db.Tbl_guests.Include("Tbl_category").Where(g => g.id_event == eventId).ToList();
                    foreach (var g in guestsDetails)
                    {
                        int arrivingId;
                        try
                        {
                            arrivingId = (int)db.Tbl_arriving.FirstOrDefault(a => a.guest_id == g.id)?.id;
                        }
                        catch (Exception)
                        {

                            arrivingId = -1;
                        }
                        GuestsForViewDto guest = new GuestsForViewDto();

                        guest.Id = g.id;
                        guest.EventId = g.id_event;
                        guest.FirstName = g.first_name;
                        guest.LastName = g.last_name;
                        guest.Email = g.email;
                        guest.Category = g.Tbl_category.description;
                        guest.DegreeBefore = g.degree_before;
                        guest.DegreeAfter = g.degree_after;

                        guest.StatusApproval = arrivingId != -1 ? true : false;
                        guest.NumOfMale = arrivingId != -1 ? db.Tbl_arriving.Count(a => a.guest_id == g.id && a.male == true) : 0;
                        guest.NumOfFemale = arrivingId != -1 ? db.Tbl_arriving.Count(a => a.guest_id == g.id && a.male == false) : 0;

                        guestsList.Add(guest);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return guestsList;
        }
        /// <summary>
        /// עדכון מצב אישורי הגעה
        /// </summary>
        /// <param name="guestDetails"></param>
        /// <returns></returns>
        public static int UpdateGuests(List<GuestsForViewDto> guestDetails)
        {
            try
            {
                using (var db = new DBContext())
                {
                    foreach (GuestsForViewDto guest in guestDetails)
                    {
                        int arrivingId;
                        try
                        {
                            arrivingId = db.Tbl_arriving.FirstOrDefault(a => a.guest_id == guest.Id).id;
                        }
                        catch (Exception)
                        {
                            arrivingId = -1;
                        }
                        if (arrivingId != -1)
                        {
                            foreach (Tbl_arriving arriving in db.Tbl_arriving.Where(a => a.guest_id == guest.Id).ToList())
                            {
                                db.Tbl_arriving.Remove(arriving);
                            }
                        }
                        for (int i = 0; i < guest.NumOfMale; i++)
                        {
                            Tbl_arriving arriving = new Tbl_arriving
                            {
                                guest_id = guest.Id,
                                male = true,
                            };
                            db.Tbl_arriving.Add(arriving);
                        }
                        for (int i = 0; i < guest.NumOfFemale; i++)
                        {
                            Tbl_arriving arriving = new Tbl_arriving
                            {
                                guest_id = guest.Id,
                                male = false,
                            };
                            db.Tbl_arriving.Add(arriving);
                        }
                    }
                    result = db.SaveChanges();
                }
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }

        /// <summary>
        /// הוספת אורח
        /// </summary>
        /// <param name="guestDetails"></param>
        /// <returns></returns>
        public static List<GuestsForViewDto> PostGuest(GuestDto guestDetails)
        {
            try
            {
                using (var db = new DBContext())
                {
                    db.Tbl_guests.Add(Converter.convertGuestDtoToTbl(guestDetails));
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return null;
            }

            return GetGuestsDetails(guestDetails.IdEvent);
        }

        /// <summary>
        /// שליפת האירוע המתאים לאורח מסוים
        /// </summary>
        /// <param name="guestId"></param>
        /// <returns></returns>
        public static string GetEventIdByGuestId(int guestId)
        {
            string eventPicture;
            try
            {
                using (var db = new DBContext())
                {
                    eventPicture = db.Tbl_guests.Include("Tbl_event").FirstOrDefault(g => g.id == guestId && g.id_event == g.Tbl_event.id).Tbl_event.picture;
                }
            }
            catch (Exception)
            {

                eventPicture = null;
            }
            return eventPicture;
        }

        /// <summary>
        /// שמירת אישור הגעה
        /// </summary>
        /// <param name="guestId"></param>
        /// <param name="numOfMale"></param>
        /// <param name="numOfFemale"></param>
        public static int Confirm(int guestId, int numOfMale, int numOfFemale)
        {
            using (var db = new DBContext())
            {
                try
                {
                    for (int i = 0; i < numOfMale; i++)
                    {
                        Tbl_arriving arriving = new Tbl_arriving();
                        arriving.guest_id = guestId;
                        arriving.male = true;
                        db.Tbl_arriving.Add(arriving);
                    }
                    for (int i = 0; i < numOfFemale; i++)
                    {
                        Tbl_arriving arriving = new Tbl_arriving();
                        arriving.guest_id = guestId;
                        arriving.male = false;
                        db.Tbl_arriving.Add(arriving);
                    }
                    result = db.SaveChanges();
                }
                catch (Exception)
                {
                    result = 0;
                }
            }
            return result;
        }

        /// <summary>
        /// שליחת ההזמנות
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<GuestDto> getGuestListToSendEmails(int eventId)
        {
            try
            {
                List<Tbl_guests> guestList;
                using (DBContext db = new DBContext())
                {
                    guestList = db.Tbl_guests.Where(g => g.id_event == eventId).ToList();
                }
                return Converter.convertGuestListTblToDto(guestList);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// שליפת קטגוריות להתגה בטופס אורח
        /// </summary>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        public static List<CategoryDto> GetCategories(byte eventTypeId)
        {
            List<Tbl_category> categories;
            try
            {
                using (DBContext db = new DBContext())
                {

                    categories = db.Tbl_category.Where(c => c.type_id == eventTypeId).ToList();
                }

                return Converter.ConvertCategoryTblToDto(categories);
            }
            catch (Exception)
            {
                return null;
            }


        }

    }
}

