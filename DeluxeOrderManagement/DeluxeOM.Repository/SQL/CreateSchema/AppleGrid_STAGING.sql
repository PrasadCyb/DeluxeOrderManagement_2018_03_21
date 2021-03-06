CREATE TABLE [dbo].[AppleGrid_Staging](
	[Title] [nvarchar](255) NULL,
	[MPM] [nvarchar](255) NULL,
	[Video Version] [nvarchar](255) NULL,
	[Local Edit] [nvarchar](255) NULL,
	[Version EIDR] [nvarchar](255) NULL,
	[Category] [nvarchar](255) NULL,
	[Primary VID] [nvarchar](255) NULL,
	[Vendor ID] [nvarchar](255) NULL,
	[EST UPC] [nvarchar](255) NULL,
	[IVOD UPC] [nvarchar](255) NULL,
	[Region] [nvarchar](255) NULL,
	[Territory] [nvarchar](255) NULL,
	[Language] [nvarchar](255) NULL,
	[Language Type] [nvarchar](255) NULL,
	[Vendor] [nvarchar](255) NULL,
	[File Type] [nvarchar](255) NULL,
	[Request Number] [nvarchar](255) NULL,
	[MPO] [nvarchar](255) NULL,
	[HAL ID] [nvarchar](255) NULL,
	[Order Status] [nvarchar](255) NULL,
	[Order Request Date] [datetime] NULL,
	[Delivery Due Date] [datetime] NULL,
	[Actual Delivery Date] [datetime] NULL,
	[First Start Date] [datetime] NULL,
	[Greenlight Due Date] [datetime] NULL,
	[Greenlight Validated by DMDM] [datetime] NULL,
	[Greenlight Sent to Packaging] [datetime] NULL,
	[Pre-Order SD Title Status] [nvarchar](255) NULL,
	[Pre-Order HD Title Status] [nvarchar](255) NULL,
	[Pre-Order 4k Title Status] [nvarchar](255) NULL,
	[EST SD Title Status] [nvarchar](255) NULL,
	[EST HD Title Status] [nvarchar](255) NULL,
	[EST 4k Title Status] [nvarchar](255) NULL,
	[VOD SD Title Status] [nvarchar](255) NULL,
	[VOD HD Title Status] [nvarchar](255) NULL,
	[VOD 4k Title Status] [nvarchar](255) NULL,
	[Announcement Pre Order SD Date] [datetime] NULL,
	[Announcement Pre Order HD Date] [datetime] NULL,
	[Announcement Pre Order 4k Date] [datetime] NULL,
	[Announcement EST SD Start] [datetime] NULL,
	[Announcement EST HD Start] [datetime] NULL,
	[Announcement EST 4k Start] [datetime] NULL,
	[Announcement VOD SD Start] [datetime] NULL,
	[Announcement VOD HD Start] [datetime] NULL,
	[Announcement VOD 4K Start] [datetime] NULL,
	[First Announced Date] [datetime] NULL,
	[Last Announcement Date] [datetime] NULL,
	[iTC Pre-Order] [nvarchar](255) NULL,
	[iTC EST Start] [nvarchar](255) NULL,
	[iTC VOD Start] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[CONCATENATE] [nvarchar](255) NULL
) ON [PRIMARY]