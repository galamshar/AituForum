FROM nginx:1.16.0-alpine
COPY ./react-api/build /var/www
COPY react-api/nginx.conf /etc/nginx/nginx.conf
ENTRYPOINT ["nginx","-g","daemon off;"]