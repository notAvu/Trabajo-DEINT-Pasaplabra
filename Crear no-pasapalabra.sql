CREATE DATABASE Pasapalabra
GO 
USE Pasapalabra
GO
CREATE TABLE Partidas
(
ID int IDENTITY CONSTRAINT PK_Pregunta PRIMARY KEY,
Nickname varchar(30) NULL, 
aciertos int NOT NULL,
fallos int NOT NULL,
tiempo time NOT NULL,
puntuacion AS aciertos-fallos
)
GO
CREATE OR ALTER VIEW Ranking AS
(
SELECT Nickname, tiempo ,aciertos, fallos, puntuacion FROM Partidas ORDER BY puntuacion, tiempo
)