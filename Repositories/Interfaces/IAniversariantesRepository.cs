using AniversariantesSubti.Models;

namespace AniversariantesSubti.Repositories.Interfaces;

public interface IAniversariantesRepository
{
    IEnumerable<Aniversariantes> ObterTodosProximo7dias();
    Aniversariantes? ObterPorId(int Id);
    IEnumerable<Aniversariantes> ObterPorNome(string Nome);
    IEnumerable<Aniversariantes> ObterPorMes(int mes);
    void Adicionar(Aniversariantes aniversariantes);
    void Remover(int Id);
}
