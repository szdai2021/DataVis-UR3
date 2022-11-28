clear
clc

files = {};

files{1} = readtable("Record No 01 - F.txt");
files{2} = readtable("Record No 02 - B.txt");
files{3} = readtable("Record No 03 - R.txt");
files{4} = readtable("Record No 04 - L.txt");
files{5} = readtable("Record No 05 - U.txt");
files{6} = readtable("Record No 06 - D.txt");

for i = 1:6
    files{i}.Var1 = erase(files{i}.Var1, '(');
    files{i}.Var3 = erase(files{i}.Var1, ')');

    subplot(6,3,1+3*(i-1));
    plot(str2double(files{i}.Var1));

    subplot(6,3,2+3*(i-1));
    plot(files{i}.Var2);

    subplot(6,3,3+3*(i-1));
    plot(str2double(files{i}.Var3));
end