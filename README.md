# Unified system of donations

## Introduction
The purpose of this master's thesis is to develop a unified collection system aimed at supporting various charitable initiatives, including donations for the military, IDPs, children, animals, etc.
Problems of the chosen subject area:
- Fragmentation of information.
- Lack of centralization.
- Lack of transparency.

## Technologies

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
