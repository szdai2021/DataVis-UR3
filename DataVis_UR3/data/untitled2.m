for i = 1:6

    subplot(6,1,1+(i-1));
    plot(str2double(files{i}.Var1).*files{i}.Var2.*str2double(files{i}.Var3));
end