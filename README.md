
# Web API with Docker

This is a simple .Net Core Web API project to demonstrate docker support for web API


## Table of Contents
1. [Features](#general-info)
2. [Docker Run](#docker)
3. [See Output](#installation)
### Features
Following things are covered in this project:

>  1. Reading from environment variable
>  2. Reading from appsettings 
>  3. Dependency injection
>  4. Injecting the environment variable in service constructor
>  5. unit test
### Docker Run
You need to run the docker run command with the image name and also mention the port mapping

```
$docker run -p <localport>:80 sharif8rifat/testcoredocker
```
for example
```
$docker run -p 8080:80 sharif8rifat/testcoredocker
```

###See Output
After the docker run successfully  you can browse to 
http://localhost:<mapped-port>/swagger/index.html

to see the API list 