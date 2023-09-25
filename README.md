# PassePartoutLoginServer
Parte BackEnd Applicativo di registrazione utente come test PassePartout
- Asp.Net Core 6.0
- Entity Framework (code first)
- Entity Framework Sql server

## Istruzioni per l'avvio
1. Eseguire il pull del progetto
2. Modificare la stringa all'interno di appsettings.json ``` "DefaultConnection" ``` (Va utilizzato SQL Server come database)
3. Eliminare la cartella migrations (in quanto non esclusa in .gitignore)
4. Aprire Tools/NuGet Package Manager/Package Manager Console e lanciare i comandi
  ``` add-migration <nome_migrazione> -o data_access/migrations ```
  e successivamente
  ``` update-database ```
