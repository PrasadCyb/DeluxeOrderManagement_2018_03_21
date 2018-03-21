USE [DeluxeOrderManagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_Insert_Jobs_Announcements]    Script Date: 11/10/2017 2:11:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_Insert_Jobs_Announcements] 
@JobID INT
AS
BEGIN
 INSERT INTO Jobs_Announcements_Inserted
			  (
					JobId,
					AnnouncementGridId,
					StartDate,
					EndDate
			  )
				SELECT DISTINCT jbs.Id,ann.Id,MIN(jbitm.StartDate) AS StartDate,MAX(jbitm.EndDate) AS EndDate FROM AnnouncementGrid ann
				INNER JOIN
				JOBS jbs ON ann.JobID=jbs.Id 
				INNER JOIN
				Jobs_Items jbitm ON jbs.Id=jbitm.JobId WHERE ann.JObId= @JobID and ann.JobAction='Inserted' GROUP BY jbs.Id, ann.Id 
END
GO


