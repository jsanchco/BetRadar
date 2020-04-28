namespace Codere.BetRadar.Domain.Services
{   
    #region Using

    using Codere.BetRadar.Domain.Entities;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceEvents
    {
        Task<ListEvents> GetEvents();
        Task<Event> GetEvent(int eventId);
    }
}
