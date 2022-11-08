--Crear tablas

Use PruebaSara
go

CREATE TABLE Profesores(
	id int primary key identity(1,1),
	[PrimerNombre] [varchar](max) NOT NULL,
	[SegundoNombre] [varchar](max) NULL,
	[PrimerApellido] [varchar](max) NOT NULL,
	[SegundoApellido] [varchar](max) NULL,
	[Correo] [varchar](max) NULL,
	[Telefono] [varchar](50) NULL,
	[FechaNacimiento] [date] NULL
	);
GO

Use PruebaSara
go
CREATE TABLE Materia (
    IDMateria int primary key identity(1,1),
	[Nombre_materia] [varchar](max) NOT NULL,    
);
GO

Use PruebaSara
go
CREATE TABLE Profesores_Materia (
    fk_IDProfesor int,
	fk_IDMateria int,
	CONSTRAINT fk_Profesor FOREIGN KEY (fk_IDProfesor) REFERENCES Profesores (id),
	CONSTRAINT fk_Materia FOREIGN KEY (fk_IDMateria) REFERENCES Materia (IDMateria),
);
GO

Use PruebaSara
go
CREATE TABLE Estudiantes (
    idEstudiante int primary key identity(1,1),
	[PrimerNombre] [varchar](max) NOT NULL,
	[SegundoNombre] [varchar](max) NULL,
	[PrimerApellido] [varchar](max) NOT NULL,
	[SegundoApellido] [varchar](max) NULL,
	[Correo] [varchar](max) NULL,
	[Telefono] [varchar](50) NULL,
	[FechaNacimiento] [date] NULL
	);
GO

Use PruebaSara
go
CREATE TABLE Notas (
    IDNota int primary key identity(1,1),
    [fk_IDEstudiante] int,
    [Nota] decimal(5,2) NOT NULL,
    [Fecha] datetime NOT NULL,
	CONSTRAINT fk_Estudaiantes FOREIGN KEY (fk_IDEstudiante) REFERENCES Estudiantes (IDEstudiante),
);

GO
--========================================================================================================

--Procedimientos almacenados profesores
USE [pruebaSara]
GO

CREATE PROCEDURE [dbo].[ActualizarDocente1]   
	@Id int,
    @PrimerNombre nvarchar(255),   
    @SegundoNombre nvarchar(255) = null,   
    @PrimerApellido nvarchar(255),
	@SegundoApellido nvarchar(255) = null,
	@Telefono nvarchar(255),
	@Correo nvarchar(255),
	@FechaNacimiento DateTime
	
AS  
update Profesores set PrimerNombre=@PrimerNombre,SegundoNombre=@SegundoNombre,PrimerApellido=@PrimerApellido,SegundoApellido=@SegundoApellido,Correo = @Correo, Telefono=@Telefono, FechaNacimiento=@FechaNacimiento where id=@Id
GO
--------------------------------------------------------------------------------------------------------

USE [pruebaSara]
GO

CREATE PROCEDURE [dbo].[ListarDocentes]
AS   
select * from Profesores

GO

-------------------------------------------------------------------------------------------------------------
USE [pruebaSara]
GO

CREATE PROCEDURE [dbo].[RegistrarDocente]  
    @PrimerNombre nvarchar(255),   
    @SegundoNombre nvarchar(255),   
    @PrimerApellido nvarchar(255),
	@SegundoApellido nvarchar(255),
	@Telefono nvarchar(255),
	@Correo nvarchar(255),
	@FechaNacimiento DateTime
AS  
insert into Profesores(PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono, Correo,FechaNacimiento) values(@PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Telefono,@Correo,@FechaNacimiento)
GO
-------------------------------------------------------------------------------------

CREATE PROCEDURE EliminarProfesor(
@id_profesor int
)
as
begin

delete from Profesores where id = @id_profesor

end

go
--====================================================================================
--procedimientos almacenados estudiantes

CREATE PROCEDURE [dbo].[ActualizarEstudiante]   
	@Id int,
    @PrimerNombre nvarchar(255),   
    @SegundoNombre nvarchar(255) = null,   
    @PrimerApellido nvarchar(255),
	@SegundoApellido nvarchar(255) = null,
	@Telefono nvarchar(255),
	@Correo nvarchar(255),
	@FechaNacimiento DateTime
	
AS  
update Estudiantes set PrimerNombre=@PrimerNombre,SegundoNombre=@SegundoNombre,PrimerApellido=@PrimerApellido,SegundoApellido=@SegundoApellido,Correo = @Correo, Telefono=@Telefono, FechaNacimiento=@FechaNacimiento where idEstudiante=@Id
GO

--------------------------------------------------------------------------------------------------------

USE [pruebaSara]
GO

CREATE PROCEDURE [dbo].[ListaEstudiantes]
AS   
select * from Estudiantes

GO

-------------------------------------------------------------------------------------------------------------
USE [pruebaSara]
GO

CREATE PROCEDURE [dbo].[RegistrarEstudiante]  
    @PrimerNombre nvarchar(255),   
    @SegundoNombre nvarchar(255),   
    @PrimerApellido nvarchar(255),
	@SegundoApellido nvarchar(255),
	@Telefono nvarchar(255),
	@Correo nvarchar(255),
	@FechaNacimiento DateTime
AS  
insert into Estudiantes(PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Telefono, Correo,FechaNacimiento) values(@PrimerNombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@Telefono,@Correo,@FechaNacimiento)
GO

-----------------------------------------------------------------------------------------
CREATE PROCEDURE EliminarEstudainte(
@id_profesor int
)
as
begin

delete from Estudiantes where idEstudiante = @id_profesor

end

go

--======================================================================================
--procedimiento alamacenado materias

USE [pruebaSara]
GO


CREATE PROCEDURE [dbo].[ActualizarMateria]   
	@NombreMateria nvarchar(255)  ,
	@Id int
AS  
update Materia set Nombre_materia=@NombreMateria where IDMateria=@Id
GO

-- ================================================
CREATE PROCEDURE [dbo].[ListaMaterias]
AS   
select * from Materia

GO

-------------------------------------------------------------------------------------------------------------
USE [pruebaSara]
GO


CREATE PROCEDURE [dbo].[RegistrarMateria]  
    @NombreMateria nvarchar(255)  
   
AS  
insert into Materia(Nombre_materia) values(@NombreMateria)
GO


---------------------------------------------------------------------------------------------

CREATE PROCEDURE EliminarMateria(
@id int
)
as
begin

delete from Materia where IDMateria = @id

end

go