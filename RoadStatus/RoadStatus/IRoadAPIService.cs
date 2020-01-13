using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus
{
    public interface IRoadAPIService
    {
        Task<object> GetRoadStatus(string roadName);
    }
}
