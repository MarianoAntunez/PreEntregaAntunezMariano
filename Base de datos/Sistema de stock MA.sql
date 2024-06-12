Create DataBase Sistema_de_Stock_MA
go

use Sistema_de_Stock_MA
go

--Tabla prdocutos
Create Table Productos
(
Id_producto int identity(1,1) primary key not null,
Codigo varchar(15)not null,
Nombre varchar(50)not null,
Descripcion varchar(100)not null,
Presentacion varchar(10)not null,
Costo_unitario decimal(12,2)not null,
Precio_venta decimal(12,2)not null,
Tipo_cargo varchar(10)not null
)
go

--Tabla inventario
Create Table inventario
(
Id_inventario int not null,
Codigo varchar(15)not null,
Nombre varchar(50)not null,
Cantidad int not null,
Costo_unitario decimal(12,2)not null,
Precio_venta decimal(12,2)not null,
Monto_total decimal(12,2)not null,
Tipo_cargo varchar(10)not null
)
go

--PRDOCEDIMIENTOS ALMACENADOS--
--Agregar Prducto
Create Proc AgregarProducto
@Codigo varchar(15),
@Nombre varchar(50),
@Descripcion varchar(100),
@Presentacion varchar(10),
@Costo_unitario decimal(12,2),
@Precio_venta decimal(12,2),
@Tipo_cargo varchar(10)
as
Insert into Productos (Codigo,Nombre,Descripcion,Presentacion,Costo_unitario,Precio_venta,Tipo_cargo)
Values (@Codigo,@Nombre,@Descripcion,@Presentacion,@Costo_unitario,@Precio_venta,@Tipo_cargo)
go

--Editar Producto
Create Proc EditarProducto
@Id_producto int,
@Codigo varchar(15),
@Nombre varchar(50),
@Descripcion varchar(100),
@Presentacion varchar(10),
@Costo_unitario decimal(12,2),
@Precio_venta decimal(12,2),
@Tipo_cargo varchar(10)
as
Update Productos Set Codigo=@Codigo,Nombre=@Nombre,Descripcion=@Descripcion,Presentacion=@Presentacion,Costo_unitario=@Costo_unitario,Precio_venta=@Precio_venta,Tipo_cargo=@Tipo_cargo Where Id_producto=@Id_producto
go

--Eliminar Producto
Create Proc EliminarProducto
@Id_producto int
as
Delete From Productos Where Id_producto=@Id_producto
go

--Trigger para agregar Producto al inventario
Create Trigger Tr_Agregar_Producto_Inventario
On Productos for Insert
as
Declare @Id_Inventario int 
Declare @Codigo varchar(15)
Declare @Nombre varchar(50)
Declare @Cantidad int 
Declare @Costo_unitario decimal(12,2)
Declare @Precio_venta decimal(12,2)
Declare @Monto_total decimal(12,2)
Declare @Tipo_cargo varchar(10)
Select @Id_Inventario=Id_producto,@Codigo=Codigo, @Nombre=Nombre, @Cantidad=0, @Costo_unitario=Costo_unitario, @Precio_venta=Precio_venta, @Monto_total=(@Cantidad * @Costo_unitario), @Tipo_cargo=Tipo_cargo From inserted
Insert Into inventario (Id_inventario,Codigo,Nombre,Cantidad,Costo_unitario,Precio_venta,Monto_total,Tipo_cargo)
Values(@Id_Inventario,@Codigo,@Nombre,@Cantidad,@Costo_unitario,@Precio_venta,@Monto_total,@Tipo_cargo)
go

IF OBJECT_ID('dbo.Tr_Eliminar_Producto_Inventario', 'TR') IS NOT NULL
    DROP TRIGGER dbo.Tr_Eliminar_Producto_Inventario
GO

--Trigger para Eliminar Producto en inventario
Create Trigger Tr_Eliminar_Producto_Inventario
On Productos for delete
as
Set nocount on
Declare @Id_Inventario int 
Declare @Cantidad int
Select @Id_Inventario=Id_producto From deleted
Delete From inventario Where Id_inventario=@Id_Inventario
go