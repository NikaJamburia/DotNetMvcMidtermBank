
using BankMidterm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankMidterm.Validators
{
    public class ClientAgeValidator : AdditionalValidator<Client>
    {
        private Dictionary<ClientGender, int> pensionAges = new Dictionary<ClientGender, int>()
            {
                {ClientGender.MALE, 65 },
                {ClientGender.FEMALE, 60 },
                {ClientGender.HELICOPTER, 1000 }
            };
        public ModelStateDictionary check(Client client, ModelStateDictionary modelState)
        {
           

            if (IsOlderThen18(client) && isNotOnPension(client))
            {
                return modelState;
            }
            else
            {
                modelState.AddModelError("BirthDate", "Client must be older then 18 and younger then " + pensionAges[client.gender]);
                return modelState;
            }
        }

        private bool isNotOnPension(Client client)
        {
            return DateTime.Now.Year < client.BirthDate.Year + pensionAges[client.gender];
        }

        private static bool IsOlderThen18(Client entity)
        {
            return DateTime.Now.Year > entity.BirthDate.Year + 18;
        }
    }
}