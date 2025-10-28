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
		<Gekka_SignTool_EnableSign_Build   ></Gekka_SignTool_EnableSign_Build>
		<Gekka_SignTool_EnableSign_Publish >false</Gekka_SignTool_EnableSign_Publish>
		<Gekka_SignTool_EnableSign_Nupkg   >false</Gekka_SignTool_EnableSign_Nupkg>

		<Gekka_SignTool_ExePath         ></Gekka_SignTool_ExePath>
		<Gekka_SignTool_TimeStampServer ></Gekka_SignTool_TimeStampServer>
		<Gekka_SignTool_Algorithms      >SHA1;SHA256;SHA384;SHA512</Gekka_SignTool_Algorithms>

		<Gekka_SignTool_AutoSelect_Issuer  ></Gekka_SignTool_AutoSelect_Issuer>
		<Gekka_SignTool_AutoSelect_Subject ></Gekka_SignTool_AutoSelect_Subject>

        <!-- For use PFX and password -->
		<Gekka_SignTool_PFX      ></Gekka_SignTool_PFX>
		<Gekka_SignTool_Password ></Gekka_SignTool_Password>

        <!-- For use Azure Trust Signing -->
		<Gekka_SignTool_Dlib_Dll_Path      ></Gekka_SignTool_Dlib_Dll_Path>
		<Gekka_SignTool_Dlib_MetaJson_Path ></Gekka_SignTool_Dlib_MetaJson_Path>

        <!-- https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-sign?WT.mc_id=DT-MVP-5000708 -->
        <Gekka_SignTool_Nupkg_FingerPrint></Gekka_SignTool_Nupkg_FingerPrint>
        <Gekka_SignTool_Nupkg_SubjectName></Gekka_SignTool_Nupkg_SubjectName>
        <Gekka_SignTool_Nupkg_StoreName></Gekka_SignTool_Nupkg_StoreName>
        <Gekka_SignTool_Nupkg_StoreLocation></Gekka_SignTool_Nupkg_StoreLocation>
        <Gekka_SignTool_Nupkg_Pfx_Path></Gekka_SignTool_Nupkg_Pfx_Path>
        <Gekka_SignTool_Nupkg_Password></Gekka_SignTool_Nupkg_Password>
    </PropertyGroup>

    <ItemDefinitionGroup>
        <Gekka_SignTool_InputFiles Include="filepath1" />
        <Gekka_SignTool_InputFiles Include="filepath2" />
    </ItemDefinitionGroup>

    <Target Name="Your Target Name" BeforeTargets="Build" >
        <ItemDefinitionGroup>
            <Gekka_SignTool_InputFiles Include="filepath3" />
            <Gekka_SignTool_InputFiles Include="filepath4" />
        </ItemDefinitionGroup>
    </Target>
```

- SignTool_ExePath  
    Path to use signtool.exe in any location
    If not set, use signtool.exe which can be referenced at developer command prompt

- SignTool_TimeStampServer  
    Url for timestamps server
    If not set, targets uses digicert's server.
    
- Gekka_SignTool_Algorithms   
    List the algorithms to be used for signing (e.g., SHA1; SHA256; SHA384; SHA512).

    If multiple values are specified, the file will be signed with multiple signatures.

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

