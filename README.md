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

## EJECUCIÓN DE PRUEBAS UNITARIAS
### <li> Backend
#### <li>Abrir una terminal y navegar a la carpeta de la solución de visual studio y ejecutar el siguiente comando
```bash
dotnet test
```
### <li> Frontend
#### <li> Abrir una terminal y navegar a la carpeta del proyecto de Angular y ejecutar el siguiente comando
```bash
ng test --code-coverage
```
