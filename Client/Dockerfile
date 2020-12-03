FROM node:12-alpine3.12
EXPOSE 80
EXPOSE 3000

WORKDIR /client/app

COPY package*.json ./
RUN npm install --silent
COPY . ./

CMD ["npm", "run", "build:local"]