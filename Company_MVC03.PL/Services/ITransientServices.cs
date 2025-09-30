namespace Company_MVC03.PL.Services
{
    public interface ITransientServices
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
