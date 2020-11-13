# Snipper

Snipper is a simple code snippet editor that can be used to save and share
code snippets amongst your team.

You can create categories to group the snippets together and have unlimited
snippets each with unlimited files.

## Running Locally

To run Snipper locally:

1) Create the stack with CloudFormation ("CFN"):
   ```
   aws cloudformation create-stack --stack-name <my-stack-name> --template-body file://snipper-stack.yaml
   ```
2) With the outputs of the CFN stack, create a `appsettings.Development.json` file
   ```
   {
    "DynamoDb": {
      "SnippetTableName": "<table-name>",
      "CategoryTableName": "<table-name>"
    }
   }

   ```
3) Run via `dotnet run` - this will run the APIs and the frontend
4) Go to `https://localhost:5001`

## Tech Stack

Snipper is built on:

- ASP.NET Core API
- Vue.js Front end
- AWS Dynamo DB tables


## TODO

- Authentication - need some sort of basic authentication
- Snippet search
