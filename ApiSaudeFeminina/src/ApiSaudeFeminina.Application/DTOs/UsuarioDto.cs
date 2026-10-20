namespace ApiSaudeFeminina.Application.DTOs
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? FaseVida { get; set; }
        public bool? UsaAnticoncepcional { get; set; }
        public int? DesejaEngravidar { get; set; }
        public int? Objetivo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateUsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        public bool? UsaAnticoncepcional { get; set; }
        public int? DesejaEngravidar { get; set; }  // 0=Não 1=Sim 2=Talvez
        public int? Objetivo { get; set; }           // 1=MonitorarCiclo 2=Engravidar 3=EvitarGravidez 4=AcompanharSintomas
    }

    public class UpdateUsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public bool? UsaAnticoncepcional { get; set; }
        public int? DesejaEngravidar { get; set; }  // 0=Não 1=Sim 2=Talvez
        public int? Objetivo { get; set; }           // 1=MonitorarCiclo 2=Engravidar 3=EvitarGravidez 4=AcompanharSintomas
    }

    public class UpdateSenhaDto
    {
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }

    public class UpdateDadosSaudeDto
    {
        public decimal? Peso { get; set; }
        public decimal? Altura { get; set; }
        public string? TipoSanguineo { get; set; }
        public string? FaseVida { get; set; }
    }
}
