# MIA - Azure Blob Storage

## Objetivo

Desarrollar una aplicación de consola en C# que permita conectarse a Azure Blob Storage y realizar operaciones básicas de manejo de archivos en la nube, como subir, listar, descargar y eliminar archivos.

## Tecnologías utilizadas

- C#
- .NET
- Azure Blob Storage
- Azure.Storage.Blobs
- Visual Studio Code
- Microsoft Azure

## Configuración de Azure

Para realizar el laboratorio se utilizó un Storage Account de Microsoft Azure.

Dentro del Storage Account se creó un contenedor llamado:

`mia-archivos`

El contenedor fue configurado con nivel de acceso privado para evitar que los archivos almacenados puedan ser consultados públicamente.

La conexión entre la aplicación y Azure Blob Storage se realizó mediante una Connection String.

## Arquitectura de la solución

La aplicación fue desarrollada como una aplicación de consola en C#.

El programa utiliza las clases proporcionadas por el paquete `Azure.Storage.Blobs` para comunicarse con Azure Blob Storage.

Se utilizaron principalmente:

- `BlobServiceClient`: permite establecer la conexión con el servicio de almacenamiento.
- `BlobContainerClient`: permite trabajar con el contenedor utilizado por la aplicación.
- `BlobClient`: permite realizar operaciones sobre un archivo o blob específico.

La aplicación presenta un menú desde el cual el usuario puede seleccionar las diferentes operaciones disponibles.

## Operaciones

### Subir archivo

La aplicación solicita la ruta de un archivo almacenado localmente.

Primero se verifica que el archivo exista. Luego se obtiene su nombre y se utiliza un `BlobClient` para subirlo al contenedor de Azure Blob Storage.

Al finalizar se muestra un mensaje indicando que el archivo fue subido correctamente.

### Listar archivos

Esta opción obtiene los blobs almacenados dentro del contenedor y muestra el nombre y tamaño en bytes de cada archivo.

Esto permite comprobar desde la aplicación qué archivos se encuentran almacenados actualmente en Azure.

### Descargar archivo

La aplicación solicita el nombre del blob que se desea descargar y verifica que exista dentro del contenedor.

Luego solicita una carpeta de destino y descarga el archivo en la ubicación indicada por el usuario.

Finalmente se muestra la ruta donde fue guardado.

### Eliminar archivo

La aplicación solicita el nombre del blob que se desea eliminar.

Antes de realizar la eliminación se verifica que el archivo exista y se solicita una confirmación al usuario.

Si el usuario confirma la operación, el archivo es eliminado de Azure Blob Storage.

## Manejo de errores

La aplicación realiza diferentes validaciones para evitar errores durante su ejecución.

Entre las principales validaciones se encuentran:

- Verificar que el archivo local exista antes de subirlo.
- Verificar que el blob exista antes de descargarlo.
- Verificar que la carpeta de destino exista.
- Verificar que el blob exista antes de eliminarlo.
- Solicitar confirmación antes de realizar una eliminación.
- Validar las opciones ingresadas en el menú.

## Protección de la Connection String

La Connection String contiene información sensible, por lo que no fue almacenada directamente dentro del código fuente ni publicada en GitHub.

Para protegerla se utilizó una variable de entorno llamada:

`AZURE_STORAGE_CONNECTION_STRING`

La aplicación obtiene la credencial utilizando:

`Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING")`

De esta forma, el código fuente puede almacenarse en GitHub sin exponer la Connection String ni la Account Key utilizada para acceder al Storage Account.

## Instrucciones para ejecutar el proyecto

1. Tener instalado .NET.
2. Descargar o clonar el proyecto.
3. Abrir una terminal dentro de la carpeta del proyecto.
4. Configurar la variable de entorno `AZURE_STORAGE_CONNECTION_STRING` con la Connection String correspondiente.
5. Ejecutar el programa con:

`dotnet run`

6. Seleccionar una opción del menú:

   - 1 - Subir archivo
   - 2 - Listar archivos
   - 3 - Descargar archivo
   - 4 - Eliminar archivo
   - 5 - Salir
   ## Connection String utilizada

Para realizar la conexión con Azure Blob Storage se utilizó la siguiente Connection String:

DefaultEndpointsProtocol=https;AccountName=miastorage12rp2026;AccountKey=LACgk3OFm2JiND430GWkdeICvEoB4j3wHu...............==;EndpointSuffix=core.windows.net

#Por motivos de seguridad, la Connection String se muestra de forma parcial, ocultando la AccountKey para evitar exponer credenciales  dentro del repositorio de GitHub como buena practica.

