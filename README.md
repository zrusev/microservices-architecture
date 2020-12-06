 ![license](https://img.shields.io/github/license/zrusev/microservices-architecture.svg) ![size](https://img.shields.io/github/repo-size/zrusev/microservices-architecture.svg) ![last commit](https://img.shields.io/github/last-commit/zrusev/microservices-architecture.svg)

# Microservices Architecture Project: My Store
This is an online store build for selling of any products online.
In this project I have mainly considered users registrations, products navigation and placing orders.
The Project also uses push notifications to follow some user actions.
All isolated services have been dockerized individually and combined using Docker Compose.

## Architecture:
- Bounded Contexts and Multiple Web Applications
- Service & Data Layers
- Separate Administration Client
- API Gateway which aggregates data from 2 microservices: top bought products per category
- Message broker and event-driven messages: user registation & product seen counter; new order created
- Push notifications using SignalR on user registrations
- Docker setup and containers for every web application
- Docker Compose for containers orchestration
- Database, HTTP and Messages failures handlers
- Outbox pattern to ensure the events will not lead to unstable or invalid data
- Health monitoring

### The users will get a Push Notification during:
- New User Registration
### The users will get a Toast Notification during:
- The Item was not available in the store
- Service error appears
- New order has been created

### Entrypoints:
- Client served @localhost:3000
- Admin panel served @localhost:50400
- Services monitoring served @localhost:5013/healthchecks-ui

### The Technologies Used to build the Server are:
- ASP.NET Core 3.1
- EF Core 3.1
- Automapper
- Serilog
- Refit
- MassTransit
- RabbitMQ
- Handfire
- HealthChecks.UI
- SignalR
- JwtBearer

### The Technologies Used to build the Client are:
- Create React App
- Redux
- SignalR
- Material UI

## Continuous Integration & Continuous Delivery with Jenkins
- [Jenkins](https://www.jenkins.io/), [Docker CLI](https://docs.docker.com/engine/reference/commandline/docker/), [Docker-compose](https://docs.docker.com/compose/), [Kubectl CLI](https://kubernetes.io/docs/reference/kubectl/overview/), [GIT](https://git-scm.com/) are installed on a EC2 [t3.medium](https://aws.amazon.com/ec2/instance-types/t3) linux instance in eu-south-1 region.
- Security group is created which exposes only 80/433 ports for http/https requests from anywhere. SSH is reserved to my personal IP address.
- Jenkins is listening on default 8080/8443 ports. Iptables port forwarding is used on port 80->8080/433->8443 as Jenkins is running its own user.
- Elastic IPv4 is allocated and assinged to instance. "A" record is provisioned to an external domain registrar.
- Branching strategy - GitFlow
- GitHub webhooks deliver tasks to a multi-branch pipeline.
- E-mail notifications on job completion/failure.
- Pipeline tasks - running unit tests, integration tests, docker builds, docker hub images push, deployment to kubernetes
- Demo - you can take a look at Jenkins configuration here:
> address: [http://jenkins.microservices.zrusev.me/](http://jenkins.microservices.zrusev.me/)
>
> username: **ivaylokenov**
>
> password: **same as Code It Up videos password**

## Clusterization with Kubernetes
- Cloud-ready Kubernetes is using AWS EKS
- Single cluster per branch provisioned - development & production
- StatefulSets used for databases
- Configuration maps & Secrets separated by environments
- Production deploy is done only after a manual configuration in the pipeline
- "CNAME" records attached in an external domain registrar to all load balancers external endpoints
- Metrics server
- Drawbacks: Default's Elastic Block Storage (EBS) PV provides only ReadWriteOnce access mode. In order to persist the ASP.NET data-protection keys to multiple replicas you have to use AWS Elastic File System (EFS) which supports ReadWriteMany mode and persists data across all availability zones or use a different persistence mechanism as described [here](https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/implementation/key-storage-providers?view=aspnetcore-5.0&tabs=visual-studio). In this case I was able to create an external EFS provisioner and mount an NFS shared storage which persists the same data across all nodes and zones.
- Development Cluster:
> http://development.client.microservices.zrusev.me/
>
> http://development.admin.microservices.zrusev.me:50400/
>
> http://development.monitoring.microservices.zrusev.me:5013/healthchecks-ui