@echo off
echo Refreshing WorkshopsDAL for main project WMS...
cd D:\Workshops\Version2\CGARMAN\RepositoryPatternWithUOW.Core
dotnet ef dbcontext scaffold "Host=localhost;Database=wms;Username=wms_user;Password=wms_user" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -c WMSContext -f
echo Done
