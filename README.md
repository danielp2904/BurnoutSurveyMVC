# BurnoutSurveyMVC

ASP.NET Core MVC (NET 8) + Entity Framework Core (SQL Server) — Pesquisa de Burnout (MBI) e avaliação SUS.

## Como rodar

1. **Pré-requisitos**
   - .NET SDK 8+
   - SQL Server local (LocalDB no Windows funciona)  

2. **Restaurar e rodar**
   ```bash
   dotnet restore
   dotnet tool install --global dotnet-ef --version 8.0.7
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   dotnet run
   ```

   > Se você não quiser gerar migrations agora, o app tentará `EnsureCreated()` caso `Migrate()` falhe.

3. **Fluxo**
   - `/` Home → `/Consent` TCLE + dados → `/MBI` → `/SUS` → `/Resultado/Resumo`

## Observações

- As perguntas do MBI e textos de explicação foram incluídas no *seed* inicial.
- As 10 questões do SUS estão implementadas e o cálculo do escore segue a fórmula padrão.
- As respostas são anônimas; o TCLE deve ser revisado e atualizado pelo pesquisador antes da coleta.
