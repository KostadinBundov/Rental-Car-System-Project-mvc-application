using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rental_Car_System_Project.Data;
using Rental_Car_System_Project.Models;
using Rental_Car_System_Project.ViewModels;

namespace Rental_Car_System_Project.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Requests.Include(r => r.Car);
            List<Request> requestsList = new List<Request>();

            foreach (var request in applicationDbContext)
            {
                request.User = await _context.Users.FindAsync(request.UserId);

                requestsList.Add(request);

            }

            if (User.IsInRole("Admin"))
            {
                return View(requestsList);
            }
            else
            {
                return View(requestsList.Where(x => x.UserId == this.GetUserId()));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            Request request = await _context.Requests.FindAsync(id);

            if (request != null)
            {
                request.IsRequestApproved = true;
            }
            else
            {
                return View("MakeARequestPage");
            }

            _context.Update(request);
            await _context.SaveChangesAsync();

            return View("Index");
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create(int id)
        {
            RequestViewModel data = new RequestViewModel();
            data.CarId = id;
            return View(data);
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel requestModel)
        {
            var allRequestsForCurrentCar = _context.Requests.Where(x => x.CarId == requestModel.CarId).ToList();
            var isThereAnyRequestForTheseDatesPT1 = allRequestsForCurrentCar.Any(x => x.PickUpDate <= requestModel.PickUpDate && requestModel.PickUpDate <= x.DropOffDate);
            var isThereAnyRequestForTheseDatesPT2 = allRequestsForCurrentCar.Any(x => x.DropOffDate <= requestModel.DropOffDate && requestModel.DropOffDate <= x.DropOffDate);

            if (isThereAnyRequestForTheseDatesPT1 == false && isThereAnyRequestForTheseDatesPT2 == false && requestModel.PickUpDate >= DateTime.Now)
            {
                if (ModelState.IsValid)
                {
                    Request request = new Request();
                    request.PickUpDate = requestModel.PickUpDate;
                    request.DropOffDate = requestModel.DropOffDate;
                    request.CarId = requestModel.CarId;
                    request.UserId = this.GetUserId();

                    _context.Add(request);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("MakeARequestPage");
            }
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "CarBrand", request.CarId);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RequestViewModel requestModel)
        {
            var edditedRequest = _context.Requests.Find(id);

            if (edditedRequest == null)
            {
                return NotFound();
            }

            var allRequestsForCurrentCar = _context.Requests.Where(x => x.CarId == requestModel.CarId && x.UserId != this.GetUserId()).ToList();
            var isThereAnyRequestForTheseDatesPT1 = allRequestsForCurrentCar.Any(x => x.PickUpDate <= requestModel.PickUpDate && requestModel.PickUpDate <= x.DropOffDate);
            var isThereAnyRequestForTheseDatesPT2 = allRequestsForCurrentCar.Any(x => x.PickUpDate <= requestModel.DropOffDate && requestModel.DropOffDate <= x.DropOffDate);
            var isDateAlreadyGone = requestModel.PickUpDate < DateTime.Now || requestModel.DropOffDate <= DateTime.Now;

            if (isThereAnyRequestForTheseDatesPT1 == false || isThereAnyRequestForTheseDatesPT2 == false || isDateAlreadyGone == false)
            {
                if (ModelState.IsValid)
                {
                    edditedRequest.CarId = requestModel.CarId;
                    edditedRequest.PickUpDate = requestModel.PickUpDate;
                    edditedRequest.DropOffDate = requestModel.DropOffDate;

                    _context.Update(edditedRequest);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("MakeARequestPage");
            }
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Car)
                .FirstOrDefaultAsync(m => m.Id == id);

            var user = _context.Users.Find(request.UserId);
            request.User = user;

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Requests'  is null.");
            }
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private string GetUserId()
        {
            var name = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == name);

            return user.Id;
        }
    }
}
