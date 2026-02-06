# 🌌 Horóscopo App - Full Stack .NET

Proyecto desarrollado con **.NET 10**, combinando **ASP.NET Core API** (backend) y **Blazor WebAssembly**(frontend). Permitiendo a los usuarios consultar su horóscopo, registrar sus datos y visualizar estadísticas/historial de popularidad por signo y género.

## 🛠️ Requisitos del Sistema

* **SDK:** .NET 10
* **Base de Datos:** SQL Server (LocalDB).
* **Herramientas de EF:** `dotnet-ef` global tool instalado.

## 🚀 Configuración de la Base de Datos

Para inicializar la persistencia de datos, se necesitan ejecutar las migraciones de Entity Framework. 

Es muy importante ejecutar estos comandos desde la **carpeta raíz de la solución** para que las referencias a los proyectos sean correctas.

### 1. Crear la Migración Inicial
Genera el código necesario para crear las tablas de `Historial` y `Estadísticas`:

Paso 1:
dotnet ef migrations add InitialCreate --project Horoscopo.Core.Repository --startup-project Horoscopo.Api
<img width="1446" height="156" alt="Captura de pantalla 2026-02-05 220412" src="https://github.com/user-attachments/assets/032c4b45-c4ad-4c4b-812c-c6189bad51e4" />

Paso 2:
dotnet ef database update --project Horoscopo.Core.Repository --startup-project Horoscopo.Api
<img width="1273" height="88" alt="Captura de pantalla 2026-02-05 220502" src="https://github.com/user-attachments/assets/c7e9c150-71f7-4bf0-a9eb-06bced4be443" />

## Y listo :) ya podes obtener datos sobre tu horoscopo!!

