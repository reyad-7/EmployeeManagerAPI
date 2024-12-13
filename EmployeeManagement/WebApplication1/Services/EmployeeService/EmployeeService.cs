using System.Text.Json;
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        
        private const string JsonFilePath = "employees.json";
        private List<Employee> Employees;
        
        public void LoadEmployeesFromFile()
        {
            if (File.Exists(JsonFilePath))
            {
                string jsonData = File.ReadAllText(JsonFilePath);
                Employees = JsonSerializer.Deserialize<List<Employee>>(jsonData) ?? new List<Employee>();
            }
            else
            {
                Employees = new List<Employee>();
            }
        }
        public void save()
        {
            string jsonData = JsonSerializer.Serialize(Employees, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonFilePath, jsonData);
        }
        public void Add(Employee employee)
        {
            Employees.Add(employee);
            save();
        }

        public void Delete(int employeeId)
        {
            Employees.RemoveAll(e => e.EmployeeID == employeeId);
            save();
        }
        public List<Employee> GetAll()
        {
            return Employees;
        }

        public Employee GetById(int employeeId)
        {
            Employee employee = Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
            return employee;
        }

        public void Update(int empId, UpdateEmployeeDto employeeDto)
        {
            Employee existingEmployee = Employees.FirstOrDefault(e => e.EmployeeID == empId);
            if (existingEmployee != null)
            {
                if (!string.IsNullOrEmpty(employeeDto.FirstName))
                    existingEmployee.FirstName = employeeDto.FirstName;

                if (!string.IsNullOrEmpty(employeeDto.LastName))
                    existingEmployee.LastName = employeeDto.LastName;

                if (!string.IsNullOrEmpty(employeeDto.Designation))
                    existingEmployee.Designation = employeeDto.Designation;
                if (employeeDto.KnownLanguages != null && employeeDto.KnownLanguages.Any())
                {
                    existingEmployee.KnownLanguages.Clear();
                    foreach(var languge in employeeDto.KnownLanguages)
                    {
                        var newLanguge = new KnownLanguage
                        {
                            LanguageName = languge.LanguageName,
                            ScoreOutof100 = languge.ScoreOutof100
                        };
                        existingEmployee.KnownLanguages.Add(newLanguge);
                    }
                }
                save();
            }
        }

        public List<Employee> GetByDesignation(string Designation)
        {
            List<Employee> employeesWithDesignation = Employees.Where(e => e.Designation.Equals(Designation, StringComparison.OrdinalIgnoreCase)).ToList();
            return employeesWithDesignation;
        }

        public List<Employee> GetByEmployeesWithScore(string language, int score)
        {
            List<Employee> employeesWithhighScore = Employees
                                                    .Where(e => e.KnownLanguages
                                                    .Any(l => l.LanguageName.Equals(language, StringComparison.OrdinalIgnoreCase) && l.ScoreOutof100 >= score))
                                                    .OrderBy(e => e.EmployeeID)
                                                    .ToList();
            return employeesWithhighScore;
        }
    }
}
