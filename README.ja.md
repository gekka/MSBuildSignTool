# MSBuildSignTool

## 概要
MSBuildでReleaseビルドを実行したら、その出力ファイルをsigntool.exeで署名します。

発行で生成される実行ファイルにも署名可能です。

## 機能

- SignTool.exeを自動的に検索
- PFXファイルとパスワードによる署名
- Azure Trust Signingでの署名
- Github Actionsでの署名
- SHA1とSHA256の多重署名 (多重署名可能なファイルの場合のみ)
- Build,Publish,Packに対応
- 複数の出力ファイルの場合の自動選択(exe+dll , exe+msi)

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

- Gekka_SignTool_ExePath  
    任意の場所にあるsigntool.exeを使用する場合のパス
    指定しない場合は開発者コマンドプロンプトで参照できるsigntool.exeを使用します

- Gekka_SignTool_TimeStampServer  
    タイムスタンプに使用するサーバーのurlを指定する
    指定しない場合はdigicertのサーバーを使用します
     
- Gekka_SignTool_Algorithms  
    署名に使用するアルゴリズムを列挙します(SHA1;SHA256;SHA384;SHA512)
 
    複数設定すると多重署名します

- Gekka_SignTool_InputFiles
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

- PE形式のファイル

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

