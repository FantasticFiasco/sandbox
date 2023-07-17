# Native ahead-of-time

Publishing your app as [native AOT](https://learn.microsoft.com/dotnet/core/deploying/native-aot/) produces an app that's self-contained and that has been ahead-of-time (AOT) compiled to native code. Native AOT apps have faster startup time and smaller memory footprints. These apps can run on machines that don't have the .NET runtime installed.

## Publish

```sh
dotnet publish --configuration Release
```
