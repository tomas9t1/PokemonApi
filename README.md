# PokemonApi

This is the project for the assignment https://github.com/mmedum/the_pokemon_api

You can get Docker images with following commands

```docker pull tomas9t1/mongofilleddata```
```docker pull tomas9t1/pokemonapi```
```docker pull tomas9t1/pokemondataapi```

**Startup:**

```
1) docker network create myapp_net
2) docker run -d --network myapp_net --hostname rabbitmqhost --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3-management
3) docker run --network myapp_net --name mongo docker pull tomas9t1/mongofilleddata
4) docker run -d --network myapp_net -p 8082:80 --name dataapi pokemondataapi
5) docker run -d --network myapp_net -p 8081:80 --name api pokemonapi
```
```
Documentation for the Api - ```http://localhost:8081/swagger
Documentation for the DataApi - ```http://localhost:8082/swagger
```

**Some thoughts about the project:**
```
The whole project is not very clean because was written while driving through Norway(sorry, I'm on holidays)

Had fun with it, quite enjoyed the task

Naming could be improved and N-Tier layer architecture should be more divided

RabbitHandler in DataApi is not separated because I believed my collegue that easynetq is great nuget for rabbit and got into
trouble while recieving bigger messages

Different topics are creating automatically though with this nugget

If moving further with project all variables should moved to settings json file

Also I would like wo implement Repository pattern and extends current Exceptions for better error handling

Created this project while practically not using internet at all
```

**Testing was not completed due to my mac, somehow could  __NOT__ Debug tests, tried NUnit ir XUnit , 
same result with both**
