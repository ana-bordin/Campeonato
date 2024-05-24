CREATE OR ALTER PROC Maior_Numero_Gols_Por_Time
AS
BEGIN
	SELECT Nome, MAX(Gols) AS Maior_Numero_Gols
		FROM (
			SELECT TC.Nome, MAX(P.Gols_Time_Casa) AS Gols
				FROM Partida AS P
					INNER JOIN Time_Campeonato AS TC ON P.Id_Time_Casa = TC.Id
				WHERE P.Id_Time_Casa = TC.Id
				GROUP BY TC.Nome
		
			UNION ALL

			SELECT TC.Nome, MAX(P.Gols_Time_Visitante) AS Gols
				FROM Partida AS P
					INNER JOIN Time_Campeonato AS TC ON P.Id_Time_Visitante = TC.Id
			WHERE P.Id_Time_Visitante = TC.Id
			GROUP BY TC.Nome
		) AS Selecao_Maior_Numero_Gol
			GROUP BY Nome;	
END

EXEC Maior_Numero_Gols_Por_Time