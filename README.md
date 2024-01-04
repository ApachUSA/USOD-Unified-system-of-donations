# Unified system of donations
#### P.S. This is my first attempt at implementing a microservice architecture, so some decisions may be wrong. 🫶
## Introduction
The purpose of this master's thesis is to develop a unified collection system aimed at supporting various charitable initiatives, including donations for the military, IDPs, children, animals, etc.
Problems of the chosen subject area:
- Fragmentation of information.
- Lack of centralization.
- Lack of transparency.

## Technologies
![Static Badge](https://img.shields.io/badge/ASP.NET-badge?style=for-the-badge&logo=.net&color=%23292929)
![Static Badge](https://img.shields.io/badge/EF.Core-badge?style=for-the-badge&logo=db&color=%23292929)
![Static Badge](https://img.shields.io/badge/SQL-badge?style=for-the-badge&logo=sql%20server&color=%23292929)
![Static Badge](https://img.shields.io/badge/RESTful%20API-badge?style=for-the-badge&logo=api&color=%23292929)
![Static Badge](https://img.shields.io/badge/SignalR-badge?style=for-the-badge&logo=SignalR&color=%23292929)
![Static Badge](https://img.shields.io/badge/Docker-badge?style=for-the-badge&logo=docker&color=%23292929)
![Static Badge](https://img.shields.io/badge/RabbitMq-badge?style=for-the-badge&logo=rabbitmq&color=%23292929)
![Static Badge](https://img.shields.io/badge/Ocelot-badge?style=for-the-badge&logo=ocelot&color=%23292929)
![Static Badge](https://img.shields.io/badge/Seq-badge?style=for-the-badge&logo=seq%20logging&color=%23292929)
![Static Badge](https://img.shields.io/badge/Postman-badge?style=for-the-badge&logo=Postman&color=%23292929)



<div align="center">
  <img src="/Domain/Screenshots/Architecture.png"/>
  <p>Architecture</p>
  <br/>
</div>


- The architectural pattern of the entire system is **Microservice Architecture**.
- The only access point is the **Ocelot** gateway.
- **JWT** token is used for authorization and authentication.
- Each microservice is implemented as a **RestFull API** and deployed in **docker** containers.
- Each microservice has its own database, which interacts with the **Entity Framework** and is implemented using the **repository pattern**.
- Each microservice sends its logs to **Seq** using **RabbitMq**
- RealTime microservice uses **SignalR** to send notifications and comments in real time.
- **Postman** was used for convenient storage and calling the api

## Screenshots
<div align="center" >
  <img width="650" src="/Domain/Screenshots/Login.png"/>
  <p>Login</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/Main.png"/>
  <p>Main</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/Profile.png"/>
  <p>Profile</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/FundList.png"/>
  <p>FundList</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/FundDetails.png"/>
  <p>FundDetails</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/FundAbout.png"/>
  <p>FundAbout</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/ProjectList.png"/>
  <p>ProjectList</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/ProjectHead.png"/>
  <img width="650" src="/Domain/Screenshots/GoalList.png"/>
  <img width="650" src="/Domain/Screenshots/ProjectComments.png"/>
  <p>ProjectDetails</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/ProjectCreate.png"/>
  <p>ProjectCreate</p>
  <br/>
</div>
<div align="center">
  <img width="650" src="/Domain/Screenshots/ApiCalling.png"/>
  <p>ApiCalling</p>
  <br/>
</div>
