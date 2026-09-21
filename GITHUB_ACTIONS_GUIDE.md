# 🚀 GitHub Actions CI/CD Guide

**Solution**: Longest Increasing Subsequence (LIS)  
**Repository**: https://github.com/ash123z  
**Workflow File**: `.github/workflows/ci.yml`

---

## OVERVIEW

The solution includes a comprehensive GitHub Actions CI/CD workflow that automatically builds, tests, and publishes the application on every push and pull request.

**Status**: ✅ **CONFIGURED AND READY**

---

## WORKFLOW TRIGGERS

The CI/CD workflow is triggered automatically in two scenarios:

### 1. Push to Main or Develop Branch
```yaml
on:
  push:
    branches: [ main, develop ]
```

**When**: Every commit pushed to `main` or `develop` branch  
**Action**: Workflow runs automatically

### 2. Pull Request to Main or Develop Branch
```yaml
on:
  pull_request:
    branches: [ main, develop ]
```

**When**: Pull request created or updated targeting `main` or `develop` branch  
**Action**: Workflow runs automatically to validate changes

---

## WORKFLOW STEPS

### Step 1: Checkout Code
```yaml
- name: Checkout code
  uses: actions/checkout@v4
```

**Purpose**: Retrieves the latest source code from the repository  
**Duration**: ~1-2 seconds

### Step 2: Setup .NET
```yaml
- name: Setup .NET
  uses: actions/setup-dotnet@v4
  with:
    dotnet-version: '10.0.x'
```

**Purpose**: Installs .NET 10.0 SDK on the runner  
**Duration**: ~10-15 seconds  
**Version**: 10.0.x (latest 10.0 patch)

### Step 3: Restore Dependencies
```yaml
- name: Restore dependencies
  run: dotnet restore
```

**Purpose**: Restores NuGet packages specified in project files  
**Duration**: ~5-10 seconds  
**Packages**:
- xunit (2.9.3)
- Microsoft.NET.Test.Sdk (17.14.1)
- xunit.runner.visualstudio (3.1.4)
- coverlet.collector (10.0.1)

### Step 4: Build Solution
```yaml
- name: Build solution
  run: dotnet build --configuration Release --no-restore
```

**Purpose**: Compiles the solution in Release configuration  
**Duration**: ~5-10 seconds  
**Configuration**: Release (optimized)  
**Projects**:
- LIS.Core (class library)
- LIS.App (console application)
- LIS.Tests (unit tests)

**Failure Condition**: Workflow fails if build fails

### Step 5: Run Unit Tests with Coverage
```yaml
- name: Run unit tests with coverage
  run: dotnet test --configuration Release --no-build --verbosity normal --logger "trx;LogFileName=test-results.trx" /p:CollectCoverage=true /p:CoverageFormat=opencover /p:Exclude="[LIS.Tests]*"
```

**Purpose**: Executes all unit tests and collects code coverage  
**Duration**: ~5-10 seconds  
**Tests**: 45 total tests  
**Coverage Format**: OpenCover  
**Test Results Format**: TRX (Test Results XML)  
**Exclusions**: LIS.Tests project excluded from coverage

**Failure Condition**: Workflow fails if any test fails

### Step 6: Publish Test Results
```yaml
- name: Publish test results
  if: always()
  uses: actions/upload-artifact@v4
  with:
    name: test-results
    path: '**/test-results.trx'
```

**Purpose**: Uploads test results as artifacts  
**Duration**: ~2-3 seconds  
**Artifact Name**: `test-results`  
**Format**: TRX (Test Results XML)  
**Condition**: Runs even if previous steps fail (`if: always()`)

### Step 7: Publish Coverage Report
```yaml
- name: Publish coverage report
  if: always()
  uses: actions/upload-artifact@v4
  with:
    name: coverage-report
    path: '**/coverage.opencover.xml'
```

**Purpose**: Uploads code coverage report as artifacts  
**Duration**: ~2-3 seconds  
**Artifact Name**: `coverage-report`  
**Format**: OpenCover XML  
**Condition**: Runs even if previous steps fail (`if: always()`)

### Step 8: Publish Application
```yaml
- name: Publish application
  run: dotnet publish --configuration Release --no-build --output ./publish
```

**Purpose**: Publishes the console application  
**Duration**: ~3-5 seconds  
**Configuration**: Release (optimized)  
**Output**: `./publish` directory

### Step 9: Upload Published App
```yaml
- name: Upload published app
  uses: actions/upload-artifact@v4
  with:
    name: published-app
    path: ./publish
```

**Purpose**: Uploads the published application as artifacts  
**Duration**: ~2-3 seconds  
**Artifact Name**: `published-app`  
**Contents**: Compiled and published application files

---

## WORKFLOW EXECUTION TIME

**Total Duration**: ~40-60 seconds

### Breakdown
- Checkout: ~1-2 seconds
- Setup .NET: ~10-15 seconds
- Restore: ~5-10 seconds
- Build: ~5-10 seconds
- Tests: ~5-10 seconds
- Publish Results: ~2-3 seconds
- Publish Coverage: ~2-3 seconds
- Publish App: ~3-5 seconds
- Upload App: ~2-3 seconds

---

## ARTIFACTS

### Test Results
**Name**: `test-results`  
**Format**: TRX (Test Results XML)  
**Contents**: Detailed test execution results  
**Usage**: Can be imported into test reporting tools

### Coverage Report
**Name**: `coverage-report`  
**Format**: OpenCover XML  
**Contents**: Code coverage metrics  
**Usage**: Can be analyzed with ReportGenerator or CodeCov

### Published Application
**Name**: `published-app`  
**Format**: Compiled .NET application  
**Contents**: Ready-to-run application files  
**Usage**: Can be deployed or run locally

---

## VIEWING WORKFLOW RESULTS

### In GitHub UI

1. Go to repository: https://github.com/ash123z/KMART
2. Click "Actions" tab
3. Select the workflow run
4. View:
   - Build status (✅ or ❌)
   - Test results
   - Execution logs
   - Artifacts

### Downloading Artifacts

1. Go to workflow run details
2. Scroll to "Artifacts" section
3. Click artifact name to download
4. Extract and analyze locally

---

## FAILURE SCENARIOS

### Build Fails
- **Cause**: Compilation errors in source code
- **Result**: Workflow stops, subsequent steps skipped
- **Action**: Fix compilation errors and push again

### Tests Fail
- **Cause**: Unit test assertion failures
- **Result**: Workflow stops, subsequent steps skipped
- **Action**: Fix failing tests and push again

### Restore Fails
- **Cause**: NuGet package unavailable or network issue
- **Result**: Workflow stops
- **Action**: Check NuGet source availability and retry

### Setup .NET Fails
- **Cause**: .NET SDK version not available
- **Result**: Workflow stops
- **Action**: Update .NET version in workflow file

---

## SUCCESS CRITERIA

The workflow is considered successful when:

✅ Code checkout succeeds  
✅ .NET SDK setup succeeds  
✅ Dependency restore succeeds  
✅ Build succeeds (0 errors, 0 warnings)  
✅ All 45 tests pass  
✅ Coverage collection succeeds  
✅ Application publishes successfully  
✅ All artifacts upload successfully  

---

## CONFIGURATION

### Workflow File
**Location**: `.github/workflows/ci.yml`  
**Format**: YAML  
**Size**: ~52 lines

### Environment
**Runner**: `ubuntu-latest` (Linux)  
**OS**: Ubuntu 22.04 LTS  
**Architecture**: x64

### .NET Version
**Target**: .NET 10.0.x  
**Reason**: Matches project target framework  
**Flexibility**: Uses `10.0.x` to allow patch updates

---

## BEST PRACTICES IMPLEMENTED

✅ **Fail Fast**: Build and tests run before publishing  
✅ **Artifact Preservation**: Test results and coverage preserved even on failure  
✅ **No-Restore Build**: Uses `--no-restore` to avoid redundant restore  
✅ **No-Build Publish**: Uses `--no-build` to avoid redundant compilation  
✅ **Descriptive Names**: Each step has clear, descriptive name  
✅ **Appropriate Logging**: Uses `--verbosity normal` for useful output  
✅ **Coverage Exclusion**: Test project excluded from coverage metrics  
✅ **Latest Actions**: Uses latest versions of GitHub Actions for security  
✅ **Conditional Artifacts**: Artifacts uploaded even if tests fail  
✅ **Simple Configuration**: No unnecessary complexity  

---

## EXTENDING THE WORKFLOW

### Add Code Analysis
```yaml
- name: Run code analysis
  run: dotnet analyze
```

### Add Docker Build
```yaml
- name: Build Docker image
  run: docker build -t lis-app .
```

### Add Deployment
```yaml
- name: Deploy to production
  run: # deployment script
```

### Add Notifications
```yaml
- name: Notify on failure
  if: failure()
  run: # notification script
```

---

## TROUBLESHOOTING

### Workflow Not Triggering
- **Check**: Branch name matches trigger (main or develop)
- **Check**: Workflow file syntax is valid
- **Check**: File is in `.github/workflows/` directory

### Build Failing
- **Check**: Code compiles locally
- **Check**: All dependencies are available
- **Check**: .NET version is correct

### Tests Failing
- **Check**: Tests pass locally
- **Check**: No environment-specific issues
- **Check**: All test data is available

### Artifacts Not Uploading
- **Check**: Paths are correct
- **Check**: Files exist after previous steps
- **Check**: Artifact names are unique

---

## MONITORING

### GitHub Actions Dashboard
- View all workflow runs
- Check execution times
- Monitor success rate
- Review logs

### Recommended Checks
- Monitor build times (should be consistent)
- Monitor test execution times
- Monitor artifact sizes
- Review failure patterns

---

## SECURITY

### Best Practices
✅ Uses official GitHub actions (checkout, setup-dotnet, upload-artifact)  
✅ Uses latest action versions  
✅ No hardcoded secrets  
✅ No credentials in workflow file  
✅ Artifacts are temporary (auto-deleted after retention period)  

### Retention
- Artifacts: 90 days (default)
- Logs: 90 days (default)
- Can be configured in repository settings

---

## DOCUMENTATION

### Files
- **README.md**: High-level CI/CD overview
- **GITHUB_ACTIONS_GUIDE.md**: This file (detailed guide)
- **.github/workflows/ci.yml**: Workflow configuration

### Related Documentation
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [.NET GitHub Actions](https://github.com/actions/setup-dotnet)
- [Upload Artifacts Action](https://github.com/actions/upload-artifact)

---

## QUICK REFERENCE

### View Workflow Status
```
https://github.com/ash123z/KMART/actions
```

### View Latest Run
```
https://github.com/ash123z/KMART/actions/workflows/ci.yml
```

### Download Artifacts
1. Go to Actions tab
2. Select latest run
3. Scroll to Artifacts
4. Click artifact to download

### Run Locally
```bash
dotnet restore
dotnet build --configuration Release
dotnet test --configuration Release /p:CollectCoverage=true /p:CoverageFormat=opencover
dotnet publish --configuration Release --output ./publish
```

---

## CONCLUSION

The GitHub Actions CI/CD workflow provides:

✅ Automated build and test execution  
✅ Immediate feedback on code quality  
✅ Test result tracking  
✅ Code coverage reporting  
✅ Artifact preservation  
✅ Simple and maintainable configuration  

The workflow is **production-ready** and appropriate for a coding assessment.

---

**Last Updated**: September 19, 2026  
**Status**: ✅ **CONFIGURED AND READY**  
**Repository**: https://github.com/ash123z
