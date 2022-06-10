
DECLARE @RC int, @Id int, @Email varchar(20) = 'aeaij2@yahoo.com'
SELECT  @Id = Id FROM Sukkot.Registration WHERE Email = @Email
SELECT * FROM Sukkot.Registration WHERE Email = @Email
EXECUTE @RC = Sukkot.stpRegistrationDelete @Id

SELECT COUNT(*) AS RowCnt, @RC AS RC FROM Sukkot.Registration WHERE Email = @Email

/*
514	1028

  SELECT * FROM dbo.ErrorLog
	SELECT * FROM zvwErrorLog
--	WHERE ErrorProcedure LIKE '%stpRegistrationDelete' 
	SELECT * FROM sys.messages WHERE message_id = 2601 AND  language_id=1033
	SELECT * FROM sys.messages WHERE message_id = 8134 AND  language_id=1033 DIVIDE BY ZERO

2601 - Violation in unique index
2627 - Violation in unique constraint (although it is implemented using unique index)
Either you can 

Cannot insert duplicate key row in object 'Sukkot.Registration' with unique index 'IX_Registration_EMail_Unique'. The duplicate key value is (aeaij@yahoo.com).

*/

