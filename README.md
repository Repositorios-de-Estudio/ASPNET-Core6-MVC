# ASPNET-Core5-MVC
Sistema de Control de Ingresos y Gastos - Full-Stack Web. Tiene conexión a la BD manual/por archivo.

## Tecnologias
- ASP.NET Core 6
- Web Application (MVC)
- Bootstrap
- SQL Server
- Entity Framework Core.

Aplicacion web de ASP.NET MVC: C#/Windows/Web, desde codigo se agrega el StringConnection a la BD y se crea la BD para el proyecto.

## Contenido
- Interacciones Front-End y Back-End
- Data Model con Entity Framework Core
- Traer y Desplegar Datos
- Forms y Validación de Datos
- Ajuste de Código y Servicios de Datos
- Back-End y Front-End Debugging
- AspNet Core

Archivos estaticos en: wwwroot/


El primero controlador y vista que se ejecutan son:

```
public class Program
    {
	...
		app.MapControllerRoute()
	...
	}
```


**Models/Categoria.cs**

```
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name ="Nombre Categoria")]
        public string NombreCategoria { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } // IN: Ingreso, GA: Gasto

        [Required]
        public bool Estado { get; set; }
```

## Datos DB y conexión

Conexión BD: appsettings.json

Se creara una DB llamada: IngresoGastosDB 

Se creara la tabla: Categorias

Datos BD:
```
Servidor: 192.168.0.20\SERVER\SQLEXPRESS,1433
DB: IngresoGastosDB
user id: bduserX
password: admin1234
```
Plantilla standar security:
`Server=myServerName\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;`

Plantilla trusted connection:
`Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;`

ConnectionStrings usado:
```
  "ConnectionStrings": {
    "ConexionBD": "Server=192.168.0.20\\SERVER\\SQLEXPRESS,1433;Database=IngresoGastosDB;User Id=bduserX;Password=admin1234;Trust Server Certificate=true;"
  },
```

## Instalar NuGet

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools (convierte modelos a tablas)


Agregar carpeta Data/ y clase: **Data/AppDBContext.cs**
```
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {

        }

        // Para inyeccion de dependencias
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConexionBD");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Categoria> Categorias { get; set; }
    }
```


## Agregar inyección de dependencias:


**Program.cs**
En la ultima linea de la seccion // Add services to the container. agregar:
```
var connectionString = builder.Configuration.GetConnectionString("ConexionBD");
builder.Services.AddDbContext<AppDBContext>(x => x.UseSqlServer(connectionString));
```

Hacer migracion de datos models a bd:

En visual > Herramientas > Administrador de paquetes Nuget > Consola:
```
add-migration MigracionCategoria
update-database
```

**Se creará la BD especificada y la tabla Categorias**




