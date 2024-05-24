CREATE OR ALTER PROC Vincular_Times_Partida
AS
BEGIN
	DECLARE Time_Casa_Cursor CURSOR FOR
		SELECT Id
		FROM Time_Campeonato
	
	OPEN Time_Casa_Cursor

	DECLARE @Id_Time_Casa INT
	
	FETCH NEXT FROM Time_Casa_Cursor
	INTO @Id_Time_Casa
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE Time_Visitante_Cursor CURSOR FOR
			SELECT Id
			FROM Time_Campeonato
	
		OPEN Time_Visitante_Cursor
		
		DECLARE @Id_Time_Visitante INT
		
		FETCH NEXT FROM Time_Visitante_Cursor
		INTO @Id_Time_Visitante
	
		WHILE @@FETCH_STATUS = 0
		BEGIN	
			IF @Id_Time_Casa != @Id_Time_Visitante
			BEGIN
				INSERT INTO Partida (Id_Time_Casa, Id_Time_Visitante, Time_Visitante_Ganhou, Time_Casa_Ganhou, Empate)
					VALUES (@Id_Time_Casa, @Id_Time_Visitante, 'F', 'F', 'F')
			END
			FETCH NEXT FROM Time_Visitante_Cursor
			INTO @Id_Time_Visitante
		END

		CLOSE Time_Visitante_Cursor
		DEALLOCATE Time_Visitante_Cursor
	
	FETCH NEXT FROM Time_Casa_Cursor
	INTO @Id_Time_Casa
	END

	CLOSE Time_Casa_Cursor
	DEALLOCATE Time_Casa_Cursor	
END

EXEC Vincular_Times_Partida
