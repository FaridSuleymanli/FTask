using AutoMapper;
using FrediTask.DAL;
using FrediTask.DTO;
using FrediTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrediTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly FrediContext _context;
        private readonly IMapper _mapper;
        public TaskController(FrediContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<CategoryGetDTO> categories = _mapper.Map<IEnumerable<CategoryGetDTO>>(await _context.Categories.Include(c => c.Products).ToListAsync());
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var category = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
            if (category == null)
            {
                return BadRequest(new { error = "Bu id-li kateqoriya movcud deyil" });
            }
            return Ok(_mapper.Map<Category, CategoryGetDTO>(category));
        }
    }
}
