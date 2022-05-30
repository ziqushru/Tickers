FROM node AS sdk

WORKDIR /usr/src/app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm run build


FROM nginx AS runtime

COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=sdk /usr/src/app/dist/ /usr/share/nginx/html
