using Codere.BetRadar.Domain.Entities;
using Codere.BetRadar.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codere.BetRadar.Domain.Helpers
{
    public static class Converter
    {
        public static StatusModel ConvertStatusEntityToModel(Status status)
        {
            StatusModel model = new StatusModel();
            model.id = status.id;
            model.name = status.name;
            model.description = status.description;

            return model;
        }

        public static IList<StatusModel> ConvertStatusEntityToModelList(IList<Status> statusList)
        {
            IList<StatusModel> modelList = new List<StatusModel>();
            foreach(Status status in statusList )
            {
                modelList.Add(ConvertStatusEntityToModel(status));
            }

            return modelList;
        }

        public static EventModel ConvertEventEntityToModel(Event entity)
        {
            EventModel model = new EventModel();
            model.id = entity.id;

            return model;
        }

        public static IList<EventModel> ConvertEventEntityToModelList(IList<Event> eventList)
        {
            IList<EventModel> modelList = new List<EventModel>();
            foreach (Event eventEntity in eventList)
            {
                modelList.Add(ConvertEventEntityToModel(eventEntity));
            }

            return modelList;
        }

        public static StreamModel ConvertStreamEntityToModel(Stream entity)
        {
            StreamModel model = new StreamModel();
            //model.name = entity.stream_name;
            //model.Url = entity.stream_name;
            return model;
        }

        public static IList<StreamModel> ConvertStreamEntityToModelList(IList<Stream> streamList)
        {
            IList<StreamModel> modelList = new List<StreamModel>();
            foreach (Stream streamEntity in streamList)
            {
                modelList.Add(ConvertStreamEntityToModel(streamEntity));
            }

            return modelList;
        }
    }
}
