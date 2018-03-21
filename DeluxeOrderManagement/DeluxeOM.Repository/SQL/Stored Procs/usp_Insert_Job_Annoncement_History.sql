-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE usp_Insert_Job_Annoncement_History
	@JobId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    /* Maintain history of newly added records */
			INSERT INTO Job_Annoncement_History
			(
				JobId,
				AnnouncementOrderId,
				[Type]
			)
			SELECT
				@JobId,
				A.Id,
				1 --Announcement
			FROM Announcementgrid A WHERE A.JobAction = 'Inserted'
			UNION
			SELECT 
				@JobId,
				O.Id,
				2 --Order
			FROM OrderGrid O WHERE O.JobAction = 'Inserted'


END
GO
