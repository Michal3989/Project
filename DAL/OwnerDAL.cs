using System;
using System.Collections.Generic;
using Common.DTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities;

namespace DAL
{
    public static class OwnerDAL
    {
        /// <summary>
        /// לתשובות של השרת
        /// </summary>
        static int result;

        public static OwnerDto Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return new OwnerDto
                {
                    IsAuthorized = false,
                    ErrorMessage = "לא התקבלו נתונים"
                };
            using (var db = new DBContext())
            {
                try
                {
                    Tbl_event_owner_ owner = db.Tbl_event_owner_.FirstOrDefault(findId => findId.email.Equals(email) && findId.password.Equals(password));
                    if (owner != null)
                        return new OwnerDto
                        {
                            IsAuthorized = true,
                            Email = owner.email,
                            Id = owner.id,
                            FirstName=owner.first_name,
                            LastName=owner.last_name,
                            Password=owner.password,
                            Phone=owner.phone,
                        };
                    return new OwnerDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שם משתמש או סיסמה שגויים"
                    };
                }
                catch (Exception ex)
                {
                    return new OwnerDto
                    {
                        IsAuthorized = false,
                        ErrorMessage = "שגיאה בהתחברות לשרת"
                    };
                }
            }
        }

        public static int AddOwner(OwnerDto newOwner)
        {
            using (var db = new DBContext())
            {
             
                try
                {
                  Tbl_event_owner_  ownerToAdd = Converter.ConvertOwnerDtoToTbl(newOwner);
                    db.Tbl_event_owner_.Add(ownerToAdd);
                    result = db.SaveChanges();
                    if (result == 1)
                   
                        return db.Tbl_event_owner_.Max(e => e.id);
                    
                    else return -1;
                   
                }
                catch (Exception ex)
                {

                    return -1;
                }
                
            }
            //return (result > 0 ? true : false);
        }

    }
}


  