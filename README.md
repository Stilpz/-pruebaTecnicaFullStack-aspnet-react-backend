# Detalles de la Aplicación

Esta aplicación ha sido desarrollada utilizando ASP.NET Core para el backend, React para el frontend, y SQL Server como base de datos relacional. A continuación se detallan las características y funcionamiento principal de la aplicación.
Funcionamiento de los Endpoints Principales

## GET /api/clientes
    Descripción: Retorna una lista paginada de todos los clientes almacenados en la base de datos.
    Uso: Este endpoint se utiliza para obtener la lista completa de clientes registrados. Los resultados se muestran paginados.
    Parámetros:
        page: Número de página para la paginación (opcional, por defecto es 1).
        pageSize: Tamaño de la página (opcional, por defecto es 10 registros por página).
    Respuesta Exitosa (200 OK): Retorna una lista paginada de clientes en formato JSON.
    Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente en caso de fallo.

## GET /api/clientes/{id}
    Descripción: Retorna un cliente específico según el ID proporcionado.
    Uso: Se utiliza para obtener los detalles de un cliente específico.
    Parámetros: {id} es el identificador único del cliente.
    Respuesta Exitosa (200 OK): Retorna los detalles del cliente solicitado en formato JSON.
    Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente si el cliente no existe o si hay un problema de servidor.

## POST /api/clientes
    Descripción: Crea un nuevo cliente con los datos proporcionados.
    Uso: Se utiliza para registrar un nuevo cliente en la base de datos.

Ejemplo de Datos (JSON):
json
```
    {
        "nombre": "Juan",
        "apellidos": "Pérez",
        "edad": 30,
        "email": "juan.perez@example.com",
        "telefono": "5551234",
        "direccion": "Calle Principal 123",
        "documento": "12345678",
        "tipo_documento": "CC"
    }
```
    Respuesta Exitosa (201 Created): Retorna los detalles del cliente creado y la URL para acceder a estos detalles.
    Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente si hay problemas de validación o de servidor.

## PUT /api/clientes/{id}

    Descripción: Actualiza los datos de un cliente existente.
    Uso: Se utiliza para modificar los datos de un cliente específico.
    Ejemplo de Datos (JSON):

    json
```
        {
            "id": 1,
            "nombre": "Juan Modificado",
            "apellidos": "Pérez",
            "edad": 31,
            "email": "juan.perez@example.com",
            "telefono": "5551234",
            "direccion": "Calle Principal 123",
            "documento": "12345678",
            "tipo_documento": "CC"
        }
```
        Respuesta Exitosa (200 OK): Retorna los detalles actualizados del cliente.
        Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente si el cliente no existe o si hay problemas de validación o de servidor.

  ## DELETE /api/clientes/{id}
      Descripción: Elimina un cliente según el ID proporcionado.
      Uso: Se utiliza para eliminar un cliente específico de la base de datos.
      Respuesta Exitosa (200 OK): Retorna el ID del cliente eliminado.
      Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente si el cliente no existe o si hay problemas de servidor.

  ## GET /api/clientes/search
      Descripción: Busca clientes por nombre y/o correo electrónico.
      Uso: Se utiliza para realizar búsquedas basadas en nombre y/o correo electrónico de los clientes.
      Parámetros Query:
          searchTerm: Término de búsqueda para nombre o correo electrónico.
      Respuesta Exitosa (200 OK): Retorna una lista de clientes que coinciden con el término de búsqueda en formato JSON.
      Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente si hay problemas de servidor.

 ##  POST /api/clientes/uploadcsv
      Descripción: Carga datos de clientes desde un archivo CSV.
      Uso: Se utiliza para importar múltiples registros de clientes desde un archivo CSV.
      Formato del Archivo CSV: El archivo debe contener las columnas: nombre, apellidos, edad, email, telefono, direccion, documento, tipo_documento.
      Respuesta Exitosa (200 OK): Retorna una lista de registros de clientes creados a partir del archivo CSV en formato JSON.
      Respuesta de Error: Retorna un mensaje de error con el código de estado correspondiente si hay problemas al procesar el archivo CSV.

## Buenas Prácticas y Patrones de Diseño Implementados

    Arquitectura de Capas: Se ha utilizado una arquitectura basada en capas separando la lógica de negocio, la capa de acceso a datos y la presentación (frontend).

    Inyección de Dependencias: ASP.NET Core utiliza inyección de dependencias de manera nativa, lo cual promueve un código más limpio, mantenible y testeable.

    Uso de DTOs (Data Transfer Objects): Se utilizan DTOs para transferir datos entre la capa de presentación (frontend) y la capa de servicio/entidad, evitando acoplamientos innecesarios y mejorando la seguridad.

    Seguridad: Se implementa seguridad a nivel de endpoints utilizando atributos de autorización y roles de usuario para proteger las operaciones sensibles como modificar o eliminar datos.

## Instrucciones para Configurar y Ejecutar la Aplicación

Para ejecutar la aplicación, sigue estos pasos:

    Backend (ASP.NET Core):
        Asegúrate de tener instalado Visual Studio con las cargas de trabajo de desarrollo de ASP.NET Core.
        Abre la solución en Visual Studio.
        Configura la cadena de conexión a la base de datos SQL Server en appsettings.json.
        Compila y ejecuta el proyecto.

    Frontend (React):
        Asegúrate de tener Node.js y npm instalados en tu sistema.
        Abre una terminal y navega hasta la carpeta del proyecto de frontend.
        Instala las dependencias usando npm install.
        Inicia la aplicación usando npm start.

    Base de Datos:
        Asegúrate de tener un servidor SQL Server instalado y disponible.
        Ejecuta las migraciones para crear la estructura de la base de datos utilizando Entity Framework Core.

## Instrucciones para Ejecutar las Pruebas Unitarias

Para ejecutar las pruebas unitarias, sigue estos pasos:

    Backend (ASP.NET Core):
        Abre el explorador de pruebas de Visual Studio.
        Ejecuta todas las pruebas unitarias proporcionadas en el proyecto.

    Frontend (React):
        Utiliza herramientas como Jest y Enzyme para las pruebas unitarias de componentes React.
        Ejecuta las pruebas usando npm test.
