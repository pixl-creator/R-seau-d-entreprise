﻿CREATE PROCEDURE [dbo].[SP_GetAllActiveEmployees]
AS
	SELECT Employee_Id, FirstName, LastName, Email, RegNat, [Address], Phone from [dbo].Employee WHERE Active=1
