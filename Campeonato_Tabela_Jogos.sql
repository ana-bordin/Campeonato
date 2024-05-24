CREATE OR ALTER PROC Tabela_Jogos
AS
BEGIN
	SELECT P.Id, TC.Nome AS Nome_Time_Casa, TV.Nome AS Nome_Time_Visitante, CONVERT(VARCHAR, Gols_Time_Casa) + 'x' + CONVERT(VARCHAR, Gols_Time_Visitante) AS Placar
		FROM Partida AS P
			INNER JOIN Time_Campeonato AS TC 
				ON P.Id_Time_Casa = TC.Id
			INNER JOIN Time_Campeonato AS TV 
				ON P.Id_Time_Visitante = TV.Id		
END

EXEC Tabela_Jogos