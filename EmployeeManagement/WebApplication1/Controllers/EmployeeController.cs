using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Model;
using WebApplication1.Services.EmployeeService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _employeeService.LoadEmployeesFromFile();
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id ) 
        {
            var emp = _employeeService.GetById(id);
            if (emp!= null) { return Ok(emp); }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddEmployee (Employee employee)
        {
            _employeeService.Add(employee);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if(_employeeService.GetById(id) == null)
                return NotFound("not Found Employee");
            _employeeService.Delete(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] UpdateEmployeeDto employeeDto)
        {
            if(_employeeService.GetById(id) == null)
                return NotFound("not Found Employee");
            _employeeService.Update(id,employeeDto);
            return Ok();
        }
        [HttpGet("designation/{designation}")]
        public IActionResult GetByDesignation(string designation)
        {
            var empsWithDesignation = _employeeService.GetByDesignation(designation);
            return Ok(empsWithDesignation);
        }
        [HttpGet("language-experts")]
        public IActionResult GetLanguageExperts([FromQuery] string language, [FromQuery] int score)
        {
            var employees = _employeeService.GetByEmployeesWithScore(language, score);
            return Ok(employees);
        }

    }
}
