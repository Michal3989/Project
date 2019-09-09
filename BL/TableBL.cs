using Common.DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TableBL
    {
        /// <summary>
    /// שבעל האירוע ממלא -כמה שולחנות הוא רוצה
    /// </summary>
    /// <param name="tables"></param>
    /// <returns></returns>
        public static bool FillTablesDetailsFromUser(List<TableDto> tables)
        {
            int eventId = tables[0].EventId;
            return TableDAL.FillTablesDetails(eventId, tables);
        }
       
        /// <summary>
        /// ????למה זה
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<TableDto> GetTableTypes(int eventId)
        {
            return TableDAL.GetTableTypes(eventId);
        }

        /// <summary>
        /// שליפת המוזמנים משובצים במקומותיהם
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static List<TablesWithGuestsDto> GetTablesWithGuests(int eventId)
        {
            return TableDAL.GetTablesWithGuests(eventId);
        }

        /// <summary>
        /// האלגוריתםםם!!!!!!!!!!!!!!
        /// </summary>
        public static void Seating(int eventId)
        {
            //שליפת כל האורחים שמגיעים לפי קבוצות ממוינות
            Dictionary<byte, List<ArrivingDto>> arrivingGroupsMale = TableDAL.GetArriving(eventId,true);
            Dictionary<byte, List<ArrivingDto>> arrivingGroupsFemale = TableDAL.GetArriving(eventId,false);
            
            List<TableDto> tablesMale = TableDAL.GetTables(eventId,true);
            List<TableDto> tablesFemale = TableDAL.GetTables(eventId, false);
            DissolutionAndConsolidationGroups(arrivingGroupsMale, tablesMale);
            DissolutionAndConsolidationGroups(arrivingGroupsFemale, tablesFemale);
        }

        /// <summary>
        /// מחזיר את השולחן עם מספר המקומות המתאים ביותר - אם אין מחזיר את השולחן עם מס' המקומות המקסימלי
        /// </summary>
        /// <param name="tableList"></param>
        /// <param name="arrivingList"></param>
        /// <returns></returns>
        public static int LookingForTableForGroupOfPeople(List<ArrivingDto> arrivingList, Dictionary<int,int> tableList)
        {
            int numOfPeople = arrivingList.Count();//count of pepole in this group
            foreach (var table in tableList)
            {
                if (table.Value >= numOfPeople)
                    return table.Key;
            }
            //when thereis no places in table:
            int max = tableList.Values.Max();//the biggest empty table
            foreach (var item in tableList)//עד שנדע איך להוציא קי לפי וליו
            {
                if (item.Value == max)
                    return item.Key;
            }
            return -1;//there is no table!
        }
        /// <summary>
        /// B פונקציה
        /// </summary>
        /// <param name="arrivingGroups"></param>
        /// <param name="tablesList"></param>
        public static void DissolutionAndConsolidationGroups(Dictionary<byte, List<ArrivingDto>> arrivingGroups, List<TableDto> tablesList)
        {
            //יצירת ואיתחול דיקשנרי המפתח=האי.די של שולחן, וערך=מספר מקומות שיש בשולחן ואחרי שמתחיל השיבוצים - מה שנשאר בשולחן
            Dictionary<int, int> tableDic = new Dictionary<int, int>();
            foreach (TableDto table in tablesList)
            {
                tableDic.Add(table.Id, table.NumOfPeople);
            }

            //ריצה על כל הקבוצות - סידור שולחן/ות לכל קבוצה
            foreach (var arrivingList in arrivingGroups)
            {
                // COUNT=0 - כל קבוצה - ריצה עד שכולם מסודרים בשולחן
                while (arrivingList.Value.Count()>0)
                {
                    int tableId = LookingForTableForGroupOfPeople(arrivingList.Value, tableDic);
                    if (tableId != -1)//נמצא שולחן כלשהוא
                    {
                        
                        int numOfPeopleInTable = tableDic[tableId];//מס' האנשים שהשולחן יכול לקלוט
                        if (numOfPeopleInTable >= arrivingList.Value.Count())//במקרה כזה השולחן מספיק לכל הקבוצה - עדכון של מס' שולחן לכל אורח ומחיקת רשימה של קטגוריה זו - נגמר ההשיבוץ שלה
                        {
                            tableDic[tableId] -= arrivingList.Value.Count();
                            foreach (var arriving in arrivingList.Value)
                            {
                                arriving.TableId = tableId;
                                int res = TableDAL.SavePlace(arriving);
                            }
                            TableDAL.PutTheTitleOfTable(tableId, arrivingList.Key);
                            arrivingList.Value.Clear();
                        }
                        else//!!!!!!!במקרה זה עדכון של מס' שולחן למס' אורחים שהשולחן יכול לקלוט ומחיקה של אורחים אלו - הם כבר משובצים
                        {
                            tableDic[tableId] = 0;
                            for (int i = numOfPeopleInTable; i > 0; i--)
                            {
                                arrivingList.Value[i].TableId = tableId;
                                int res = TableDAL.SavePlace(arrivingList.Value[i]);
                                ArrivingDto itemToRemove = arrivingList.Value.First(a => a.Id == arrivingList.Value[i].Id);
                                arrivingList.Value.Remove(itemToRemove);
                            }
                            TableDAL.PutTheTitleOfTable(tableId, arrivingList.Key);
                        }
                        //tableDic =tableDic.OrderBy(a => a.Value).ToDictionary<int,int>()
                        //var t = tableDic.OrderBy(f => f.Value);
                        //t.ToDictionary(pair => pair.Key, pair => pair.Value);
                        tableDic= tableDic.OrderBy(f => f.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

                    }
                    else
                    {
                       // "אין אפשרות לשיבוץ";
                    }
                }
               
            }
        }













        //public static int LookingForTableForGroupOfPeople(List<ArrivingDto> arrivingList, List<TableDto> tables)
        //{
        //    int numOfPeopleInGroup = arrivingList.Count();//count of pepole in this group
        //    foreach (var table in tables)
        //    {
        //        if (table.NumOfPeople >= numOfPeopleInGroup)
        //            return table.Id;
        //    }
        //    //when thereis no places in table:
        //    int max = tables[tables.Count() - 1].NumOfPeople;//the biggest empty table
        //    foreach (var item in tables)//עד שנדע איך להוציא קי לפי וליו
        //    {
        //        if (item.NumOfPeople == max)
        //            return item.Id;
        //    }
        //    return -1;//there is no table!
        //}
        ///// <summary>
        ///// B פונקציה
        ///// </summary>
        ///// <param name="arrivingGroups"></param>
        ///// <param name="tablesList"></param>
        //public static void DissolutionAndConsolidationGroups(Dictionary<byte, List<ArrivingDto>> arrivingGroups, List<TableDto> tablesList)
        //{
        //    //יצירת ואיתחול דיקשנרי המפתח=האי.די של שולחן, וערך=מספר מקומות שיש בשולחן ואחרי שמתחיל השיבוצים - מה שנשאר בשולחן
           
        //    //ריצה על כל הקבוצות - סידור שולחן/ות לכל קבוצה
        //    foreach (var arrivingList in arrivingGroups)
        //    {
        //        // COUNT=0 - כל קבוצה - ריצה עד שכולם מסודרים בשולחן
        //        while (arrivingList.Value.Count() > 0)
        //        {
        //            int tableId = LookingForTableForGroupOfPeople(arrivingList.Value, tablesList);
        //            if (tableId != -1)//נמצא שולחן כלשהוא
        //            {

        //                int numOfPeopleInTable = tablesList[tableId].NumOfPeople;//מס' האנשים שהשולחן יכול לקלוט
        //                if (numOfPeopleInTable >= arrivingList.Value.Count())//במקרה כזה השולחן מספיק לכל הקבוצה - עדכון של מס' שולחן לכל אורח ומחיקת רשימה של קטגוריה זו - נגמר ההשיבוץ שלה
        //                {
        //                    tablesList[tableId].NumOfPeople-= (byte)arrivingList.Value.Count();
        //                    foreach (var arriving in arrivingList.Value)
        //                    {
        //                        arriving.TableId = tableId;
        //                        int res = TableDAL.SavePlace(arriving);
        //                    }
        //                    tablesList[tableId].Title=""+arrivingList.Value.
        //                    arrivingList.Value.Clear();
        //                }
        //                else//!!!!!!!במקרה זה עדכון של מס' שולחן למס' אורחים שהשולחן יכול לקלוט ומחיקה של אורחים אלו - הם כבר משובצים
        //                {
        //                    tablesList[tableId].NumOfPeople = 0;
        //                    for (int i = numOfPeopleInTable; i > 0; i--)
        //                    {
        //                        arrivingList.Value[i].TableId = tableId;
        //                        int res = TableDAL.SavePlace(arrivingList.Value[i]);
        //                        ArrivingDto itemToRemove = arrivingList.Value.First(a => a.Id == arrivingList.Value[i].Id);
        //                        arrivingList.Value.Remove(itemToRemove);
        //                    }

        //                }
                        
        //                tablesList = tablesList.OrderBy(t => t.NumOfPeople).ToList();
        //            }
        //            else
        //            {
        //                // "אין אפשרות לשיבוץ";
        //            }
        //        }

        //    }
        //}


    }
}
