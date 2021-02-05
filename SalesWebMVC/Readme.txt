Adicionando Banco de Dados
--------------------------
- Quando adicionamos um item com Scaffolding era gera um código compativel sql server se for usar outro banco tem que fazer adaptação
  1- Atualizar string de conexão na appsettings.json
  2- Atualiza Startup.cs adicionando a dependencia Mysql no serviço

Migration
---------

- É um script para gerar e versionar a base de dados
- Tipos de WorkFlow 
  - Cod-Fist
    Escreve as classes e apartir delas gera o banco de dados 
  - Database first
    Quando cria o banco de dados e apartir do banco que gera as classes
- Para criar Migration no projeto:
  1- Abre o console gerenciador do NuGet e executa os comando:
     - Add-Migration Nome da Migration
     - Update-Database para criar a tabela no banco configurado
