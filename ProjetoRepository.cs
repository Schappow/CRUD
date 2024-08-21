using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exo.WebApi.Repositories
{
    public class ProjetoRepository
    {
        private readonly ExoContext _context;

        public ProjetoRepository(ExoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Projeto>> ObterTodos()
        {
            return await _context.Projetos.ToListAsync();
        }

        public async Task Criar(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
        }

        public async Task<Projeto> ObterPorId(int id)
        {
            return await _context.Projetos.FindAsync(id);
        }

        public async Task Atualizar(int id, Projeto projeto)
        {
            var projetoBuscado = await ObterPorId(id);
            if (projetoBuscado != null)
            {
                projetoBuscado.NomeDoProjeto = projeto.NomeDoProjeto;
                projetoBuscado.Area = projeto.Area;
                projetoBuscado.Status = projeto.Status;
                _context.Projetos.Update(projetoBuscado);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Projeto não encontrado");
            }
        }

        public async Task Deletar(int id)
        {
            var projetoBuscado = await ObterPorId(id);
            if (projetoBuscado != null)
            {
                _context.Projetos.Remove(projetoBuscado);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Projeto não encontrado");
            }
        }
    }
}