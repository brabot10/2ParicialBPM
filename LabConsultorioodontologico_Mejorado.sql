-- DDL
CREATE DATABASE Parcial2BPM


USE master
GO
CREATE LOGIN usrparcial2 WITH PASSWORD=N'12345678',
	DEFAULT_DATABASE=Parcial2BPM,
	CHECK_EXPIRATION=OFF,
	CHECK_POLICY=ON
GO
USE Parcial2BPM
GO        
CREATE USER usrparcial2 FOR LOGIN usrparcial2
GO
ALTER ROLE db_owner ADD MEMBER usrparcial2
GO



DROP TABLE Serie;

CREATE TABLE Serie(
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  titulo VARCHAR(250) NOT NULL,
  sinopsis VARCHAR(5000) NOT NULL,
  director VARCHAR(100) NOT NULL,
  duracion INT NOT NULL,
  fechaEstreno DATE NOT NULL,
);

ALTER TABLE Serie ADD estado SMALLINT NOT NULL DEFAULT 1; -- -1: Eliminación lógica, 0: Inactivo, 1: Activo


CREATE PROC paSerieListar @parametro VARCHAR(50) 
AS
  SELECT id, titulo, sinopsis, director, duracion, fechaEstreno, estado 
  FROM Serie  --De que tabla lo tomaremos
  WHERE estado<>-1 AND titulo LIKE '%'+REPLACE(@parametro,' ','%')+'%'; -- Busqueda por titulo

EXEC paSerieListar 'Juan';


--DML

INSERT INTO Serie (titulo, sinopsis, director, duracion, fechaEstreno)
VALUES ('Antes de ti', 'Pelicula Romantica', 'Pérez',  987, '2023-07-20'),
       ('Médico', 'Serie Medica', 'García',  120, '2023-07-20');


SELECT * FROM Serie;








