namespace WebApplication1.Model
{
    public class KnownLanguage
    {
        public string LanguageName { get; set; }
        public int ScoreOutof100 { get; set; }


    }
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public List <KnownLanguage> KnownLanguages { get; set; }
    }
}
