using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Models;
using CoreTemplate.ApplicationCore.Specifications;
using CoreTemplate.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(ApplicationDbContext context, IRepository<Item> itemRepository, IMapper mapper)
        {
            _context = context;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        // GET: Items
        //public async Task<IActionResult> Index()
        //{
        //    var items = await _itemRepository.GetAllAsync();
        //    List<ItemViewModel> itemVMs = _mapper.Map<List<ItemViewModel>>(items.ToList());
        //    return View(itemVMs);
        //}

        public async Task<IActionResult> Index([FromQuery]Paginated<ItemViewModel> paginationQuery)
        {
            
            var parameters = new BasicQueryParameters<Item>();
            parameters.ApplyPaging(paginationQuery.PageNumber, paginationQuery.PageSize);

            var itemsCount = await _itemRepository.CountAsync();
            
            var items = await _itemRepository.ListAsync(parameters);

            List<ItemViewModel> itemVMs = _mapper.Map<List<ItemViewModel>>(items.ToList());
            paginationQuery.Model = itemVMs;
            paginationQuery.Count = itemsCount;

            return View(paginationQuery);
        }

        // TODO: Move to models
        public class DataTableParameter
        {
            public int draw { get; set; }
            public int length { get; set; }
            public int start { get; set; }
            public DTSearch search { get; set; }
        }
        public class DTSearch
        {
            public string Value { get; set; }
            public bool Regex { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> LoadItems([FromBody] DataTableParameter obj)
        {
            IQueryable<Item> result = _context.Items.AsQueryable();
            var resultCount = await _context.Items.CountAsync();
            var searchValue = obj.search?.Value;

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                result = result.Where(x => x.Name.Contains(searchValue) 
                                           || x.Description.Contains(searchValue));
            }
            
            var recordsFilteredCount = result.Count();

            //todo: add filtering
            return Json(new
            {
                draw = obj.draw,
                recordsTotal = resultCount,
                recordsFiltered = recordsFilteredCount,
                data = await result
                    .Skip(obj.start)
                    .Take(obj.length)
                    .ToListAsync()
            });
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Priority")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.UniqueId = Guid.NewGuid();
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // GET: Items/EditPopup/5
        public async Task<IActionResult> EditPopup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return PartialView("_EditModal", item);
        }
        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Priority")] ItemViewModel itemVM)
        {
            if (id != itemVM.Id)
            {
                return NotFound();
            }
            var item = await _itemRepository.Get(id);
            _mapper.Map(itemVM, item);
            if (ModelState.IsValid)
            {
                try
                {
                    await _itemRepository.UpdateAsync(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
