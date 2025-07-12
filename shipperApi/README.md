packages added

dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=3306;Database=MMK;User=root;Password=Qwerty123456;SslMode=None;" Pomelo.EntityFrameworkCore.MySql -o Models --context MMKDbContext --no-onconfiguring --force

dotnet add package MySql.Data --version 8.0.*

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.*

dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.*

dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.*

dotnet add package MiniValidation

dotnet add package FastReport.OpenSource


dotnet tool install --global dotnet-ef --version 8.0.*


dotnet sln mmkApi.sln add mmkApi/mmkApi.csproj 


dotnet new sln --name mmkApi


dotnet publish shipApi.csproj -c Release -r linux-x64 --self-contained true


