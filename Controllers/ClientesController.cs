using apiCargueClientes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiCargueClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext context;

        public ClientesController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public ActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var clientes = context.cliente
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener clientes: {ex.Message}");
            }
        }
        /*
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.cliente.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        */


        // GET api/<ClientesController>/5
        [HttpGet("{id}", Name="GetCliente")]
        public ActionResult Get(int id)
        {
            try
            {
                var cliente = context.cliente.FirstOrDefault(c => c.id == id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public ActionResult Post([FromBody]Cliente cliente)
        {
            try
            {
                context.cliente.Add(cliente);
                context.SaveChanges();
                return CreatedAtRoute("GetCliente", new { id = cliente.id }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Cliente cliente)
        {
            try
            {
                if (cliente.id == id)
                {
                    context.Entry(cliente).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetCliente", new { id = cliente.id }, cliente);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var cliente = context.cliente.FirstOrDefault(c => c.id == id);
                if (cliente != null)
                {
                    context.cliente.Remove(cliente);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/clientes/search
        [HttpGet("search")]
        public ActionResult SearchClientes(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return BadRequest("El término de búsqueda no puede estar vacío.");

                var clientes = context.cliente
                    .Where(c => c.nombre.Contains(searchTerm) || c.email.Contains(searchTerm))
                    .ToList();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al buscar clientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Carga datos de clientes desde un archivo CSV.
        /// </summary>
        /// <param name="file">Archivo CSV a cargar.</param>
        /// <returns>Respuesta HTTP 200 OK con los registros creados a partir del CSV.</returns>
        [HttpPost("uploadcsv")]
        public ActionResult UploadCSV(IFormFile file)
        {
            // Verificar si se subió un archivo y si es de tipo CSV
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha subido ningún archivo.");
            }

            if (Path.GetExtension(file.FileName).ToLowerInvariant() != ".csv")
            {
                return BadRequest("El archivo debe ser de tipo CSV.");
            }

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var registros = new List<Cliente>();
                    int registrosCreados = 0;


                    // Leer línea por línea
                    int lineNumber = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;

                        // Saltar la primera línea si no tiene encabezado
                        if (lineNumber == 1 && line.Split(',').Any(s => !s.Trim().Equals("nombre", StringComparison.OrdinalIgnoreCase)))
                            continue;

                        // Procesar la línea
                        var values = line.Split(',');

                        if (values.Length < 8)
                        {
                            return BadRequest($"El archivo CSV en la línea {lineNumber} no tiene la cantidad correcta de campos.");
                        }

                        var cliente = new Cliente
                        {
                            nombre = values[0].Trim(),
                            apellidos = values[1].Trim(),
                            edad = Convert.ToInt32(values[2].Trim()),
                            email = values[3].Trim(),
                            telefono = Convert.ToInt32(values[4].Trim()),
                            direccion = values[5].Trim(),
                            documento = Convert.ToInt32(values[6].Trim()),
                            tipo_documento = values[7].Trim()
                        };

                        //registros.Add(cliente);
                        context.cliente.Add(cliente);
                        context.SaveChanges();
                        registrosCreados++;
                    }

                    // Guardar todos los registros en la base de datos
                    /*context.cliente.AddRange(registros);
                    context.SaveChanges();

                    return Ok(registros);*/
                    return Ok(new { registrosCreados = registrosCreados });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al procesar el archivo CSV: {ex.Message}");
            }
        }


    }
}
