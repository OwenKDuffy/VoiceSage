using VoiceSageExample.Models;

namespace VoiceSageExample.Factories
{
    public class ContactsFactory
    {
        public Contact BuildContact(Contact db, List<Group> grps)
        {
            db.Memberships = grps;
            return db;
        }
    }
}
