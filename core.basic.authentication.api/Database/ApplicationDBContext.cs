﻿using core.basic.authentication.api.Models;
using Microsoft.EntityFrameworkCore;

namespace core.basic.authentication.api.Database
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            :base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
