:: calls all tests

for %%f in (test*.bat)  do call %%f

cd schedule
for %%f in (test*.bat) do call %%f
cd ..

call CleanTestOutput.bat
