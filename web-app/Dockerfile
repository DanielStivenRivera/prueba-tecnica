# Utiliza la imagen oficial de Node.js como base
FROM node:latest as build

# Establece el directorio de trabajo
WORKDIR /app

# Copia el archivo package.json y ejecuta npm install
COPY package*.json ./
RUN npm install

# Copia el resto de los archivos de la aplicación
COPY . .

# Construye la aplicación Angular
RUN ng build --prod

# Utiliza la imagen oficial de Nginx como base para la imagen de producción
FROM nginx:latest

# Copia la configuración de Nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Copia la aplicación Angular compilada
COPY --from=build /app/dist/* /usr/share/nginx/html/

# Establece las variables de entorno para la URL del servidor
ENV API_URL http://api:8080

# Exposición del puerto 80
EXPOSE 4200

# Comando para ejecutar la aplicación
CMD ["nginx", "-g", "daemon off;"]
