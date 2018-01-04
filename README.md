# EmailAzureFunction
Super simple Azure Function to make sending emails super simple (using SendGrid).

## Calling the Function

To send an email, post the following json to your function's end point:

```
{
	"fromAddress": "sender@email.com",
	"fromName": "Sender Name",
	"subject": "Test Email",
	"body": "This is a test email",
	"isHtml": false,
	"toAddresses": ["reciver@email.com"]
}
```



## SendGrid API Key

The API Key is retrieved from the Function's Application settings. The variable is named "ApiKey". A free/trial key key can (as of 4th Jan 2018) can be created within the Azure Portal.

