echo off

rem ***********HT����M�v���O����(CL to HT)�N���_�~�[


rem �N���C�A���g�t�@�C��
set client_file=C:\VIVA-SA\DATA\SA-CLTOHT.DAT
rem �T�[�o�[�t�@�C��
set server_file=%1

rem �T�[�o�[�t�@�C���̑��݃`�F�b�N
if not exist %server_file% goto error

rem �t�@�C���R�s�[����
copy %server_file% %client_file%

rem �R�s�[�G���[����
if errorlevel 1 goto error

C:\VIVA-SA\HT_Com.exe 2 %server_file%

rem �߂�l�̑ޔ�
set retcd=%errorlevel% 

if %errorlevel% == 0 goto normal
if %errorlevel% == 5 goto normal

:error
rem �ُ�I��
exit 9


:normal
rem ����I��
exit %retcd%
