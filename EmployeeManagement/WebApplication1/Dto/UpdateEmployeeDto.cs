using WebApplication1.Model;

namespace WebApplication1.Dto
{
    public class UpdateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public List<KnownLanguage> KnownLanguages { get; set; }
    }

}
