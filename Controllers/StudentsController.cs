using Microsoft.AspNetCore.Mvc;
using StudentManagerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using StudentManagerApp.DTOs.Request;
using StudentManagerApp.DTOs.Response;
using StudentManagerApp.Repositories;

namespace StudentManagerApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ApplicationController<StudentsController>
    {
        private readonly IStudentRepository _studentRepository;

        private readonly IRepository<Course> _courseRepository;

        private readonly IMapper _mapper;

        public StudentsController(
            IStudentRepository studentRepository, IRepository<Course> courseRepository, ILogger<StudentsController> logger, IMapper mapper
        )
            : base(logger)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<StudentBaseResponse>>(students));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentRepository.GetByIdWithCourseAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentDetailResponse>(student));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest studentRequest)
        {
            if (ModelState.IsValid)
            {
                var student = new Student();
                student.Name = studentRequest.Name;
                student.EnrollmentDate = DateTime.Parse(studentRequest.EnrollmentDate);
                if (studentRequest.SelectedCourseIds != null)
                {
                    student.Courses = new List<Course>();
                    foreach (var courseId in studentRequest.SelectedCourseIds)
                    {
                        var courseToAdd = await _courseRepository.FindByIdAsync(courseId);
                        if (courseToAdd != null)
                        {
                            student.Courses.Add(courseToAdd);
                        }
                    }
                }
                await _studentRepository.AddAsync(student);
                return Created(student.Id.ToString(), string.Empty);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                _logger.LogError(error.ErrorMessage);
            }

            return BadRequest();
        }

        // // GET: Student/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var student = await _context.Students.Include(s => s.Courses).FirstOrDefaultAsync(m => m.Id == id);
        //     if (student == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewBag.Courses = _context.Courses.ToList();
        //     return Ok(student);
        // }

        // // POST: Student/Edit/5
        // [HttpPost]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnrollmentDate")] Student student, int[] selectedCourses)
        // {
        //     if (id != student.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(student);
        //             await _context.SaveChangesAsync();

        //             var existingStudent = await _context.Students
        //                                                 .Include(s => s.Courses)
        //                                                 .FirstOrDefaultAsync(s => s.Id == id);

        //             if (existingStudent != null)
        //             {
        //                 existingStudent.Courses.Clear();
        //                 if (selectedCourses != null)
        //                 {
        //                     foreach (var courseId in selectedCourses)
        //                     {
        //                         var courseToAdd = await _context.Courses.FindAsync(courseId);
        //                         if (courseToAdd != null)
        //                         {
        //                             existingStudent.Courses.Add(courseToAdd);
        //                         }
        //                     }
        //                 }

        //                 await _context.SaveChangesAsync();
        //             }
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!StudentExists(student.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return Ok(student);
        // }

        // private bool StudentExists(int id)
        // {
        //     return _context.Students.Any(e => e.Id == id);
        // }
    }
}
