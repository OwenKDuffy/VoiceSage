using VoiceSageExample.Models;
    
namespace VoiceSageExample.Repos
{
    public class ContactsRepo
    {
        private List<Contact> _contacts;

        public ContactsRepo()
        {
            _contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "James Smith", Description = "PO" , Email = "JamesSmith@dept.ie", Phone = "0871234567"},
                new Contact { Id = 2, Name = "Nora Barnes", Description = "AP" , Email = "NoraBarnes@dept.ie", Phone = "0871234567"},
                new Contact { Id = 3, Name = "Jane Doe", Description = "HEO" , Email = "JaneDoe@dept.ie", Phone = "0871234567"},
                new Contact { Id = 4, Name = "John Doe", Description = "EO" , Email = "JohnDoe@dept.ie", Phone = "0871234567"},
                new Contact { Id = 5, Name = "Barry Tee", Description = "Customer" , Email = "BarryTee@gmail.com", Phone = "0871234567"},
                new Contact { Id = 6, Name = "Graham O'Shea", Description = "IT Consultant" , Email = "GrahamO@consultants.com", Phone = "0871234567"},
            };
        }

        public List<Contact> GetContacts()
        {
            // if production this would query a db and return
            // for demo purposes will just use objects 
            return _contacts;
        }
        
        public List<Contact> GetContacts(List<int> ids)
        {
            // if production this would query a db and return
            // for demo purposes will just use objects 
            return _contacts.Where(x => ids.Contains(x.Id)).ToList();
        }

        public int AddContact(Contact g)
        {
            if(g.Id <= 0)
                g.Id = _contacts.Count + 1;
            _contacts.Add(g);
            return g.Id;
        }

        public Contact? FindContact(int? id)
        {
            if (id != null)
            {
                return _contacts.Find(x => x.Id == id);
            }
            return null;
        }

        public bool updateContact(Contact g)
        {
            var index = _contacts.FindIndex(x => x.Id == g.Id);

            if (index > -1)
            {
                _contacts[index] = g;
                return true;
            }
            return false;
        }

        public bool RemoveContact(Contact g)
        {
            return _contacts.Remove(g);
        }

    }
}
