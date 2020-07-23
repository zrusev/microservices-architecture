 ![license](https://img.shields.io/github/license/zrusev/microservices-architecture.svg) ![size](https://img.shields.io/github/repo-size/zrusev/microservices-architecture.svg) ![last commit](https://img.shields.io/github/last-commit/zrusev/microservices-architecture.svg)

# Microservices Architecture Project: My Store
This is an online store build for selling of any products online.
In this project I have mainly considered users registrations, products navigation and placing orders.
The Project also uses push notifications to follow some user actions.
All isolated services have been dockerized individually and combined using Docker Compose.

### Architecture:
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