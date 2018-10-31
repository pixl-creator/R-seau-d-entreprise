﻿CREATE TRIGGER [OnDeleteEmployee]
ON Employee
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON
		--DECLARE @EmpId int
	--SELECT @EmpId = Employee_Id FROM deleted
	--INSERT INTO EmployeeStatusHistory(Employee_Id,EmployeeStatus_Id) VALUES (  @EmpId,2)
	INSERT INTO EmployeeStatusHistory(Employee_Id,EmployeeStatus_Id) SELECT Employee_Id, 2 FROM deleted;
	UPDATE Employee SET Employee.Active = 0 WHERE Employee_Id IN (SELECT deleted.Employee_Id FROM deleted);
END