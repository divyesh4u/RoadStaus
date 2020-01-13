using System;
using System.Collections.Generic;
using System.Text;

namespace RoadStatusModel
{
    public class RoadError
    {

        public DateTime timestampUtc { get; set; }
        public string exceptionType { get; set; }
        public int httpStatusCode { get; set; }
        public string httpStatus { get; set; }
        public string relativeUri { get; set; }
        public string message { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var nonExistRoad = (RoadError)obj;
            if (this.httpStatus == nonExistRoad.httpStatus
                && this.exceptionType == nonExistRoad.exceptionType
                && this.httpStatusCode == nonExistRoad.httpStatusCode
                && this.message == nonExistRoad.message
                && this.relativeUri == nonExistRoad.relativeUri)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return 133 * this.exceptionType.GetHashCode() * this.httpStatus.GetHashCode() * this.httpStatusCode.GetHashCode() * this.message.GetHashCode() * this.relativeUri.GetHashCode();
        }
    }
}
