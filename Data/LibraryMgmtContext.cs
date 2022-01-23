using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public class LibraryMgmtContext: DbContext
    {
        public DbSet<Books> books { get; set; }
        public DbSet<Member> members { get; set; }

        public DbSet<IssuedBooks> issuedBooks { get; set; }
    }
}