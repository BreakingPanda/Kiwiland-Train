using Microsoft.Practices.Unity;
using System.Web.Http;
using Train.Service.DataReaders;
using Train.Service.Graph;
using Train.Service.Queue;
using Unity.WebApi;

namespace Train.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ITrackReader, TrackReader>();
            container.RegisterType<ITrainQueue, TrainQueue>();
            container.RegisterType<ITrainGraph, TrainGraph>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}