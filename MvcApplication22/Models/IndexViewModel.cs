using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PeopleSearch.Data;

namespace MvcApplication22.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Person> People { get; set; }
        public string LastName { get; set; }
    }
}