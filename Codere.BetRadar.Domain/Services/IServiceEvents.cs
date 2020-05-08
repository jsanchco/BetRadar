namespace Codere.BetRadar.Domain.Services
{
    #region Using

    using Codere.BetRadar.Domain.Entities;
    using Codere.BetRadar.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    public interface IServiceEvents
    {
        Task<ListEvents> GetEvents();
        Task<Event> GetEvent(int eventId);
        Task<IList<StatusModel>> GetStatuses();
        Task<Stream> GetStream(string stream_id, string stream_type);
        Task<ListEvents> GetEventsByStreamStatus(Nullable<int> IdStreamStatus);
        Task<ResponseUrlModel> GetAllStream();
        Task<ResponseStreamingInfo> GetStreamingInfo(string idEvent, bool isMovil);
    }
}
