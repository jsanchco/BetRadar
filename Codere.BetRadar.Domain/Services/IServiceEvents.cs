namespace Codere.BetRadar.Domain.Services
{   
    #region Using

    using Codere.BetRadar.Domain.Entities;

    #endregion

    public interface IServiceEvents
    {
        ListEvents GetEvents();
        Event GetEvent(int eventId);
    }
}
