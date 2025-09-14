# AuthApi - Gestión de Usuarios

Este proyecto implementa una **API para gestionar usuarios** y su acceso a una aplicación.  
La base de datos se monta en **SQL Server** y la solución expone endpoints documentados con **Swagger**.

---

## 🚀 Requisitos previos

- **SQL Server** instalado y corriendo
- **SQL Server Management Studio (SSMS)** o cualquier cliente para ejecutar scripts SQL
- **.NET 8 SDK**
- **Visual Studio** o **VS Code**
- **Postman** (opcional, para pruebas de endpoints)

---

## 📦 Configuración de la Base de Datos

1. **Crear la base de datos y la tabla de Usuarios**  
   Ejecutar en SQL Server el script [`AuthApiDb.sql`](BasedeDatos/AuthApiDb.sql):


   > Asegúrate de estar en el servidor correcto antes de ejecutar el script.

2. **Crear el trigger para fechas de control**  
   Una vez creada la base, ejecútalo el script [`trg_set_fechas_usuarios.sql`](BasedeDatos/trg_set_fechas_usuarios.sql):

   > Importante: el trigger debe ejecutarse después de haber creado la tabla `Usuarios`.

---

## ⚙️ Configuración de la Solución

### Crear archivo de configuración `appsettings.json`

1. Copiar el archivo `appsettingsExample.json` que viene en el proyecto.
2. Renombrarlo como `appsettings.json`.
3. Editar los valores necesarios:

#### Cadena de conexión (`ConnectionStrings:CadenaSQL`)

Reemplaza `TU_SERVIDOR_SQL` con el nombre de tu instancia de SQL Server.  
Ejemplo:

```json
"CadenaSQL": "Data Source=DESKTOP-VO0JQKR;Initial Catalog=AuthApi;Integrated Security=True;TrustServerCertificate=True;"
```

#### Clave JWT (`jwt:key`)

No uses la que trae por defecto. Genera una nueva ejecutando en SQL Server:

```sql
SELECT NEWID();
```

Copia el GUID generado y pégalo como valor de `jwt:key`.  
Ejemplo:

```json
"jwt": {
  "key": "EB911749-554B-4D5F-B7A1-A353B8A28A80"
}
```

---

## 📄 Ejemplo de `appsettings.txt`

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "CadenaSQL": "Data Source=TU_SERVIDOR_SQL;Initial Catalog=AuthApi;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "jwt": {
    "key":"REEMPLAZAR_CON_TU_LLAVE_UNICA"
  }
}
```

---

## 📌 Restaurar paquetes NuGet

```bash
dotnet restore
```

## 📌 Compilar la solución

```bash
dotnet build
```

## 📌 Correr el proyecto

```bash
dotnet run
```

---

## 🌐 Probar la API

### Swagger UI

Una vez que el proyecto esté corriendo, abre en tu navegador:

```
https://localhost:5001/swagger
```

Ahí podrás ver la documentación y probar los endpoints directamente.

### Postman

1. Abre Postman.
2. Crea una nueva colección llamada `AuthApi`.
3. Agrega requests a los endpoints de Usuarios (ejemplo: `POST /api/usuarios`, `GET /api/usuarios`).
4. Usa body en JSON para probar la creación de usuarios.

Ejemplo de request `POST /api/usuarios`:

```json
{
  "nombres": "Juan",
  "apellidos": "Pérez",
  "fechaNacimiento": "1990-05-12",
  "direccion": "San Salvador",
  "password": "MiPasswordSeguro123",
  "telefono": "+50312345678",
  "email": "juan.perez@example.com",
  "estado": "A"
}
```

