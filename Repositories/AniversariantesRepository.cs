using System.Collections;
using AniversariantesSubti.Models;
using AniversariantesSubti.Repositories.Interfaces;

namespace AniversariantesSubti.Repositories;

public class AniversariantesRepository : IAniversariantesRepository
{

    private readonly List<Aniversariante> _aniversariantes;


    public AniversariantesRepository()
    {
        _aniversariantes = new List<Aniversariante>
        {
            new Aniversariante { Id = 1, Nome = "Aniversariante 01", DataNascimento = new DateOnly(2002, 1, 13)},
            new Aniversariante { Id = 2, Nome = "Aniversariante 02", DataNascimento = new DateOnly(2000, 3, 16)},
            new Aniversariante { Id = 3, Nome = "Aniversariante 03", DataNascimento = new DateOnly(1998, 1, 29)},
            new Aniversariante { Id = 4, Nome = "Aniversariante 04", DataNascimento = new DateOnly(1995, 5, 22) }
        };
    }


    IEnumerable<Aniversariante> IAniversariantesRepository.ObterTodosProximo7dias()
    {
        return _aniversariantes.Where(p => p.DataNascimento.Month == DateTime.Now.Month &&
                                           p.DataNascimento.Day >= DateTime.Now.Day &&
                                           p.DataNascimento.Day < DateTime.Now.Day + 7);
    }

    //public Aniversariantes? ObterPorMes(int mes)
    //{
    //    return _aniversariantes.FirstOrDefault(x => x.Id == mes);
    //}

    public IEnumerable<Aniversariante> ObterPorNome(string nome)
    {
        return _aniversariantes.Where(x => x.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
    }
    public Aniversariante? ObterPorId(int Id)
    {
        return _aniversariantes.FirstOrDefault(x => x.Id == Id);
    }

    IEnumerable<Aniversariante> IAniversariantesRepository.ObterPorMes(int mes)
    {
        return _aniversariantes.Where(x => x.DataNascimento.Month == mes);
    }

    public void Adicionar(Aniversariante aniversariantes)
    {
        _aniversariantes.Add(aniversariantes);
    }
    public void IAniversariantesRepository.Remover(int Id)
    {
        var AniversarianteParaRemover = _aniversariantes.First(x => x.Id == Id);
        _aniversariantes.Remove(AniversarianteParaRemover);
    }


}