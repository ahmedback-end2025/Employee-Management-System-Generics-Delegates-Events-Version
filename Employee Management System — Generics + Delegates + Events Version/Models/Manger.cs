using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Models
{
    public class Mangaer:Employee
    {
        public List<Employee> TeamMembers = new List<Employee>();


        public Mangaer():base()
        {
            
        }

        public Mangaer(int existId):base(existId)
        {
            
        }
    }
}
