using Microsoft.AspNetCore.Antiforgery;

namespace AniversariantesSubti.Models;

public class Aniversariante
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
}
