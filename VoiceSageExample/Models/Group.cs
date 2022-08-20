namespace VoiceSageExample.Models
{
    public class Group
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Contact> members { get; set; }
    }
}
