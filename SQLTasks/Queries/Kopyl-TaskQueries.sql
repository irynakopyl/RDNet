USE CompanyManager 
GO

INSERT INTO dbo.Tasks
VALUES (3, 'Navigation', 'Open','20190729', 3, 3, 2)

--1. �������� ������ ���� ���������� �������� � ����������� 
--����������� �� ������ �� ���

SELECT  COUNT(IdWorker) AS Amount, Name
FROM  Tasks as t inner join Positions as P
ON t.IdPosition=P.PositionID GROUP BY Name

--2. ���������� ������ ���������� ��������, �� 
--������� ��� �����������

SELECT Name FROM Positions
WHERE PositionID NOT IN 
	(SELECT IdPosition 
	FROM Tasks)

--3. �������� ������ �������� � ���������, ������� 
--����������� ������ ��������� �������� �� �������
SELECT  COUNT(IdWorker) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID GROUP BY ProjectName

--������� ��� ������� �������� ������
INSERT INTO dbo.Tasks
VALUES (4, 'Relations', 'Open','20190730', 3, 2, 2)
INSERT INTO dbo.Tasks
VALUES (5, 'Marketplace', 'Open','20190730', 1, 2, 3)
INSERT INTO dbo.Tasks
VALUES (5, 'Marketplace', 'Open','20190730', 1, 2, 3)
INSERT INTO dbo.Tasks
VALUES (6, ' NEW API', 'Open','20190730', 1, 2, 3)
INSERT INTO dbo.Tasks
VALUES (7, 'Financial', 'Open','20190730', 1, 2, 3)
INSERT INTO dbo.Tasks
VALUES (8, 'Editing', 'Open','20190805', 3, 2, 3)

--4. ��������� �� ������ �������, ����� � ������� 
--���������� ����� ���������� �� ������� ����������

SELECT COUNT(TaskID)/COUNT(DISTINCT IdWorker) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID GROUP BY ProjectName 

--5. ���������� ������������ ���������� ������� �������
SELECT P.ProjectName, DATEDIFF(day, P.CreationDate ,P.ClosingDate )AS Length
FROM Projects AS P

--6. ���������� ����������� � ����������� ����������� ���������� �����
	SELECT TOP 1 COUNT(TaskId)as Amount, WorkerName
	FROM  Tasks as t inner join Workers as P
	ON t.IdWorker=P.WorkerID 
	WHERE Status NOT IN (N'Accepted', N'Done')
	GROUP BY WorkerName
	ORDER BY Amount ASC

--7. ���������� ����������� � ������������ ����������� ���������� �����, 
--������� ������� ��� �����
	SELECT TOP 1 COUNT(TaskId)as Amount, WorkerName
	FROM  Tasks as t inner join Workers as P
	ON t.IdWorker=P.WorkerID 
	WHERE Deadline <= '20190728' AND Status NOT IN (N'Accepted', N'Done')
	GROUP BY WorkerName
	ORDER BY Amount DESC

--8. �������� ������� ���������� ����� �� 5 ����
UPDATE Tasks 
SET Deadline = DATEADD(d,5,Deadline)
WHERE Status  NOT IN (N'Accepted', N'Done')

--9. ��������� �� ������ ������� ���������� �����, � ������� ��� �� ����������

SELECT COUNT(TaskID) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID 
WHERE Status NOT IN (N'Accepted', N'Done')
GROUP BY ProjectName 

--10. ��������� ������� � ��������� ������, ��� ������� ��� ������ ������� 
--� ������ ����� �������� �������� �������� ������ �������, �������� ���������

UPDATE Projects
SET State = 'Close' 
WHERE ProjectID IN
(SELECT IdProject 
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID 
WHERE Status = N'Done' AND IdProject NOT IN
(SELECT IdProject 
FROM  Tasks 
WHERE Status = N'Open')
GROUP BY IdProject )

--11. �������� �� ���� ��������, ����� ���������� �� ������� �� ����� ���������� �����
SELECT COUNT(TaskID) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID 
WHERE Status IN (N'Accepted', N'Done')
GROUP BY ProjectName 

--12. �������� ������ (�� ��������) ������� ��������� �� ���������� � ����������� ����������� ����������� �� �����
CREATE PROCEDURE [dbo].[GetNewTask] (@NAME NVARCHAR(50))
AS
BEGIN
	UPDATE Tasks
	SET IdWorker = 
	 ( SELECT IdWorker FROM
	  (SELECT TOP 1 IdWorker As IdWorker, COUNT(IdWorker) AS Amount FROM  Tasks
	  GROUP BY IdWorker
	  ORDER BY Amount ASC) AS T )
	  WHERE TaskName = @NAME
END

