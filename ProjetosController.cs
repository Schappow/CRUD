using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetosController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Projeto>), 200)]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _projetoRepository.ObterTodos());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Projeto), 201)]
        public async Task<IActionResult> Criar([FromBody] Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projetoRepository.Criar(projeto);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Projeto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            Projeto projeto = await _projetoRepository.ObterPorId(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return Ok(projeto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projetoRepository.Atualizar(id, projeto);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _projetoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}