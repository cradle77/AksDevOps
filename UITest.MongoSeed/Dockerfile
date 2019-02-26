FROM alpine

RUN apk update && apk add mongodb-tools
WORKDIR /
COPY UITest.MongoSeed/execute.sh .
RUN chmod 700 /execute.sh

WORKDIR /data
COPY UITest.MongoSeed/people.json .

ENTRYPOINT ["/bin/sh", "/execute.sh"]
