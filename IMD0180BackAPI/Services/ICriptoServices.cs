namespace IMD0180BackAPI.Services
{
    public interface ICriptoServices
    {
        string Hash(string original, string salt);
    }
}
