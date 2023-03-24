For exercise 3

```app.MapPost("/userIds", async (List<int> userIds) => await GetUsersAsync(userIds));```

In postman we can give them just as array like [1,2, 3]

If I have the UserIds as class

  ```
class UserList
{
    public List<int> UserIds {get;set;}
}
app.MapPost("/userIds", async (UserList userIds) => await GetUsersAsync(userIds));
```

then on Postman give it as an object like 
{"userIds":[1,2, 3]} 
