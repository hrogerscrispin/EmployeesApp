create database EmpleadosDB;
use EmpleadosDB;

create table Puesto
(
	PuestoId int identity(1,1) not null primary key,
	Descripcion varchar(100)
)

create table Estado
(
	EstadoId int identity(1,1) not null primary key,
	Descripcion varchar(20)
)

create table Empleados
(
	EmpleadoId int identity(1,1) not null primary key,
	Fotografia varchar(MAX),
	Nombre varchar(100),
	Apellido varchar(100),
	PuestoId int constraint FK_PuestoId foreign key references Puesto(PuestoId),
	FechaNacimiento date,
	FechaContratacion date,
	Direccion varchar(100),
	Telefono varchar(20),
	CorreoElectronico varchar(50),
	EstadoId int constraint FK_EstadoId foreign key references Estado(EstadoId)
)

Insert into Estado values('Activo'),('No activo')
Insert into Puesto values('Programador Front-End'),('Ingeniero de Software'),('Tecnico de redes'),('Contable'),('Administrador'),('Analista de datos')
Insert into Puesto values('Técnico Hardware')


Update puesto set Descripcion =
Case 
when PuestoId = 1 then 'Ingeniero de Sofware'
when PuestoId = 2 then  'Programador Back-End'
when puestoid = 3 then 'Programador Front-End' end 
where puestoid in (2,1,3)


select * from puesto
select * from estado
select * from empleados


insert into Empleados values(NULL,'Julie','Sandoval', 4, '1989-3-1','2015-11-22','Ensanche Naco, sto. dgo.','829-246-9548','jsandoval@hotmail.com',1)

select a.nombre +' '+ a.apellido [Nombre Completo], b.descripcion [Puesto], a.fechaNacimiento, a.Fechacontratacion, a.direccion,
a.telefono, a.correoelectronico, c.descripcion [Estado]
from empleados a
join puesto b on a.puestoid = b.puestoid
join estado c on a.estadoid= c.estadoid