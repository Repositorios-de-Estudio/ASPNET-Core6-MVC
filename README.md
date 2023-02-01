# ASPNET-Core5-MVC
Sistema de Control de Ingresos y Gastos - Full-Stack Web. Tiene conexión a la BD manual/por archivo.

Se pueden ver, crea datos de tipo Ingreso o Gasto por medio de una lista desplegable con codigo quemado.
se reemplaza texto para mostrar otro texto en la vista. IN = Ingreso, GA = Gasto. 
Se usan plantillas Boostrap.
Se agrega un ForeignKey entre dos tablas.

## Tecnologias
- ASP.NET Core: **6.0**
- Web Application (MVC)
- Bootstrap: **v5.1.0**
- SQL Server: **2019**
- Entity Framework Core: **7.0.2**

Aplicacion web de ASP.NET MVC: C#/Windows/Web, desde codigo se agrega el StringConnection a la BD y se crea la BD para el proyecto.

- falta: no se pudo alimentar el dropdownlist desde una tabla

## Contenido
- Interacciones Front-End y Back-End
- Data Model con Entity Framework Core
- Traer y Desplegar Datos
- Forms y Validación de Datos
- Ajuste de Código y Servicios de Datos
- Back-End y Front-End Debugging
- AspNet Core

Archivos estaticos en: "wwwroot/"


El primer controlador y vista que se ejecutan son:
Version de Boostrap: "...\IngresosGastos\wwwroot\lib\bootstrap\dist\css\bootstrap.css"

```
	public class Program
    {
	...
		app.MapControllerRoute()
	...
	}
```

## Instalar NuGet

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools (convierte modelos a tablas)

**Models/Categoria.cs**
```
    public class Categoria
    {
        // estas anotaciones sirven para cuando se cree la BD y tabla Categoria se usen como campos de la tabla
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
    }
```

**Models/NombreCategoria.cs**
```
    public class NombreCategoria
    {
        // para mostrar el DropDownList: Ingreso, Gasto
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } // Ingreso o Gasto

    }
```

## Datos DB y conexión

Conexión BD: appsettings.json

Se creara una DB llamada: IngresoGastosDB 
Se creara la tabla: Categorias
Se creara la tabla: NombreCategoria

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

Agregar carpeta Data/ y clase: **Data/AppDBContext.cs** Esto solo se hace en MVC 6 porque MVC 3 tiene el *StartUp.cs*
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
        public DbSet<NombreCategoria> NombreCategoria { get; }


    }
```


## Agregar inyección de dependencias:


**Program.cs**
En la ultima linea de la seccion *// Add services to the container.* Esto solo se hace en MVC 6 porque MVC 3 tiene el *StartUp.cs*. Agregar:
```
var connectionString = builder.Configuration.GetConnectionString("ConexionBD");
builder.Services.AddDbContext<AppDBContext>(x => x.UseSqlServer(connectionString));
```

Hacer migracion de datos models a bd:

En visual > Herramientas > Administrador de paquetes Nuget > Consola:
```
add-migration MigracionCategoria1
update-database
```

**Se creará la BD especificada y las tablas Categorias, NombreCategoria**

Agregar datos a la tabla:
```
INSERT INTO NombreCategoria (Nombre) VALUES ('Ingreso');
INSERT INTO NombreCategoria (Nombre) VALUES ('Gasto');
```

## Controladores

Crear **Models/CategoriasController.cs** Controlador MVC con vistas Entity Framework
- Clase: Categoria (de controllers/models)
- Categoria: AppDBContext (de data/in)
- Generar vistas, referencias, usar pagina de diseño
	- diseño: views/shared/_Layout.cshtml
- Nombre: CategoriasController	
- *Vista index creada:* **Views/Categorias/Index.cshtml**

## Mostrar vista creada
Agregar en CategoriasController **Views/Shared/_Layout** vista de **CategoriasController**
```
...
<div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
<ul class="navbar-nav flex-grow-1">
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
	</li>
	 <li class="nav-item">
	   <a class="nav-link text-dark" asp-area="" asp-controller="Categorias" asp-action="Index">Categorias</a>
	</li>
	<li class="nav-item">
		<a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
	</li>
</ul>
</div>
```

## Agregar otro Modelo

Para relacionar el ingreso / gasto, Agregar Fecha y valor a los Ingresos/Gastos ya creados.
**Models/IngresoGasto**
```
    public class IngresoGasto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } // Categoria de la clase/tabla Categoria

        [Required]
        [Display(Name ="Fecha")] 
        public DateTime Fecha { get; set; } //cuando se ingreso el ingreso o gasto

        [Required]
        [Range(1,100000)] // para que siempre sea mayor que 1
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name ="Valor")]
        public double Valor { get; set; }
	}
```

Ahora se agregar al contexto en: **Data/AppDBContext**
```
public DbSet<IngresoGasto> IngresoGasto { get; set; }
```

Hacer migracion de datos models a bd con nombre MigracionCategoria2:

En visual > Herramientas > Administrador de paquetes Nuget > Consola:
```
add-migration MigracionCategoria2
update-database
```

Agregar Controlador: **Controllers/IngresoGastos**

Crear **Models/IngresoGastos.cs** Controlador MVC con vistas Entity Framework
- Clase: IngresoGasto (de controllers/models)
- Categoria: AppDBContext (de data/in)
- Generar vistas, referencias, usar pagina de diseño
	- diseño: views/shared/_Layout.cshtml
- Nombre: IngresoGastosController	
- *Vista index creada:* **Views/IngresoGastos/Index.cshtml**

Agregar seccion en la vista: **views/shared/_Layout**
```
 <li class="nav-item">
   <a class="nav-link text-dark" asp-area="" asp-controller="IngresoGastos" asp-action="Index">Ingreso-Gastos</a>
</li>
```


## Cambiado diseño con Boostrap y añadiendoo DropDownList

### Llenar un DROPDOWNLIST en index
**Views/Categorias/Index**
Reemplaza texto IN, GA por Ingreso, Gasto. Seccion de @foreach
```
            <td>
                @* @Html.DisplayFor(modelItem => item.Tipo) *@
	            @if (item.Tipo=="IN")
		            {
                        <p class="btn btn-outline-success">Ingreso</p>
		            }
		            else
		            {
                        <p class="btn btn-outline-danger">Gasto</p>
		            }
            </td>
```

**Views/Categorias/Index**
Reemplaza casilla true/false Estado por Activo, Inactivo. Seccion de @foreach
```
            <td>
	            @* @Html.DisplayFor(modelItem => item.Estado) *@
	            @if (item.Estado)
		            {
                        <p class="btn btn-outline-primary">Activo</p>
		            }
		            else
		            {
                        <p class="btn btn-outline-info">Inactivo</p>
		            }
            </td>
```

## Filtrar
Filtrar Categorias en IngresoGasto para mostrar solo Categorias Activas:
*//GET de Create()* del controller, agregar `.Where(var => var.Estado==true)` en la conexion a la BD de Categorias.

```
// GET: IngresoGastos/Create
        public IActionResult Create()
        {
            //filtrar solo por categorias Activas
            ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(var => var.Estado==true), "Id", "NombreCategoria");
            //ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria");
            return View();
        }
```

### Llenar un DROPDOWNLIST en create
**Views/Categorias/Create**
Reemplaza texto IN, GA por una lista desplegable con Ingreso, Gasto con codigo quemado. dentro de *<form asp-action="Create">* de Tipo y Estado. Quitar <input> y agregar <select>
```
<div class="form-group">
	<label asp-for="Tipo" class="control-label"></label>
	@*<input asp-for="Tipo" class="form-control" />*@
	<select asp-for="Tipo" class="form-control">
		<option value="IN">Ingreso</option>
		<option value="GA">Gasto</option>
	</select>
	<span asp-validation-for="Tipo" class="text-danger"></span>
</div>
```
**Views/Categorias/Create**
Quitar "form-check". en <label> reemplazar "form-check-label" por "control-label", reemplazar todo el <input> por Estado, Agregar luego del </label>: <select>

```
<div class="form-group">
	<label class="control-label">Estado</label>
	<select asp-for="Estado" class="form-control">
		<option value=true>Activo</option>
		<option value=false>Inactivo</option>
	</select>
</div>
````
**Views/Categorias/Edit**
Reemplaza texto IN, GA por una lista desplegable con Ingreso, Gasto con codigo quemado. dentro de *<form asp-action="Create">* de Tipo y Estado. Quitar <input> y agregar <select>
```
<div class="form-group">
	<label asp-for="Tipo" class="control-label"></label>
	@*<input asp-for="Tipo" class="form-control" />*@
	<select asp-for="Tipo" class="form-control">
		<option value="IN">Ingreso</option>
		<option value="GA">Gasto</option>
	</select>
	<span asp-validation-for="Tipo" class="text-danger"></span>
</div>
```
**Views/Categorias/Edit**
Quitar "form-check". en <label> reemplazar "form-check-label" por "control-label", reemplazar todo el <input> por Estado, Agregar luego del </label>: <select>

```
<div class="form-group">
	<label class="control-label">Estado</label>
	<select asp-for="Estado" class="form-control">
		<option value=true>Activo</option>
		<option value=false>Inactivo</option>
	</select>
</div>
````

asdad
**Views/Categorias/Details**
Reemplaza texto IN, GA por Ingreso, Gasto. Seccion de @foreach
```
        <dd class = "col-sm-10">
            @* @Html.DisplayFor(model => model.Tipo)*@
            @if (Model.Tipo == "IN")
            {
                <p class="btn btn-outline-success">Ingreso</p>
            }
            else
            {
                <p class="btn btn-outline-danger">Gasto</p>
            }
        </dd>
```

**Views/Categorias/Details**
Reemplaza casilla true/false Estado por Activo, Inactivo. Seccion de @foreach
```
        <dd class = "col-sm-10">
            @*@Html.DisplayFor(model => model.Estado)*@
            @if (Model.Estado)
            {
                <p class="btn btn-outline-primary">Activo</p>
            }
            else
            {
                <p class="btn btn-outline-info">Inactivo</p>
            }
        </dd>
```

**Controllers/IngresoGastos**

### Cambiando diseño con Boostrap
En **Views/Categorias/Index** agregar clases de Boostrap

Evitar que se autocompleten los campos input agregando `autocomplete="off"`
Agregar diseño en los inputos con etiquetas botones.
Agregar diseño de tabla:
- Encabezado Tablas: <thead class="table-dark">
- Cuerpo tablas: <table class="table table-striped">
- Texto: class="btn btn-primary"



***

# SOLUCION DE PROBLEMAS

## The Categoria field is requiered
Cuando agregaba un **IngresoGasto** la pagina no realizaba ninguna acción. Revisé el método de *Create* para revisar si habian fallos. Por alguna razón *ModelState.IsValid* siempre era false.

Usar el modo Depuración y mirar el mensaje del *ModelState*. agregar:
```
var errors = ModelState.Values.SelectMany(v => v.Errors);
```

Quedaría así:
```
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaId,Fecha,Valor")] IngresoGasto ingresoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", ingresoGasto.CategoriaId);
            return View(ingresoGasto);
        }
```

Ver mensaje en:
*errors>vista de resultados>[0]>ErrorMessage* 
Resultado:
`ErrorMessage: "The Categoria field is required."`

*Solución*
Como el problema era con *Categoria* en el controlador de **IngresoGasto** coloque el parametro como opcional.

```
[ForeignKey("CategoriaId")]
public Categoria? Categoria { get; set; }
```
***

# Notas

- la tabla de la bd se define en AppDBContext y el controller lo usa como *_context*.
- los query de las tablas son: *_context.IngresoGasto.{query}*
	- Select -> .Include(i=>i.categoria)
	- Where -> .Where(i=>i.categoria.Estado==true)
- Enviar datos de back a front
	- En el controller usar ViewData["NombreVar"] = .... y en la vista usarlo como "asp-for="CategoriaId" ... asp-items="ViewBag.NombreVar"
- Se pueden cambiar los atributes de un texto que se mnuestra en html colocandolo dentro de una etqiqueta <p></p>
- 


# Creditos:
Basado en el curso: [Desarrollo Web en ASP.NET CORE 5 (2021)](https://www.udemy.com/course/desarrollo-web-en-aspnet-core-5-2021/)
[Boostrap 5.1 Componentes](https://getbootstrap.com/docs/5.1/components/buttons/)