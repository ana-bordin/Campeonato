--• Quem é o campeão no final do campeonato?
	SELECT TOP(1) Nome, Apelido, Pontuacao
		FROM Time_Campeonato
		ORDER BY Pontuacao DESC

--• Como faremos para verificar os 5 primeiros times do campeonato? 
	SELECT Nome, Apelido, Pontuacao
		FROM Time_Campeonato
		ORDER BY Pontuacao DESC

--• Como faremos para calcular a pontuação dos times de acordo com os jogos que aconteceram? 
	--EXEC Gerar_Resultado_Partida

--• Como ficaria com trigger ou como ficaria com uma stored procedure?

--• Quem é o time que mais fez gols no campeonato? 
	SELECT TOP(1) Nome, Apelido, Gols_Feitos
		FROM Time_Campeonato
		ORDER BY Gols_Feitos DESC

--• Quem é que tomou mais gols no campeonato?
	SELECT TOP(1) Nome, Apelido, Gols_Tomados
		FROM Time_Campeonato
		ORDER BY Gols_Tomados DESC

--• Qual é o jogo que teve mais gols?
	--EXEC Jogo_Com_Mais_Gols

--• Qual é o maior número de gols que cada time fez em um único jogo?
	--EXEC Maior_Numero_Gols_Por_Time