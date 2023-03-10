# sample-nunit
___
Your guide to run the first C# + NUnit test with reporting to Zebrunner.
___
## Prerequisites
Before you start performing NUnit automation testing with Zebrunner, you would need to be installed:
- Mono 6.10.0 or higher - you can download [here](https://www.mono-project.com/download/stable/)
___
## Configuration
### _Step 1: Basic project setup_
Do the following steps:
- clone Zebrunner’s [NUnit demo project](https://github.com/zebrunner/nunit-agent-samples.git)
- install necessary dependencies
- build the project

as shown below:

```
git clone https://github.com/zebrunner/nunit-agent-samples.git &&
cd nunit-sample-demo &&
nuget restore NunitAgentSample.sln &&
msbuild
```
> Please note that on different systems this set of commands may vary


### _Step 2: Setup your authentication_
In Zebrunner:
- Navigate to "Account and profile" section by clicking on the User icon from the top right side;
- Click on "API Tokens" tab;
- Press "Token" button, create a token and copy it before closing the dialog (you won't be able to see the token later).

### _Step 3: Select project for your launches_
In Zebrunner:
- Click on "Projects" dropdown from the top left side;
- Select "View all Projects";
- Find out a project where you would like to see your launches and copy its `KEY`.

> You need to be an Admin of the workspace to have the ability to create projects

### _Step 4: Configure your .env file_
The `zebrunner-env` file holds all the required configuration to enable reporting of your tests on Zebrunner.

- Open the file located inside root directory of cloned repository;
- Update the `NunitAgentSample/zebrunner-env` config file with:
    - `REPORTING_SERVER_HOSTNAME` with your Zebrunner workspace;
    - `REPORTING_SERVER_ACCESS_TOKEN` with `token` from step #2;
    - `REPORTING_PROJECT_KEY` with `KEY` from step #3 (if not defined, `DEF` will be used by default);

#### **`zebrunner-env`**
```
#!/bin/bash
export REPORTING_SERVER_HOSTNAME=_server_hostname_
# demo token below is auto-generated and will expire in 60 minutes
export REPORTING_SERVER_ACCESS_TOKEN=_token_
export REPORTING_ENABLED=true
export REPORTING_PROJECT_KEY=_key_
export REPORTING_RUN_BUILD=0.0.0.1-SNAPSHOT
export REPORTING_RUN_ENVIRONMENT=DEMO_NUnit

alias nunit3-console="mono packages/NUnit.ConsoleRunner.3.16.3/tools/nunit3-console.exe"
PS1="[Zebrunner-env] $ "
```

### _Step 5: Initiate source_
Run the following command in the terminal to initiate source
```
source zebrunner-env 
```

### _Step 5: Run sample test_

Run a sample test with Zebrunner reporting:
```
nunit3-console bin/Debug/NunitAgentSample.dll --where name=SimpleTest
```
___
For more information about framework refer to NUnit [documentation](https://docs.nunit.org/).<br>
For more information about reporting refer to Zebrunner [documentation](https://zebrunner.com/documentation/reporting/nunit/).
___