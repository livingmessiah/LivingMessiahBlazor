SELECT Id, FeastDayId, Detail, Name FROM KeyDate.FeastDayDetail
/*
Id	FeastDayId	Detail	Name

		Passover
1				3					1				Passover Seder Meal
2				3					2				Feast of unleavened bread (1st day)
3				3					3				First Day of Omer
4				3					4				Feast of unleavened bread (last day)

		Tabernacles									
5				7					1				Preperation day, High Sabbath begins at sunset
6				7					2				1st day
7				7					3				Last Great Day
8				7					4				Ending of Sukkot at sundown

		Hanukkah
9				4/8				1				HanukkahLastDay

		Yom Kippur
10			6					1				Yom Kippur Begins
*/

SELECT fd.Id, Name, DateId, CONVERT(nvarchar(30), d.Date, 111) AS DateYMD
FROM KeyDate.FeastDay fd
INNER JOIN KeyDate.Date d ON fd.DateId = d.Id

/*
Id	Name					DateId	DateYMD
1		Hanukkah				4			2020/12/10
2		Purim						9			2021/02/26
3		Passover				13		2021/04/25
4		Weeks						16		2021/06/15
5		Trumpets				22		2021/10/06
6		Yom Kippur			24		2021/10/16
7		Tabernacles			25		2021/10/20

*/