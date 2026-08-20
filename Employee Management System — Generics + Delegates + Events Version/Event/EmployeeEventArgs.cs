using Employee_Management_System___Generics___Delegates___Events_Version.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Event
{
    internal class EmployeeEventArgs:EventArgs
    {
        public Employee employee { get; }

        public string message { get; }
        public DateTime time { get; }

        public EmployeeEventArgs(Employee _emp , string _message )
        {
            employee = _emp;
            message = _message;
            time = DateTime.Now;
        }
    }
}
