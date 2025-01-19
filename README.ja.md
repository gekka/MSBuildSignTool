# MSBuildSignTool

## 概要
MSBuildでReleaseビルドを実行したら、その出力ファイルをsigntool.exeで署名します。

発行で生成される実行ファイルにも署名可能です。

## 機能

- SignTool.exeを自動的に検索
- - PFXファイルとパスワードによる署名
- Azure Trust Signingでの署名
- Github Actionsでの署名
- SHA1とSHA256の多重署名 (多重署名可能なファイルの場合のみ)
- Build,Publish,Packに対応

## 要求事項
Windows SDKなどでsigntool.exeがインストールされていること。  
開発者コマンドプロンプトからsigntoolを参照できること。  
インストールおよび参照がない場合に、signtool.exeファイルを別途用意する場合はSignTool_ExePathプロパティに設定すること。

証明書ストアを使用する場合はストアに証明書がインストールされていること。

Azure Trust Signingで署名する場合は プロジェクトに nugetで[Microsoft.Trusted.Signing.Client](https://www.nuget.org/packages/Microsoft.Trusted.Signing.Client/)をインストールしておくこと

## プロジェクトファイルにて指定可能な設定

プロジェクトファイル内に追記すると設定変更できます

```xml
<Project >
    <PropertyGroup>
        <SignTool_TimeStampServer></SignTool_TimeStampServer>
        <SignTool_Algorithm_SHA1>false</SignTool_Algorithm_SHA1>
        <SignTool_Algorithm_SHA256>true</SignTool_Algorithm_SHA256>
        <SignTool_ExePath>path to signtool.exe</SignTool_ExePath>

        <!-- For use PFX and passwork -->
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
    任意の場所にあるsigntool.exeを使用する場合のパス
    指定しない場合は開発者コマンドプロンプトで参照できるsigntool.exeを使用します

- SignTool_TimeStampServer  
    タイムスタンプに使用するサーバーのurlを指定する
    指定しない場合はdigicertのサーバーを使用します
     
- SignTool_Algorithm_SHA1  
    trueに設定するとSHA1で署名する
 
- SignTool_Algorithm_SHA256  
    trueに設定するとSHA256で署名する
    
    SHA1とSHA256を同時に有効にすると多重署名します

- SignTool_InputFiles
    追加で署名したいファイルがある場合はIncldueでパスを指定する。

    プロジェクトファイルが評価される前にファイルが存在しない場合は、ビルド前に実行されるTarget内で指定する必要があります



## 署名可能なファイルの種類

### 多重署名可能 

- .exe 
- .dll
- .ocx
- .scr
- .sys
- .cab
- .cat
- .efi

### SHA1もしくはSHA256のどちらか一方のみ

- .msi
- .ps1
- .vbs
- .vbe
- .js (JScript)

### SHA256のみ
- .msix
- .msixbundle
- .appx
- .appxbundle

