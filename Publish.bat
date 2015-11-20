cd "C:\Users\baranov\Desktop\WebApp_GitRepo"
"C:\Program Files (x86)\Git\bin\sh.exe" --login -i RepoCheckout.sh > gitlog.txt
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" build.xml /p:Configuration=Release;SolutionName=Reports;ProjectName=WebMvc 
xcopy /e /s /y C:\inetpub\wwwroot\DocAppNew "C:\Users\baranov\Documents\Visual Studio 2010\Projects\test_backup\"
set now=%DATE: =0% %TIME: =0%
@echo %now% >"C:\Users\baranov\Documents\Visual Studio 2010\Projects\test\Content\lastpublish.txt"
cd "C:\Users\baranov\Documents\Visual Studio 2010\Projects\test\"
for /d %%B in (*) do ( xcopy /e /s /y %%B "C:\inetpub\wwwroot\DocAppNew\%%B\" )
cd "C:\Users\baranov\Desktop\WebApp_GitRepo"

