using AniversariantesSubti.Models;

namespace AniversariantesSubti.Repositories.Interfaces;

public interface IAniversariantesRepository
{
    IEnumerable<Aniversariante> ObterTodosProximo7dias();
    Aniversariante? ObterPorId(int Id);
    IEnumerable<Aniversariante> ObterPorNome(string Nome);
    IEnumerable<Aniversariante> ObterPorMes(int mes);
    public void Adicionar(Aniversariante aniversariantes);
    public void Remover(int Id);
}
