using VoiceSageExample.Models;

namespace VoiceSageExample.Factories
{
    public class GroupsFactory
    {
        public GroupsFactory()
        {

        }
        public Group BuildGroup(Group db, List<Contact> contacts)
        {
            return new Group { Id = db.Id, Name = db.Name, Description = db.Description, Contacts = contacts };
        }
    }
}
