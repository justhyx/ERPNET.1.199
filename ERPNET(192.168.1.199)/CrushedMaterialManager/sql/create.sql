--新增加两个字段 inputArea inputName
-- inputArea 有两个值 1 代表现场录入 2代表品检录入
-- inputName 是为了记录而增加的 

CREATE TABLE [dbo].[shatter_Parts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[goodsName] [nvarchar](20) NOT NULL,
	[count] [int] NOT NULL,
	[price] [money] NOT NULL,
	[moneySum] [money] NOT NULL,
	[badContent] [nvarchar](50) NOT NULL,
	[produceTime] [datetime] NULL,
	[employeeName] [nchar](20) NULL,
	[produceArea] [char](10) NOT NULL,
	[purchaseConfirm] [nvarchar](50) NULL,
	[PMCConfirm] [nvarchar](50) NULL,
	[directorConfirm] [nvarchar](50) NULL,
	[inputTime] [datetime] NULL,
	[topManagerConfirm] [nvarchar](50) NULL,
	[managerConfirm] [nvarchar](50) NULL,
	[tableSign] [int] NULL,
	[inputArea] [int] NULL,
	[inputName] [nvarchar](20) NULL,
 CONSTRAINT [PK_shatter_Parts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
