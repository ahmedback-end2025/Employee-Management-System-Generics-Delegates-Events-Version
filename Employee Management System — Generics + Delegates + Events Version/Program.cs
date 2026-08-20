using Employee_Management_System___Generics___Delegates___Events_Version.Models;
using Employee_Management_System___Generics___Delegates___Events_Version.Services;

namespace Employee_Management_System___Generics___Delegates___Events_Version
{
    public class Program
    {
        static void SeedData(Company company)
        {
           
            company.AddDepartment(new Department { id = 1, Name = "Software Development" });
            company.AddDepartment(new Department { id = 2, Name = "Human Resources" });
            company.AddDepartment(new Department { id = 3, Name = "Quality Assurance" });

            
            company.AddSkill("C#");
            company.AddSkill("SQL");
            company.AddSkill("OOP");
            company.AddSkill("Problem Solving");

           
            company.AddEmployee(new Employee { Name = "Ahmed Ali", DepartmentId = 1 });
            company.AddEmployee(new Employee { Name = "Sara Mohamed", DepartmentId = 2 });
            company.AddEmployee(new Employee { Name = "Omar Hassan", DepartmentId = 1 });
            company.AddEmployee(new Employee { Name = "Mona Youssef", DepartmentId = 3 });

            
            //company.ProcessEMPQueue();
            //company.ProcessEMPQueue();
            //company.ProcessEMPQueue();
        }

        static void Main(string[] args)
        {
            Company MyCompany = new Company();

      
            SeedData(MyCompany);

            MyCompany.EmployeeOnBoarding += (sender, e) =>
            {
                Console.WriteLine($"[Notification] {e.message}");
            };

            MyCompany.EmployeeOnPromoting += (sender, e) =>
            {
                Console.WriteLine($"[Notification] {e.message}");
            };

            do
            {
                Console.WriteLine("==================================");
                Console.WriteLine("   EMPLOYEE MANAGEMENT SYSTEM     ");
                Console.WriteLine("==================================");
                Console.WriteLine(" 1- Apply New Employee");
                Console.WriteLine(" 2- Acceptance List");
                Console.WriteLine(" 3- Add Department");
                Console.WriteLine(" 4- Add Unique Skill to Company");
                Console.WriteLine(" 5- Search Employee by ID");
                Console.WriteLine(" 6- Search Employee by Name");
                Console.WriteLine(" 7- Promote Employee to Manager");
                Console.WriteLine(" 8- View All Active Employees");
                Console.WriteLine(" 9- View Employees in a Specific Department");
                Console.WriteLine("10- View Department Employee Counts Report");
                Console.WriteLine("11- Calculate Average Salary for Active Employees");
                Console.WriteLine("12- View Unique Skills in Company");
                Console.WriteLine("13- View Action History");
                Console.WriteLine("14- Other Options");
                Console.WriteLine(" 0- Exit");

                try
                {
                    Console.Write("Enter Your Choice: ");
                    int input1 = int.Parse(Console.ReadLine()!);

                    switch (input1)
                    {
                        case 0:
                            Console.WriteLine("====================");
                            Console.WriteLine("  Exiting Program   ");
                            Console.WriteLine("====================");
                            return;

                        case 1:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("  Job Application   ");
                            Console.WriteLine("====================");

                            Employee emp = new Employee();
                            Console.Write("Enter Name : ");
                            emp.Name = Console.ReadLine()!;
                            Console.Write("Enter Department Id : ");
                            emp.DepartmentId = int.Parse(Console.ReadLine()!);

                            var EmpResult = MyCompany.AddEmployee(emp);
                            if (EmpResult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(EmpResult.Message);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(EmpResult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 2:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("  Acceptance List   ");
                            Console.WriteLine("====================");

                            while (true)
                            {
                                var procResult = MyCompany.ProcessEMPQueue();
                                if (!procResult.IsSuccess)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(procResult.Message);
                                    Console.ResetColor();
                                    break;
                                }

                                Console.WriteLine(procResult.data.ToString());
                                Console.WriteLine("--------------------------------");
                            }

                            Console.ReadKey();
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("   Add Department   ");
                            Console.WriteLine("====================");

                            Department dept = new Department();
                            Console.Write("Enter Department Name : ");
                            dept.Name = Console.ReadLine()!;
                            Console.Write("Enter Department Id : ");
                            dept.id = int.Parse(Console.ReadLine()!);

                            var DeptResult = MyCompany.AddDepartment(dept);
                            if (DeptResult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(DeptResult.Message);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(DeptResult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("  Add Unique Skill  ");
                            Console.WriteLine("====================");

                            Console.Write("Enter A Unique Skill: ");
                            string skill = Console.ReadLine()!;
                            var skillResult = MyCompany.AddSkill(skill);

                            if (skillResult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(skillResult.Message);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(skillResult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 5:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("Search Employee By ID");
                            Console.WriteLine("====================");

                            Console.Write("Enter Id To Search: ");
                            int id = int.Parse(Console.ReadLine()!);
                            var empresultID = MyCompany.SearchById(id);

                            if (empresultID.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(empresultID.Message);
                                Console.ResetColor();
                                Console.WriteLine(empresultID.data.ToString());
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(empresultID.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 6:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("Search Employee By Name");
                            Console.WriteLine("====================");

                            Console.Write("Enter Name To Search: ");
                            string name = Console.ReadLine()!;
                            var empresult = MyCompany.SearchByName(name);

                            if (empresult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(empresult.Message);
                                Console.ResetColor();
                                Console.WriteLine(empresult.data.ToString());
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(empresult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 7:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("Promote To Manager  ");
                            Console.WriteLine("====================");

                            Console.Write("Enter Employee Id : ");
                            int PromotionId = int.Parse(Console.ReadLine()!);
                            var PromotionResult = MyCompany.PromotionToManger(PromotionId);

                            if (PromotionResult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(PromotionResult.Message);
                                Console.ResetColor();
                                Console.WriteLine("\n--- Manager Details ---");
                                Console.WriteLine(PromotionResult.data.ToString());
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(PromotionResult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 8:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("All Active Employees");
                            Console.WriteLine("====================");

                            var allActiveResult = MyCompany.GetAllActiveEmployees();
                            if (allActiveResult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(allActiveResult.Message);
                                Console.ResetColor();
                                foreach (var item in allActiveResult.data)
                                {
                                    Console.WriteLine(item.ToString());
                                    Console.WriteLine("--------------------------------");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(allActiveResult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 9:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("Department Employees");
                            Console.WriteLine("====================");

                            Console.Write("Enter Department Id: ");
                            int deptId = int.Parse(Console.ReadLine()!);
                            var ResultEmployees = MyCompany.ShowDepartmentEmployees(deptId);

                            if (ResultEmployees.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(ResultEmployees.Message);
                                Console.ResetColor();
                                foreach (var item in ResultEmployees.data)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ResultEmployees.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 10:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("Employees Count/Dept");
                            Console.WriteLine("====================");

                            var ResultCount = MyCompany.GetDepartmentEmployeeCounts();
                            if (ResultCount.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(ResultCount.Message);
                                Console.ResetColor();
                                foreach (var item in ResultCount.data)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(ResultCount.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 11:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("   Average Salary   ");
                            Console.WriteLine("====================");

                            var Averageresult = MyCompany.CalculateAverageSalary();
                            if (Averageresult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(Averageresult.Message);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Averageresult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 12:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("   Company Skills   ");
                            Console.WriteLine("====================");

                            var uniqueResult = MyCompany.ShowUniqueSkills();
                            if (uniqueResult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(uniqueResult.Message);
                                Console.ResetColor();
                                foreach (var item in uniqueResult.data)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(uniqueResult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 13:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("   Action History   ");
                            Console.WriteLine("====================");

                            var historyresult = MyCompany.ShowActionHistory();
                            if (historyresult.IsSuccess)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(historyresult.Message);
                                Console.ResetColor();
                                foreach (var item in historyresult.data)
                                {
                                    Console.WriteLine("-" + item);
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(historyresult.Message);
                                Console.ResetColor();
                            }

                            Console.ReadKey();
                            break;

                        case 14:
                            Console.Clear();
                            Console.WriteLine("====================");
                            Console.WriteLine("   Other Options    ");
                            Console.WriteLine("====================");
                            Console.WriteLine("1- Filter Employees by Salary");
                            Console.WriteLine("2- Filter Employees by Department ID");
                            Console.WriteLine("3- Filter Managers Only");
                            Console.WriteLine("0- Back to Main Menu");
                            Console.WriteLine("====================");
                            Console.Write("Enter Your Choice: ");
                            int input2 = int.Parse(Console.ReadLine()!);

                            switch (input2)
                            {
                                case 0:
                                    
                                    break;

                                case 1:
                                    Console.Write("Enter Salary: ");
                                    decimal salary_ = decimal.Parse(Console.ReadLine()!);
                                    var filterResult = MyCompany.FilterEmployees(e => e.Salary > salary_);

                                    if (filterResult.IsSuccess)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(filterResult.Message);
                                        Console.ResetColor();
                                        foreach (var item in filterResult.data)
                                        {
                                            Console.WriteLine("-" + item);
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(filterResult.Message);
                                        Console.ResetColor();
                                    }
                                    Console.ReadKey();
                                    break;

                                case 2:
                                    Console.Write("Enter Department Id: ");
                                    int DeptId = int.Parse(Console.ReadLine()!);
                                    var filterResult2 = MyCompany.FilterEmployees(e => e.DepartmentId == DeptId);

                                    if (filterResult2.IsSuccess)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(filterResult2.Message);
                                        Console.ResetColor();
                                        foreach (var item in filterResult2.data)
                                        {
                                            Console.WriteLine("-" + item);
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(filterResult2.Message);
                                        Console.ResetColor();
                                    }
                                    Console.ReadKey();
                                    break;

                                case 3:
                                    var filterResult3 = MyCompany.FilterEmployees(e => e is Mangaer);

                                    if (filterResult3.IsSuccess)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(filterResult3.Message);
                                        Console.ResetColor();
                                        foreach (var item in filterResult3.data)
                                        {
                                            Console.WriteLine("-" + item);
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(filterResult3.Message);
                                        Console.ResetColor();
                                    }
                                    Console.ReadKey();
                                    break;

                                default:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid Option Selected.");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    break;
                            }
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Choice.");
                            Console.ResetColor();
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.ReadKey();
                }

                Console.Write("\nDo you Want to return Main Page ? [Y/N] : ");
                string answer = Console.ReadLine()!;
                if (answer.ToLower() == "n")
                {
                    break;
                }

                Console.Clear();
            } while (true);
        }
    }
}