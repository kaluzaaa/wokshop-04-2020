FROM ubuntu
RUN apt update -y &&  apt install -y tree
WORKDIR /src
RUN pwd && sleep 5
COPY . .
RUN tree && sleep 5
WORKDIR /app
RUN tree && sleep 5
CMD pwd && ls -la && tree