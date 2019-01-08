﻿CREATE PROCEDURE [dbo].[GetAllActiveEvents]
AS
	SELECT Event_Id,CreatorId,DepartmentId,Name,Description,Address,StartDate,EndDate,CreationDate,Cancelled,[Open], NULL AS Subscribed
	FROM Event 
	WHERE SYSDATETIME()<EndDate AND Cancelled =0
RETURN 0