CREATE OR ALTER PROC Gerar_Resultado_Partida
	@Id_Partida INT, 
	@Gols_Time_Casa INT,
	@Gols_Time_Visitante INT
AS
BEGIN
	UPDATE Partida
		SET Gols_Time_Casa = @Gols_Time_Casa,
			Gols_Time_Visitante = @Gols_Time_Visitante	
		WHERE Id = @Id_Partida
		
	DECLARE @Id_Time_Casa INT,
			@Id_Time_Visitante INT
		
		SELECT @Id_Time_Casa = Id_Time_Casa, @Id_Time_Visitante = Id_Time_Visitante
		FROM Partida
			WHERE Id = @Id_Partida 

		UPDATE Time_Campeonato
			SET Gols_Feitos += @Gols_Time_Casa,
				Gols_Tomados += @Gols_Time_Visitante
				WHERE Id = @Id_Time_Casa
		
		UPDATE Time_Campeonato
			SET Gols_Feitos += @Gols_Time_Visitante,
				Gols_Tomados += @Gols_Time_Casa
				WHERE Id = @Id_Time_Visitante

		IF @Gols_Time_Casa < @Gols_Time_Visitante
		BEGIN
			UPDATE Partida
			SET Time_Visitante_Ganhou = 'V'
			WHERE Id = @Id_Partida	

			UPDATE Time_Campeonato
			SET Pontuacao += 5
			WHERE Id = @Id_Time_Visitante
		END
		
		IF @Gols_Time_Casa > @Gols_Time_Visitante
		BEGIN
			UPDATE Partida
			SET Time_Casa_Ganhou = 'V'
			WHERE Id = @Id_Partida	

			UPDATE Time_Campeonato
			SET Pontuacao += 3
			WHERE Id = @Id_Time_Casa
		END
		
		IF @Gols_Time_Casa = @Gols_Time_Visitante
		BEGIN
			UPDATE Partida
			SET Empate = 'V'
			WHERE Id = @Id_Partida	

			UPDATE Time_Campeonato
			SET Pontuacao += 1
			WHERE Id = @Id_Time_Casa 

			UPDATE Time_Campeonato
			SET Pontuacao += 1
			WHERE Id = @Id_Time_Visitante 
		END
END

EXEC Gerar_Resultado_Partida 101, 1, 1

