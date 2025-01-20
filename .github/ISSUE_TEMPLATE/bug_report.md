---
name: Bug report
about: Create a Bug report
title: ''
labels: '[bug]'
assignees: ''
---
English or Japanese

**Describe the bug**  
A clear and concise description of what the bug is.

**Project**  
- Project file:
```xml
<Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup Condition="?">
		<SignTool_?> ? </SignTool_?>
	</PropertyGroup>

    <ItemDefinitionGroup Condition="?">
        <SignTool_InputFiles Include="?" />
    </ItemDefinitionGroup>
```

- Target: [e.g. exe, msi]

**Expected behavior**  
A clear and concise description of what you expected to happen.

**Error Messages**  
Need detail (msbuild -v:d)

**Desktop:**  
 - OS: [e.g. windows 11 Enterprise]
 - Version: [e.g. 23H2 22631.3737]
 - Language: [e.g. Japanese]

**signtool.exe:**  
 - Version:  
	Type `(Get-Item (&where.exe signtool.exe)).VersionInfo | Format-List` in developer powershell.

**Additional context**  
Add any other context about the problem here.
