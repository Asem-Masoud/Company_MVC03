
namespace Company_MVC03.PL.Services
{
    public class ScopedServices : IScopedService
    {
        public ScopedServices()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
