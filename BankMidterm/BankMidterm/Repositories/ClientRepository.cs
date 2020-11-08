
using BankMidterm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankMidterm.Repositories
{
    public class ClientRepository : CrudRepository<Client>
    {
        private BankDatabase Database;

        public ClientRepository(BankDatabase db)
        {
            Database = db;
        }

        public bool delete(int id)
        {
            try
            {
                Database.Clients.Remove(getById(id));
                Database.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool exists(int id)
        {
            if (Database.Clients.Find(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Client> getAll()
        {
            List<Client> clients = Database.Clients.ToList();
            foreach (Client client in clients)
            {
                Database.Entry(client).Reference(a => a.Voucher).Load();
                Database.Entry(client).Reference(a => a.City).Load();
                Database.Entry(client).Reference(a => a.Country).Load();
            }

            return clients;
        }

        public Client getById(int id)
        {
            try
            {
                Client client = Database.Clients.Find(id);
                Database.Entry(client).Reference(a => a.Voucher).Load();
                Database.Entry(client).Reference(a => a.City).Load();
                Database.Entry(client).Reference(a => a.Country).Load();
                return client;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Client save(Client entity)
        {
            City city = Database.Cities.Find(entity.City.Id);
            Country country = Database.Countries.Find(entity.Country.Id);

            if (city != null && country != null)
            {
                entity.City = city;
                entity.Country = country;
                Client newAuthor = Database.Clients.Add(entity);
                Database.SaveChanges();
                return newAuthor;
            }
            else
            {
                throw new Exception("city or country not found");
            }

        }

        public Client update(Client updatedEntity, int id)
        {
            Client clientToUpdate = getById(id);
            try
            {
                Database.Entry(clientToUpdate).CurrentValues.SetValues(updatedEntity);
                Database.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            return updatedEntity;
        }
    }
}