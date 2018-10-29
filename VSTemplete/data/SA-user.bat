echo off

rem ***********ユーザー情報送信起動ダミー


rem クライアントファイル
set client_file=C:\VIVA-SA\DATA\SA-user.DAT
rem サーバーファイル
set server_file=%2

rem サーバーファイルの存在チェック
if not exist %server_file% goto error

rem ファイルコピー処理
copy %client_file% %server_file% 

rem コピーエラー判定
if errorlevel 1 goto error

:normal
rem 正常終了
exit %retcd%

:error
rem 異常終了
exit 9


