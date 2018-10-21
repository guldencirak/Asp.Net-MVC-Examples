using System;
using System.ComponentModel.DataAnnotations;

namespace MyOrganizationApp.Models
{
    public class Subscriber
    {
        private string _name;
        private string _eMail;
        private DateTime _birthDate;

        public string Name { get => _name; set => _name = value; }
        public string EMail { get => _eMail; set => _eMail = value; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get => _birthDate; set => _birthDate = value; }
    }
}
