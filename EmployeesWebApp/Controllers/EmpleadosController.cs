using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesWebApp.Models;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using EmployeesWebApp.Models.Entities;

namespace EmployeesWebApp.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly EmpleadosDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public EmpleadosController(EmpleadosDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Empleados
        public async Task<IActionResult> Index(int pg = 1)
        {

            var empleadosDbContext = _context.Empleados.Include(e => e.Estado).Include(e => e.Puesto);
            var emp = await empleadosDbContext.ToListAsync();

            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }

            int count = emp.Count();

            var pagination = new Pagination(count,pg,pageSize);

            //int skip = (pg - 1) * pageSize;
            int skip = Math.Max(0, (pg - 1) * pageSize);


            var data = emp.Skip(skip).Take(pagination.PageSize).ToList();

            this.ViewBag.Pagination = pagination;

            //return View(emp);
            return View(data);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.Estado)
                .Include(e => e.Puesto)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Descripcion");
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "Descripcion");
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,Fotografia,formFile,Nombre,Apellido,PuestoId,FechaNacimiento,FechaContratacion,Direccion,Telefono,CorreoElectronico,EstadoId")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                if(empleado.EmpleadoId == 0)
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(empleado.formFile.FileName);
                    string extension = Path.GetExtension(empleado.formFile.FileName);

                    empleado.Fotografia = fileName = fileName + DateTime.Now.ToString("ddMMyyyy") + extension;

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await empleado.formFile.CopyToAsync(fileStream);
                    }

                    _context.Add(empleado);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Descripcion", empleado.EstadoId);
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "Descripcion", empleado.PuestoId);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Descripcion", empleado.EstadoId);
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "Descripcion", empleado.PuestoId);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Fotografia,formFile,Nombre,Apellido,PuestoId,FechaNacimiento,FechaContratacion,Direccion,Telefono,CorreoElectronico,EstadoId")] Empleado empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(empleado.formFile != null)
                    {
                        string wwwRootPath = hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(empleado.formFile.FileName);
                        string extension = Path.GetExtension(empleado.formFile.FileName);
                        empleado.Fotografia = fileName = fileName + DateTime.Now.ToString("ddMMyyyy") + extension;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await empleado.formFile.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {   
                        empleado.Fotografia = _context.Empleados.AsNoTracking().FirstOrDefault(e => e.EmpleadoId == empleado.EmpleadoId)?.Fotografia;
                    }

                    ModelState.Remove("formFile");

                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoId))
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
            ViewData["EstadoId"] = new SelectList(_context.Estados, "EstadoId", "Descripcion", empleado.EstadoId);
            ViewData["PuestoId"] = new SelectList(_context.Puestos, "PuestoId", "Descripcion", empleado.PuestoId);
            return View(empleado);
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.EmpleadoId == id)).GetValueOrDefault();
        }


    }
}
