using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.ConcerEntity
{
    public class SalaryConcertEntity : Salary
    {
        public string StaffName { get; set; }
        public int AppendantPeople { get; set; }
    }
}