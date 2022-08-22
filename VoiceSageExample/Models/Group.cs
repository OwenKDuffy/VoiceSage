﻿namespace VoiceSageExample.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Contact> Contacts { get; set; }
    }

    
}
