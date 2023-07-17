# Trim self-contained apps

The [trim-self-contained deployment model](https://learn.microsoft.com/dotnet/core/deploying/trimming/trim-self-contained) is a specialized version of the self-contained deployment model that is optimized to reduce deployment size. Minimizing deployment size is a critical requirement for some client-side scenarios like Blazor applications. Depending on the complexity of the application, only a subset of the framework assemblies are referenced, and a subset of the code within each assembly is required to run the application. The unused parts of the libraries are unnecessary and can be trimmed from the packaged application.

We've also configured the project to produce a single executable using `PublishSingleFile`.

## Publish

```sh
dotnet publish --configuration Release
```
