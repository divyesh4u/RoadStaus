using System;

namespace RoadStatusModel
{
    public class Road
    {
        public string type { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public string statusSeverity { get; set; }
        public string statusSeverityDescription { get; set; }
        public string bounds { get; set; }
        public string envelope { get; set; }
        public string url { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var road = (Road)obj;
            if (this.id == road.id
                && this.displayName == road.displayName
                && this.statusSeverity == road.statusSeverity
                && this.statusSeverityDescription == road.statusSeverityDescription
                && this.url == road.url)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return 133 * this.id.GetHashCode() * this.displayName.GetHashCode() * this.statusSeverity.GetHashCode() * this.statusSeverityDescription.GetHashCode() * this.url.GetHashCode();
        }
    }
}
