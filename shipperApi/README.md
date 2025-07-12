packages added

dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=3306;Database=shippingdev;User=root;Password=Qwerty123456;SslMode=None;" Pomelo.EntityFrameworkCore.MySql -o Models --context ShippingDevContex --no-onconfiguring --force

dotnet add package MySql.Data --version 8.0.*

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.*

dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.*

dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.*

dotnet add package MiniValidation

dotnet add package FastReport.OpenSource
dotnet add package FastReport.OpenSource.Export.PdfSimple


dotnet tool install --global dotnet-ef --version 8.0.*


dotnet sln shipperApi.sln add shipperApi/shipperApi.csproj 


dotnet new sln --name shipperApi


dotnet publish shipApi.csproj -c Release -r linux-x64 --self-contained true

sudo apt-get update && sudo apt-get install -y libgdiplus
