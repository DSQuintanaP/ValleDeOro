using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValleDeOro.Migrations
{
    /// <inheritdoc />
    public partial class fifthGear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosReserva",
                columns: table => new
                {
                    IdEstadoReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEstadoReserva = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadosR__3E654CA8EEF1FB6D", x => x.IdEstadoReserva);
                });

            migrationBuilder.CreateTable(
                name: "Imagenes",
                columns: table => new
                {
                    IdImagen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Imagenes__B42D8F2A03FBD281", x => x.IdImagen);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPago",
                columns: table => new
                {
                    IdMetodoPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomMetodoPago = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MetodoPa__6F49A9BE5E1CC485", x => x.IdMetodoPago);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPermiso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permisos__0D626EC87EDBDF3B", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomRol = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__2A49584CFA603911", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTipoDocumento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoDocu__3AB3332F84FFA246", x => x.IdTipoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "TipoHabitaciones",
                columns: table => new
                {
                    IdTipoHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipoHabitacion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NumeroPersonas = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoHabi__AB75E87CABB617B4", x => x.IdTipoHabitacion);
                });

            migrationBuilder.CreateTable(
                name: "TipoServicios",
                columns: table => new
                {
                    IdTipoServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipoServicio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoServ__E29B3EA7C54560A1", x => x.IdTipoServicio);
                });

            migrationBuilder.CreateTable(
                name: "PermisosRoles",
                columns: table => new
                {
                    IdPermisosRoles = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: true),
                    IdPermiso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permisos__8C257ED9F00B1085", x => x.IdPermisosRoles);
                    table.ForeignKey(
                        name: "FK__PermisosR__IdPer__3C69FB99",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso");
                    table.ForeignKey(
                        name: "FK__PermisosR__IdRol__3B75D760",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    NroDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: true),
                    Nombres = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Apellidos = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Celular = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Correo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Contrasena = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Clientes__CC62C91C37319598", x => x.NroDocumento);
                    table.ForeignKey(
                        name: "FK__Clientes__IdRol__5EBF139D",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol");
                    table.ForeignKey(
                        name: "FK__Clientes__IdTipo__5FB337D6",
                        column: x => x.IdTipoDocumento,
                        principalTable: "TipoDocumento",
                        principalColumn: "IdTipoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    IdHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoHabitacion = table.Column<int>(type: "int", nullable: true),
                    NombreHabitacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habitaci__8BBBF90169F7EDE9", x => x.IdHabitacion);
                    table.ForeignKey(
                        name: "FK__Habitacio__IdTip__4316F928",
                        column: x => x.IdTipoHabitacion,
                        principalTable: "TipoHabitaciones",
                        principalColumn: "IdTipoHabitacion");
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoServicio = table.Column<int>(type: "int", nullable: true),
                    NomServicio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Servicio__2DCCF9A2C83BF525", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK__Servicios__IdTip__4BAC3F29",
                        column: x => x.IdTipoServicio,
                        principalTable: "TipoServicios",
                        principalColumn: "IdTipoServicio");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroDocumentoCliente = table.Column<int>(type: "int", nullable: true),
                    FechaReserva = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    MetodoPago = table.Column<int>(type: "int", nullable: true),
                    IdEstadoReserva = table.Column<int>(type: "int", nullable: true, defaultValueSql: "('1')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reservas__0E49C69DC61D1C84", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK__Reservas__IdEsta__68487DD7",
                        column: x => x.NroDocumentoCliente,
                        principalTable: "Clientes",
                        principalColumn: "NroDocumento");
                    table.ForeignKey(
                        name: "FK__Reservas__IdEsta__693CA210",
                        column: x => x.IdEstadoReserva,
                        principalTable: "EstadosReserva",
                        principalColumn: "IdEstadoReserva");
                    table.ForeignKey(
                        name: "FK__Reservas__Metodo__6A30C649",
                        column: x => x.MetodoPago,
                        principalTable: "MetodoPago",
                        principalColumn: "IdMetodoPago");
                });

            migrationBuilder.CreateTable(
                name: "ImagenHabitacion",
                columns: table => new
                {
                    IdImagenHabi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdImagen = table.Column<int>(type: "int", nullable: true),
                    IdHabitacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ImagenHa__5B5FF6ADC08B6613", x => x.IdImagenHabi);
                    table.ForeignKey(
                        name: "FK__ImagenHab__IdHab__46E78A0C",
                        column: x => x.IdHabitacion,
                        principalTable: "Habitaciones",
                        principalColumn: "IdHabitacion");
                    table.ForeignKey(
                        name: "FK__ImagenHab__IdIma__45F365D3",
                        column: x => x.IdImagen,
                        principalTable: "Imagenes",
                        principalColumn: "IdImagen");
                });

            migrationBuilder.CreateTable(
                name: "Paquetes",
                columns: table => new
                {
                    IdPaquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPaquete = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    IdHabitacion = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paquetes__DE278F8BEF81B661", x => x.IdPaquete);
                    table.ForeignKey(
                        name: "FK__Paquetes__IdHabi__52593CB8",
                        column: x => x.IdHabitacion,
                        principalTable: "Habitaciones",
                        principalColumn: "IdHabitacion");
                });

            migrationBuilder.CreateTable(
                name: "ImagenServicio",
                columns: table => new
                {
                    IdImagenServi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdImagen = table.Column<int>(type: "int", nullable: true),
                    IdServicio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ImagenSe__3C03784C0CD215C6", x => x.IdImagenServi);
                    table.ForeignKey(
                        name: "FK__ImagenSer__IdIma__4E88ABD4",
                        column: x => x.IdImagen,
                        principalTable: "Imagenes",
                        principalColumn: "IdImagen");
                    table.ForeignKey(
                        name: "FK__ImagenSer__IdSer__4F7CD00D",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio");
                });

            migrationBuilder.CreateTable(
                name: "Abono",
                columns: table => new
                {
                    IdAbono = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReserva = table.Column<int>(type: "int", nullable: true),
                    FechaAbono = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CantAbono = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Abono__A4693DA7A09D6923", x => x.IdAbono);
                    table.ForeignKey(
                        name: "FK__Abono__IdReserva__75A278F5",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva");
                });

            migrationBuilder.CreateTable(
                name: "DetalleReservaServicio",
                columns: table => new
                {
                    IdDetalleReservaServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdServicio = table.Column<int>(type: "int", nullable: true),
                    IdReserva = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleR__D3D91A5A08BCC4D4", x => x.IdDetalleReservaServicio);
                    table.ForeignKey(
                        name: "FK__DetalleRe__IdRes__6E01572D",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva");
                    table.ForeignKey(
                        name: "FK__DetalleRe__IdSer__6D0D32F4",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio");
                });

            migrationBuilder.CreateTable(
                name: "DetalleReservaPaquete",
                columns: table => new
                {
                    DetalleReservaPaquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaquete = table.Column<int>(type: "int", nullable: true),
                    IdReserva = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleR__2E8BFF25F75AACB1", x => x.DetalleReservaPaquete);
                    table.ForeignKey(
                        name: "FK__DetalleRe__IdPaq__70DDC3D8",
                        column: x => x.IdPaquete,
                        principalTable: "Paquetes",
                        principalColumn: "IdPaquete");
                    table.ForeignKey(
                        name: "FK__DetalleRe__IdRes__71D1E811",
                        column: x => x.IdReserva,
                        principalTable: "Reservas",
                        principalColumn: "IdReserva");
                });

            migrationBuilder.CreateTable(
                name: "ImagenPaquete",
                columns: table => new
                {
                    IdImagenPaque = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdImagen = table.Column<int>(type: "int", nullable: true),
                    IdPaquete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ImagenPa__AD53DF944CAE36D6", x => x.IdImagenPaque);
                    table.ForeignKey(
                        name: "FK__ImagenPaq__IdIma__5535A963",
                        column: x => x.IdImagen,
                        principalTable: "Imagenes",
                        principalColumn: "IdImagen");
                    table.ForeignKey(
                        name: "FK__ImagenPaq__IdPaq__5629CD9C",
                        column: x => x.IdPaquete,
                        principalTable: "Paquetes",
                        principalColumn: "IdPaquete");
                });

            migrationBuilder.CreateTable(
                name: "PaqueteServicio",
                columns: table => new
                {
                    IdPaqueteServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaquete = table.Column<int>(type: "int", nullable: true),
                    IdServicio = table.Column<int>(type: "int", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaqueteS__DE5C2BC2CB321C96", x => x.IdPaqueteServicio);
                    table.ForeignKey(
                        name: "FK__PaqueteSe__IdPaq__59063A47",
                        column: x => x.IdPaquete,
                        principalTable: "Paquetes",
                        principalColumn: "IdPaquete");
                    table.ForeignKey(
                        name: "FK__PaqueteSe__IdSer__59FA5E80",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio");
                });

            migrationBuilder.CreateTable(
                name: "ImagenAbono",
                columns: table => new
                {
                    IdImagenAbono = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdImagen = table.Column<int>(type: "int", nullable: true),
                    IdAbono = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ImagenAb__A40FAE53D797E305", x => x.IdImagenAbono);
                    table.ForeignKey(
                        name: "FK__ImagenAbo__IdAbo__797309D9",
                        column: x => x.IdAbono,
                        principalTable: "Abono",
                        principalColumn: "IdAbono");
                    table.ForeignKey(
                        name: "FK__ImagenAbo__IdIma__787EE5A0",
                        column: x => x.IdImagen,
                        principalTable: "Imagenes",
                        principalColumn: "IdImagen");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abono_IdReserva",
                table: "Abono",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdRol",
                table: "Clientes",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdTipoDocumento",
                table: "Clientes",
                column: "IdTipoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReservaPaquete_IdPaquete",
                table: "DetalleReservaPaquete",
                column: "IdPaquete");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReservaPaquete_IdReserva",
                table: "DetalleReservaPaquete",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReservaServicio_IdReserva",
                table: "DetalleReservaServicio",
                column: "IdReserva");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleReservaServicio_IdServicio",
                table: "DetalleReservaServicio",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_IdTipoHabitacion",
                table: "Habitaciones",
                column: "IdTipoHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenAbono_IdAbono",
                table: "ImagenAbono",
                column: "IdAbono");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenAbono_IdImagen",
                table: "ImagenAbono",
                column: "IdImagen");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenHabitacion_IdHabitacion",
                table: "ImagenHabitacion",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenHabitacion_IdImagen",
                table: "ImagenHabitacion",
                column: "IdImagen");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenPaquete_IdImagen",
                table: "ImagenPaquete",
                column: "IdImagen");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenPaquete_IdPaquete",
                table: "ImagenPaquete",
                column: "IdPaquete");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenServicio_IdImagen",
                table: "ImagenServicio",
                column: "IdImagen");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenServicio_IdServicio",
                table: "ImagenServicio",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Paquetes_IdHabitacion",
                table: "Paquetes",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteServicio_IdPaquete",
                table: "PaqueteServicio",
                column: "IdPaquete");

            migrationBuilder.CreateIndex(
                name: "IX_PaqueteServicio_IdServicio",
                table: "PaqueteServicio",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_IdPermiso",
                table: "PermisosRoles",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosRoles_IdRol",
                table: "PermisosRoles",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdEstadoReserva",
                table: "Reservas",
                column: "IdEstadoReserva");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_MetodoPago",
                table: "Reservas",
                column: "MetodoPago");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_NroDocumentoCliente",
                table: "Reservas",
                column: "NroDocumentoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IdTipoServicio",
                table: "Servicios",
                column: "IdTipoServicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleReservaPaquete");

            migrationBuilder.DropTable(
                name: "DetalleReservaServicio");

            migrationBuilder.DropTable(
                name: "ImagenAbono");

            migrationBuilder.DropTable(
                name: "ImagenHabitacion");

            migrationBuilder.DropTable(
                name: "ImagenPaquete");

            migrationBuilder.DropTable(
                name: "ImagenServicio");

            migrationBuilder.DropTable(
                name: "PaqueteServicio");

            migrationBuilder.DropTable(
                name: "PermisosRoles");

            migrationBuilder.DropTable(
                name: "Abono");

            migrationBuilder.DropTable(
                name: "Imagenes");

            migrationBuilder.DropTable(
                name: "Paquetes");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "TipoServicios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "EstadosReserva");

            migrationBuilder.DropTable(
                name: "MetodoPago");

            migrationBuilder.DropTable(
                name: "TipoHabitaciones");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TipoDocumento");
        }
    }
}
