using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstebanJimenezEFP6API.Models;
using EstebanJimenezEFP6API.ModelsDTOs;

namespace EstebanJimenezEFP6API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsksController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public AsksController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Asks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ask>>> GetAsks()
        {
          if (_context.Asks == null)
          {
              return NotFound();
          }
            return await _context.Asks.ToListAsync();
        }

        // GET: api/Asks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ask>> GetAsk(long id)
        {
          if (_context.Asks == null)
          {
              return NotFound();
          }
            var ask = await _context.Asks.FindAsync(id);

            if (ask == null)
            {
                return NotFound();
            }

            return ask;
        }
        [HttpGet("GetInfoByAsk")]
        public ActionResult<IEnumerable<AskDTO>> GetInfoByAsk(string Pask)
        {
            //acá creamos un linq que combina información de dos entidades 
            //(user inner join userrole) y la agreaga en el objeto dto de usuario 

            var query = (from u in _context.Asks
                         select new
                         {
                             idPregunta = u.AskId,
                             fecha = u.Date,
                             pregunta = u.Ask1,
                             idUsuario = u.UserId,
                             IdEstadoPregunta = u.AskStatusId,
                             activo = u.IsStrike,
                             URLimagen = u.ImageUrl,
                             detallePregunta = u.AskDetail
                         }).ToList();

            //creamos un objeto del tipo que retorna la función 
            List<AskDTO> list = new List<AskDTO>();

            foreach (var item in query)
            {
                AskDTO NewItem = new AskDTO()
                {
                    IdPregunta = item.idPregunta,
                    fecha = item.fecha,
                    pregunta = item.pregunta,
                    idusuario = item.idUsuario,
                    idEstadoPregunta = item.IdEstadoPregunta,
                    activo = item.activo,
                    urlUmagen = item.URLimagen,
                    detallePregunta = item.detallePregunta
                };

                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }

            return list;

        }

        // PUT: api/Asks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsk(long id, AskDTO ask)
        {
            if (id != ask.IdPregunta)
            {
                return BadRequest();
            }
            Ask? NewEFAsk = GetAskByID(id);

            if (NewEFAsk != null)
            {
                NewEFAsk.Date = ask.fecha;
                NewEFAsk.Ask1 = ask.pregunta;
                NewEFAsk.UserId = ask.idusuario;
                NewEFAsk.AskStatusId = ask.idEstadoPregunta;
                NewEFAsk.ImageUrl = ask.urlUmagen;
                NewEFAsk.AskDetail = ask.detallePregunta;


                _context.Entry(NewEFAsk).State = EntityState.Modified;

            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private Ask? GetAskByID(long id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Asks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ask>> PostAsk(Ask ask)
        {
          if (_context.Asks == null)
          {
              return Problem("Entity set 'AnswersDBContext.Asks'  is null.");
          }
            _context.Asks.Add(ask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsk", new { id = ask.AskId }, ask);
        }

        // DELETE: api/Asks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsk(long id)
        {
            if (_context.Asks == null)
            {
                return NotFound();
            }
            var ask = await _context.Asks.FindAsync(id);
            if (ask == null)
            {
                return NotFound();
            }

            _context.Asks.Remove(ask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AskExists(long id)
        {
            return (_context.Asks?.Any(e => e.AskId == id)).GetValueOrDefault();
        }
        private Ask? GetAskByID(int id)
        {
            var ask = _context.Asks.Find(id);

            //var user = _context.Users?.Any(e => e.UserId == id);

            return ask;
        }
    }
}
