from mcr.microsoft.com/dotnet/sdk:6.0 as sdk

workdir /home/app

copy . .

run dotnet publish ./AspDotNet/AspDotNet.csproj -o /publish/


from mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

workdir /publish

copy --from=sdk /publish .

expose 80
expose 443

run apt-get update && \
    apt-get install libterm-readkey-perl -yq && \
    apt-get install -yq tzdata && \
    ln -fs /usr/share/zoneinfo/Europe/Athens /etc/localtime && \
    dpkg-reconfigure -f noninteractive tzdata && \
    apt-get upgrade -yq

entrypoint ["dotnet", "AspDotNet.dll"]
