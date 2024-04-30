namespace MVCStartApp.Models.Db
{
    public interface IRequestRepository
    {
        Task Log(Request request);
        Task<Request[]> GetRequests();
    }
}
