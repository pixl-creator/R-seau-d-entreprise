﻿CREATE PROCEDURE [dbo].[GetEvent]
	@EventId int
AS
	SELECT Event_Id,Name,CreatorId,DepartmentId,[Description],[Address],StartDate,
	EndDate,CreationDate,[Open],Cancelled, NULL AS Subscribed FROM Event WHERE Event_Id = @EventId
RETURN 0