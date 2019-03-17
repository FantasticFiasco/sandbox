# CodePipeline notifications

## Introduction

The CloudFormation template is creating a CodePipeline capable of reporting when it is stared, succeeds or fails via e-mail.

## Provisioning

Create the stack by executing the following command.

```bash
aws cloudformation create-stack --stack-name CodePipeline-Notifications --template-body file://codepipeline-notifications.yaml --parameter ParameterKey=EmailAddress,ParameterValue=<email address> --capabilities CAPABILITY_IAM
```
