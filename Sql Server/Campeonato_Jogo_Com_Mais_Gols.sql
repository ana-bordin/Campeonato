CREATE OR ALTER PROC Jogo_Com_Mais_Gols
AS
BEGIN
	SELECT TOP(1) P.Id, TC1.Nome AS Nome_Time_Casa, TC2.Nome AS Nome_Time_Visitante, (P.Gols_Time_Casa + P.Gols_Time_Visitante) AS Gols_Totais 
		FROM Partida AS P 
			INNER JOIN Time_Campeonato AS TC1 
				ON P.Id_Time_Casa = TC1.Id INNER JOIN Time_Campeonato AS TC2 ON P.Id_Time_Visitante = TC2.Id 
			ORDER BY Gols_Totais DESC;	
END

EXEC Jogo_Com_Mais_Gols