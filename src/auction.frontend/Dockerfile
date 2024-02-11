# Stage 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install --force
RUN npm install -g @angular/cli

CMD ["ng", "serve", "--host", "0.0.0.0", "-c", "production"]

EXPOSE 4200
