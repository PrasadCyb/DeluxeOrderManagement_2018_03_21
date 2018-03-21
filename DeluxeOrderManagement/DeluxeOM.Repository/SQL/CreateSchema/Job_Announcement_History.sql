CREATE TABLE Job_Annoncement_History
(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	JobId INT NOT NULL,
	AnnouncementOrderId INT NOT NULL,
	Type INT NOT NULL --1->Announcement/2->Order
)
