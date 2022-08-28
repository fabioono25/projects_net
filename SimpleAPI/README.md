## Adding the Database

I am using a simple instance of Postgres database. Here is an example of command:

```
docker run --name postgresql -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin -p 5432:5432 -v /data:/var/lib/postgres/data -d postgres
```

