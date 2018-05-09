FROM microsoft/aspnetcore-build
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=microsoft/aspnetcore-build /code/out ./
EXPOSE 80
ENTRYPOINT ["dotnet","WebUserAPI.dll"]