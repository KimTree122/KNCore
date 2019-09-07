using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Comm.TreeNodeHelper
{
    class TreeNodeSQL
    {
        string SQL = @"CREATE table #fidtable (ID int, FatherID int)
CREATE table #fidinser (ID int,FatherID int)
CREATE table #fidinser2 (ID int,FatherID int)

INSERT INTO #fidinser 
SELECT Id, FatherID FROM dbo.Authority WHERE Id IN ('5','8','16') GROUP BY Id, FatherID
INSERT INTO #fidtable 
SELECT Id, FatherID FROM dbo.Authority WHERE Id IN ('5','8','16') GROUP BY Id, FatherID

DECLARE @fathercounnt int

SET @fathercounnt = (SELECT COUNT(*) FROM dbo.Authority WHERE Id IN(SELECT #fidinser.FatherID FROM #fidinser WHERE #fidinser.FatherID <>0)  ) 

WHILE @fathercounnt > 0
BEGIN
INSERT INTO #fidtable
SELECT Id, FatherID FROM dbo.Authority WHERE Id IN (SELECT #fidinser.FatherID FROM #fidinser WHERE #fidinser.FatherID <>0)
INSERT INTO #fidinser2
SELECT Id, FatherID FROM dbo.Authority WHERE Id IN (SELECT #fidinser.FatherID FROM #fidinser WHERE #fidinser.FatherID <>0)
DELETE FROM #fidinser
INSERT INTO #fidinser
SELECT Id, FatherID FROM dbo.Authority WHERE Id IN (SELECT #fidinser2.ID FROM #fidinser2)
SET @fathercounnt = (SELECT COUNT(*) FROM dbo.Authority WHERE Id IN(SELECT #fidinser2.FatherID FROM #fidinser2  WHERE #fidinser2.FatherID <>0)) 
DELETE FROM #fidinser2
END
SELECT * FROM dbo.Authority WHERE Id IN (SELECT #fidtable.ID FROM dbo.#fidtable)";
    }
}
