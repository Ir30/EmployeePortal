using EmployeePortal.Application.DTOs.Department;
using EmployeePortal.Application.Interfaces.IServices;
using EmployeePortal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✅ Require authentication for all endpoints by default
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // ✅ GET: api/department
        // Allow any authenticated user (Admin or User)
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        // ✅ GET: api/department/{id}
        // Allow any authenticated user (Admin or User)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound();

            return Ok(department);
        }

        // ✅ POST: api/department
        // Admin only
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = new Department
            {
                DepartmentName = dto.DepartmentName
            };

            await _departmentService.AddDepartmentAsync(department);

            return CreatedAtAction(nameof(GetById), new { id = department.DepartmentId }, department);
        }

        // ✅ PUT: api/department/{id}
        // Admin only
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _departmentService.GetDepartmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.DepartmentName = dto.DepartmentName;

            await _departmentService.UpdateDepartmentAsync(existing);
            return NoContent();
        }

        // ✅ DELETE: api/department/{id}
        // Admin only
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _departmentService.GetDepartmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}
