using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucketListAdventures.Data;
using BucketListAdventures.Models;
using BucketListAdventures.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BucketListAdventures.Controllers
{
    public class DestinationsController : Controller
    {
        private ApplicationDbContext context;
        
        public DestinationsController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Destination> destinations = context.Destinations.ToList();

            return View(destinations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddDestinationViewModel addDestinationViewModel = new AddDestinationViewModel();
            return View(addDestinationViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddDestinationViewModel addDestinationViewModel)
        {
            if (ModelState.IsValid)
            {
                Destination newDestination = new Destination
                {
                    Name = addDestinationViewModel.Name,
                    Location = addDestinationViewModel.Location,
                    Description = addDestinationViewModel.Description,
                };

                context.Destinations.Add(newDestination);
                context.SaveChanges();

                return Redirect("/Destinations");
            }

            return View(addDestinationViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.destinations = context.Destinations.ToList();

            return View();
        }

        [HttpPost]
        [Route("/Destinations/Delete")]
        public IActionResult Delete(int[] destinationIds)
        {
            foreach (int destinationId in destinationIds)
            {
                Destination theDestination = context.Destinations.Find(destinationId);
                context.Destinations.Remove(theDestination);
            }

            context.SaveChanges();

            return Redirect("/Destinations");
        }

        [HttpGet]
        [Route("Destinations/Edit/{destinationId}")]
        public IActionResult Edit(int destinationId)
        {
            Destination editingDestination = context.Destinations.Find(destinationId);
            ViewBag.destinationToEdit = editingDestination;
            ViewBag.title = "Edit Destination " + editingDestination.Name + "(id = " + editingDestination.Id + ")";

            return View();
        }

        [HttpPost]
        [Route("Destinations/Edit")]
        public IActionResult SubmitDestinationForm(int destinationId, string name, string description)
        {
            Destination editingDestination = context.Destinations.Find(destinationId);
            editingDestination.Name = name;
            editingDestination.Description = description;

            context.SaveChanges();

            return Redirect("/Destinations");
        }

    }
}