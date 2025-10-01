--Posicionamiento
USE master
GO

--Crear base de datos
CREATE DATABASE MoviesAppDB
GO

--Usar base de datos creada
USE MoviesAppDB
GO

--Crear tablas
CREATE TABLE Director(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(200) NOT NULL UNIQUE,
	Nationality VARCHAR(100) NULL,
	Age INT CHECK (Age >= 18),
	Active BIT DEFAULT 0 
);
GO

CREATE TABLE Movies(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(200) NOT NULL UNIQUE,
	ReleaseYear Date NULL,
	Gender VARCHAR(50) NULL,
	Duration Time NULL,
	FKDirector VARCHAR(200),
	CONSTRAINT FK_Movies_Director
    FOREIGN KEY (FKDirector)
    REFERENCES Director(Name)
);
GO

--Insertar datos
INSERT INTO Director
VALUES 
('Edgar Silvestre', 'Mexicana', 27, 1),
('Fabian Ramirez', 'Mexicana', 27, 0);

INSERT INTO Movies
VALUES
('Movie1', GETDATE(), 'Gender1', SYSDATETIME(), 'Fabian Ramirez'),
('Movie2', DATEADD(DAY, 1, GETDATE()), 'Gender2', DATEADD(HOUR, 1, SYSDATETIME()), 'Edgar Silvestre');

--Consultar datos
SELECT Id, Name Nombre, Nationality Nacionalidad, Age Edad, IIF(Active = 1, 'Activo', 'Inactivo') Estatus  FROM Director
SELECT Id, Name AS Nombre, FORMAT(ReleaseYear, 'dd/MM/yyyy') AS Lanzamiento, Gender AS Genero, CONVERT(VARCHAR(5), Duration, 108) AS Duracion, FKDirector AS Director FROM Movies

--Posicionamiento
USE master
GO

--Eliminar base de datos
DROP DATABASE MoviesAppDB
GO