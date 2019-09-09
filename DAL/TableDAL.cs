using AutoMapper;
using Common.DTO;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TableDAL
    {

        /// <summary>
        ///dbלקבלת הסטטוס של התשובה של ה
        /// </summary>
        static int result;

        /// <summary>
        /// הזנת נתוני השולחנות שהמשתמש הכניס 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="tables"></param>
        /// <returns></returns>
        public static bool FillTablesDetails(int eventId, List<TableDto> tables)
        {
            using (var db = new DBContext())
            {
                List<Tbl_table> tableList = Converter.ConvetrTableDtoToTbl(tables);
                foreach (var t in tableList)
                {
                    try
                    {
                        db.Tbl_table.Add(t);
                    }
                    catch (Exception)
                    {
                        result = 0;
                    }
                    result = db.SaveChanges();
                }
            }
            return (result > 0 ? true : false);
        }

        /// <summary>
        /// שליפת סוגי שולחנות
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<TableDto> GetTableTypes(int eventId)
        {
            List<TableDto> convertedTables = new List<TableDto>();
            using (var db = new DBContext())
            {
                try
                {
                    List<Tbl_table> tables = db.Tbl_table.Where(t => t.event_id == eventId).ToList();
                    foreach (var currentTable in tables)
                    {
                        var tableToAdd = Converter.ConvetrTableTblToDto(currentTable);
                        convertedTables.Add(tableToAdd);
                    }
                }
                catch (Exception ex)
                {
                    convertedTables = null;
                }
            }
            return convertedTables;
        }

        /// <summary>
        /// שליפת השיבוץ של אורחים בשולחנות 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<TablesWithGuestsDto> GetTablesWithGuests(int eventId)
        {
            List<TablesWithGuestsDto> tablesWithGuestsList = new List<TablesWithGuestsDto>();
            TablesWithGuestsDto tableWithGuests;

            using (var db = new DBContext())
            {
                List<Tbl_arriving> arrivingTbl = db.Tbl_arriving.Include("Tbl_guests")
                    .Where(a => a.Tbl_guests.id_event == eventId).OrderBy(a => a.table_id).ToList();

                List<Tbl_table> tablesTbl = db.Tbl_table.Where(t => t.event_id == eventId).ToList();
                foreach (var table in tablesTbl)
                {
                    List<string> guestsFullName = new List<string>();
                    tableWithGuests = new TablesWithGuestsDto()
                    {
                        TableId = table.id,
                        Male = table.male,
                        Title = table.title,
                    };
                    foreach (var arrive in arrivingTbl)
                    {
                        if (arrive.table_id == table.id)
                            guestsFullName.Add(arrive.Tbl_guests.first_name + " " + arrive.Tbl_guests.last_name);
                    }
                    tableWithGuests.GuestsFullName = guestsFullName;
                    tablesWithGuestsList.Add(tableWithGuests);
                }
            }
            return tablesWithGuestsList;
        }

        /// <summary>
        /// שליפת המגיעים מקובצים לפי קטגוריות וממוינים
        /// </summary>
        /// <param name="eventId"></param>
        public static Dictionary<byte, List<ArrivingDto>> GetArriving(int eventId, bool gender)
        {
            Dictionary<byte, List<ArrivingDto>> arrivingGroups;
            using (var db = new DBContext())
            {
                arrivingGroups = new Dictionary<byte, List<ArrivingDto>>();
                IOrderedQueryable<IGrouping<byte, Tbl_arriving>> categoryList = db.Tbl_arriving.Include("Tbl_guests")
                    .Where(a => a.Tbl_guests.id_event == eventId && a.male == gender)
                    .GroupBy(g => g.Tbl_guests.category_code)
                    .OrderByDescending(g => g.Count());

                foreach (var item in categoryList)
                {
                    List<ArrivingDto> arriving = Converter.ConvetrArrivingTblListToDto(item.ToList());
                    arrivingGroups.Add(item.Key, arriving);
                }
            }
            return arrivingGroups;
        }

        /// <summary>
        /// קבלת שולחנות ממוינים לפי מס' מקומות ישיבה
        /// </summary>
        /// <param name="eventId"></param>
        public static List<TableDto> GetTables(int eventId, bool gender)
        {
            List<TableDto> tablesDto = new List<TableDto>();
            using (var db = new DBContext())
            {
                List<Tbl_table> tables = db.Tbl_table.Where(t => t.event_id == eventId && t.male == gender)
                    .OrderBy(t => t.num_of_people).ToList();

                foreach (Tbl_table t in tables)
                {
                    tablesDto.Add(Converter.ConvetrTableTblToDto(t));
                }
            }
            return tablesDto;
        }

        /// <summary>
        /// שמירת השיבוץ של אורח בשולחן
        /// </summary>
        /// <param name="arriving"></param>
        /// <returns></returns>
        public static int SavePlace(ArrivingDto arriving)
        {

            using (var db = new DBContext())
            {
                Tbl_arriving arrive = db.Tbl_arriving.First(a => a.id == arriving.Id);
                arrive.table_id = arriving.TableId;
                result = db.SaveChanges();
            }
            return result;
        }

        public static int PutTheTitleOfTable(int tableId, byte categoryId)
        {
            using (var db = new DBContext())
            {
                Tbl_table table = db.Tbl_table.First(t => t.id == tableId);
                table.title = table.title + "-" + db.Tbl_category.First(c => c.id == categoryId).description;
                table.category_code = categoryId;
                result = db.SaveChanges();
            }
            return result;
        }


    }
}
