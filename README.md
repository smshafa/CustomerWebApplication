# CustomerWebApplication
Tadkar's project

Database is created by EFCodeFirst(EntityFramework).


*****************************
Error:
if you face some problems with refrences, please do the below operations:
1- Delete all of ...\CustomerWebApplication\packages folders
2- In nuGet manager setting Clear nuGet Chaches.
3- In solution right click and press Clean solution.
4- In solution right click and press Restore NuGet packeages.
5- In solution right click and press Build Solution.

*********************************
Error:
if you face the below error, please follow the instruction:
Could not find a part of the path 'F:\Projects\Repository\CustomerWebApplication\CustomerWebApplication\bin\roslyn\csc.exe'.

In my case the solution was to re-install/upgrade Nuget packages:

    Microsoft.Net.Compilers 1.1.1
    Microsoft.CodeDom.Providers.DotNetCompilerPlatform 1.0.1

Then I looked into .csproj and made sure that the paths to packages are correct (in my case ..\..\packages\*.*) inside tags <ImportProject> on top and in <Target> with name "EnsureNuGetPackageBuildImports" on the bottom. This is on MVC 5 and .NET Framework 4.5.2.

Short answer - run this in the Package Manager Console:

++++=======:)>   Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r


**************************
Apply changes downloaded through fetch using the merge command. Merge takes the commits retrieved from fetch and tries to add them to your local branch. The merge will keep the commit history of your local changes so that when you share your branch with push Git will know how others should merge your changes.

    fetch: bring in changes without merging them
    pull: bring in changes and do merge them
    push: send out your changes.
