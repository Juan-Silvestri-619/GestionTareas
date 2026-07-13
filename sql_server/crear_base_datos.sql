CREATE DATABASE gestion_tareas;

USE gestion_tareas;

CREATE TABLE tarea(
	id INT IDENTITY(1,1) PRIMARY KEY,
	titulo VARCHAR(100) NOT NULL, 
	descripcion VARCHAR(250),
	completada BIT NOT NULL DEFAULT 0,
	fecha_creacion DATETIME NOT NULL DEFAULT GETDATE()
);