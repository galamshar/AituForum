## AituForum
Forum for students ASTANA IT UNIVERSITY, where students can discuss general topics of interest and create different topics on different topics.

## How To Deploy

Make sure you have installed and configured docker in your environment. After that, you can run the below commands from the directory and get started with the AituForum immediately.

```docker
docker-compose build
docker-compose up
```
You should be able to browse different components of the application by using the below URLs :
```plaintext
WebApi : http://webapi:8080/
ForumDb : https://forumdb:5432/
Frontend : https://frontend:3000/
```

## Architecture
![alt Database](https://i.imgur.com/sWd54PP.png)
- Docker container based fullstack project

## Features

- JWT Authentication
- Creating threads and posts
- Personal profile page
- DDD pattern backend
- Well optimized fullstack app

## Platforms

- Web

## Potential technologies for development

Backend:

- C# (ASP.NET, Entity Framework)
- MediatR
- AutoMapper
- PostgreSQL

Frontend:

- Pure ReactJS
- Nginx

## Plan of development

| Week | Plan | Status |
| --- | --- | - |
| Week 3 | Creating architecture (ERD diagrams, database building)|:heavy_check_mark:|
| Week 4 | Developing Auth API|:heavy_check_mark:|
| Week 5 | Developing  Topics,Posts API|:heavy_check_mark:|
| Week 6 | Developing Account, Admin API|:heavy_check_mark:|
| Week 7 | Designing web &#39;s frontend. Layouting pages&#39;s view. | :heavy_check_mark:|
| Week 8 | Connecting backend with web app and mobile app |:heavy_check_mark:|
| Week 9 | Refactoring, code clean up, testing. |:heavy_check_mark:|
| Week 10 | Presentation pre-final project |:heavy_check_mark:|

## Wiki
[Documentation](https://github.com/galamshar/AituForum/wiki)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
