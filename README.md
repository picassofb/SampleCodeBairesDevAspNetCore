# Description

This Api was made to create Projects and assing Tasks to them using a Code First approach. 

```json
{
    "projectId": "efc41e38-4c7f-4258-b32e-8a055318e9dc",
    "projectName": "Liverpool Solar Panel",
    "isDone": false,
    "isDeleted": false,
    "projectTasks": [
        {
            "projectTaskId": "a8aeab6b-f0ed-43f6-972b-0e7e73681557",
            "task": "Inspection the site",
            "startDate": "2019-04-09T00:00:00",
            "endDate": "2019-04-22T00:00:00",
            "isDeleted": false,
            "projectId": "efc41e38-4c7f-4258-b32e-8a055318e9dc"
        },
        {
            "projectTaskId": "ee8e10cf-a483-477b-9cb0-4708bebbcd39",
            "task": "Buy Equipments",
            "startDate": "2019-04-09T00:00:00",
            "endDate": "2019-04-22T00:00:00",
            "isDeleted": false,
            "projectId": "efc41e38-4c7f-4258-b32e-8a055318e9dc"
        },
        {
            "projectTaskId": "0c3ee535-4f8a-49f3-98f5-8e482f5001f1",
            "task": "Delivery of the project",
            "startDate": "2019-04-09T00:00:00",
            "endDate": "2019-04-22T00:00:00",
            "isDeleted": false,
            "projectId": "efc41e38-4c7f-4258-b32e-8a055318e9dc"
        }
    ]
}
```

# Tecnologies

This was made using ASP.NET Core 2.2, Entity Framework Core to interact with Sql Server. The services on the startup are configure to use Jwt Token and the authentication with lowered security for demonstration purpose only.


# How to Use

First you need to change the appsettings.json file and change the defaultConnection string to yours.

```json
{
  "ConnectionStrings": {
    "defaultConnection": "YourConnectionString"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

After that is necessary to run **update-database** in the package management console to create the database and the tables.



To start using first you need to create a user and login to obtain the JWT Token, with this token you can consume all the endpoints.

Please visit this [webpage](https://documenter.getpostman.com/view/2622970/S1ENxJ6i) for more information about this API.


Thank you!