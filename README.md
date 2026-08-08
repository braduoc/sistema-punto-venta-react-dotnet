# Sistema de Punto de Venta React + .NET

Aplicación web de punto de venta desarrollada con React en el frontend y ASP.NET Core en el backend, utilizando Entity Framework Core y SQL Server. El sistema permite gestionar usuarios, categorías, productos, ventas y visualizar un dashboard con métricas básicas.

## Características principales

- Autenticación básica de usuarios con login y sesión persistente.
- Gestión de usuarios, categorías y productos.
- Registro de ventas con cálculo automático de subtotal, IGV y total.
- Historial y reportes de ventas.
- Dashboard con indicadores y gráficos de ventas y productos más vendidos.
- Validaciones básicas en backend y mejoras de manejo de errores en el frontend.

## Tecnologías utilizadas

- Frontend: React, React Router, Reactstrap, Bootstrap, Chart.js, SweetAlert2.
- Backend: ASP.NET Core, Entity Framework Core, SQL Server.
- Herramientas: SPA Proxy, npm, .NET 6.

## Estructura del proyecto

- ClientApp/: frontend React.
- Controllers/: controladores ASP.NET Core para usuarios, productos, ventas, sesión y utilidades.
- Models/: entidades y DTOs del sistema.
- Migrations/: migraciones de Entity Framework.
- Utils/: utilidades auxiliares como hashing de contraseñas.

## Requisitos

- Node.js
- .NET SDK 6+
- SQL Server

## Ejecución local

1. Clona el repositorio.
2. Configura la cadena de conexión en appsettings.json o appsettings.Development.json.
3. Ejecuta las migraciones de Entity Framework si es necesario.
4. En la carpeta ClientApp, instala las dependencias:
   ```bash
   npm install
   ```
5. Inicia la aplicación:
   ```bash
   dotnet run
   ```

## Notas

El proyecto sigue una base funcional para un POS y ha sido mejorado con validaciones de negocio, manejo de errores más claro y protección básica de contraseñas.
