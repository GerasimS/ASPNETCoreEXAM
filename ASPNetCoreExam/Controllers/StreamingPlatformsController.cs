using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNetCoreExam.Data;
using ASPNetCoreExam.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ASPNetCoreExam.Controllers
{
    public class StreamingPlatformsController : Controller
    {
        private readonly StreamingPlatformContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StreamingPlatformsController(StreamingPlatformContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.StreamingPlatforms.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streamingPlatform = await _context.StreamingPlatforms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (streamingPlatform == null)
            {
                return NotFound();
            }

            return View(streamingPlatform);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Date,ImageName,ImageFile")] StreamingPlatform streamingPlatform)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(streamingPlatform.ImageFile.FileName);
                string extension = Path.GetExtension(streamingPlatform.ImageFile.FileName);
                streamingPlatform.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                streamingPlatform.Date = DateTime.Now;


                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await streamingPlatform.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(streamingPlatform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(streamingPlatform);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streamingPlatform = await _context.StreamingPlatforms.FindAsync(id);
            if (streamingPlatform == null)
            {
                return NotFound();
            }
            return View(streamingPlatform);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] StreamingPlatform streamingPlatform)
        {
            if (id != streamingPlatform.ID)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Entry(streamingPlatform).State = EntityState.Modified;
                    _context.Entry(streamingPlatform).Property("ImageName").IsModified = false;
                    streamingPlatform.Date = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreamingPlatformExists(streamingPlatform.ID))
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
            return View(streamingPlatform);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var streamingPlatform = await _context.StreamingPlatforms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (streamingPlatform == null)
            {
                return NotFound();
            }

            return View(streamingPlatform);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
                    
            var streamingPlatform = await _context.StreamingPlatforms.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", streamingPlatform.ImageName);
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.StreamingPlatforms.Remove(streamingPlatform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreamingPlatformExists(int id)
        {
            return _context.StreamingPlatforms.Any(e => e.ID == id);
        }
    }
}
