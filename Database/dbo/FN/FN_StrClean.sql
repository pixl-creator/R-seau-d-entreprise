﻿CREATE FUNCTION [dbo].[FN_StrClean](@str NVARCHAR(MAX))
RETURNS NVARCHAR(MAX) 
AS
BEGIN
RETURN (rtrim(ltrim(@str)));
END