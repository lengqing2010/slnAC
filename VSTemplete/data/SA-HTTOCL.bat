echo on

rem �N���C�A���g�t�@�C��
set client_file=C:\VIVA-SA\DATA\SA-HTTOCL.DAT
rem �T�[�o�[�t�@�C��
set server_file=%1

rem ***********HT����M�v���O�����N���_�~�[

C:\VIVA-SA\HT_Com.exe 1 %client_file%

rem �߂�l�̑ޔ�
set retcd=%errorlevel% 

if %errorlevel% == 0 goto datacopy
if %errorlevel% == 5 goto datacopy

rem �ُ�I��
exit 9

rem �t�@�C���R�s�[����
:datacopy

rem ***********�N���C�A���g����T�[�o�[�փf�[�^���R�s�[
rem �N���C�A���g���t�@�C�����݃`�F�b�N
if not exist %client_file% goto error

rem �t�@�C���R�s�[
copy %client_file%  %server_file%

rem �R�s�[�G���[����
if errorlevel 1 goto error

rem ����I��
exit %retcd%

:error
rem �ُ�I��
exit 9
