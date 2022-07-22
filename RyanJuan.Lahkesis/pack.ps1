dotnet build --configuration Release
dotnet build --configuration Release-zh-Hant

$xml = [Xml] (Get-Content .\RyanJuan.Lahkesis.csproj)
$version = $xml.Project.PropertyGroup.Version
echo "Target package version: $version"
