using QuizNov8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNov8
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeBuisnessLogic logic = new EmployeeBuisnessLogic(new DataAccess());
            Console.WriteLine(logic.ListTopSalaryEmployees());
            Console.ReadLine();
        }


    }
}

public interface IDataAccess
{
    double GetEmployeeSalary(int id);
    List<Employee> GetTop3EmployeesBySalary();
    DateTime GetEmployeeHiringDate(int id);
}

public class DataAccess : IDataAccess
{
    Nov8QuizEntities db = new Nov8QuizEntities();
    public double GetEmployeeSalary(int id)
    {
        return (double)db.Employees.Find(id).Salary;
    }

    public DateTime GetEmployeeHiringDate(int id) => (DateTime)db.Employees.Find(id).HiringDate;

    public List<Employee> GetTop3EmployeesBySalary()
    {
        return db.Employees.OrderByDescending(x => x.Salary).Take(3).ToList();
    }
}


public class EmployeeBuisnessLogic
{
    IDataAccess DataAccess;
    public EmployeeBuisnessLogic(IDataAccess dataAccess)
    {
        DataAccess = dataAccess;
    }
    public double CalcualteBonus(int id)
    {
        float yearsAtCompany = DateTime.Now.Year - DataAccess.GetEmployeeHiringDate(id).Year; //2019-2016=3

        return (DataAccess.GetEmployeeSalary(id) * (yearsAtCompany / 100));
    }

    public string ListTopSalaryEmployees()
    {
        string returnString = "";

        foreach (var employee in DataAccess.GetTop3EmployeesBySalary())
        {
            returnString += "Name:" + employee.Name + " Salary:" + employee.Salary.ToString() + "\n";
        }

        return returnString;
    }
}
