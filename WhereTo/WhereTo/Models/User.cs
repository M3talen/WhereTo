using System;
using System.Collections.Generic;
using System.Text;

namespace WhereTo.Models
{
   public class User : BaseDataObject
    {
        public User()
        {
        }

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _description = string.Empty;
        private string _profilePicURL = string.Empty;
        private string _descShort = string.Empty;
        private string _attending = string.Empty;
        private string _attended = string.Empty;

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Description { get => _description; set => _description = value; }
        public string ProfilePicURL { get => _profilePicURL; set => _profilePicURL = value; }
        public string DescShort { get => _descShort; set => _descShort = value; }
        public string Attending { get => _attending; set => _attending = value; }
        public string Attended { get => _attended; set => _attended = value; }
    }
}
