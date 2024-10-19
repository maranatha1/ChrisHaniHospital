using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChrisHaniHospital.Areas.Identity.Data;
using ChrisHaniHospital.Data;
using ChrisHaniHospital.Models;
using ChrisHaniHospital.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ChrisHaniContext _context;
        private readonly SignInManager<ChrisHaniUser> _signInManager;
        private readonly UserManager<ChrisHaniUser> _userManager;
        private readonly IUserStore<ChrisHaniUser> _userStore;
        private readonly IUserEmailStore<ChrisHaniUser> _emailStore;

        public UserManagementController(
            UserManager<ChrisHaniUser> userManager,
            IUserStore<ChrisHaniUser> userStore,
            SignInManager<ChrisHaniUser> signInManager,
            ChrisHaniContext context
        )
        {
            _userManager = userManager;
            _userStore = userStore;

            _signInManager = signInManager;

            _context = context;
        }

        // GET: UserManagement
        public async Task<IActionResult> Staff()
        {
            var Surgeons = _context.Surgeons.Include(a => a.User).ToList();
            var Nurses = _context.Nurses.Include(a => a.User).ToList();
            var Pharmacist = _context.Pharmacists.Include(a => a.User).ToList();
            var Anaesthesiologists = _context.Anaesthesiologists.Include(a => a.User).ToList();

            StaffVM staffVM = new StaffVM
            {
                Surgeon = Surgeons,
                Anaesthesiologist = Anaesthesiologists,
                Pharmacist = Pharmacist,
                Nurse = Nurses
            };

            return View(staffVM);
        }

        // GET: UserManagement/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userVM = await _context.UserVM.FirstOrDefaultAsync(m => m.Id == id);
        //    if (userVM == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userVM);
        //}

        // GET: UserManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                //create a new user
                ChrisHaniUser chrisHaniUser = new ChrisHaniUser
                {
                    UserName = userVM.Email,
                    Email = userVM.Email,
                    PhoneNumber = userVM.PhoneNumber,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    EmailConfirmed = true,
                    IdentityNumber = null
                };
                var result = await _userManager.CreateAsync(chrisHaniUser, userVM.Password);
                if (result.Succeeded)
                {
                    //add user to role
                    await _userManager.AddToRoleAsync(chrisHaniUser, userVM.Speciality);
                }
                else
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Error Adding the staff as a User, please try again"
                    );

                    return View(userVM);
                }
                //
                if (userVM.Speciality == Roles.Surgeon)
                {
                    Surgeon surgeon = new Surgeon
                    {
                        User = chrisHaniUser,
                        UserID = chrisHaniUser.Id,
                        Registration_Number = userVM.Registration_Number
                    };
                    _context.Add(surgeon);
                    await _context.SaveChangesAsync();
                }
                else if (userVM.Speciality == Roles.Nurse)
                {
                    Nurse nurse = new Nurse
                    {
                        User = chrisHaniUser,
                        UserID = chrisHaniUser.Id,
                        Registration_Number = userVM.Registration_Number
                    };
                    _context.Add(nurse);
                    await _context.SaveChangesAsync();
                }
                else if (userVM.Speciality == Roles.Pharmacist)
                {
                    Pharmacist pharmacist = new Pharmacist
                    {
                        User = chrisHaniUser,
                        UserID = chrisHaniUser.Id,
                        Registration_Number = userVM.Registration_Number
                    };
                    _context.Add(pharmacist);
                    await _context.SaveChangesAsync();
                }
                else if (userVM.Speciality == Roles.Anaesthesiologist)
                {
                    Anaesthesiologist anaesthesiologist = new Anaesthesiologist
                    {
                        User = chrisHaniUser,
                        UserID = chrisHaniUser.Id,
                        Registration_Number = userVM.Registration_Number
                    };
                    _context.Add(anaesthesiologist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Error Adding the staff as a User, please try again"
                    );

                    return View(userVM);
                }
                TempData["Success"] = "Staff Added Successfully";
                return RedirectToAction(nameof(Staff));
            }
            return View(userVM);
        }

        //    // GET: UserManagement/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var userVM = await _context.UserVM.FindAsync(id);
        //        if (userVM == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(userVM);
        //    }

        //    // POST: UserManagement/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(
        //        int id,
        //        [Bind("Id,FirstName,LastName,IdentityNumber,Email,PhoneNumber,Speciality")]
        //            UserVM userVM
        //    )
        //    {
        //        if (id != userVM.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(userVM);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!UserVMExists(userVM.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(userVM);
        //    }

        //    // GET: UserManagement/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var userVM = await _context.UserVM.FirstOrDefaultAsync(m => m.Id == id);
        //        if (userVM == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(userVM);
        //    }

        //    // POST: UserManagement/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var userVM = await _context.UserVM.FindAsync(id);
        //        if (userVM != null)
        //        {
        //            _context.UserVM.Remove(userVM);
        //        }

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool UserVMExists(int id)
        //    {
        //        return _context.UserVM.Any(e => e.Id == id);
        //    }
    }
}
