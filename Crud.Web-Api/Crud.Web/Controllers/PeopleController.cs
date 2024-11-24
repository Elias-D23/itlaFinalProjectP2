using Microsoft.AspNetCore.Mvc;
using Crud.Domain.Entities;

namespace Crud.Web.Controllers
{

    public class PeopleController : Controller
    {

        //private readonly CrudDbContext _Dbcontext;

        //public PeopleController(CrudDbContext context)
        //{
        //    _Dbcontext = context;
        //}


        public IActionResult Create()
        {
            return View();
        }


        //public async Task<IActionResult> Create(Person person)
        //{
        //    await _Dbcontext.People.AddAsync(person);
        //    await _Dbcontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(List));
        //}


        public async Task<IActionResult> List()
        {
            //List<Person> listPersons = await _Dbcontext.People.ToListAsync();
            //return View(listPersons);

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        //[HttpGet]
        ////public async Task<IActionResult> Edit(int id)
        ////{
        ////    Person person = await _Dbcontext.People.FirstAsync(e => e.Id == id);
        ////    return View(person);
        ////}



        //public async Task<IActionResult> Edit(Person person)
        //{
        //    _Dbcontext.People.Update(person);
        //    await _Dbcontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(List));
        //}


        //public async Task<IActionResult> Edit(int id)
        //{
        //    var person = await _Dbcontext.People.FirstOrDefaultAsync(p => p.Id == id);

        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person); 
        //}

        //public async Task<IActionResult> Edit(Person person)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(person); 
        //    }

        //    _Dbcontext.People.Update(person);
        //    await _Dbcontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(List)); 
        //}


        //public async Task<IActionResult> Delete(int id)
        //{
        //    Person person = await _Dbcontext.People.FirstAsync(e => e.Id == id);

        //    _Dbcontext.People.Remove(person);
        //    await _Dbcontext.SaveChangesAsync();
        //    return RedirectToAction(nameof(List));
        //}
    }

}
