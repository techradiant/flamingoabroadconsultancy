
[Create a Razor Pages web app](https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-6.0)

Open the integrated terminal.

Change to the directory (cd) that will contain the project.

Run the following commands:
--Create a new project
dotnet new webapp -o FAC.Web --framework net6.0
-- Open this project in the VS code
code -r FAC.Web


Trust the HTTPS development certificate by running the following command:
dotnet dev-certs https --trust

-- Run the project
dotnet run

In Visual Studio Code, press Ctrl+F5 to run the app. At the Select environment prompt, select .NET Core.

The default browser launched with the following URL: https://localhost:5001



References:
[ASP .NET Core 5 Razor Pages: how to properly use Partial View and validate it's model state?](
https://stackoverflow.com/questions/67119411/asp-net-core-5-razor-pages-how-to-properly-use-partial-view-and-validate-its)