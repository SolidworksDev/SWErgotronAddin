@ECHO OFF
REM Written by Filipe Venceslau for Solidworks Forums
REM Detects system type (64/32) and Framework version (v2.0 or v4.0)

REM Change This Line To Match The Location of Your Assembly
SET AssemblyPath="C:\Program Files (x86)\Ergotron SolidWorks Addin\SWErgotronAddin.dll"

IF EXIST "%ProgramFiles(x86)%" (set FMWK=Framework64) ELSE (set FMWK=Framework)

IF EXIST "%Windir%\Microsoft.NET\%FMWK%\v4.0.30319" (set FMWKVersion=v4.0.30319) ELSE (set FMWKVersion=v2.0.50727)

echo Detected %FMWK% %FMWKVersion%
echo Running command...

@ECHO ON
"%Windir%\Microsoft.NET\%FMWK%\%FMWKVersion%\regasm" /codebase %AssemblyPath%

@ECHO OFF

