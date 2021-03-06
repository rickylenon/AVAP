USE [ava]
GO
/****** Object:  Table [dbo].[rfcNatureOfBusiness]    Script Date: 03/29/2012 13:46:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcNatureOfBusiness](
	[NatureOfBusinessId] [int] IDENTITY(1,1) NOT NULL,
	[NatureOfBusinessName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_rfcNatureOfBusiness] PRIMARY KEY CLUSTERED 
(
	[NatureOfBusinessId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductBrands]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductBrands](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [char](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubCategoryId] [int] NOT NULL,
 CONSTRAINT [PK_rfcProductBrands] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductCategory]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductCategory](
	[CategoryId] [nvarchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CategoryName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CategoryDesc] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[NatureOfBusinessId] [int] NULL,
 CONSTRAINT [PK_rfcProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductSubCategory]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductSubCategory](
	[SubCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [nvarchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubCategoryName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubCategoryDesc] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_rfcProductSubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcUserTypes]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rfcUserTypes](
	[UserType] [int] NOT NULL,
	[UserTypeDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_rfcUserTypes] PRIMARY KEY CLUSTERED 
(
	[UserType] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblComments]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblComments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblComments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblDnbFinancialReport]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbFinancialReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Year1] [datetime] NULL,
	[yr1Revenue] [float] NULL,
	[yr1NetIncome] [float] NULL,
	[yr1NetEquity] [float] NULL,
	[Year2] [datetime] NULL,
	[yr2Revenue] [float] NULL,
	[yr2NetIncome] [float] NULL,
	[yr2NetEquity] [float] NULL,
	[Year3] [datetime] NULL,
	[yr3Revenue] [float] NULL,
	[yr3NetIncome] [float] NULL,
	[yr3NetEquity] [float] NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF__tblDnbFin__DateC__59063A47]  DEFAULT (getdate()),
	[maxExpLimit] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[creditExpLimit] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tblDnbFinancialReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDnbLegalReport]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbLegalReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfCase] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateFiled] [datetime] NULL,
	[Attachment] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DateCreated]  DEFAULT (getdate()),
	[VendorId] [int] NOT NULL,
 CONSTRAINT [PK_tblDnbLegalReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDnbRating]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbRating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[dnbUserId] [int] NULL,
	[dnbDuns] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[dnbRating] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[dnbCompRating] [int] NULL,
	[condition] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblDnb] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserPassword] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RefNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [smallint] NOT NULL CONSTRAINT [DF_tblUsers_Status]  DEFAULT (1),
	[TempPassword] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsAuthenticated] [smallint] NOT NULL CONSTRAINT [DF_tblUsers_IsAuthenticated]  DEFAULT (0),
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[SessionId] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LoginStatus] [bit] NULL CONSTRAINT [DF_tblUsers_LoginStatus]  DEFAULT (0),
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[FirstName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MiddleName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAdd] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AuthenticationTicket] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserTypes]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserTypes](
	[UserId] [int] NOT NULL,
	[UserType] [int] NOT NULL CONSTRAINT [DF_tblUsers_UserType]  DEFAULT (2),
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblUserTypes] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[UserType] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorApplicants]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApplicants](
	[CompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmailAdd] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LOIFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[IsAuthenticated] [smallint] NOT NULL DEFAULT (0),
	[ApprovedBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ApprovedDt] [datetime] NULL,
	[RejectedBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RejectedDt] [datetime] NULL,
 CONSTRAINT [PK_tblVendorApplicants] PRIMARY KEY CLUSTERED 
(
	[CompanyName] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyFAALFinance]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyFAALFinance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_tblVendorApprovalbyFAALFinance_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorApprovalbyFAALFinance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorApprovalbyFAALogistics]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyFAALogistics](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_tblVendorApprovalbyFAALogistics_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorApprovalbyFAALogistics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorApprovalbyLegal]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyLegal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[legalUserId] [int] NOT NULL,
	[legalApproved] [tinyint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorApprovalbyLegal] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmIssue]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyVmIssue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vmIssueUserId] [int] NOT NULL,
	[withIncidentReport] [tinyint] NULL,
	[ISRInvolvement] [tinyint] NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_tblVendorApprovalbyVmIssue_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorApprovalbyVmIssue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmReco]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyVmReco](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_tblVendorApprovalbyVmReco_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorApprovalbyVmReco] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmTech]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyVmTech](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vmTechUserId] [int] NOT NULL,
	[newVendor] [tinyint] NULL,
	[validThruProdInfo] [tinyint] NULL,
	[validThruProdPresent] [tinyint] NULL,
	[validThruSiteVisit] [tinyint] NULL,
	[historicalPOIssuedMo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[historicalPOIssuedPO] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[historicalLatestPerfEval] [int] NULL,
	[evalProdServQ1Score] [int] NULL,
	[evalProdServQ1Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalProdServQ2Score] [int] NULL,
	[evalProdServQ2Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalCompetenceQ1Score] [int] NULL,
	[evalCompetenceQ1Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalCompetenceQ2Score] [int] NULL,
	[evalCompetenceQ2Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalTrackRecQ1Score] [int] NULL,
	[evalTrackRecQ1Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalTrackRecQ2Score] [int] NULL,
	[evalTrackRecQ2Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalSuppSysQ1Score] [int] NULL,
	[evalSuppSysQ1Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalSuppSysQ2Score] [int] NULL,
	[evalSuppSysQ2Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalSuppSysQ3Score] [int] NULL,
	[evalSuppSysQ3Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[evalSuppSysQ4Score] [int] NULL,
	[evalSuppSysQ4Remarks] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TotalScore] [int] NULL,
	[DateCreated] [datetime] NULL CONSTRAINT [DF_tblVendorApprovalbyVmTech_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorApprovalbyVmTech] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBackOnKeyPersonnel]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBackOnKeyPersonnel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Position] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DegreeEarned] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EducInstitution] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YearGraduated] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Nationality] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Age] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PastWorkExp] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorBackOnKeyPersonnel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBankInformation]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBankInformation](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[biBankName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[biBranch] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[biAccountType] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[biContact] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBoardMembers]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBoardMembers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[bmMemberOfTheBoard] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[bmNationality] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[bmPostion] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBranches]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBranches](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[brAddr] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[brUsedAs] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[brEmplNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[brArea] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[brOwned] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorBranches] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorCertifications]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorCertifications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Certification] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorCertifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorConflictOfInterest]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorConflictOfInterest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[YesNo] [int] NOT NULL,
	[NatureOfBusinessId] [int] NULL,
	[CompetitorName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoYears] [int] NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[VendorId] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorConflictOfInterest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorFinancialInformation]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorFinancialInformation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Year] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TotalAssets] [float] NULL,
	[TotalLiabilities] [float] NULL,
	[NetEquity] [float] NULL,
	[CurrentAssets] [float] NULL,
	[Inventories] [float] NULL,
	[CurrentLiabilities] [float] NULL,
	[FileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorFinancialInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorIncidentReport]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorIncidentReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[irDetails] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[VendorId] [int] NULL,
 CONSTRAINT [PK_tblVendorIncidentReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorInformation]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorInformation](
	[VendorId] [int] NOT NULL,
	[VendorCode] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[regBldgCode] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regBldgRoom] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regBldgFloor] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regBldgHouseNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regStreetName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regCity] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regProvince] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regCountry] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regPostal] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regArea] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[regOwned] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBidName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBidPosition] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBidEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBidMobile] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBidTelNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBidFaxNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conLegName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conLegPosition] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conLegEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conLegMobile] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conLegTelNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conLegFaxNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBonName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBonPosition] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBonEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBonMobile] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBonTelNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conBonFaxNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conTecName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conTecPosition] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conTecEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conTecMobile] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conTecTelNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conTecFaxNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conSalName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conSalPosition] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conSalEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conSalMobile] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conSalTelNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[conSalFaxNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[parentCompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[parentCompanyAddr] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repOfcCompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repOfcCompanyAddr] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repOfcMobile] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repOfcTelNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repOfcFaxNo] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[manResourceRegular] [int] NULL,
	[manResourceContractual] [int] NULL,
	[manResourceTotal] [int] NULL,
	[benefitsPagibig] [int] NULL,
	[benefitsPagibigFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[benefitsPHIC] [int] NULL,
	[benefitsPHICFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[benefitsSSS] [int] NULL,
	[benefitsSSSFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[benefits13th] [int] NULL,
	[benefits13thFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[benefitsOtherMed] [int] NULL,
	[benefitsOtherMedFName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[benefitsOthers] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[benefitsOthersFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[assetsMachineries] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[assetsMachineriesFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[assetsCompanyProfile] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[assetsCompanyProfileFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[assetsOthers] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[assetsOthersFileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[legalStrucOrgType] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[legalStrucDateReg] [datetime] NULL,
	[legalStrucRegNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[legalStrucSECAttachement] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[legalStrucDateStartedOp] [datetime] NULL,
	[legalStrucPrevBusName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[legalStrucDateChanged] [datetime] NULL,
	[legalStrucTIN] [varchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[busPermitDateReg] [datetime] NULL,
	[busPermitNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[busPermitAttachement] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[birRegTIN] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[birRegAttachement] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[corpAuthorizedCapital] [float] NULL,
	[corpSubscribedCapital] [float] NULL,
	[corpPaidUpCapital] [float] NULL,
	[corpPerValue] [float] NULL,
	[corpAsOfDate] [datetime] NULL,
	[SecurityArangement] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[step8FullName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[step8OfficialTitle] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[step8OfCompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[step8bindCompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[repOfcEmail] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ1] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ2] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ3] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ4] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ5] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ6] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ7] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ8] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ9] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ10] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[suppDeclarationQ11] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tblVendors] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorInsuranceInformation]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorInsuranceInformation](
	[InsuranceId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[iCompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[iAddress] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorLegalCompliance]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorLegalCompliance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
	[VendorId] [int] NULL,
 CONSTRAINT [PK_tblVendorLegalCompliance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorNatureOfBusiness]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorNatureOfBusiness](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[NatureOfBusinessId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorNatureOfBusiness] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblVendorProductsAndServices]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorProductsAndServices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[CategoryId] [nvarchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubCategoryId] [int] NULL,
	[BrandId] [int] NULL,
	[NoYears] [int] NULL,
	[MajorClients] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorProductsAndServices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorRegulatoryRequirements]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorRegulatoryRequirements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[RegulatoryRequirement] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateRegistered] [datetime] NULL,
	[PermitNo] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorRegulatoryRequirements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorShareHolders]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorShareHolders](
	[ShareHolderId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[shShareHolderName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[shNationality] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[shSubsribedCapital] [float] NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSubsidiaries]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSubsidiaries](
	[SubsidiaryId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[subCompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[subAddr] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[subEquity] [float] NULL,
	[subOwned] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorSubsidiaries] PRIMARY KEY CLUSTERED 
(
	[SubsidiaryId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSupplierReferences]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSupplierReferences](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[SupplierName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContactPerson] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContactNo] [float] NULL,
	[Terms] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorSupplierReferences] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSuppliersDeclaration]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSuppliersDeclaration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Description] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FileName] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorSuppliersDeclaration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorTopCompetitors]    Script Date: 03/29/2012 13:46:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorTopCompetitors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[CompanyName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_tblVendorTopCompetitors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF