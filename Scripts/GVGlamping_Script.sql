USE master;
GO

-- Verifica si la base de datos existe y elimínala
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'GVGlamping')
    DROP DATABASE GVGlamping;
GO

-- Crea la nueva base de datos
CREATE DATABASE GVGlamping;
GO

-- Cambia el contexto a la nueva base de datos
USE GVGlamping;
GO

create table Permisos (
	IdPermiso int primary key identity(1,1),
	NomPermiso varchar(50)
);

create table Roles (
	IdRol int primary key identity(1,1),
	NomRol varchar(20),
	Estado bit,
);

create table PermisosRoles (
	IdPermisosRoles int primary key identity(1,1),
	IdRol int,
	IdPermiso int,
	foreign key (IdRol) references Roles(IdRol),
	foreign key (IdPermiso) references Permisos(IdPermiso)
);

create table Imagenes (
	IdImagen int primary key identity(1,1),
	UrlImagen nvarchar(max)
);




create table TipoHabitaciones(
	IdTipoHabitacion int primary key identity(1,1),
	NombreTipoHabitacion varchar(20),
	NumeroPersonas int,
	Estado bit,
);

create table Habitaciones(
	IdHabitacion int primary key identity(1,1),
	IdTipoHabitacion int,
	NombreHabitacion varchar(50),
	Estado bit,
	Descripcion varchar(50),
	Costo decimal,

	foreign key (IdTipoHabitacion) references TipoHabitaciones(IdTipoHabitacion)
);

create table ImagenHabitacion(
	IdImagenHabi int primary key identity(1,1),
	IdImagen int,
	IdHabitacion int,

	foreign key (IdImagen) references Imagenes(IdImagen),
	foreign key (IdHabitacion) references Habitaciones(IdHabitacion)
);

create table TipoServicios(
	IdTipoServicio int primary key identity(1,1),
	NombreTipoServicio varchar(50)
);

create table Servicios(
	IdServicio int primary key identity(1,1),
	IdTipoServicio int,
	NomServicio varchar(50),
	Costo decimal, 
	Estado bit,

	foreign key (IdTipoServicio) references TipoServicios(IdTipoServicio)
);

create table ImagenServicio(
	IdImagenServi int primary key identity(1,1),
	IdImagen int,
	IdServicio int,

	foreign key (IdImagen) references Imagenes(IdImagen),
	foreign key (IdServicio) references Servicios(IdServicio)
);

create table Paquetes(
	IdPaquete int primary key identity(1,1),
	NomPaquete varchar(50),
	Costo decimal,
	IdHabitacion int,
	Estado bit,
	Descripcion varchar(200),

	foreign key (IdHabitacion) references Habitaciones (IdHabitacion)

);

create table ImagenPaquete(
	IdImagenPaque int primary key identity(1,1),
	IdImagen int,
	IdPaquete int,

	foreign key (IdImagen) references Imagenes(IdImagen),
	foreign key (IdPaquete) references Paquetes(IdPaquete)
);

create table PaqueteServicio(
	IdPaqueteServicio int primary key identity(1,1),
	IdPaquete int,
	IdServicio int,
	Costo decimal,

	foreign key (IdPaquete) references Paquetes(IdPaquete),
	foreign key (IdServicio) references Servicios(IdServicio)
);

create table TipoDocumento(
	IdTipoDocumento int primary key identity(1,1),
	NomTipoDocumento varchar(50),
);

create table Clientes(

	NroDocumento int primary key identity,
	IdTipoDocumento int,
	Nombres varchar(20),
	Apellidos varchar(20),
	Celular varchar(12),
	Correo varchar(30),
	Contrasena varchar(15),
	Estado bit,
	IdRol int,

	foreign key (IdRol) references Roles(IdRol),
	foreign key (IdTipoDocumento) references TipoDocumento(IdTipoDocumento)
);

create table MetodoPago(
	IdMetodoPago int primary key identity(1,1),
	NomMetodoPago varchar(20)
);

create table EstadosReserva(
	IdEstadoReserva int primary key identity(1,1),
	NombreEstadoReserva varchar(15)
);

create table Reservas(
	IdReserva int primary key identity(1,1),
	NroDocumentoCliente int,
	FechaReserva datetime default getdate(),
	FechaInicio datetime,
	FechaFinalizacion datetime,
	IVA decimal,
	MontoTotal decimal,
	MetodoPago int,
	IdEstadoReserva int default '1'

	foreign key (NroDocumentoCliente) references Clientes(NroDocumento),
	foreign key (IdEstadoReserva) references EstadosReserva(IdEstadoReserva),
	foreign key (MetodoPago) references MetodoPago(IdMetodoPago)
);


create table DetalleReservaServicio (
	IdDetalleReservaServicio int primary key identity(1,1),
	IdServicio int,
	IdReserva int,
	Cantidad int,
	Costo decimal,

	foreign key (IdServicio) references Servicios(IdServicio),
	foreign key (IdReserva) references Reservas(IdReserva)
);


create table DetalleReservaPaquete (
	DetalleReservaPaquete int primary key identity(1,1),
	IdPaquete int,
	IdReserva int,
	Cantidad int,
	Costo decimal,

	foreign key (IdPaquete) references Paquetes(IdPaquete),
	foreign key (IdReserva) references Reservas(IdReserva)
);

create table Abono(
	IdAbono int primary key identity(1,1),
	IdReserva int,
	FechaAbono datetime default getdate(),
	CantAbono decimal,

	foreign key (IdReserva) references Reservas(IdReserva)
);

create table ImagenAbono(
	IdImagenAbono int primary key identity(1,1),
	IdImagen int,
	IdAbono int,

	foreign key (IdImagen) references Imagenes(IdImagen),
	foreign key (IdAbono) references Abono(IdAbono)
);

GO

insert into TipoDocumento(NomTipoDocumento)
values ('C.C'),
	   ('T.I')

insert into Roles(NomRol,Estado)
values ('Administrador',1),
       ('Cliente',1)

insert into TipoHabitaciones(NombreTipoHabitacion,NumeroPersonas,Estado)
values ('Simple',2,1),
       ('Doble',4,1)

insert into Habitaciones(IdTipoHabitacion,NombreHabitacion,Estado,Descripcion,Costo)
values (1,'Habitacion Simple',1,'Habitacion para dos personas',200000),
       (2,'Habitacion Doble',1,'Habitacion para cuatro personas',300000)

insert into TipoServicios(NombreTipoServicio)
values ('Comida'),
	   ('Aire libre')

insert into Servicios(IdTipoServicio,NomServicio,Costo,Estado)
values (1,'Desayuno',30000,1),
	   (1,'Almuerzo',30000,1),
	   (1,'Cena',30000,1),
	   (2,'Cabalgata',100000,1)


insert into MetodoPago(NomMetodoPago)
values ('Efectivo'),
       ('Targeta'),
	   ('DataCuerpo')

insert into EstadosReserva(NombreEstadoReserva)
values ('Reservado'),
       ('Por Confirmar'),
	   ('Confirmado'),
	   ('En Ejecución'),
	   ('Anulado'),
	   ('Finalizado')

insert into Permisos(NomPermiso)
values ('Dashboard'),
	   ('Listar Roles'),
	   ('Buscar Roles'),
	   ('Crear Roles'),
	   ('Ver Detalles Roles'),
	   ('Editar Roles'),
	   ('Cambiar Estado Roles'),
	   ('Listar Usuarios'),
	   ('Buscar Usuarios'),
	   ('Crear Usuarios'),
	   ('Ver Detalles Usuarios'),
	   ('Editar Usuarios'),
	   ('Cambiar Estado Usuarios'),
	   ('Listar Clientes'),
	   ('Buscar Clientes'),
	   ('Crear Clientes'),
	   ('Ver Detalles Clientes'),
	   ('Editar Clientes'),
	   ('Cambiar Estado Clientes'),
	   ('Listar Habitaciones'),
	   ('Buscar Habitaciones'),
	   ('Crear Habitaciones'),
	   ('Ver Detalles Habitaciones'),
	   ('Editar Habitaciones'),
	   ('Cambiar Estado Habitaciones'),
	   ('Listar Tipo Habitaciones'),
	   ('Buscar Tipo Habitaciones'),
	   ('Crear Tipo Habitaciones'),
	   ('Editar Tipo Habitaciones'),
	   ('Cambiar Estado Tipo Habitaciones'),
	   ('Listar Servicios'),
	   ('Buscar Servicios'),
	   ('Crear Servicios'),
	   ('Ver Detalles Servicios'),
	   ('Editar Servicios'),
	   ('Cambiar Estado Servicios'),
	   ('Listar Tipo Servicio'),
	   ('Buscar Tipo Servicios'),
	   ('Crear Tipo Servicios'),
	   ('Editar Tipo Servicios'),
	   ('Listar Paquetes'),
	   ('Buscar Paquetes'),
	   ('Crear Paquetes'),
	   ('Ver Detalles Paquetes'),
	   ('Editar Paquetes'),
	   ('Cambiar Estado Paquetes'),
	   ('Listar Reservas'),
	   ('Buscar Reservas'),
	   ('Crear Reservas'),
	   ('Ver Detalles Reservas'),
	   ('Editar Reservas'),
	   ('Cambiar Estado Reservas'),
	   ('Anular Reserva'),
	   ('Listar Abono'),
	   ('Buscar Abono'),
	   ('Crear Abono'),
	   ('Ver Detalle Abono'),
	   ('Anular Abono')


insert into PermisosRoles(IdRol,IdPermiso)
values	(1,1),
		(1,2),
		(1,3),
		(1,4),
		(1,5),
		(1,6),
		(1,7),
		(1,8),
		(1,9),
		(1,10),
		(1,11),
		(1,12),
		(1,13),
		(1,14),
		(1,15),
		(1,16),
		(1,17),
		(1,18),
		(1,19),
		(1,20),
		(1,21),
		(1,22),
		(1,23),
		(1,24),
		(1,25),
		(1,26),
		(1,27),
		(1,28),
		(1,29),
		(1,30),
		(1,31),
		(1,32),
		(1,33),
		(1,34),
		(1,35),
		(1,36),
		(1,37),
		(1,38),
		(1,39),
		(1,40),
		(1,41),
		(1,42),
		(1,43),
		(1,44),
		(1,45),
		(1,46),
		(1,47),
		(1,48),
		(1,49),
		(1,50),
		(1,51),
		(1,52),
		(1,53),
		(1,54),
		(1,55),
		(1,56),
		(1,57),
		(1,58)