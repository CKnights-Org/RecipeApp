# Recipe App
Repository Template with common used files


How to setup the project:
1. Open the src\*.sln file in Visual Studio
2. Right click on the MVC app and click "Set up as a startup project"
3. Open Package Manager Console
4. Set the default project in Package Manage Console to "RecipeAppDAL"
5. Run the following command:
    > Update-Database -Context RecipeAppDBContext
6. Set the default project in Package Manage Console to "RecipeAppDAL"
7. Run the following command:
    > Update-Database -Context AppIdentityDbContext
8. If everything went fine, you can start the app :)
