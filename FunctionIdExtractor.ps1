$filename = "R375-developer\amd64\nvapi64.lib"
$dumpbinAddress = "$Env:VS140COMNTOOLS..\..\VC\bin\dumpbin.exe"
$dumpbinParameter = "/DISASM $filename"
Start-Process $dumpbinAddress $dumpbinParameter -Wait -WindowStyle Hidden -RedirectStandardOutput "$filename.asm"
$content = Get-Content "$filename.asm"
$functionName = ""
foreach ($line in $content)
{
	if (!$line)
	{
		$functionName = ""
		continue;
	}
	if ($functionName -eq "" -and $line.EndsWith(":") -and ($line.StartsWith("NvAPI_") -or $line.StartsWith("?Module_InitializeHelper")))
	{
		$functionName = $line.TrimEnd(':')
		if ($functionName.StartsWith("?Module_InitializeHelper"))
		{
			$functionName = "NvAPI_Initialize"
		}
		continue;
	}
	$leadingPattern = "ecx,"
	if ($functionName -ne "" -and $line.Contains($leadingPattern) -and $line.EndsWith("h"))
	{
		$functionAddress = $line.Substring($line.IndexOf($leadingPattern) + $leadingPattern.Length).TrimEnd('h')
		$functionAddressNumberic = 0
		if ([int32]::TryParse($functionAddress, 
			[System.Globalization.NumberStyles]::HexNumber, 
			[System.Globalization.CultureInfo]::CurrentCulture, 
			[ref] $functionAddressNumberic))
		{
			$functionAddress = $functionAddressNumberic.ToString("X")
			Write-Host "$functionName = 0x$functionAddress,"
			$functionName = ""
			continue;
		}
	}
}
Remove-Item "$filename.asm"