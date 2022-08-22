using Microsoft.AspNetCore.Mvc;
using VoiceSageExample.Factories;
using VoiceSageExample.Models;
using VoiceSageExample.Repos;

namespace VoiceSageExample.Controllers
{
    public class GroupsController : Controller
    {
        GroupsRepo _groupsRepo;
        ContactsRepo _contactsRepo;
        GroupsToContactMap _groupsToContactMap;
        GroupsFactory _gf;

        public GroupsController(GroupsRepo groupsRepo, GroupsToContactMap gtc, GroupsFactory gf, ContactsRepo contactsRepo)
        { 
            _groupsRepo = groupsRepo;
            _contactsRepo = contactsRepo;
            _groupsToContactMap = gtc;
            _gf = gf;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        { 
            return View(_groupsRepo.GetGroups());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _groupsRepo.GetGroups().FirstOrDefault(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }
            var members = _contactsRepo.GetContacts(_groupsToContactMap.getMembers(group.Id));
            
            return View(_gf.BuildGroup(group, members));
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Group @group)
        {
            if (!String.IsNullOrEmpty(group.Name) && !String.IsNullOrEmpty(group.Description))
            {
                _groupsRepo.AddGroup(@group);
                return RedirectToAction(nameof(Index), _groupsRepo.GetGroups());
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _groupsRepo == null)
            {
                return NotFound();
            }

            var @group = _groupsRepo.FindGroup(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }
        
        // GET: Groups/Remove/5
        public async Task<IActionResult> Remove(int? groupId, int? memberId )
        {
            if (groupId != null || memberId != null)
            {
                _groupsToContactMap.removeFromGroup((int)groupId, (int)memberId);

                return RedirectToAction(nameof(Details), _groupsRepo.FindGroup(groupId));
            }
            else
            {
                return NotFound();
                
            }
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (_groupsRepo.updateGroup(@group))
                    return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }
        public async Task<IActionResult> SelectGroupToAddTo(int memberId)
        {
            ViewData["memberID"] = memberId;
            return View(_groupsRepo.GetGroups());
        }

        public async Task<IActionResult> AddToGroup(int groupId, int memberId)
        {
            _groupsToContactMap.addToGroup((int)groupId, (int)memberId);
            return RedirectToAction(nameof(Details), "Contacts", new { id = memberId });
        }


        //// GET: Groups/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _groupsRepo.Group == null)
        //    {
        //        return NotFound();
        //    }

        //    var @group = await _groupsRepo.Group
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (@group == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@group);
        //}

        //// POST: Groups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_groupsRepo.Group == null)
        //    {
        //        return Problem("Entity set 'VoiceSageExampleContext.Group'  is null.");
        //    }
        //    var @group = await _groupsRepo.Group.FindAsync(id);
        //    if (@group != null)
        //    {
        //        _groupsRepo.Group.Remove(@group);
        //    }

        //    await _groupsRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GroupExists(int id)
        //{
        //  return (_groupsRepo.Group?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
