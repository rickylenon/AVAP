USE [ava]
GO

/****** Object:  Table [dbo].[rfcLandingContent]    Script Date: 03/24/2015 10:38:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rfcLandingContent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[content] [text] NULL,
	[date] [datetime] NULL,
	[active] [smallint] NULL,
 CONSTRAINT [PK_rfcLandingContent] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[rfcLandingContent] ADD  CONSTRAINT [DF_rfcLandingContent_date]  DEFAULT (getdate()) FOR [date]
GO

