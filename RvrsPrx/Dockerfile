FROM nginx:1.21-alpine

RUN apk add --no-cache openssl

COPY default.conf /etc/nginx/conf.d/
COPY gzip.conf options-ssl-nginx.conf hsts.conf /etc/nginx/includes/
COPY site.conf.tpl /customization/
COPY rvrs_prx.sh /customization/

RUN chmod +x /customization/rvrs_prx.sh

EXPOSE 80

CMD ["/customization/rvrs_prx.sh"]
