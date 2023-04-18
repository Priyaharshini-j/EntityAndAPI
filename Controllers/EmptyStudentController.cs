using EntityFramework_API.Data;
using EntityFramework_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EntityFramework_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmptyStudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmptyStudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult  Get()
        {
            try
            {
                List<StudentModel> list = _context.Students.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public ActionResult AddStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var studentModel = _context.Students.Where(x=> x.Id== id).ToList();

            if (studentModel == null)
            {
                return NotFound();
            }

            return Ok(studentModel);
        }
        // PUT: api/EmptyStudent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutStudent(int id, StudentModel studentModel)
        {
            if (id != studentModel.Id)
            {
                return BadRequest();
            }

            var existing = _context.Students.Where(x => x.Id == id).ToList();
            if(existing ==  null)
            {
                return BadRequest(studentModel);
            }
            try
            {
                existing[0].Name = studentModel.Name;
                existing[0].Dept = studentModel.Dept;
                existing[0].Age = studentModel.Age;
                existing[0].DateOfBirth = studentModel.DateOfBirth;
                existing[0].Email = studentModel.Email;

                _context.SaveChanges();
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            
        }

        // POST: api/Student
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostStudent(StudentModel studentModel)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            _context.Students.Add(studentModel);
            _context.SaveChanges();

            return Ok("Added Successfully");
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            List<StudentModel> studentModel = _context.Students.Where(x => x.Id == id).ToList();
            if (studentModel == null)
            {
                return NotFound();
            }

            _context.Students.Remove(studentModel[0]);
            _context.SaveChanges();

            return NoContent();
        }
        private bool StudentModelExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
