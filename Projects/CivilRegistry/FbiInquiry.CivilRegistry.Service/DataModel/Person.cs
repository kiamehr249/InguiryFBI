using System;

namespace FbiInquiry.CivilRegistry.Service
{
    public class Person
    {
        public int Id { get; set; }
        public string NationalNumber { get; set; }
        public string DeathDate { get; set; }
        public string JsonResult { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
