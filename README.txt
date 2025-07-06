# FoodOrdering Project

## Overview

FoodOrdering is a microservices-based application built with .NET 9, designed for managing food orders, payments, and notifications.

## Architecture

- **Ordering Service:** Handles customer orders.
- **Payment Service:** Processes payments.
- **Notification Service:** Sends notifications (email, SMS).
- **Infrastructure:** Kafka (event bus), Zookeeper, MongoDB, Redis, ElasticSearch, Portainer.

## Prerequisites

- Docker and Docker Compose
- .NET 9 SDK
- (Optional) Node.js and Angular CLI for frontend

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/FoodOrdering.git
   cd FoodOrdering


## Project Structure

/FoodOrdering
│
├── docker-compose.yml      # Docker Compose file for services and infra
├── README.md               # Project documentation
├── .gitignore              # Git ignore rules
├── .github/                # GitHub workflows for CI/CD
├── backend/                # Backend microservices source code
├── frontend/               # FrontEnd
│       ├── FoodOrdering.OrderingService/
│       ├── FoodOrdering.PaymentService/
│       ├── FoodOrdering.NotificationService/
├── docker-compose.yml
├── README.md
└── .gitignore
