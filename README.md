# Inventory Hub Email Service

The following repository is a serverless microservice to process queue messages and send emails to users.

The email provider used is Azure Communication Services, so setup that env variable.

## Running locally

To run locally, either install azure functions SDK and dotnet 7 SDK and setup the environment variables in the `local.settings.json` or create a docker container and set the env variables there:

```bash
docker build -t inventory-hub-email-service .
docker run -it -e "SENDER_ADDRESS=no-reply@inventory-hub.space" -e "AzureCommunicationServicesConnectionString=<connection string>" inventory-hub-email-service
```

## Contracts

Invitation email, queue `invitation-messages`:

```json
{
  "to": "sample.email@example.com",
  "fullName": "John Doe",
  "token": "ASB12323123",
  "callbackUrl": "https://inventory-hub.space/sign-up"
}
```

The generated link will be `https://inventory-hub.space/sign-up?token=ASB12323123`

Note: Sanitize the values, they are interpolated in HTML.
