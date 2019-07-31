USE CompanyManager 
GO

INSERT INTO dbo.Tasks
VALUES (3, 'Navigation', 'Open','20190729', 3, 3, 2)

--1. Получить список всех должностей компании с количеством 
--сотрудников на каждой из них

SELECT  COUNT(IdWorker) AS Amount, Name
FROM  Tasks as t inner join Positions as P
ON t.IdPosition=P.PositionID GROUP BY Name

--2. Определить список должностей компании, на 
--которых нет сотрудников

SELECT Name FROM Positions
WHERE PositionID NOT IN 
	(SELECT IdPosition 
	FROM Tasks)

--3. Получить список проектов с указанием, сколько 
--сотрудников каждой должности работает на проекте
SELECT  COUNT(IdWorker) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID GROUP BY ProjectName

--добавим еще немного тестовых данных
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

--4. Посчитать на каждом проекте, какое в среднем 
--количество задач приходится на каждого сотрудника

SELECT COUNT(TaskID)/COUNT(DISTINCT IdWorker) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID GROUP BY ProjectName 

--5. Подсчитать длительность выполнения каждого проекта
SELECT P.ProjectName, DATEDIFF(day, P.CreationDate ,P.ClosingDate )AS Length
FROM Projects AS P

--6. Определить сотрудников с минимальным количеством незакрытых задач
	SELECT TOP 1 COUNT(TaskId)as Amount, WorkerName
	FROM  Tasks as t inner join Workers as P
	ON t.IdWorker=P.WorkerID 
	WHERE Status NOT IN (N'Accepted', N'Done')
	GROUP BY WorkerName
	ORDER BY Amount ASC

--7. Определить сотрудников с максимальным количеством незакрытых задач, 
--дедлайн которых уже истек
	SELECT TOP 1 COUNT(TaskId)as Amount, WorkerName
	FROM  Tasks as t inner join Workers as P
	ON t.IdWorker=P.WorkerID 
	WHERE Deadline <= '20190728' AND Status NOT IN (N'Accepted', N'Done')
	GROUP BY WorkerName
	ORDER BY Amount DESC

--8. Продлить дедлайн незакрытых задач на 5 дней
UPDATE Tasks 
SET Deadline = DATEADD(d,5,Deadline)
WHERE Status  NOT IN (N'Accepted', N'Done')

--9. Посчитать на каждом проекте количество задач, к которым еще не приступили

SELECT COUNT(TaskID) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID 
WHERE Status NOT IN (N'Accepted', N'Done')
GROUP BY ProjectName 

--10. Перевести проекты в состояние закрыт, для которых все задачи закрыты 
--и задать время закрытия временем закрытия задачи проекта, принятой последней

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

--11. Выяснить по всем проектам, какие сотрудники на проекте не имеют незакрытых задач
SELECT COUNT(TaskID) AS Amount, ProjectName
FROM  Tasks as t inner join Projects as P
ON t.IdProject=P.ProjectID 
WHERE Status IN (N'Accepted', N'Done')
GROUP BY ProjectName 

--12. Заданную задачу (по названию) проекта перевести на сотрудника с минимальным количеством выполняемых им задач
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

