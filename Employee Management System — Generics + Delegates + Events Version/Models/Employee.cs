using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Models
{
    public class Employee
    {
        public static  int next_id =1;
        public Employee()
        {
            Id = next_id++;
            Salary = 10000M;
        }
        public Employee(int existId)
        {
            Id = existId;
        }
        public int Id { get; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public override string ToString()
        {
            return $"-Name : {Name}\n-Id : {Id}\n-DepartmentId : {DepartmentId}\n-Salary : {Salary}\n-HireDate : {HireDate}";
        }
    }
}
