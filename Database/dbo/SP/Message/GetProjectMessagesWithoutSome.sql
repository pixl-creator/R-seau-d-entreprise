﻿CREATE PROCEDURE [dbo].[GetProjectMessagesWithoutSome]
	@ProjectId int,
	@NotNeededIds varchar(max)
AS
BEGIN
    DECLARE @temptable table(id int)
	DECLARE @index int
	WHILE(LEN(@NotNeededIds) > 0)
	BEGIN
	    SET @index = CHARINDEX(',', @NotNeededIds)
	    INSERT INTO @temptable(id) VALUES(SUBSTRING(@NotNeededIds, 1, @index-1))
		SET @NotNeededIds = SUBSTRING(@NotNeededIds, @index+1, LEN(@NotNeededIds))
	END

	SELECT m.Message_Id, m.Message_Title, m.Message_Date, m.Message_Message, m.Message_Parent, m.Message_Author
	FROM [Message] m JOIN MessageProject mp ON m.Message_Id=mp.Message_Id
	LEFT JOIN @temptable t ON m.Message_Id=t.id
	WHERE mp.Project_Id=@ProjectId
	AND t.id IS NULL
END
