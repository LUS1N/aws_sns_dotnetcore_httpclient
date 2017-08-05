# aws_sns_dotnetcore_httpclient
AWS SNS HTTP client seed as AWS Lambda .NET Core 1.0 Web Api 

Seed made with the purpose of avoiding writing boiler plate code that any SNS Topic subsriber will have to do.

To use it as an SNS HTTPS endpoint deploy it to lambda and register the URL as SNS topic subsriber.

The app will confirm the subscription by sending a HTTP `GET` request to the `SubscribeURL` extracted from the SNS `SubscriptionConfirmation` request.
