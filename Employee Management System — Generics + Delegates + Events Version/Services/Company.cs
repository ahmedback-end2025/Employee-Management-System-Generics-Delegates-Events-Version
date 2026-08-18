using Employee_Management_System___Generics___Delegates___Events_Version.Common;
using Employee_Management_System___Generics___Delegates___Events_Version.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Services
{
    internal class Company
    {
         List<Employee> ActiveEmployees = new List<Employee>();

         Dictionary<int, Department> Departments = new Dictionary<int, Department>();

         HashSet<string> Skills = new HashSet<string>();

         Queue<Employee> OnBoarding = new Queue<Employee>();
         Stack<string> HistoryAction = new Stack<string>();



        public Result<Employee> AddEmployee(Employee emp)
        {
            if (emp==null)
            {
                return Result<Employee>.Failure("Employee Data is NULL"); 
            }
            if(Departments.ContainsKey(emp.DepartmentId))
            {
                return Result<Employee>.Failure($"Department -{emp.DepartmentId}- Not exist");
            }
            if(emp.Salary<=0)
            {
                return Result<Employee>.Failure("Salary Must be greater than Zero ");
            }

            OnBoarding.Enqueue(emp);
            HistoryAction.Push("Apply New Application of an Employee , Under Processing..... ");
            return Result<Employee>.Success(emp, "Successfully Applying Application Form Registration ");
        }

        public Result<Department> Department(Department dept)
        {
            if (dept==null)
            {
                return Result<Department>.Failure("Department Is Null , Add Vaild Data");
            }
            Departments[dept.id] = dept;
            HistoryAction.Push("Add new Department to Company ");
            return Result<Department>.Success(dept, "Successfully adding Department To Our Company ");
        }


        public Result<Employee> ProcessEMPQueue()
        {
            if (OnBoarding.Count == 0)
            {
               return  Result<Employee>.Failure($"Process Application Is Already Finished ");
            }
            Employee proc_Emp = OnBoarding.Dequeue();
            ActiveEmployees.Add(proc_Emp);
            HistoryAction.Push($"Apply new Employee {proc_Emp.Name} , ADDED to Active Employees ");
            return Result<Employee>.Success(proc_Emp, "Employee Added To Active Employees  ");

        }


        public Result<string>  AddSkill(string skill)
        {
            if (string.IsNullOrEmpty(skill))
            {
                return  Result<string>.Failure($"Error:Skill Name is Empty ");
            }
            string nSkill = skill.Trim().ToLower();

            if(Skills.Add(nSkill))
            {
                return Result<string>.Success("Skill  Added Successfully.");
            }
            else
            {
                return Result<string>.Failure("Skill already Added.");
            }
        }


    }
}
