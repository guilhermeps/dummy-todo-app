# dummy-todo-app
Very simple implementation following The Clean Architecture Principles, with no use of frameworks. 

### Running from source
Running from Windows machine:
```
$ dotnet run ".\source\DummyTodoApp.WebApi\DummyTodoApp.WebApi.csproj"
```

### Details
The Core project consist in all strong rules of the app. There are the application use cases, their boundaries to connect to outside layers and the abstraction of the repositories it might need. 

The Infrastructure project consist in the details of the application such as the database. There we can find the implementation using EF Core. At this layer we may consider to add another resources such background tasks and cache implementations. 

The WebApi project consist in how the todos are delivered to the user. In our particular case all todos are delivered by a http interface. There we can find an action filter to handle some no desired exceptions and the implementation of presenters as well. The presenters are the responsibles to deliver, in webapi context, the successful response or a bad request response. 

### Automated testes
At this moment I have just implementend some component tests for successful post and get todos by owner. The main point here is on raise a specific web host where we have full control. 
The unit tests and integration test were be immplemented very soon.
To run the existing tests just run the commando bellow:
```
$ dotnet test ".\source\DummyTodoApp.IntegrationTests\DummyTodoApp.IntegrationTests.csproj"
```

### Contributions
Be welcome to contribute with this example and perform your pull requests. I will appreciate that.