# ReadyToRun

.NET application startup time and latency can be improved by compiling your application assemblies as [ReadyToRun (R2R)]((https://learn.microsoft.com/dotnet/core/deploying/ready-to-run)) format. R2R is a form of ahead-of-time (AOT) compilation.

R2R binaries improve startup performance by reducing the amount of work the just-in-time (JIT) compiler needs to do as your application loads. The binaries contain similar native code compared to what the JIT would produce. However, R2R binaries are larger because they contain both intermediate language (IL) code, which is still needed for some scenarios, and the native version of the same code. R2R is only available when you publish an app that targets specific runtime environments (RID) such as Linux x64 or Windows x64.

## Publish

```sh
dotnet publish --configuration Release
```
