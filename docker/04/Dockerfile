FROM ubuntu
RUN apt update
RUN apt install -y iproute2
ARG demo="to jest demo"
ARG workshop_day="to jest domyslne"
ARG VCS_REF
ARG build_name
ENV workshop_day=${workshop_day}
RUN echo $build_name && sleep 5
RUN printenv && sleep 5
ENV GIT_SHA $VCS_REF
CMD echo "Hello world! ${workshop_day}" && printenv