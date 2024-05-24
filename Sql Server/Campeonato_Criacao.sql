CREATE DATABASE Campeonato;

USE Campeonato

CREATE TABLE Time_Campeonato
(
	Id INT IDENTITY(100,1) NOT NULL,
	Nome VARCHAR(100) NOT NULL,
	Apelido VARCHAR(100),
	Dt_Criacao DATE NOT NULL, 
	Pontuacao INT DEFAULT 0,
	Gols_Tomados INT DEFAULT 0, 
	Gols_Feitos INT DEFAULT 0

	CONSTRAINT pk_Time PRIMARY KEY (Id),
);

CREATE TABLE Partida  
(
	Id INT IDENTITY(100,1) NOT NULL,
	Id_Time_Casa INT NOT NULL,
	Id_Time_Visitante INT NOT NULL,
	Gols_Time_Casa INT DEFAULT 0,
	Gols_Time_Visitante INT DEFAULT 0,
	Time_Visitante_Ganhou CHAR(1) DEFAULT 'F' NOT NULL,
	Time_Casa_Ganhou CHAR(1) DEFAULT 'F' NOT NULL,
	Empate CHAR(1) DEFAULT 'F' NOT NULL,

	CONSTRAINT pk_Partida PRIMARY KEY (Id),
	CONSTRAINT fk_Partida_Time_Casa FOREIGN KEY (Id_Time_Casa) REFERENCES Time_Campeonato(Id),
	CONSTRAINT fk_Partida_Time_Visitante FOREIGN KEY (Id_Time_Visitante) REFERENCES Time_Campeonato(Id)
);