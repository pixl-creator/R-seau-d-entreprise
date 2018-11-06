﻿CREATE PROCEDURE [dbo].[GetEmployeeProjectManagerHistory]
	@Employee_Id int
AS
	SELECT p.Project_Id, p.Project_Name, pm.[Date] AS StartDate, dbo.FN_EndDateManager(p.Project_Id, pm.[Date]) AS EndDate
	FROM Project p INNER JOIN ProjectManager pm ON p.Project_Id=pm.Project_Id
	WHERE pm.Employee_Id=@Employee_Id
	ORDER BY (CASE WHEN EndDate IS NULL THEN 0 ELSE 1 END), EndDate DESC