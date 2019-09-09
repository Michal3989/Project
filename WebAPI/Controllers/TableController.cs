using BL;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TableController : ApiController
    {

        [Route("api/Table/setSeating/{eventId}")]
        [HttpGet]
        // GET: api/Table
        public bool SetSeating(int eventId)
        {
            TableBL.Seating(eventId);
            return true;
        }

        [Route("api/Table/GetTablesWithGuests/{eventId}")]
        [HttpGet]
        // GET: api/Table/5
        public List<TablesWithGuestsDto> GetTablesWithGuests(int eventId)
        {
             return TableBL.GetTablesWithGuests(eventId);
           
        }

        [Route("api/Table/GetTableTypes")]
        [HttpGet]
        public List<TableDto> GetTableTypes(int eventId)
        {
            return TableBL.GetTableTypes(eventId);
        }

        [Route("api/Table/FillTablesDetails")]
        [HttpGet, HttpPost]
        // POST: api/Table
        public bool Post( List<TableDto> tables)
        {
            return TableBL.FillTablesDetailsFromUser(tables);
        }

        //// PUT: api/Table/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Table/5
        //public void Delete(int id)
        //{
        //}
    }
}
