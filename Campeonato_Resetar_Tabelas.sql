CREATE OR ALTER PROC Resetar_Tabelas
AS
BEGIN
	DELETE 
		FROM Partida
	DBCC CHECKIDENT ('Partida', RESEED, 100);
	
	DELETE 
		FROM Time_Campeonato
	DBCC CHECKIDENT ('Time_Campeonato', RESEED, 100);
END

EXEC Resetar_Tabelas