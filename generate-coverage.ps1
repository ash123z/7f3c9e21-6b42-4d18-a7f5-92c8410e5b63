# Generate Code Coverage Report
# This script generates a code coverage report using Coverlet

param(
    [string]$Format = "opencover",
    [string]$OutputDir = "./coverage",
    [switch]$IncludeTests = $false
)

Write-Host "Generating code coverage report..." -ForegroundColor Green

# Create output directory if it doesn't exist
if (-not (Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
    Write-Host "Created output directory: $OutputDir" -ForegroundColor Cyan
}

# Build the exclude pattern
$exclude = "[LIS.Tests]*"
if ($IncludeTests) {
    $exclude = ""
}

# Run tests with coverage collection
Write-Host "Running tests with coverage collection..." -ForegroundColor Cyan
$testCommand = @(
    "test",
    "-c", "Release",
    "/p:CollectCoverage=true",
    "/p:CoverageFormat=$Format",
    "/p:CoverageOutputDir=$OutputDir",
    "/p:Exclude=`"$exclude`""
)

& dotnet @testCommand

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nCode coverage report generated successfully!" -ForegroundColor Green
    Write-Host "Output directory: $(Resolve-Path $OutputDir)" -ForegroundColor Cyan
    
    # List generated files
    $files = Get-ChildItem -Path $OutputDir -Filter "*.xml" -ErrorAction SilentlyContinue
    if ($files) {
        Write-Host "`nGenerated files:" -ForegroundColor Green
        $files | ForEach-Object { Write-Host "  - $($_.Name)" -ForegroundColor White }
    }
} else {
    Write-Host "`nError generating coverage report!" -ForegroundColor Red
    exit 1
}
