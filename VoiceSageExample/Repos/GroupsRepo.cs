using VoiceSageExample.Models;

namespace VoiceSageExample.Repos
{
    public class GroupsRepo
    {
        private List<Group> _groupsList;
        public GroupsRepo()
        {
            _groupsList = new List<Group>{
                new Group {Id = 1, Name = "Admins", Description = "Account Administrators"},
                new Group {Id = 2, Name = "Customers", Description = "Customer Accounts"},
                new Group {Id = 3, Name = "Staff", Description = "Staff Accounts"}
            };
        }

        public List<Group> GetGroups()
        {
            // if production this would query a db and return
            // for demo purposes will just use objects 
            return _groupsList;
        }
        public List<Group> GetGroupsById(List<int> ids)
        {
            
            return _groupsList.Where(x => ids.Contains(x.Id)).ToList();
        }

        public bool AddGroup(Group g)
        {
            if (g.Id <= 0 || g.Id == null)
                g.Id = _groupsList.Count + 1;
            _groupsList.Add(g);
            return true;
        }

        public Group FindGroup(int? id)
        {
            if (id != null)
            {
                return _groupsList.Find(x => x.Id == id);
            }
            return null;
        }

        public bool updateGroup(Group g)
        {
            var index = _groupsList.FindIndex(x => x.Id == g.Id);
            Console.WriteLine($"id = {g.Id}, N = {g.Name}, D = {g.Description}");

            if (index > -1)
            {
                _groupsList[index] = g;
                return true;
            }
            return false;
        }
    }
}
