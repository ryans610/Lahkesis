﻿$name = "RyanJuan.Lahkesis"

Remove-Item -LiteralPath ".\obj" -Force -Recurse
Remove-Item -LiteralPath ".\lib" -Force -Recurse

$csproj = ".\$name.csproj"
dotnet build $csproj -c Release
dotnet build $csproj -c Release-zh-Hant

$xml = [Xml] (Get-Content $csproj)
$version = $xml.Project.PropertyGroup.Version
echo "Target package version: $version"
nuget pack ".\$name.nuspec" -Version $version
