using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using VoiceSageExample.Factories;
using VoiceSageExample.Models;
using VoiceSageExample.Repos;

namespace VoiceSageExample.Controllers
{
    public class ContactsController : Controller
    {
        ContactsRepo _ContactsRepo;
        GroupsRepo _groupsRepo;
        GroupsToContactMap _gtc;
        ContactsFactory _cf;


        public ContactsController(ContactsRepo ContactsRepo, GroupsRepo groupsRepo, GroupsToContactMap gtc, ContactsFactory cf)
        {
            _ContactsRepo = ContactsRepo;
            _groupsRepo = groupsRepo;
            _gtc = gtc;
            _cf = cf;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(_ContactsRepo.GetContacts());
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @Contact = _ContactsRepo.GetContacts().FirstOrDefault(m => m.Id == id);
            if (@Contact == null)
            {
                return NotFound();
            }
            var memberships = _groupsRepo.GetGroupsById(_gtc.getMemberships(Contact.Id));
            return View(_cf.BuildContact(Contact, memberships));
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Contact @Contact)
        {
            if (!String.IsNullOrEmpty(Contact.Name) && !String.IsNullOrEmpty(Contact.Description))
            {
                _ContactsRepo.AddContact(@Contact);
                return RedirectToAction(nameof(Index), _ContactsRepo.GetContacts());
            }
            return View(@Contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _ContactsRepo == null)
            {
                return NotFound();
            }

            var @Contact = _ContactsRepo.FindContact(id);
            if (@Contact == null)
            {
                return NotFound();
            }
            return View(@Contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Contact @Contact)
        {
            if (id != @Contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (_ContactsRepo.updateContact(@Contact))
                    return RedirectToAction(nameof(Index));
            }
            return View(@Contact);
        }

        public async Task<IActionResult> Remove(int? memberId, int? groupId)
        {
            if (groupId != null || memberId != null)
            {
                _gtc.removeFromGroup((int)groupId, (int)memberId);

                return RedirectToAction(nameof(Details), _ContactsRepo.FindContact(memberId));
            }
            else
            {
                return NotFound();

            }
        }

        public async Task<IActionResult> SelectMembersToAdd(int groupId)
        {
            ViewData["groupID"] = groupId;
            return View(_ContactsRepo.GetContacts());
        }

        public async Task<IActionResult> AddToGroup(int groupId, int memberId)
        {
            _gtc.addToGroup((int)groupId, (int)memberId);
            return RedirectToAction(nameof(Details), "Groups", new { id = groupId });
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _ContactsRepo == null)
            {
                return NotFound();
            }

            var @Contact = _ContactsRepo.FindContact(id);
            if (@Contact == null)
            {
                return NotFound();
            }

            return View(@Contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @Contact = _ContactsRepo.FindContact(id);
            if (@Contact != null)
            {
                _ContactsRepo.RemoveContact(Contact);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool ContactExists(int id)
        //{
        //  return (_ContactsRepo.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
