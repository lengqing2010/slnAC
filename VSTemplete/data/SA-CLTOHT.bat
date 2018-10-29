echo off

rem ***********HT送受信プログラム(CL to HT)起動ダミー


rem クライアントファイル
set client_file=C:\VIVA-SA\DATA\SA-CLTOHT.DAT
rem サーバーファイル
set server_file=%1

rem サーバーファイルの存在チェック
if not exist %server_file% goto error

rem ファイルコピー処理
copy %server_file% %client_file%

rem コピーエラー判定
if errorlevel 1 goto error

C:\VIVA-SA\HT_Com.exe 2 %server_file%

rem 戻り値の退避
set retcd=%errorlevel% 

if %errorlevel% == 0 goto normal
if %errorlevel% == 5 goto normal

:error
rem 異常終了
exit 9


:normal
rem 正常終了
exit %retcd%
