using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BucketListAdventures.Models
{
    public class Destination
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string? LocationId { get; set; }


        public Destination(string name, string location, string description, string? locationId)
        {
            Name = name;
            Location = location;
            Description = description;
            LocationId = locationId;
        }

        public Destination() { }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Destination destination &&
                   Id == destination.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}