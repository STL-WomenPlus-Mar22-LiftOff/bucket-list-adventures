using System;
using BucketListAdventures.Models;
using Microsoft.EntityFrameworkCore;



namespace BucketListAdventures.Data
{
    public class BucketListDbContext : DbContext
    { 
        public BucketListDbContext(DbContextOptions<BucketListDbContext> options) : base(options) 
        { 
        } 
    }
  
} 



//NEED TO FIX THIS BUT JUST STARTED FOR PURPOSE OF STARTING SET UP FOR THE DATABASE