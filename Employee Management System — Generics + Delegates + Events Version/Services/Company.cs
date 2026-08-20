using Employee_Management_System___Generics___Delegates___Events_Version.Common;
using Employee_Management_System___Generics___Delegates___Events_Version.Delegates;
using Employee_Management_System___Generics___Delegates___Events_Version.Event;
using Employee_Management_System___Generics___Delegates___Events_Version.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System___Generics___Delegates___Events_Version.Services
{
    internal class Company
    {
        #region Event
        public event EventHandler<EmployeeEventArgs> EmployeeOnBoarding;
        public event EventHandler<EmployeeEventArgs> EmployeeOnPromoting;
        #endregion

        #region Collections
        List<Employee> ActiveEmployees = new List<Employee>();

        Dictionary<int, Department> Departments = new Dictionary<int, Department>();

        HashSet<string> Skills = new HashSet<string>();

        Queue<Employee> OnBoarding = new Queue<Employee>();
        Stack<string> HistoryAction = new Stack<string>();
        #endregion

        //Functions
        public Result<Employee> AddEmployee(Employee emp)
        {
            if (emp == null)
            {
                return Result<Employee>.Failure("Employee Data is NULL");
            }
            if (!Departments.ContainsKey(emp.DepartmentId))
            {
                return Result<Employee>.Failure($"Department -{emp.DepartmentId}- Not exist");
            }
            if (emp.Salary <= 0)
            {
                return Result<Employee>.Failure("Salary Must be greater than Zero ");
            }

            OnBoarding.Enqueue(emp);
            HistoryAction.Push("Apply New Application of an Employee , Under Processing..... ");
            return Result<Employee>.Success(emp, "Successfully Applying Application Form Registration ");
        }

        public Result<Department> AddDepartment(Department dept)
        {
            if (dept == null)
            {
                return Result<Department>.Failure("Department Is Null , Add Vaild Data");
            }

            if (Departments.ContainsKey(dept.id))
            {
                return Result<Department>.Failure("Department Id already registered ");
            }
            Departments[dept.id] = dept;
            HistoryAction.Push("Add new Department to Company ");
            return Result<Department>.Success(dept, "Successfully adding Department To Our Company ");
        }


        public Result<Employee> ProcessEMPQueue()
        {
            if (OnBoarding.Count == 0)
            {
                return Result<Employee>.Failure($"Process Application Is Already Finished ");
            }
            Employee proc_Emp = OnBoarding.Dequeue();
            ActiveEmployees.Add(proc_Emp);
            HistoryAction.Push($"Apply new Employee {proc_Emp.Name} , ADDED to Active Employees ");
            EmployeeOnBoarding?.Invoke(this, new EmployeeEventArgs(proc_Emp, $"Employee {proc_Emp.Name} Applied to work in  Department{proc_Emp.DepartmentId} "));
            return Result<Employee>.Success(proc_Emp, "Employee Added To Active Employees  ");

        }




        public Result<string> AddSkill(string skill)
        {
            if (string.IsNullOrEmpty(skill))
            {
                return Result<string>.Failure($"Error:Skill Name is Empty ");
            }
            string nSkill = skill.Trim().ToLower();

            if (Skills.Add(nSkill))
            {
                HistoryAction.Push($"Skill '{skill}' Added To Unique Skills");
                return Result<string>.Success(nSkill,"Skill  Added Successfully.");
            }
            else
            {
                return Result<string>.Failure("Skill already Added.");
            }
        }

        public Result<List<Employee>> FilterEmployees(EmployeeFilter filter)
        {
            List<Employee> result = new();

            if (filter == null)
            {
                return Result<List<Employee>>.Failure("Filter Condition not Provided ");
            }

            foreach (var emp in ActiveEmployees)
            {
                if (filter(emp))
                {
                    result.Add(emp);
                }
            }
            return Result<List<Employee>>.Success(result,$"Found {result.Count} Matching Employee ");
        }

        #region Search/Filter [Manual] Function
        public Result<Employee> SearchById(int id)
        {
            for (int i = 0; i < ActiveEmployees.Count; i++)
            {
                if (ActiveEmployees[i].Id == id)
                {
                    //  return ActiveEmployees[i];
                    return Result<Employee>.Success(ActiveEmployees[i], $"Employee With Id {id} Has Been Founded");
                }

            }
            return Result<Employee>.Failure("Employee Not Found");
        }


        public Result<Employee> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                //Console.WriteLine("Enter Vaild Name ");
                return Result<Employee>.Failure("Enter Vaild Name");
            }

            for (int i = 0; i < ActiveEmployees.Count; i++)
            {
                if (string.Equals(ActiveEmployees[i].Name?.Trim(), name.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return Result<Employee>.Success(ActiveEmployees[i], $"Employee {name} Has Been Founded"); 
                }

            }
            return Result<Employee>.Failure("Employee Not Found");
        }
        #endregion
        public Result<Mangaer> PromotionToManger(Employee employee)
        {
            Result<Employee> verifyEmp = SearchById(employee.Id);

            if (!verifyEmp.IsSuccess)
            {
                return Result<Mangaer>.Failure("Cannot Promote Employee Becuase he is not in Active Employees ");
            }


            if (verifyEmp.data is Mangaer)
            {
                return Result<Mangaer>.Failure("Cannot Promote Employee Becuase he isAlready Manager ");
            }

            Mangaer manager = new Mangaer
            {
                Name = verifyEmp.data.Name,
                Id = verifyEmp.data.Id,
                DepartmentId = verifyEmp.data.DepartmentId,
                Salary = verifyEmp.data.Salary + 10000M,
                HireDate = verifyEmp.data.HireDate
            };

            int index = ActiveEmployees.IndexOf(verifyEmp.data);
            ActiveEmployees[index] = manager;


            HistoryAction.Push($"Promoted {manager.Name} to Manager.");
            EmployeeOnPromoting?.Invoke(this, new EmployeeEventArgs(manager, $"Employee {employee.Name} has Promoted To Manager of Department {manager.DepartmentId}"));
            return Result<Mangaer>.Success(manager, $"SuccessFully Promoted Employee {employee.Name} To Manager");

        }


        #region Departmnet Reports
        public Result< List<Employee>> ShowDepartmentEmployees(int deptId)
        {
            if (!Departments.ContainsKey(deptId))
            {
                return Result < List<Employee>>.Failure("Department Id Not Found ");
            }

            List<Employee> result = new List<Employee>();

            for (int i = 0; i < ActiveEmployees.Count; i++)
            {
                if (ActiveEmployees[i].DepartmentId == deptId)
                {
                    result.Add(ActiveEmployees[i]);
                }
            }


            return Result<List<Employee>>.Success(result, $"Found {result.Count} employee(s) in department '{Departments[deptId].Name}'.");

        }



        public Result<Dictionary<string,int>> GetDepartmentEmployeeCounts()
        {
            if (Departments.Count == 0)
            {
                
                return Result<Dictionary<string,int>>.Failure("There No Departments In our Company");
            }

            Dictionary<string, int> reprort = new Dictionary<string, int>();
            foreach (var item in Departments.Values)
            {
                int count = 0;

                for (int i = 0; i < ActiveEmployees.Count; i++)
                {
                    if (ActiveEmployees[i].DepartmentId == item.id)
                    {
                        count++;
                    }
                }

                reprort.Add(item.Name, count);
               
            }

            return Result<Dictionary<string, int>>.Success(reprort, "Department Repoert Counts For Empolyees Generated Successfully ");
        }
        #endregion


        public Result<decimal> CalculateAverageSalary()
        {
            if (ActiveEmployees.Count == 0)
            {
                return Result<decimal>.Failure("No active employees to calculate average salary.");
            }

            decimal totalSalary = 0;

           
            for (int i = 0; i < ActiveEmployees.Count; i++)
            {
                totalSalary += ActiveEmployees[i].Salary;
            }

            decimal average = totalSalary / ActiveEmployees.Count;

            return Result<decimal>.Success(average, $"Average salary calculated successfully: {average}");
        }


        public Result<List<string>> ShowActionHistory()
        {
            if(HistoryAction.Count==0)
            {
                return Result<List<string>>.Failure("No actions recorded yet .");
            }
            List<string> historyresult = new List<string>(HistoryAction);


            return Result<List<string>>.Success(historyresult, "Sucessfully retrived action History ");
        }



        public Result<HashSet<string>> ShowUniqueSkills()
        {
            if (Skills.Count == 0)
            {
               
                return Result<HashSet<string>>.Failure("No Skills In Our Department Right Now ");
            }
            
            return Result<HashSet<string>>.Success(Skills, "Sucessfully retrived unique Skills ");
        }

        public Result<List<Employee>> GetAllActiveEmployees()
        {
            if (ActiveEmployees.Count == 0)
            {
                return Result<List<Employee>>.Failure("No active employees currently registered.");
            }

            return Result<List<Employee>>.Success(new List<Employee>(ActiveEmployees), "Active employees retrieved successfully.");
        }


    }
}
