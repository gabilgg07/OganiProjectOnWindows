using System;
namespace Ogani.WebUI.Models.Entity
{
    public class Audit : BaseEntity
    {
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Method { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public int StatusCode { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}

