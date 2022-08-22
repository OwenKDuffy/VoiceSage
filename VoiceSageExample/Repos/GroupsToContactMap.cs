namespace VoiceSageExample.Repos
{
    public class GroupsToContactMap
    {
        private class GtoCMaps{
            public int Id { get; set; }
            public int GroupId { get; set; }
            public int ContactId { get; set; }

            public GtoCMaps(int id, int groupId, int contactId)
            {
                Id = id;
                GroupId = groupId;
                ContactId = contactId;
            }
        }

        List<GtoCMaps> repoMock;

        // consider this a mock of a SQL table linking foreign keys to assign members to groups
        public GroupsToContactMap()
        {
            repoMock = new List<GtoCMaps>
            {
                new GtoCMaps(1, 1, 6),
                new GtoCMaps(2, 3, 2),
                new GtoCMaps(3, 3, 3),
                new GtoCMaps(4, 3, 4),
                new GtoCMaps(5, 3, 6),
                new GtoCMaps(6, 2, 5),
                new GtoCMaps(7, 3, 1),
            };
        }
            
        public List<int> getMembers(int groupId)
        {
            var valsfromDB = repoMock.Where(x => x.GroupId == groupId);
            return valsfromDB.Select(x => x.ContactId).ToList();
        }

        public List<int> getMemberships(int contactId)
        {
            var valsfromDB = repoMock.Where(x => x.ContactId == contactId);
            return valsfromDB.Select(x => x.GroupId).ToList();
        }

        public bool addToGroup(int groupId, int contactId)
        {
            repoMock.Add(new GtoCMaps(repoMock.Count + 1, groupId, contactId));
            return true;
        }

        public bool removeFromGroup(int groupId, int contactId)
        {
            repoMock.RemoveAll(x => x.GroupId == groupId && x.ContactId == contactId);
            return true;
        }
        
    }
}
