echo off

rem ***********���[�U�[��񑗐M�N���_�~�[


rem �N���C�A���g�t�@�C��
set client_file=C:\VIVA-SA\DATA\SA-user.DAT
rem �T�[�o�[�t�@�C��
set server_file=%2

rem �T�[�o�[�t�@�C���̑��݃`�F�b�N
if not exist %server_file% goto error

rem �t�@�C���R�s�[����
copy %client_file% %server_file% 

rem �R�s�[�G���[����
if errorlevel 1 goto error

:normal
rem ����I��
exit %retcd%

:error
rem �ُ�I��
exit 9


