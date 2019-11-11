Front
1.Установить npm
2.Перейти в консоли в папку \TickTackToe\TttFront
3.Выполнить npm install
4.Выполнить npm start
Back
1.Установить .net core 3.0 sdk, ms sql server
2.Перейти в консоли в папку \TickTackToe
3.Выполнить dotnet restore
4.dotnet tool install --global dotnet-ef
5.dotnet ef database update --project Data -s Api
6.dotnet run --project Api
Back через Visual Studio:
1.Открыть package manager console
2.Выполнить update-database
3.Запустить проект
4.Открыть проект Reports -> report.rdl -> preview(необходимо установить  SSDT для Visual Studio и Sql Server Reporting Services)