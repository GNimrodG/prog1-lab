@echo off
dotnet new console --use-program-main --langVersion 10 -o .\%1 -n %1
dotnet sln .\progLab.sln add .\%1\%1.csproj
git add .\%1
