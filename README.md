# Requisitos
### <li> Docker instalado en el sistema
### Construir y ejecutar el Docker Compose
#### <li>Construye la imagen Docker 
```bash
docker-compose build
```
#### <li>Ejecuta el contenedor Docker 
```bash
docker-compose up -d
```
#### <li>Verifica que el contenedor esté funcionando
```bash
docker-compose ps
```

##### <li> ahora vamos a iniciar el proyecto de angular
```bash
npm install
```

##### <li> luego ejecutar el siguiente comando
```bash
ng serve
```

## NOTA:
El sistema cuenta con autenticación y autorización las credenciales son ````contraseña: hashedpasswordexample```` y ````correo: default@example.com````
estas son requeridas principalmente para entrar a la sección de perfil y crear nuevas reservas.

## EJECUCIÓN DE PRUEBAS UNITARIAS
### ES NECESARIO EJECUTAR PRIMERO EL CONTENEDOR DE DOCKER
### <li> Backend
#### <li>Abrir una terminal y navegar a la carpeta de la solución de visual studio y ejecutar el siguiente comando
```bash
dotnet test
```
### NOTA:
durante el proceso de integración con el frontend fue necesario realizar varios cambios y por falta de tiempo no se modificaron los test unitarios que fueron desarrollados.
### <li> Frontend
#### <li> Los test del frontend no fueron terminados


# Explicación de arquitectura usada en el proyecto de angular

Inicialmente, deshabilité zone.js y utilicé la característica experimental de zoneless con signals, con el objetivo de optimizar las renderizaciones del DOM, asegurando que solo se actualicen los componentes realmente necesarios. Además, se implementaron dos módulos: uno para la página principal y otro para la sección de perfil.

La sección principal es la encargada de listar las horas disponibles de cada día de la semana como también las franjas horarias que están ocupadas.

Se crearon servicios para conectar con cada uno de los endpoints expuestos por el backend, con el fin de mantener el código organizado y modular. También se incorporó un interceptor encargado de agregar el token de autenticación a las peticiones HTTP, evitando la repetición de código en distintas partes de la aplicación.

Adicionalmente, se desarrolló un servicio para gestionar los dos diálogos que se muestran en la aplicación, lo que permite reutilizar el código y evitar duplicaciones. Para la parte visual, se utilizó Tailwind CSS para estilizar la interfaz de usuario de manera eficiente y flexible.

# NOTA
EL repositorio cuenta con un archivo swagger.json para la documentación del backend
