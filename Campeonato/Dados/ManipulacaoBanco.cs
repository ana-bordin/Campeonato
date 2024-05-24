using Campeonato.Entidade;
using Microsoft.Data.SqlClient;

namespace Campeonato.Dados
{
    public class ManipulacaoBanco
    {
        static readonly Conexao _conn = new Conexao();
        static readonly SqlConnection _conexaoSql = new SqlConnection(_conn.PegarConexao());
        public ManipulacaoBanco()
        { }

        public void AdicionarTimes(Time time)
        {
            try
            {
                _conexaoSql.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Time_Campeonato(Nome, Apelido, Dt_Criacao) VALUES (@Nome, @Apelido, @Dt_Criacao);";

                SqlParameter nome = new SqlParameter("@Nome", System.Data.SqlDbType.VarChar, 100);
                SqlParameter apelido = new SqlParameter("@Apelido", System.Data.SqlDbType.VarChar, 100);
                SqlParameter dt_criacao = new SqlParameter("@Dt_Criacao", System.Data.SqlDbType.Date);

                nome.Value = time.Nome;
                apelido.Value = time.Apelido;
                dt_criacao.Value = time.Dt_Criacao;

                cmd.Parameters.Add(nome);
                cmd.Parameters.Add(apelido);
                cmd.Parameters.Add(dt_criacao);

                cmd.Connection = _conexaoSql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao Adicionar Times: " + e.Message);
            }

            _conexaoSql.Close();
        }

        public void CadastrarJogos()
        {
            try
            {
                _conexaoSql.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "EXEC Vincular_Times_Partida;";
                cmd.Connection = _conexaoSql;
                cmd.ExecuteNonQuery();
                RegistrarResultadoJogo();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao Adicionar Times: " + e.Message);
            }

            _conexaoSql.Close();
        }

        public void RegistrarResultadoJogo()
        {
            try
            {
                Random random = new Random();
                SqlCommand cmdSelecionar = new SqlCommand();

                cmdSelecionar.Connection = _conexaoSql;
                cmdSelecionar.CommandText = "SELECT COUNT(ID) FROM Partida";

                int qtdJogos = (int)cmdSelecionar.ExecuteScalar();

                for (int i = 0; i < qtdJogos; i++)
                {
                    SqlCommand cmdResultadoPartida = new SqlCommand();
                    cmdResultadoPartida.CommandText = "EXEC Gerar_Resultado_Partida @Partida, @NumGolsTimeCasa, @NumGolsTimeVisitante";

                    SqlParameter partida = new SqlParameter("@Partida", System.Data.SqlDbType.Int);
                    SqlParameter numGolsTimeCasa = new SqlParameter("@NumGolsTimeCasa", System.Data.SqlDbType.Int);
                    SqlParameter numGolsTimeVisitante = new SqlParameter("@NumGolsTimeVisitante", System.Data.SqlDbType.Int);

                    partida.Value = 100 + i;
                    numGolsTimeCasa.Value = random.Next(0, 10);
                    numGolsTimeVisitante.Value = random.Next(0, 10);

                    cmdResultadoPartida.Parameters.Add(partida);
                    cmdResultadoPartida.Parameters.Add(numGolsTimeCasa);
                    cmdResultadoPartida.Parameters.Add(numGolsTimeVisitante);

                    cmdResultadoPartida.Connection = _conexaoSql;
                    cmdResultadoPartida.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao Registrar os Resultado dos Jogo: " + e.Message);
            }
        }

        public void ResetarTabelas()
        {
            try
            {
                _conexaoSql.Open();

                SqlCommand cmdResetar = new SqlCommand();

                cmdResetar.CommandText = "EXEC Resetar_Tabelas";
                cmdResetar.Connection = _conexaoSql;
                cmdResetar.ExecuteNonQuery();

                Console.WriteLine("Dados da tabelas apagados com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao apagar dados das tabelas: " + e.Message);
            }

            _conexaoSql.Close();
        }

        public string EscolherSelect(int op)
        {
            //PontuacaoCampeao
            if (op == 1)
                return "SELECT TOP(1) Nome, Apelido, Pontuacao, Gols_Tomados, Gols_Feitos FROM Time_Campeonato ORDER BY Pontuacao DESC;";
            //Pontuacao
            else if (op == 2)
                return "SELECT Nome, Apelido, Pontuacao, Gols_Tomados, Gols_Feitos FROM Time_Campeonato ORDER BY Pontuacao DESC;";
            //GolsFeitos
            else if (op == 3)
                return "SELECT TOP(1) Nome, Apelido, Pontuacao, Gols_Tomados, Gols_Feitos FROM Time_Campeonato ORDER BY Gols_Feitos DESC";
            //GolsTomados
            else
                return "SELECT TOP(1) Nome, Apelido, Pontuacao, Gols_Tomados, Gols_Feitos FROM Time_Campeonato ORDER BY Gols_Tomados DESC";
        }

        public void MostrarColocacaoTimes(int op)
        {
            string conexao = EscolherSelect(op);
            try
            {
                _conexaoSql.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = conexao;
                cmd.Connection = _conexaoSql;
                cmd.ExecuteNonQuery();

                if (op == 1)
                {
                    Console.WriteLine($"O GANHADOR FOI:");
                    LerDadosTime(cmd, 1);
                }

                if (op == 2)
                    LerDadosTime(cmd, 2);
                if (op == 3)
                    LerDadosTime(cmd, 3);
                if (op == 4)
                    LerDadosTime(cmd, 4);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao mostrar colocação: " + e.Message);
            }
            
            _conexaoSql.Close();
        }

        public void LerDadosTime(SqlCommand cmd, int tipo)
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                int colocacao = 1;
                while (reader.Read())
                {
                    string nome = reader.GetString(0);
                    string apelido = reader.GetString(1);
                    int pontuacao = reader.GetInt32(2);
                    int golsTomados = reader.GetInt32(3);
                    int golsFeitos = reader.GetInt32(4);

                    if (tipo == 2)
                        Console.WriteLine($"{colocacao}ª POSIÇÃO:");
                    Console.WriteLine($"Nome: {nome}, {apelido};");

                    if (tipo == 1 || tipo == 2)
                        Console.WriteLine($"Pontuação: {pontuacao};");

                    if (tipo == 3)
                        Console.WriteLine($"Quantidade de Gols Feitos: {golsFeitos};");

                    if (tipo == 4)
                        Console.WriteLine($"Quantidade de Gols Tomados: {golsTomados};");

                    Console.WriteLine("\n");
                    colocacao++;
                }
            }
        }

        public string ProcTabelaJogo(int op)
        {
            if (op == 1)
                return "EXEC Jogo_Com_Mais_Gols";
            else if (op == 2)
                return "EXEC Maior_Numero_Gols_Por_Time";
            else
                return "EXEC Tabela_Jogos";
        }

        public void OperacaoTabelaPartida(int op)
        {
            string conexao = ProcTabelaJogo(op);
            try
            {
                _conexaoSql.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = conexao;
                cmd.Connection = _conexaoSql;
                cmd.ExecuteNonQuery();

                if (op == 1)
                    MostrarTabelaJogo(cmd, 1);
                if (op == 2)
                {
                    Console.WriteLine("O maior número de gols que cada time fez em um único jogo");
                    MostrarTabelaJogo(cmd, 2);
                }   
                if (op == 3)
                    MostrarTabelaJogo(cmd, 3); 
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao selecionar tabela jogo: " + e.Message);
            }
            
            _conexaoSql.Close();
        }

        public void MostrarTabelaJogo(SqlCommand cmd, int op)
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (op == 1 || op == 3)
                    {
                        int id = reader.GetInt32(0);
                        string nomeTimeCasa = reader.GetString(1);
                        string nomeTimeVisitante = reader.GetString(2);
                        if (op == 1)
                        {
                            int golsTotais = reader.GetInt32(3);

                            Console.WriteLine($"O jogo com mais gols foi entre:");
                            Console.WriteLine($"Time da Casa: {nomeTimeCasa};");
                            Console.WriteLine($"Time da Visitante: {nomeTimeVisitante};");
                            Console.WriteLine($"Quantidade de Gols: {golsTotais};");
                        }
                        else
                        {
                            string resultado = reader.GetString(3);
                            Console.WriteLine($"| {id} | {nomeTimeCasa} | {nomeTimeVisitante} | {resultado} |");
                        }
                    }

                    if (op == 2)
                    {
                        string nomeTime = reader.GetString(0);
                        int maiorQtdGols = reader.GetInt32(1);
                        Console.WriteLine($"{nomeTime} fez {maiorQtdGols} gols; ");
                    }
                }
            }
        }
    }
}

