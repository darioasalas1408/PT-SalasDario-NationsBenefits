# PT-SalasDario

Instrucciones para ejecutar la aplicación: 
1) Descomprima el repositorio en su computadora
2) Haga click derecho en el proyecto con nombre: PT-SalasDario.API
3) Seleccione la opción: "Manage User Secrets"
4) Pegue el siguiente contenido, cambiando los valores de acuerdo a su configuración:
```json
{
  "ConnectionStrings:DefaultConnection": "Server=localhost;User Id=MY_USER;Database=MY_DB;Password=MY_PASSWORD;Connection Timeout=1000000"
}
```
5) Aseugrese de que el proyecto con nombre PT-SalasDario.API sea el marcado como Startup Proyect
6) Presione F5 o haga click en el botón Ejecutar

# Importante

En proyecto de Importación de consola, no esta funcionando los secrets, la cadena de conexión se va tener que cambiar en appsettings.json

