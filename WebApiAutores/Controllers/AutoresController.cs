﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly AplicationDbContext context;

        public AutoresController(AplicationDbContext context)
        {
            this.context = context;
        }
        //[HttpGet]// api/autores
        //[HttpGet("listado")]// api/autores/listado
        //[HttpGet("/listado")]
        //public async Task<ActionResult<List<Autor>>> Get()
        //{
        //    return await context.Autores.Include(x =>x.Libros).ToListAsync();

        //    //return new List<Autor>()
        //    //{
        //    //    new Autor(){Id=1,Nombre="Felipe"},
        //    //    new Autor(){Id=2, Nombre="Claudia"}
        //    //};
        //}

        [HttpGet]// api/autores
        [HttpGet("listado")]// api/autores/listado
        [HttpGet("/listado")]
        public List<Autor> Get()
        {
            return  context.Autores.Include(x => x.Libros).ToList();

          
        }
        [HttpGet("primero")]// api/autores/primero]// api/autores/primero

        public async Task<ActionResult<Autor>> PrimerAutor()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }
        [HttpGet("{id:int}/{param2=Person}")]
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);


            if (autor == null)
            {
                return NotFound();
            }



            //    return autor;

            //}
            //[HttpGet("{id:int}/{param2=Person}")]
            //public ActionResult<Autor> Get(int id, string param2)
            //{
            //    var autor =  context.Autores.FirstOrDefault(x => x.Id == id);


            //    if (autor == null)
            //    {
            //        return NotFound();
            //    }



            //    return autor;

            //}

            //[HttpGet("{id:int}/{param2=Person}")]
            //public IActionResult Get(int id, string param2)
            //{
            //    var autor = context.Autores.FirstOrDefault(x => x.Id == id);


            //    if (autor == null)
            //    {
            //        return NotFound();
            //    }



            //    return Ok(autor);

            //}

            [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));


            if (autor == null)
            {
                return NotFound();
            }



            return autor;

        }
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {

            //var ExisteAutor = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);

            //if (ExisteAutor)
            //{
            //    return BadRequest($"Ya existe {autor.Nombre}");
            //}

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();

        }


        [HttpPut("{id:int}")]// api/autores/1
        public async Task<ActionResult> Put(Autor autor, int id)
        {

            if (autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }


            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
