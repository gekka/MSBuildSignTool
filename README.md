# MSBuildSignTool

## Summary
Insert msbuild task to project for add sign to output files at Release build using signtool.exe.

This targets can sign to output file and publish file.

# Feature

- Search signtool.exe automticaly
- Sign using pfx file and password.
- Sign using Azure Trust Signing.
- Sign in Github Actions.
- Support Dual sign (SHA1 + SHA256).
- Auto select multiple output files (exe+dll , exe+msi)

## Requirement
The signtool.exe must be installed, e.g., with the Windows SDK.  
It must be possible to reference signtool from the developer command prompt.  

If sign using Azure Trust Signing , need install [Microsoft.Trusted.Signing.Client](https://www.nuget.org/packages/Microsoft.Trusted.Signing.Client/)  

## Optional settings in project file 

You can change the settings by adding them in the project file.

```xml
<Project >
    <PropertyGroup>
        <SingTool_EnableSign_Build   >true</SingTool_EnableSign_Build>
        <SingTool_EnableSign_Publish >true</SingTool_EnableSign_Publish>
        <SingTool_EnableSign_Nupkg   >true</SingTool_EnableSign_Nupkg>

        <SignTool_ExePath></SignTool_ExePath>

        <SignTool_TimeStampServer></SignTool_TimeStampServer>
        <SignTool_Algorithm_SHA1>false</SignTool_Algorithm_SHA1>
        <SignTool_Algorithm_SHA256>true</SignTool_Algorithm_SHA256>
        
        <SignTool_AutoSelect_Subject ></SignTool_AutoSelect_Subject>
        <SignTool_AutoSelect_Issuer  ></SignTool_AutoSelect_Issuer>

        <!-- For use PFX and password -->
        <SignTool_PFX      ></SignTool_PFX>
        <SignTool_Password ></SignTool_Password>

        <!-- For use Azure Trust Signing -->
        <SignTool_Dlib_Dll_Path ></SignTool_Dlib_Dll_Path>
        <SignTool_Dlib_MetaJson_Path Condition></SignTool_Dlib_MetaJson_Path Condition>

        <!-- https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-sign?WT.mc_id=DT-MVP-5000708 -->
        <SignTool_Nupkg_FingerPrint></SignTool_Nupkg_FingerPrint>
        <SignTool_Nupkg_SubjectName></SignTool_Nupkg_SubjectName>
        <SignTool_Nupkg_StoreName></SignTool_Nupkg_StoreName>
        <SignTool_Nupkg_StoreLocation></SignTool_Nupkg_StoreLocation>
        <SignTool_Nupkg_Pfx_Path></SignTool_Nupkg_Pfx_Path>
        <SignTool_Nupkg_Password></SignTool_Nupkg_Password>
    </PropertyGroup>

    <ItemDefinitionGroup>
        <SignTool_InputFiles Include="filepath1" />
        <SignTool_InputFiles Include="filepath2" />
    </ItemDefinitionGroup>

    <Target Name="Your Target Name" BeforeTargets="Build" >
        <ItemDefinitionGroup>
            <SignTool_InputFiles Include="filepath3" />
            <SignTool_InputFiles Include="filepath4" />
        </ItemDefinitionGroup>
    </Target>
```

- SignTool_ExePath  
    Path to use signtool.exe in any location
    If not set, use signtool.exe which can be referenced at developer command prompt

- SignTool_TimeStampServer  
    Url for timestamps server
    If not set, targets uses digicert's server.
    
- SignTool_Algorithm_SHA1  
    If set to true, sign with SHA1
 
- SignTool_Algorithm_SHA256  
    If set to true, sign with SHA256
    
    This target sign multiple signatures to one file if SHA1 and SHA256 are enabled.

- SignTool_InputFiles  
    If there are additional files to be signed, specify the path in Incldue.

    If the files does not exist before the project file is evaluated, it must be specified in custom target executed before the build


## Supporting file types

### Multi signable

- .exe 
- .dll
- .ocx
- .scr
- .sys
- .cab
- .cat
- .efi

- PE files

### Only one of SHA1 or SHA256

- .msi
- .ps1
- .vbs
- .vbe
- .js (JScript)

### Only SHA256
- .msix
- .msixbundle
- .appx
- .appxbundle

