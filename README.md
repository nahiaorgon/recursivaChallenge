# 🌌 Horóscopo App - Full Stack .NET

Proyecto desarrollado con **.NET 10**, combinando **ASP.NET Core API** (backend) y **Blazor WebAssembly**(frontend). Permitiendo a los usuarios consultar su horóscopo, registrar sus datos y visualizar estadísticas/historial de popularidad por signo y género.

## 🛠️ Requisitos del Sistema

* **SDK:** .NET 10
* **Base de Datos:** SQL Server (LocalDB).
* **Herramientas de EF:** `dotnet-ef` global tool instalado.

## 🚀 Configuración de la Base de Datos

Para inicializar la persistencia de datos, tienes que ejecutar las migraciones de Entity Framework. 

Es muy importante ejecutar estos comandos desde la **carpeta raíz de la solución** para que las referencias a los proyectos sean correctas.

### 1. Crear la Migración Inicial
Genera el código necesario para crear las tablas de `Historial` y `Estadísticas`:

Paso 1:
dotnet ef migrations add InitialCreate --project Horoscopo.Core.Repository --startup-project Horoscopo.Api

Paso 2:
dotnet ef database update --project Horoscopo.Core.Repository --startup-project Horoscopo.Api

## Y listo :) ya podes obtener datos sobre tu horoscopo!!

