echo on

rem クライアントファイル
set client_file=C:\VIVA-SA\DATA\SA-HTTOCL.DAT
rem サーバーファイル
set server_file=%1

rem ***********HT送受信プログラム起動ダミー

C:\VIVA-SA\HT_Com.exe 1 %client_file%

rem 戻り値の退避
set retcd=%errorlevel% 

if %errorlevel% == 0 goto datacopy
if %errorlevel% == 5 goto datacopy

rem 異常終了
exit 9

rem ファイルコピー処理
:datacopy

rem ***********クライアントからサーバーへデータをコピー
rem クライアント側ファイル存在チェック
if not exist %client_file% goto error

rem ファイルコピー
copy %client_file%  %server_file%

rem コピーエラー判定
if errorlevel 1 goto error

rem 正常終了
exit %retcd%

:error
rem 異常終了
exit 9
