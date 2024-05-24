namespace Campeonato.Entidade
{
    public class Time
    {
        public string Nome {  get; set; }
        public string Apelido { get; set; }
        public DateOnly Dt_Criacao { get; set; }
        
        public Time() { }

        public Time(string nome, string apelido, DateOnly dt_Criacao)
        {
            Nome = nome;
            Apelido = apelido;
            Dt_Criacao = dt_Criacao;
        }
    }
}
