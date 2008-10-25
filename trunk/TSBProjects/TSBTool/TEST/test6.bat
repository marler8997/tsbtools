:: Tests the TSBTool program
::
::
::@echo off

copy ..\bin\Debug\*.exe* .
::copy ..\bin\Debug\*.exe.config .
copy ..\tsb32team.nes .
.\TSBToolSupreme tsb32team.nes Test6.txt -out:output.nes
.\TSBToolSupreme -j -n -f -a -s -sch -pb -of output.nes > OUTPUT.txt
"C:\Program Files\ExamDiff\ExamDiff.exe" .\Test6.txt .\OUTPUT.txt
::FC /C /L Test1.txt OUTPUT.txt > RESULTS.txt
::del *.nes

::start notepad  RESULTS.txt


