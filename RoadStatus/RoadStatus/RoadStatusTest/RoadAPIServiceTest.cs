using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatusModel;
using RoadStatus;
using System.Configuration;
using Unity;
using Unity.Resolution;

namespace RoadStatusTest
{
    [TestClass]
    public class RoadAPIServiceTest
    {
        IRoadAPIService roadAPIService;

        [TestInitialize]
        public void SetUp()
        {
            var config = ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location);
            IUnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterType<IRoadAPIService, RoadAPIService>();
            roadAPIService = unityContainer.Resolve<IRoadAPIService>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("appSettings", config.AppSettings.Settings)
                                   });
        }

        [TestMethod]
        public void Road_Status_Test()
        {
            var expectedRoadObj = new Road
            {
                displayName = "A2",
                id = "a2",
                statusSeverity = "Good",
                statusSeverityDescription = "No Exceptional Delays",
                url = "/Road/a2"
            };

            var roadSatus = roadAPIService.GetRoadStatus("A2").Result;
            var actualroadObj = (Road)roadSatus;
            Assert.AreEqual(expectedRoadObj, actualroadObj);

        }

        [TestMethod]
        public void Road_NotExists_Status_Test()
        {
            string roadName = "A233";
            var expectedRoadObj = new RoadError
            {
                exceptionType = "EntityNotFoundException",
                httpStatus = "NotFound",
                httpStatusCode = 404,
                relativeUri = "/Road/A233",
                message = "The following road id is not recognised: A233"
            };

            var roadSatus = roadAPIService.GetRoadStatus(roadName).Result;
            var actualRoadObj = (RoadError)roadSatus;
            Assert.AreEqual(expectedRoadObj, actualRoadObj);
        }
    }
}
