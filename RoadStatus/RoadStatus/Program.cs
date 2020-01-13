using System;
using RoadStatusModel;

namespace RoadStatus
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please enter road name");
            }
            else
            {
                string roadName = args[0];

                string app = APISettings.APISettingInstance.Settings["AppId"].Value;
                if (APISettings.APISettingInstance.Settings["AppId"].Value.Equals(string.Empty) || APISettings.APISettingInstance.Settings["Key"].Value.Equals(string.Empty))
                {
                    Console.WriteLine("Please update App.config for AppId and Key");
                    return;
                }
                IRoadAPIService roadAPIService = new RoadAPIService(APISettings.APISettingInstance.Settings);
                var road = roadAPIService.GetRoadStatus(roadName).Result;

                if (road != null && road.GetType().Equals(typeof(Road)))
                {
                    var roadStatus = (Road)road;
                    Console.WriteLine("The status of the {0} is as follows", "A");
                    Console.WriteLine("Road Status is {0}", roadStatus.statusSeverity);
                    Console.WriteLine("Road Staus Descripton {0}", roadStatus.statusSeverityDescription);
                }
                if (road != null && road.GetType().Equals(typeof(RoadError)))
                {
                    Console.WriteLine("{0} is not a valid road", roadName);
                }
               
            }
        }
    }
}
