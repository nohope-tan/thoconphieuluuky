$files = Get-ChildItem -Path "Assets\Scripts" -Filter "*.cs" -Recurse
foreach ($file in $files) {
    Write-Host "Processing $($file.FullName)..."
    $content = Get-Content $file.FullName -Raw
    $pattern = '(@?"(?:\\.|[^"])*")|(@?''(?:\\.|[^''])*'')|(/\*[\s\S]*?\*/)|(//.*)'
    
    $newContent = [regex]::Replace($content, $pattern, {
        param($m)
        if ($m.Groups[3].Value -or $m.Groups[4].Value) {
            return ""
        } else {
            return $m.Value
        }
    })
    
    $newContent | Set-Content $file.FullName
}
