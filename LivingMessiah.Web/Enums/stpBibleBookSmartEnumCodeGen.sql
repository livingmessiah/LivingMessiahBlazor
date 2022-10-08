ALTER PROCEDURE dbo.stpBibleBookSmartEnumCodeGen ( @Table nvarchar(50) = 'Bible')
AS
/*
	DECLARE @RC int
	EXEC @RC = dbo.stpBibleBookSmartEnumCodeGen 'Bible'

	--			1	  Genesis	Gen	       1	            1533	             1	    Beresheeth    ---        	50

	SELECT 	Id, Title, Abrv, ScriptureId_Beg, ScriptureId_End, BookGroupId, HebrewTitle, HebrewName, LastChapter 
	FROM Bible.Book 
	ORDER BY Id

	public enum BookGroupEnum { Torah = 1, History = 2, ...
	public enum BookEnum { Genesis = 1, Exodus = 2 ...
*/

DECLARE @CRLF AS CHAR(2) = CHAR(13) + CHAR(10)
DECLARE @TAB AS CHAR(1) = CHAR(9)
DECLARE @Q AS CHAR(1) = '''' 
DECLARE @DQ AS CHAR(1) = CHAR(34) -- Double Quote
DECLARE @DQ2 AS CHAR(2) = CHAR(34)+CHAR(34) -- Double Quote
DECLARE @SQ AS CHAR(1) = CHAR(39) -- Single Quote
DECLARE @S1 AS varchar(255) = ''

DECLARE @ClassName AS VARCHAR(75) = @Table + 'Book'
DECLARE @InstanceSuffix AS VARCHAR(25) = 'SE'

DECLARE @ExtraFields nvarchar(255) 

BEGIN

	DECLARE @BibleBookTable AS TABLE 
	(Id int NOT NULL,
	Value nvarchar(35) NOT NULL PRIMARY KEY,
	Title nvarchar(75) NOT NULL,
	TitleCS nvarchar(75) NOT NULL,
	Abrv nvarchar(75) NOT NULL,
	BookGroupEnum int NOT NULL,
	BookGroup nvarchar(100) NOT NULL,
	BookEnum int NOT NULL,
	LastChapter int NOT NULL,
	TransliterationInHebrew nvarchar(100) NOT NULL,
	NameInHebrew nvarchar(100) NULL)
	
	INSERT INTO @BibleBookTable (Id, Value, Title, TitleCS, Abrv, BookGroupEnum, BookGroup, BookEnum, LastChapter, TransliterationInHebrew, NameInHebrew )

	SELECT 	b.Id, CAST(b.Id AS varchar(10)) AS Value, REPLACE(Title, ' ', '') AS Title
	, CASE 
		 WHEN b.Id=9	THEN 'FirstSamuel'
		 WHEN b.Id=10 THEN 'SecondSamuel'
		 WHEN b.Id=11 THEN 'FirstKings'
		 WHEN b.Id=12 THEN 'SecondKings'
		 WHEN b.Id=13 THEN 'FirstChronicles'
		 WHEN b.Id=14 THEN 'SecondChronicles'
		 WHEN b.Id=46 THEN 'FirstCorinthians'
		 WHEN b.Id=47 THEN 'SecondCorinthians'
		 WHEN b.Id=52 THEN 'FirstThessalonians'
		 WHEN b.Id=53 THEN 'SecondThessalonians'
		 WHEN b.Id=54 THEN 'FirstTimothy'
		 WHEN b.Id=55 THEN 'SecondTimothy'
		 WHEN b.Id=60 THEN 'FirstPeter'
		 WHEN b.Id=61 THEN 'SecondPeter'
		 WHEN b.Id=62 THEN 'FirstJohn'
		 WHEN b.Id=63 THEN 'SecondJohn'
		 WHEN b.Id=64 THEN 'ThirdJohn'
									ELSE REPLACE(Title, ' ', '')
	END AS TitleCS
	, Abrv, CAST(BookGroupId AS varchar(10)) AS BookGroupEnum
	, CASE 
		 WHEN BookGroupId = 3 THEN 'Poetry'
		 WHEN BookGroupId = 4 THEN 'MajorProphets'
		 WHEN BookGroupId = 5 THEN 'MinorProphets'
		 WHEN BookGroupId = 7 THEN 'PaulsEpistles'
		 WHEN BookGroupId = 8 THEN 'GeneralEpistles'
													ELSE bg.Descr 
	END AS BookGroup
, CAST(b.Id AS varchar(10)) AS BookEnum, CAST(LastChapter AS varchar(10)) AS LastChapter
	, HebrewTitle AS TransliterationInHebrew, HebrewName AS NameInHebrew	
	FROM Bible.Book b
		JOIN Bible.BookGroup bg ON b.BookGroupId=bg.Id
	ORDER BY b.Id

	-- 	#region  Declared Public Instances

	SELECT
	'public static readonly ' + @ClassName + ' ' + TitleCS + ' = new ' + TitleCS  + 'SE();'
	AS CodeGenDeclPubInst
	FROM @BibleBookTable
	ORDER BY Id


	SELECT 
	'private sealed class ' + TitleCS + @InstanceSuffix + ' : ' +  @ClassName  + @CRLF +  '{ ' 
	+ @CRLF + '	public ' + TitleCS + @InstanceSuffix + '() : base(' + QUOTENAME(TitleCS, CHAR(34)) + ', ' + [Value] + ') { }' 

	+ @CRLF + @TAB + 'public override string Title => ' + QUOTENAME(Title, CHAR(34)) + ';'
	+ @CRLF + @TAB + 'public override string Abrv => ' + QUOTENAME(Abrv, CHAR(34)) + ';'
	+ @CRLF + @TAB + 'public override BookGroupEnum BookGroupEnum => BookGroupEnum.' +  BookGroup  + ';'
	+ @CRLF + @TAB + 'public override BookEnum BookEnum => BookEnum.' + TitleCS  + ';'
	+ @CRLF + @TAB + 'public override int LastChapter => ' +  CAST(LastChapter AS VARCHAR(10))  + ';'
	+ @CRLF + @TAB + 'public override string TransliterationInHebrew => ' +  QUOTENAME(TransliterationInHebrew, CHAR(34))  + ';'
	+ @CRLF + @TAB + 'public override string NameInHebrew => ' +  IIF(NameInHebrew IS NULL, @DQ2, QUOTENAME(NameInHebrew, CHAR(34)))  + ';'


	+ @CRLF + ' } ' AS CodeGenInstantiation
	FROM @BibleBookTable
	ORDER BY Id


END


/*
  Note
		The CRLF CHAR(13) + CHAR(10) get's lost when copying the results from SSMS to
		https://blog.sqlauthority.com/2016/06/03/sql-server-maintain-carriage-return-enter-key-ssms-2016-copy-paste/
*/


