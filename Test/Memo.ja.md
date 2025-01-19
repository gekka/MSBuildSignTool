# Test

署名のテストを行うプロジェクト

証明書ストアからの自動選択はハードウェアトークン(USBドングル)にある証明書で試しています。

ほかの方法で証明書を渡す場合はプロジェクトファイルを書き換えるか、ビルドプロパティを外部設定する必要があります。
各プロジェクトに設定するかDirectoty.Build.targetsに設定してください。


## プロジェクト
- AzureTrustSigning  
	Azure Trust Sigining(信頼された署名)で署名する

	Azure Trust Siginingで設定をして、az_sign.metadata.jsonファイルに設定値を記入する必要があります

- CppProject  
	C++のプロジェクトの出力ファイルへの署名をテストする  
    
	証明書は証明書ストアから自動選択
 
- CSConsoleCore  
	C#で複数のランタイムバージョンでのビルド出力ファイルへの署名をテストする  
	
	プロジェクトファイルに設定してあるpfxファイルとパスワードを使用して署名をテストする

- FileTypeTest  
	signtool.exeが対応しているファイルの種類をテストする

	証明書は証明書ストアから自動選択

- SignOterProjectOutput  
	プロジェクト外にあるファイルへの署名をテストする
 
 	証明書は証明書ストアから自動選択

- Wix,WixBundle  
	Wixで作られるインストーラーへの署名をテストする

	証明書は証明書ストアから自動選択

- Setup1  
	VisualStudio Installer Projectで作られるインストーラーへの署名をテストする

	証明書は証明書ストアから自動選択

