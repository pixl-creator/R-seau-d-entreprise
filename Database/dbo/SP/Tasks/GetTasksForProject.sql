﻿CREATE PROCEDURE [dbo].[GetTasksForProject]
	@ProjectId int,
	@UserId int
AS
	SELECT 
	Task.Task_Id AS Task_Id,
	Task.[Name],
	Task.Team_Id,
	[Description],
	StartDate,
	EndDate,
	Deadline,
	SubtaskOf,
	ISNULL(TaskStatus.TaskStatus_Id,1) AS LastStatusId,  
	ISNULL(TaskStatusHistory.date,StartDate) AS LastStatusDate,
	LAST_VALUE(TaskStatus.Name) OVER (ORDER BY TaskStatusHistory.date) AS LastStatusName
	FROM Task 
	LEFT JOIN TaskStatusHistory ON Task.Task_Id = TaskStatusHistory.Task_Id 
	LEFT JOIN EmployeeTask ON EmployeeTask.Task_Id = Task.Task_Id
	LEFT JOIN TaskStatus ON TaskStatus.TaskStatus_Id = TaskStatus.TaskStatus_Id
	WHERE Task.Project_Id = @ProjectId
	ORDER BY TaskStatusHistory.date