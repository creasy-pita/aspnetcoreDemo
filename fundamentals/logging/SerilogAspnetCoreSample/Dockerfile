#本地 build ，只使用 runtime基础镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
COPY  . ./
EXPOSE 5000
ENTRYPOINT ["dotnet","SerilogAspnetCoreSample.dll"]

#使用build 和 runtime基础镜像
#FROM mcr.microsoft.com/dotnet/core/sdk:2.1
#WORKDIR /code
#COPY *.csproj ./
#RUN dotnet restore
#COPY . ./
#RUN dotnet publish -c Release -o out
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
#WORKDIR /app
#COPY --from=mcr.microsoft.com/dotnet/core/sdk:2.1 /code/out ./
#EXPOSE 5000
#ENTRYPOINT ["dotnet","SerilogAspnetCoreSample.dll"]


#FROM centos:centos7 
#WORKDIR /app
#COPY . ./
#ENTRYPOINT ["/app/SerilogAspnetCoreSample"]