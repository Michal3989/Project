using Entities;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace DAL
{
    public class Converter
    {
        public static Tbl_event ConvertEventDtoToTbl(EventDto eventDto)
        {
            Tbl_event tblEvent = new Tbl_event
            {
                //id = eventDto.Id,
                id_event_owner = eventDto.IdEventOwner,
                event_type_code = eventDto.EventTypeCode,
                date = eventDto.Date,
                picture = eventDto.Picture,
                free_text = eventDto.FreeText,
                name = eventDto.Name
            };
            return tblEvent;
        }
        public static EventDto ConvertEventTblToDto(Tbl_event eventTbl)
        {
            EventDto eventDto = new EventDto
            {
                Id = eventTbl.id,
                IdEventOwner = eventTbl.id_event_owner,
                EventTypeCode = eventTbl.event_type_code,
                Date = eventTbl.date,
                Picture = eventTbl.picture,
                FreeText = eventTbl.free_text,
                Name = eventTbl.name
            };
            return eventDto;
        }

        public static EventTypeDto ConvertEventTypeTblToDto(Tbl_event_type tblEventType)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Tbl_event_type, EventTypeDto>();

            });

           return Mapper.Map<Tbl_event_type, EventTypeDto>(tblEventType);
        }

        //public static Tbl_table ConvertTableDtoToTbl(TableDto tableDto)
        //{
        //    Tbl_table tableTbl = new Tbl_table
        //    {
        //        event_id = tableDto.EventId,
        //        num_of_people = tableDto.NumOfPeople,
        //        male = tableDto.Male,
        //        title = tableDto.Title
        //    };
        //    return tableTbl;
        //}

        public static Tbl_event_owner_ ConvertOwnerDtoToTbl(OwnerDto ownerDto)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<OwnerDto, Tbl_event_owner_>();
            });
           return  Mapper.Map<OwnerDto, Tbl_event_owner_>(ownerDto);
        }
        public static List<Tbl_table> ConvetrTableDtoToTbl(List<TableDto> tables)
        {
            List<Tbl_table> tableList=new List<Tbl_table>();
            foreach (var t in tables)
            {
                for (int i = 0; i < t.Amount; i++)
                {
                    Tbl_table tableToAdd = new Tbl_table
                    {
                        event_id = t.EventId,
                        num_of_people = (byte)t.NumOfPeople,
                        male = t.Male
                    };
                    tableList.Add(tableToAdd);
                }
            }
            return tableList;
        }

        public static Tbl_guests convertGuestDtoToTbl(GuestDto guestDetails)
        {
            Tbl_guests guestTbl = new Tbl_guests
            {
                id=guestDetails.Id,
                id_event=guestDetails.IdEvent,
                degree_before=guestDetails.DegreeBefore,
                degree_after=guestDetails.DegreeAfter,
                first_name=guestDetails.FirstName,
                last_name=guestDetails.LastName,
                email=guestDetails.Email,
                category_code=guestDetails.CategoryCode
            };
            return guestTbl;
        }

        public static TableDto ConvetrTableTblToDto(Tbl_table tableTbl)
        {
            TableDto tableDto = new TableDto
            {
                Id=tableTbl.id,
                EventId=tableTbl.event_id,
                Male = tableTbl.male,
                Amount =-1,               
                NumOfPeople=tableTbl.num_of_people,
                Title=tableTbl.title
            };
            return tableDto;           
        }

        public static List<ArrivingDto> ConvetrArrivingTblListToDto(List<Tbl_arriving> arrivingTbl)
        {
            List<ArrivingDto> arrivingList = new List<ArrivingDto>();
            foreach (Tbl_arriving a in arrivingTbl)
            {
                ArrivingDto arriving = new ArrivingDto
                {
                    Id = a.id,
                    GuestId = a.guest_id,
                    Male = a.male,
                    TableId = a.table_id
                };
                arrivingList.Add(arriving);
            }
            return arrivingList;
        }
        public static List<GuestDto> convertGuestListTblToDto(List<Tbl_guests> guestTbl)
        {
            List<GuestDto> guestList = new List<GuestDto>();
            foreach (Tbl_guests guest in guestTbl)
            {
                GuestDto guestDto = new GuestDto
                {
                    Id = guest.id,
                    IdEvent = guest.id_event,
                    DegreeBefore = guest.degree_before,
                    DegreeAfter = guest.degree_after,
                    FirstName = guest.first_name,
                    LastName = guest.last_name,
                    Email = guest.email,
                    CategoryCode = guest.category_code
                };
                guestList.Add(guestDto);
            }
            return guestList;
        }

        public static List<CategoryDto> ConvertCategoryTblToDto(List<Tbl_category> categories)
        {
            List<CategoryDto> categoryDtos=new List<CategoryDto>();
            foreach (var c in categories)
            {
                CategoryDto category = new CategoryDto
                {
                    Id = c.id,
                    Description = c.description,
                    TypeId = c.type_id
                };
                categoryDtos.Add(category);
            }
            return categoryDtos;
        }
    }
}
