using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Models
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public override string ToString()
        {
            return $"-Name : {Name}\n-Id : {Id}\n-DepartmentId : {DepartmentId}\n-Salary : {Salary}\n-HireDate : {HireDate}";
        }
    }
}
