FROM ubuntu
RUN apt update -y &&  apt install -y tree
WORKDIR /src
RUN pwd && sleep 5
COPY . .
CMD pwd && ls -la && tree