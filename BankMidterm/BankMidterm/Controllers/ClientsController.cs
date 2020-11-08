using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BankMidterm.Dto.Request;
using BankMidterm.Models;
using BankMidterm.Repositories;
using BankMidterm.Validators;

namespace BankMidterm.Controllers
{
    public class ClientsController : Controller
    {
        private BankDatabase database = new BankDatabase();
        private ClientRepository clientRepository;
        private List<AdditionalValidator<Client>> additionalValidators;

        public ClientsController()
        {
            this.additionalValidators = new List<AdditionalValidator<Client>>
            {
                new ClientAgeValidator(),
                new ClientIdentificationNuberValidator(database.Clients)
            };
            this.clientRepository = new ClientRepository(database);
        }



        // GET: Clients
        public ActionResult Index()
        {
            return View(database.Clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = database.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.Cities = database.Cities.ToList();
            ViewBag.Countries = database.Countries.ToList();
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,gender,IdentityNumber,BirthDate,telephone,Email,CountryId,CityId,VoucherId,VoucherRelationToClient")] AddClientRequest request)
        {
           
            Voucher voucher = new Voucher(request.VoucherRelationToClient, request.VoucherId);
            database.Vouchers.Add(voucher);

            Client client = mapRequestToClient<AddClientRequest>(request);
            client.Country = database.Countries.Find(request.CountryId);
            client.City = database.Cities.Find(request.CityId);
            client.Voucher = voucher;

            performAdditionalValidation(ModelState, client);
            if (ModelState.IsValid)
            {
                database.Clients.Add(client);
                database.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = database.Cities.ToList();
            ViewBag.Countries = database.Countries.ToList();
            ViewBag.Errors = ModelState.Values;
            return View(client);
        }

        private Client mapRequestToClient<T>(T request)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<T, Client>();
            });

            return config.CreateMapper().Map<T, Client>(request);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = database.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,IdentityNumber,telephone,Email,BirthDate,gender")] EditClientRequest request)
        {
            Client client = mapRequestToClient<EditClientRequest>(request);

            performAdditionalValidation(ModelState, client);
            if (ModelState.IsValid)
            {
                clientRepository.update(client, request.Id);
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = database.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = database.Clients.Find(id);
            database.Clients.Remove(client);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }

        private void performAdditionalValidation(ModelStateDictionary modelState, Client author)
        {
            foreach (AdditionalValidator<Client> validator in additionalValidators)
            {
                modelState = validator.check(author, modelState);
            }
        }
    }
}
