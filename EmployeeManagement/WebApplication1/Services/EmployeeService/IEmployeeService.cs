using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Services.EmployeeService
{
    public interface IEmployeeService
    {
        void LoadEmployeesFromFile();
        void save();
        void Add(Employee employee); // 
        void Delete(int employeeId); // 
        void Update(int empId, UpdateEmployeeDto employeeDto); //
        Employee GetById(int employeeId); // 
        List<Employee> GetAll(); // 
        List<Employee> GetByDesignation(string Designation); //
        List<Employee> GetByEmployeesWithScore(string language,int score); // 

    }
}
