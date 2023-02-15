# Projeto Tryitter

### Sobre

 - Aplicação desenvolvida para ser uma rede social, baseada em texto
 - Utilizamos uma API REST para criação dos endpoints

### Hora de rodar a aplicação

 - Para iniciar utilizar o `docker-compose up` rodando o serviço do `db`
 - Utilizar o comando `dotnet restore` para instalar os pacotes da aplicação
 - `dotnet ef database update` irá rodar as migrations para criação da tabela no banco
 - `dotnet run` para iniciar a aplicação
 - Acessando o endpoint **colocar o endpoint do swagger** iremos ter acesso a documentação da aplicação no swagger

### Testes

 - Rodar os testes utilizar o comando `dotnet test`
 - Verificar a cobertura de testes utilizar o comando `dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage.opencover.xml`
