FROM mcr.microsoft.com/dotnet/sdk:5.0

RUN git clone https://github.com/BushchikIvan/roulette

WORKDIR roulette/roulette

ENTRYPOINT ["dotnet", "run"]
