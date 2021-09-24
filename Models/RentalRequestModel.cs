using System;
using System.Data.Entity;
using System.Linq;

namespace TheatreCMS3.Areas.Rent.Models
{
    public class RentalRequest
    {
        public int RentalRequestID { get; set; }
        public string ContactPerson { get; set; }
        public string Company { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ProjectInfo { get; set; }
        public int RentalCode { get; set; }
        public bool Accepted { get; set; }
        public bool ContractSigned { get; set; }
        public bool IgnoreSurveyPrompt { get; set; }
    }
}