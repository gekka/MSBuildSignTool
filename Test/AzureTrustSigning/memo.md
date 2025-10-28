## [信頼された署名を使用するように署名統合を設定する](https://learn.microsoft.com/ja-jp/azure/trusted-signing/how-to-signing-integrations?WT.mc_id=DT-MVP-5000708)

"Endpoint"
	https://portal.azure.com/#browse/Microsoft.CodeSigning%2Fcodesigningaccounts で一覧にある場所

"CodeSigningAccountName"
	Azure Trusted Signing のアカウント名
	ユーザーがAzureにサインインするアカウント名ではなく、Azure Trusted Signingに対してつけた名前
	https://portal.azure.com/#browse/Microsoft.CodeSigning%2Fcodesigningaccounts で一覧にある名前
"CertificateProfileName"
	AzureTrustedSigningのCergificateProfiesで作成したプロファイルの名前


環境変数も設定しておくこと
https://learn.microsoft.com/ja-jp/dotnet/api/azure.identity.environmentcredential?WT.mc_id=DT-MVP-5000708&view=azure-dotnet
	
- Set azure_tenant_id=(Trusted Signingを作成したテナントid)