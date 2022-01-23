using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class IssuedBooks
    {
        public int Id { get; set; }

        public int IssueId { get; set; }

        public string IssuedTo { get; set; }

        public int BookId { get; set; }

        public virtual Books books { get; set; }

        public int Period { get; set; }

        public DateTime IssuedDate { get; set; }

        public bool IsReturned { get; set; }

    }
}