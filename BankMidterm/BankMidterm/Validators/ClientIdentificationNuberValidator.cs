using BankMidterm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankMidterm.Validators
{
    public class ClientIdentificationNuberValidator: AdditionalValidator<Client>
    {
        private DbSet<Client> Clients;

        public ClientIdentificationNuberValidator(DbSet<Client> clients)
        {
            Clients = clients;
        }

        public ModelStateDictionary check(Client entity, ModelStateDictionary modelState)
        {
            Client existingClient = Clients.ToList().Find(c => c.IdentityNumber == entity.IdentityNumber && c.Id != entity.Id);
            if (existingClient != null)
            {
                modelState.AddModelError("IdentityNumber", "Client with given IdentityNumber already exists");
                return modelState;
            } 
            else
            {
                return modelState;
            }
        }
    }
}