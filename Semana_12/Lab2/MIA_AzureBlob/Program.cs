// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;

string? connectionString =
    Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("No se encontró la Connection String.");
    Console.WriteLine("Configure la variable AZURE_STORAGE_CONNECTION_STRING.");
    return;
}

string nombreContainer = "mia-archivos";

BlobServiceClient blobServiceClient =
    new BlobServiceClient(connectionString);

BlobContainerClient containerClient =
    blobServiceClient.GetBlobContainerClient(nombreContainer);

int opcion;

do
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("     MIA - AZURE BLOB STORAGE");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Subir archivo");
    Console.WriteLine("2. Listar archivos");
    Console.WriteLine("3. Descargar archivo");
    Console.WriteLine("4. Eliminar archivo");
    Console.WriteLine("5. Salir");
    Console.WriteLine("=================================");
    Console.Write("Seleccione una opción: ");

    if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.WriteLine("Opción inválida.");
        opcion = 0;
        continue;
    }

    switch (opcion)
    {
        case 1:
            await SubirArchivo(containerClient);
            break;

        case 2:
            ListarArchivos(containerClient);
            break;

        case 3:
            await DescargarArchivo(containerClient);
            break;

        case 4:
            await EliminarArchivo(containerClient);
            break;

        case 5:
            Console.WriteLine("Saliendo del programa...");
            break;

        default:
            Console.WriteLine("Opción inválida.");
            break;
    }

} while (opcion != 5);


static async Task SubirArchivo(BlobContainerClient containerClient)
{
    Console.Write("\nIngrese la ruta del archivo: ");
    string? ruta = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
    {
        Console.WriteLine("El archivo no existe.");
        return;
    }

    string nombreArchivo = Path.GetFileName(ruta);

    BlobClient blobClient =
        containerClient.GetBlobClient(nombreArchivo);

    await blobClient.UploadAsync(ruta, overwrite: true);

    Console.WriteLine("Archivo subido correctamente.");
}


static void ListarArchivos(BlobContainerClient containerClient)
{
    Console.WriteLine("\nARCHIVOS EN AZURE");
    Console.WriteLine("--------------------------------");
    Console.WriteLine($"{"Nombre",-25} {"Tamaño"}");
    Console.WriteLine("--------------------------------");

    bool existenArchivos = false;

    foreach (var blob in containerClient.GetBlobs())
    {
        existenArchivos = true;

        long tamanio = blob.Properties.ContentLength ?? 0;

        Console.WriteLine(
            $"{blob.Name,-25} {tamanio} bytes"
        );
    }

    if (!existenArchivos)
    {
        Console.WriteLine("No hay archivos almacenados.");
    }
}


static async Task DescargarArchivo(BlobContainerClient containerClient)
{
    Console.Write("\nIngrese el nombre del blob: ");
    string? nombre = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("Nombre inválido.");
        return;
    }

    BlobClient blobClient =
        containerClient.GetBlobClient(nombre);

    if (!await blobClient.ExistsAsync())
    {
        Console.WriteLine("El archivo no existe en Azure.");
        return;
    }

    Console.Write("Ingrese la carpeta de destino: ");
    string? carpeta = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(carpeta) ||
        !Directory.Exists(carpeta))
    {
        Console.WriteLine("La carpeta no existe.");
        return;
    }

    string rutaDestino =
        Path.Combine(carpeta, nombre);

    await blobClient.DownloadToAsync(rutaDestino);

    Console.WriteLine("Archivo descargado correctamente.");
    Console.WriteLine($"Ubicación: {rutaDestino}");
}


static async Task EliminarArchivo(BlobContainerClient containerClient)
{
    Console.Write("\nIngrese el nombre del blob: ");
    string? nombre = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("Nombre inválido.");
        return;
    }

    BlobClient blobClient =
        containerClient.GetBlobClient(nombre);

    if (!await blobClient.ExistsAsync())
    {
        Console.WriteLine("El archivo no existe en Azure.");
        return;
    }

    Console.Write($"¿Desea eliminar {nombre}? (S/N): ");

    string? respuesta = Console.ReadLine();

    if (respuesta?.ToUpper() != "S")
    {
        Console.WriteLine("Operación cancelada.");
        return;
    }

    await blobClient.DeleteAsync();

    Console.WriteLine("Archivo eliminado correctamente.");
}
