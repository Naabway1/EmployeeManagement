using EmployeeManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    internal class Employee : ObservableObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Position { get; set; }

        public Employee()
        {
            Birthday = Birthday.Date;
        }
    }
}
