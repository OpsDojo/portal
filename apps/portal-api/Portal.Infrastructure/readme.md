## Handy Commands

``` bash
# restore tools
dotnet tool restore

# add a new migration
dotnet ef migrations add InitialCreate -p Portal.Infrastructure -s Portal.Host -o EF/Migrations

# apply migrations
dotnet ef database update -p Portal.Infrastructure -s Portal.Host
```