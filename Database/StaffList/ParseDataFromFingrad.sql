--select * from Fingrag_csv 



UPDATE Fingrag_csv SET [����_���������] = case when year([����_���������]) = 1899 then null else [����_���������] end
											,[���_���������] = case when len([���_���������]) = 0 or [���_���������] = N'-' then null else [���_���������] end
											,[������_������������] = case when len([������_������������]) = 0 or [������_������������] = N'-' then null else [������_������������] end
											,[�����������_������������] = case when len([�����������_������������]) = 0 or [�����������_������������] = N'-' then null else [�����������_������������] end
											,[������] = case when len([������]) = 0 or [������] = N'-' then null else REPLACE(REPLACE([������], N',', N'.'), CHAR(32), '') end
											,[�������_���������] = case when len([�������_���������]) = 0 or [�������_���������] = N'-' then null else [�������_���������] end
											,[����������_�����] = case when len([����������_�����]) = 0 or [����������_�����] = N'-' then null else [����������_�����] end
											,[�����_���] = case when len([�����_���]) = 0 or [�����_���] = N'-' then null else [�����_���] end
											,[������_�������������] = case when len([������_�������������]) = 0 or [������_�������������] = N'-' then null else [������_�������������] end
											,[����_��������_�����] = case when year([����_��������_�����]) = 1899 then null else [����_��������_�����] end
											,[����_ ��������_�����] = case when year([����_ ��������_�����]) = 1899 then null else [����_ ��������_�����] end
											,[������������_���������] = case when len([������������_���������]) = 0 or [������������_���������] = N'-' then null else [������������_���������] end
											,[�������_�������������] = case when len([�������_�������������]) = 0 then '0' else REPLACE([�������_�������������], N',', N'.') end	--�������� ����
											,[���������_��������] = case when len([���������_��������]) = 0 or [���������_��������] = N'-' then null else [���������_��������] end
											,[�����_������������_�������] = case when len([�����_������������_�������]) = 0 then '0' else REPLACE([�����_������������_�������], N',', N'.') end	--�������� ����
											,[���_��������] = case when len([���_��������]) = 0 or [���_��������] = N'-' then null else [���_��������] end
											,[���_���] = case when len([���_���]) = 0 or [���_���] = N'-' then null else [���_���] end
											,[���_��������] = case when len([���_��������]) = 0 or [���_��������] = N'-' then null else [���_��������] end
											,[���_��] = case when len([���_��]) = 0 or [���_��] = N'-' then null else [���_��] end
											,[���_1�] = case when len([���_1�]) = 0 or [���_1�] = N'-' then null else [���_1�] end
											,[���_�������������] = case when len([���_�������������]) = 0 or [���_�������������] = N'-' then null else [���_�������������] end
											,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
											,[������������_�_���_��] = case when len([������������_�_���_��]) = 0 or [������������_�_���_��] = N'-' then null else [������������_�_���_��] end
											,[��_��������] = case when len([��_��������]) = 0 or [��_��������] = N'-' then null else [��_��������] end
											,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
											,[�������_���_�������������] = case when len([�������_���_�������������]) = 0 or [�������_���_�������������] = N'-' then null else [�������_���_�������������] end
											,[Front_Back1] = case when len([Front_Back1]) = 0 or [Front_Back1] = N'-' then null else [Front_Back1] end
											,[�������������_��������_��������] = case when len([�������������_��������_��������]) = 0 or [�������������_��������_��������] = N'-' then null else [�������������_��������_��������] end
											,[������_������] = case when len([������_������]) = 0 or [������_������] = N'-' then null else [������_������] end
											,[�����_��_����_��_���������_�_�������_�����] = case when len([�����_��_����_��_���������_�_�������_�����]) = 0 or [�����_��_����_��_���������_�_�������_�����] = N'-' then null else [�����_��_����_��_���������_�_�������_�����] end
											,[������������_������] = case when len([������������_������]) = 0 or [������������_������] = N'-' then null else [������������_������] end
											,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
											,[�������] = case when len([�������]) = 0 or [�������] = N'-' then null else [�������] end
											,[�����_��������] = case when len([�����_��������]) = 0 or [�����_��������] = N'-' then null else [�����_��������] end
											,[�����_������_�����_�������_�_��] = case when len([�����_������_�����_�������_�_��]) = 0 or [�����_������_�����_�������_�_��] = N'-' then null else [�����_������_�����_�������_�_��] end
											,[�������������_�������������_������] = case when len([�������������_�������������_������]) = 0 or [�������������_�������������_������] = N'-' then null else [�������������_�������������_������] end
											,[�������������_�������������_���������] = case when len([�������������_�������������_���������]) = 0 or [�������������_�������������_���������] = N'-' then null else [�������������_�������������_���������] end
											,[����_�������_������_������] = case when year([����_�������_������_������]) = 1899 then null else [����_�������_������_������] end
											,[���_��_����������_����������_�_��������_�����] = case when len([���_��_����������_����������_�_��������_�����]) = 0 then '0' else REPLACE([���_��_����������_����������_�_��������_�����], N',', N'.') end
											,[��������] = case when len([��������]) = 0 or [��������] = N'-' then null else [��������] end
											,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
											,[�������_�����] = case when len([�������_�����]) = 0 or [�������_�����] = N'-' then null else [�������_�����] end
											,[���������_�������_�����] = case when len([���������_�������_�����]) = 0 or [���������_�������_�����] = N'-' then null else [���������_�������_�����] end
											,[����������] = case when len([����������]) = 0 or [����������] = N'-' then null else [����������] end
											,[���_���������] = case when len([���_���������]) = 0 or [���_���������] = N'-' then null else [���_���������] end
											,[����_�������_���������_������] = case when year([����_�������_���������_������]) = 1899 then null else [����_�������_���������_������] end
											,[ID_��������] = case when len([ID_��������]) = 0 or [ID_��������] = N'-' then null else [ID_��������] end
											,[���_������_�����] = case when len([���_������_�����]) = 0 or [���_������_�����] = N'-' then null else [���_������_�����] end
											,[����_������_�������_�����] = case when year([����_������_�������_�����]) = 1899 then null else [����_������_�������_�����] end
											,[����_�������������_������_�����] = case when year([����_�������������_������_�����]) = 1899 then null else [����_�������������_������_�����] end
											,[J_�����_���������] = case when len([J_�����_���������]) = 0 or [J_�����_���������] = N'-' then null else [J_�����_���������] end
											,[�������������_��_��������_�����] = case when len([�������������_��_��������_�����]) = 0 or [�������������_��_��������_�����] = N'-' then null else [�������������_��_��������_�����] end
											,[���_���] = case when len([���_���]) = 0 or [���_���] = N'-' then null else [���_���] end
											,[���_GE] = case when len([���_GE]) = 0 or [���_GE] = N'-' then null else [���_GE] end
											,[���������_���������_����������] = case when len([���������_���������_����������]) = 0 or [���������_���������_����������] = N'-' then null else [���������_���������_����������] end
											,[���������_��������_�������] = case when len([���������_��������_�������]) = 0 or [���������_��������_�������] = N'-' then null else [���������_��������_�������] end
											,[���������_��������_������] = case when len([���������_��������_������]) = 0 or [���������_��������_������] = N'-' then null else [���������_��������_������] end
											,[���������_�����_������] = case when len([���������_�����_������]) = 0 or [���������_�����_������] = N'-' then null else [���������_�����_������] end
											,[�������_��������_�_����������] = case when len([�������_��������_�_����������]) = 0 or [�������_��������_�_����������] = N'-' then null else [�������_��������_�_����������] end
											,[������������_��] = case when len([������������_��]) = 0 or [������������_��] = N'-' then null else [������������_��] end
											,[����������2] = case when len([����������2]) = 0 or [����������2] = N'-' then null else [����������2] end
											,[��������_��_��������] = case when len([��������_��_��������]) = 0 or [��������_��_��������] = N'-' then null else [��������_��_��������] end
											,[���_��_�_�������_��_��������] = case when len([���_��_�_�������_��_��������]) = 0 or [���_��_�_�������_��_��������] = N'-' then null else [���_��_�_�������_��_��������] end
											,[ID_������_������_������_������] = case when len([ID_������_������_������_������]) = 0 or [ID_������_������_������_������] = N'-' then null else [ID_������_������_������_������] end
											,[ID_��������_��������] = case when len([ID_��������_��������]) = 0 or [ID_��������_��������] = N'-' then null else [ID_��������_��������] end
											,[����������_��������_������_������] = case when len([����������_��������_������_������]) = 0 or [����������_��������_������_������] = N'-' then null else [����������_��������_������_������] end
											,[ID_����������_��������_����������_��������] = case when len([ID_����������_��������_����������_��������]) = 0 or [ID_����������_��������_����������_��������] = N'-' then null else [ID_����������_��������_����������_��������] end
											,[�����] = case when len([�����]) = 0 or [�����] = N'-' then null else [�����] end
											,[�����] = case when len([�����]) = 0 or [�����] = N'-' then null else [�����] end


UPDATE Fingrag_csv SET [������] = REPLACE([������], char(160), '')
UPDATE Fingrag_csv SET [������] = SUBSTRING([������], 1, isnull(charindex('.', [������]), 1) - 1) --WHERE [���_�������������] = '04-07-24-011'
UPDATE Fingrag_csv SET [������] = SUBSTRING([������], 1, 6) 
UPDATE Fingrag_csv SET ���_��_����������_����������_�_��������_����� = null where ���_��_����������_����������_�_��������_����� = '06.08.2014'

UPDATE Fingrag_csv SET [���_������_�����] = '1111110' WHERE [���_������_�����] = '111110'