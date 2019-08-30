# dummy-todo-app
Very simple implementation following The Clean Architecture Principles, with no use of frameworks. 

### Running from source
Running from Windows machine:
```
$ dotnet run .\source\DummyTodoApp.WebApi\DummyTodoApp.WebApi.csproj
```

### Dummy Todo App Core
The Core layer consists in all strong rules of the app. There are the application use cases, their boundaries to connect to outside layers and the abstraction of the repositories it might need. Here reside the main rules, just it. As simple as that.

### Dummy Todo App Domain
In Domain layer resides our domain, in this particular case, the class who represents a Todo, a task to be done. This domain is able to validate yourself and has only the properties that make sense for the business. Here we won't see properties or resources that are specific to the details of persistence or something like that. For now, we look only to the business.

### Dummy Todo App Infrastructure
The Infrastructure layer consists in the details of the application such as the database. There we can find the implementation of persistence using EF Core. In this layer we also have specific entities that make supports the database needs. In our case the database implementation is SQL, so we have annotations on model's properties to attend specific SQL Server needs. In this layer we may consider to add another resources such background tasks, cache implementations, http calls for another APIs and so on.

### Dummy Todo App WebApi
The WebApi layer consists in how the todos are delivered to the user. In our particular case all todos are delivered by a http interface. There we can find an action filter to handle some no desired exceptions and the implementation of presenters as well. The presenters are the responsibles to deliver, in webapi context, the successful response or a bad request response. 

### Dependencies direction
The dependency direction goes from WebApi to Core. In fact, WebApi references Infrastructure and Core implementations. Infrastructure dependends on Core and Domain layer, Core itself depends on Domain layer and Domain layer dependes on no one. 

### Automated tests
At this moment I have just implementend some component tests for successful post and get todos by owner. The main point here is on raise a specific web host where we have full control. 
The unit tests and integration test were be immplemented very soon.
To run the existing tests just run the command bellow:
```
$ dotnet test .\tests\DummyTodoApp.IntegrationTests\DummyTodoApp.IntegrationTests.csproj
```

### Contributions
Be welcome to contribute with this example and perform your pull requests. I will appreciate that.
