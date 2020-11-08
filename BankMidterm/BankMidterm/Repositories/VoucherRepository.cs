using BankMidterm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankMidterm.Repositories
{
    public class VoucherRepository : CrudRepository<Voucher>
    {

        private BankDatabase Database;

        public VoucherRepository(BankDatabase db)
        {
            Database = db;
        }
        public bool delete(int id)
        {
            try
            {
                Database.Vouchers.Remove(getById(id));
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
            if (Database.Vouchers.Find(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Voucher> getAll()
        {
            List<Voucher> vouchers = Database.Vouchers.ToList();
            return vouchers;
        }

        public Voucher getById(int id)
        {
            try
            {
                Voucher voucher = Database.Vouchers.Find(id);
                Database.Entry(voucher).Reference(p => p.client).Load();
                return voucher;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Voucher save(Voucher entity)
        {
            Client client = Database.Clients.Find(entity.client.Id);
            if (client != null)
            {
                //entity.Publisher = publisher;
                Voucher newVoucher = Database.Vouchers.Add(entity);
                Database.SaveChanges();
                return newVoucher;
            }
            else
            {
                throw new Exception("Publisher does not exist");
            }

        }

        public Voucher update(Voucher updatedEntity, int id)
        {
            Voucher voucherToUpdate = getById(id);
            try
            {
                Database.Entry(voucherToUpdate).CurrentValues.SetValues(updatedEntity);
                Database.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return updatedEntity;
        }
    }
}