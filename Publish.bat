"C:\Program Files (x86)\Git\bin\sh.exe" --login -i RepoCheckout.sh > gitlog.txt

"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" build.xml /p:Configuration=Release;SolutionName=Reports;ProjectName=WebMvc > buildlog.txt

for /d %B in ("C:\Users\baranov\Documents\Visual Studio 2010\Projects\test\*") do ( xcopy /e /s %B C:\inetpub\wwwroot\DocAppNew ) > copylog.txt
