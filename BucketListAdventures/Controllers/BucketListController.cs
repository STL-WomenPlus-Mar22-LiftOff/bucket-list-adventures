/* FEEL FREE TO DELETE THIS FILE IN YOUR PERSONAL FORKS: 
 * BucketListController.cs, BucketList.cs, AddBucketListViewModel.cs
 * and the BucketList folder in Views
 * These files were created but never worked on or finished before 
 * the end of the team capstone project for LiftOff. 
 * I plan to work with these files in my own personal fork & I
 * can add them to the team project once completed
 * (The files will in the future be used to create multiple Bucket Lists
 * that are categorized by a location and possibly categorize lists)
 * DELETING THE FILES MENTIONED ABOVE WILL NOT EFFECT THE APPLICATION AS THE FEATURE IS YET TO BE INTEGRATED.
 * -Natasha-
 * --------------------------------------------------------------------

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
    public class BucketListController : Controller
    {
        static private List<Destination> Destinations = new List<Destination>();

        //GET: /<controllers>
        [HttpGet]
        public IActionResult Index()
        {
            List<Destination> destinations = new List<Destination>(DestinationData.GetAll());

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

                DestinationData.Add(newDestination);

                return Redirect("/Destinations");
            }

            return View(addDestinationViewModel);
        }

        public IActionResult Delete()
        {
            List<Destination> destinations = new List<Destination>(DestinationData.GetAll());

            return View(destinations);
        }

        [HttpPost]
        [Route("/Destinations/Delete")]
        public IActionResult Delete(int[] destinationIds)
        {
            foreach (int destinationId in destinationIds)
            {
                DestinationData.Remove(destinationId);
            }

            return Redirect("/Events");
        }

        [HttpGet]
        [Route("Destinations/Edit/{destinationId}")]
        public IActionResult Edit(int destinationId)
        {
            Destination editingDestination = DestinationData.GetById(destinationId);
            ViewBag.destinationToEdit = editingDestination;
            ViewBag.title = "Edit Destination " + editingDestination.Name + "(id = " + editingDestination.Id + ")";

            return View();
        }

        [HttpPost]
        [Route("Events/Edit")]
        public IActionResult SubmitEditEventForm(int destinationId, string name, string description)
        {
            Destination editingDestination = DestinationData.GetById(destinationId);
            editingDestination.Name = name;
            editingDestination.Description = description;

            return Redirect("/Destination");
        }
    }
}
*/
