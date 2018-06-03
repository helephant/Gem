
$packageVersion =  ("1.1.0."  + [int](new-timespan -start 01-06-2018 -end (get-date)).TotalMinutes)
#$packageVersion =  "1.1.0.10" 

dotnet pack  ../src/gem/gem.csproj -o Packages --include-symbols --include-source --configuration release /p:Version=$packageVersion