# Inventory Hub Email Service

The following repository is a serverless microservice to process queue messages and send emails to users.

The email provider used is Azure Communication Services, so setup that env variable.

## Running locally

To run locally, either install azure functions SDK and dotnet 7 SDK and setup the environment variables in the `local.settings.json` or create a docker container and set the env variables there:

```bash
docker build -t inventory-hub-email-service .
docker run -it -e "AzureCommunicationServicesConnectionString=<connection string>" inventory-hub-email-service
```

## Contracts

Invitation email, queue `invitation-emails`:

```json
{
  "to": "a@a.a",
  "token": "122244444",
  "callbackUrl": "https://nigger.com"
}
```
