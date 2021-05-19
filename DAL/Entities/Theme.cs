using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Theme
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
