namespace PersonalPlanung.Core.Model
{
    public class Status : ValueObject<Person>
    {
        public static Status Rentner = new Status("Rentner");
        public static Status Student = new Status("Student");
        public static Status Kollege = new Status("Kollege");
        public static Status Dienstleister = new Status("Dienstleister");

        public Status(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}