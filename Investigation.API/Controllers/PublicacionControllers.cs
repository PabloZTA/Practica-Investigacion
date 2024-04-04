﻿using Investigation.API.Data;
using Investigation.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Investigation.API.Controllers
{

    [ApiController]
    [Route("/api/publicacion")]
    public class PublicacionControllers:ControllerBase
    {
        private readonly DataContext _context;

        public PublicacionControllers(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Publicacion.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Publicacion publicacion)
        {
            _context.Add(publicacion);
            await _context.SaveChangesAsync();
            return Ok(publicacion);
        }


        [HttpGet("id:int")]
        public async Task<ActionResult> Get(int id)
        {

            var publicacion = await
                _context.Publicacion.FirstOrDefaultAsync(x => x.Id
                == id);
            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Publicacion publicacion)
        {
            _context.Add(publicacion);
            await _context.SaveChangesAsync();
            return Ok(publicacion);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> Delete(int id)
        {

            var Filasafectadas = await _context.Investigadores

                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (Filasafectadas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}