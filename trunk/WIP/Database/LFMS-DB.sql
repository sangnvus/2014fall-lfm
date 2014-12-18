USE [master]
GO

/****** Object:  Database [LFMS]    Script Date: 12/18/2014 00:53:42 ******/
CREATE DATABASE [LFMS] ON  PRIMARY 
( NAME = N'LFMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\LFMS.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LFMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\LFMS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [LFMS] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LFMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [LFMS] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [LFMS] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [LFMS] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [LFMS] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [LFMS] SET ARITHABORT OFF 
GO

ALTER DATABASE [LFMS] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [LFMS] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [LFMS] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [LFMS] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [LFMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [LFMS] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [LFMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [LFMS] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [LFMS] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [LFMS] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [LFMS] SET  DISABLE_BROKER 
GO

ALTER DATABASE [LFMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [LFMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [LFMS] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [LFMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [LFMS] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [LFMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [LFMS] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [LFMS] SET  READ_WRITE 
GO

ALTER DATABASE [LFMS] SET RECOVERY FULL 
GO

ALTER DATABASE [LFMS] SET  MULTI_USER 
GO

ALTER DATABASE [LFMS] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [LFMS] SET DB_CHAINING OFF 
GO


USE [LFMS]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description]) VALUES (1, N'Admin', N'Quản trị viên')
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description]) VALUES (2, N'Creator', N'Luật sư có quyền tạo hồ sơ mới')
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Description]) VALUES (3, N'Viewer', N'Luật sư chỉ có thể xem hồ sơ')
SET IDENTITY_INSERT [dbo].[Roles] OFF
/****** Object:  Table [dbo].[StaffGroups]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffGroups](
	[StaffGroupId] [int] IDENTITY(1,1) NOT NULL,
	[StaffGroupName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[BaseSalary] [float] NULL,
 CONSTRAINT [PK_StaffGroups] PRIMARY KEY CLUSTERED 
(
	[StaffGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[StaffGroups] ON
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (1, N'Luật sư hợp đồng', N'Luật sư hợp đồng', 20000000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (2, N'Luật sư tập sự', N'Luật sư tập sự', 4500000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (3, N'Luật sư thử việc', N'Luật sư thử việc', 6000000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (4, N'Nhân viên hợp đồng', N'Nhân viên hợp đồng', 7000000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (5, N'Nhân viên tập sự', N'Nhân viên tập sự', 4500000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (6, N'Nhân viên thử việc', N'Nhân viên thử việc', 4000000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (7, N'Cộng tác viên', N'Cộng tác viên', 3000000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (10, N'Quản lí', N'Luật sư quản lí các văn phòng', 50000000)
INSERT [dbo].[StaffGroups] ([StaffGroupId], [StaffGroupName], [Description], [BaseSalary]) VALUES (11, N'dfd', N'dfdf', 232424)
SET IDENTITY_INSERT [dbo].[StaffGroups] OFF
/****** Object:  Table [dbo].[ServiceTypes]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceTypes](
	[ServiceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ServiceTypes] PRIMARY KEY CLUSTERED 
(
	[ServiceTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ServiceTypes] ON
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (1, N'Đại diện ngoài tố tụng', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (2, N'Trợ giúp pháp lý miễn phí', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (3, N'Dịch vụ tố tụng dân sự', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (4, N'Dịch vụ tố tụng hành chính', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (5, N'Dịch vụ tố tụng hình sự', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (6, N'Dịch vụ tố tụng kinh tế', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (7, N'Tư vấn pháp luật thường xuyên', N'')
INSERT [dbo].[ServiceTypes] ([ServiceTypeId], [ServiceTypeName], [Description]) VALUES (8, N'Tư vấn pháp luật theo vụ việc', N'')
SET IDENTITY_INSERT [dbo].[ServiceTypes] OFF
/****** Object:  Table [dbo].[Offices]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offices](
	[OfficeId] [int] IDENTITY(1,1) NOT NULL,
	[OfficeName] [nvarchar](100) NOT NULL,
	[Manager] [nvarchar](50) NOT NULL,
	[TaxCode] [nvarchar](14) NULL,
	[Address] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Website] [nvarchar](50) NULL,
	[BankAccount] [nvarchar](20) NULL,
	[BankBranch] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Offices] PRIMARY KEY CLUSTERED 
(
	[OfficeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Offices] ON
INSERT [dbo].[Offices] ([OfficeId], [OfficeName], [Manager], [TaxCode], [Address], [PhoneNumber], [Fax], [Email], [Website], [BankAccount], [BankBranch], [Active]) VALUES (1, N'Luật Thuận Nguyễn Hồ Chí Minh', N'Nguyễn Hòa Thuận', N'', N'218 Tân Hương, Phường Tân Quý, Quận Tân Phú, HCM', N'0866766963', N'', N'info@luatthuannguyen.com', N'www.luatthuannguyen.com', N'', N'', 1)
INSERT [dbo].[Offices] ([OfficeId], [OfficeName], [Manager], [TaxCode], [Address], [PhoneNumber], [Fax], [Email], [Website], [BankAccount], [BankBranch], [Active]) VALUES (2, N'Luật Thuận Nguyễn Vĩnh Long', N'Nguyễn Hòa Thuận', N'', N'161/10 Long Thuận A, Long Phước, Long Hồ, Vĩnh Long', N'0703948481', N'', N'info@luatthuannguyen.com', N'', N'', N'', 1)
INSERT [dbo].[Offices] ([OfficeId], [OfficeName], [Manager], [TaxCode], [Address], [PhoneNumber], [Fax], [Email], [Website], [BankAccount], [BankBranch], [Active]) VALUES (3, N'Thừa Phát Lại Vĩnh Long', N'Nguyễn Hòa Thuận', N'', N'Số 2/7A đường Mậu Thân, khóm 1, phường 3, thành phố Vĩnh Long', N'0966611139', N'', N'info@thuaphatlaivinhlong.com', N'www.thuaphatlaivinhlong.com', N'', N'', 1)
INSERT [dbo].[Offices] ([OfficeId], [OfficeName], [Manager], [TaxCode], [Address], [PhoneNumber], [Fax], [Email], [Website], [BankAccount], [BankBranch], [Active]) VALUES (4, N'Luật Vũng Tàu Thuận', N'duy', N'0918273627', N'23 Trần Phú Thành phố Vũng Tàu', N'01203938471', N'0120303847', N'duypvse60634@fpt.edu.vn', N'', N'0018998223', N'Tien Phong Bank', 0)
SET IDENTITY_INSERT [dbo].[Offices] OFF
/****** Object:  Table [dbo].[CustomerGroups]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerGroups](
	[CustomerGroupId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerGroupName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomerGroups] PRIMARY KEY CLUSTERED 
(
	[CustomerGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CustomerGroups] ON
INSERT [dbo].[CustomerGroups] ([CustomerGroupId], [CustomerGroupName], [Description]) VALUES (1, N'Cá nhân', N'')
INSERT [dbo].[CustomerGroups] ([CustomerGroupId], [CustomerGroupName], [Description]) VALUES (2, N'Doanh nghiệp', N'')
INSERT [dbo].[CustomerGroups] ([CustomerGroupId], [CustomerGroupName], [Description]) VALUES (3, N'Tổ chức khác', N'')
INSERT [dbo].[CustomerGroups] ([CustomerGroupId], [CustomerGroupName], [Description]) VALUES (4, N'a', N'AD')
SET IDENTITY_INSERT [dbo].[CustomerGroups] OFF
/****** Object:  Table [dbo].[Cases]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cases](
	[CaseId] [int] IDENTITY(1,1) NOT NULL,
	[CaseCode] [nvarchar](50) NOT NULL,
	[CreatorId] [int] NOT NULL,
	[OfficeId] [int] NOT NULL,
	[ReceiptDate] [date] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CaseContent] [nvarchar](max) NOT NULL,
	[DisputeSubject] [nvarchar](max) NULL,
	[DisputeRelation] [nvarchar](max) NULL,
	[LimitationStatute] [nvarchar](max) NULL,
	[LegalEvent] [nvarchar](max) NULL,
	[ErrorFactor] [nvarchar](max) NULL,
	[ProtectiveGoal] [nvarchar](max) NULL,
	[OpeningProcedure] [nvarchar](max) NULL,
	[InquiryProcedure] [nvarchar](max) NULL,
	[ArgumentProcedure] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_OperationDocuments] PRIMARY KEY CLUSTERED 
(
	[CaseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cases] ON
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1, N'HS20140001', 1, 1, CAST(0x14380B00 AS Date), N'Đang thụ lý', N'Tranh chấp tài sản thừa kế', N'Tài sản thừa kế', N'Gia đình Con cái', N'', N'', N'Giành quyền thừa kế tài sản', N'', N'', N'', N'', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (2, N'HS20140002', 1, 2, CAST(0x2D380B00 AS Date), N'Đang thụ lý', N'Đòi bồi thường tai nạn giao thông', N'Tiền bồi thường', N'Người bị hại', N'', N'', N'Giành tiền bồi thường thương tích', N'', N'', N'', N'', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (3, N'DSST2014001', 1, 1, CAST(0x5B380B00 AS Date), N'Đã thụ lý', N'Tranh chấp quyền sử dụng đất và kiện đòi tài sản', N'- Nhà và đất tọa lạc ấp Cẩm Sơn, xã Xuân Mỹ, huyện Cẩm Mỹ theo giấy chứng nhận quyền sử dụng đất và quyền sở hữu nhà số K303571 do UBND huyện Long Khánh cấp ngày 14.4.1997 với diện tích còn lại sau thu hồi là 1.085,5 m2 theo bản vẽ hiện trạng ngày 28/8/2011;', N'- Quyền sử dụng đất và tài sản gắn liền với đất
- Đòi tài sản', N'- Còn trong thời hiệu khởi kiện căn cứ điểm a, khoản 3 điều 159 BL TTDS;
- Thuộc thẩm quyền của Tand huyện Cẩm Mỹ căn cứ K2 + K7, Đ.25; K1, Đ.33 BLTTDS.', N'', N'', N'Buộc bị đơn trả lại cho nguyên đơn những tài sản sau đây: 

Trả lại toàn bộ nhà đất tọa lạc ấp Cẩm Sơn, xã Xuân Mỹ, huyện Cẩm Mỹ theo giấy chứng nhận quyền sử dụng đất và quyền sở hữu nhà số K303571 do UBND huyện Long Khánh cấp ngày 14.4.1997 với diện tích còn lại sau thu hồi là 1.085,5 m2 theo bản vẽ hiện trạng ngày 28/8/2011', N'', N'', N'', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (4, N'TLST2014001', 3, 1, CAST(0x4F380B00 AS Date), N'Đã thụ lý', N'Tranh chấp hợp đồng vay tài sản.', N'Số tiền 800 triệu đồng', N'Hợp đồng vay tài sản', N'Vẫn còn
Căn cứ: Điều 2 Hợp đồng vay tiền => quyền lợi của nguyên đơn bị xâm phạm từ ngày 14/6/2009 đến nay.
Cơ sở pháp lý: Điểm a, khoản 3, điều 159 BLTTDS', N'', N'', N'Bác toàn bộ yêu cầu khởi kiện của nguyên đơn', N'', N'1.	Hỏi nguyên đơn
- Nguyên đơn cho HĐXX biết, tại buổi đối chất ngày 05/01/2011 và ngày 26/4/2011, nguyên đơn thừa nhận là giữa nguyên đơn và bị đơn có ký hợp đồng vay 800 triệu nhưng hai bên không có giao nhận bất cứ khoản tiền nào mà thực chất đây là một sự bảo lãnh nợ. Như vậy thì nguyên đơn cho biết rõ ai bảo lãnh nợ cho ai? bảo lãnh số tiền bao nhiêu, thời điểm nào?
Có phải bị đơn bảo lãnh khoản nợ cho bà Lê Tuyết Nhung không?
Tại sao tại bản tự khai ngày 20/01/2011, bà Nhung khai không biết gì về số tiền vay 800 triệu giữa nguyên đơn và bị đơn.

2.	Hỏi bị đơn
- Chị giải thích cho HĐXX biết rõ, với tư cách là người đại diện cho bà Thủy, tại sao ngày 01/10/2010, người đại diện cho chị Thủy khai có sự hứa thưởng 400 triệu với nguyên đơn?
- Tại thời điểm ngày 01/10/2010, người đại diện cho chị Thủy khai có sự hứa thưởng 400 triệu với nguyên đơn là nội dung khai chưa đúng sự thật. Khi người đại diện nhận làm đại diện cho chị Thủy chỉ mới tiếp xúc với anh Hà Minh Trí là người đại diện cho anh Đệ và chỉ nghe anh Trí trình bày nội dung sự việc là có sự hứa thưởng nên mới khai như vậy.
Về sau anh Trí có thái độ bất hợp tác với Tòa án và không tiếp tục làm đại diện cho anh Đệ thì người đại diện cho chị Thủy đã tìm anh Đệ yêu cầu hủy ủy quyền của anh Đệ cho anh Trí và đồng thời đề nghị anh Đệ chị Thủy lập lại ủy quyền mới cho tôi. Khi đó, chúng tôi mới được trao đổi trực tiếp với anh Đệ, chị Thủy và hiểu rõ nội dung của sự việc.
Ngoài ra, tôi khẳng định ở đây không có sự hứa thưởng hay bảo lãnh nợ gì cả vì lẽ, số tiền anh Đệ, chị Thủy mua đất của bà Nhung là _________________ đồng, và sau đó bán được ________________  đồng. Như vậy chị Thủy và anh Đệ thu được số tiền sau khi bán nhà và đất là ___________________ đồng, với số tiền này thì không thể có sự hứa thưởng với nguyên đơn được như lời nguyên đơn trình bày', N'Kính thưa Hội đồng xét xử, với tư cách là luật sư bảo vệ quyền và lợi ích hợp pháp cho bà Vương Thị Bích Thủy và ông Nguyễn Hoàng Đệ, tại phiên tòa hôm nay, tôi xin trình bày quan điểm pháp lý của tôi bảo vệ quyền lợi cho bị đơn như sau:
Thứ nhất, đối với hợp đồng vay tiền ngày 13/12/2008 giữa nguyên đơn và bị đơn được ký tại Phòng công chứng số 5 thì căn cứ điều 471 BLDS, hợp đồng nêu trên có nội dung và hình thức phù hợp quy định pháp luật
Thứ hai, đối với số tiền vay 800 triệu:

Theo lời khai của bà Lê Tuyết Nhung ngày 20/01/2011 thì bà Nhung khai rằng bà không biết gì về khoản vay 800 triệu giữa nguyên đơn và bị đơn. Như vậy nguyên đơn cho rằng 800 triệu đồng là khoản nợ mà bị đơn đứng ra bảo lãnh cho bà Nhung đối với nguyên đơn là hoàn toàn không có cơ sở tin cậy. 

Đồng thời, tại bản khai ngày 01/10/2010, người đại diện cho bà Thủy khai có sự hứa thưởng 400 triệu với nguyên đơn nhưng nội dung khai này đã được chính bà Thủy bác bỏ và cải chính tại bản khai các ngày 01/10/2010, ngày 19/4/2011. Sự việc này phù hợp với các tình tiết khác có tại hồ sơ và phù hợp với những lời trình bày của người đại diện cho bà Thủy tại phiên tòa hôm nay.

Hơn nữa, lời khai nêu trên chỉ do người đại diện của bà Thủy khai, không có sự nhất trí quan điểm của ông Đệ hoặc người đại diện của ông Đệ. Do đó, lời khai tại biên bản ngày 01/10/2010 do người đại diện cho bà Thủy khai là không có cơ sở.

Căn cứ theo lời khai của nguyên đơn tại các buổi đối chất ngày 05/01/2011 và ngày 26/4/2011 và lời khai của nguyên đơn đã được thẩm tra tại phiên tòa hôm nay thì có đủ cơ sở khẳng định nguyên đơn chưa giao số tiền cho vay 800 triệu đồng theo hợp đồng vay tiền cho bị đơn. Do đó, căn cứ 472 BLDS thì bị đơn chưa là chủ sở hữu số tiền nêu trên.

Bởi vậy cho nên hợp đồng vay tiền ngày 13/12/2008 nêu trên không là căn cứ làm phát sinh nghĩa vụ trả tiền của bị đơn đối với nguyên đơn.

Từ những phân tích nêu trên, tôi đề nghị HĐXX xem xét bác toàn bộ yêu cầu khởi kiện của nguyên đơn.', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (5, N'KDTM2014001', 1, 1, CAST(0xED370B00 AS Date), N'Đã thụ lý', N'Về tranh chấp hợp đồng cho thuê tài chính và đòi tài sản', N'Hợp đồng thuê tài chính số 096/07/ALCII-HĐ ngày 19/7/2007 và đòi nợ số tiền cọc là 1.513.000.000 đồng, tiền ký cược là 378.000.000 đồng và lãi quá hạn từ ngày 19/7/2007 đến ngày 15/03/2011 là 621.193.500 đồng', N'Tranh chấp hợp đồng và kiện đòi tài sản', N'Còn', N'', N'', N'Thu hồi:
-	tiền cọc là 1.513.000.000 đồng, 
-	tiền ký cược là 378.000.000 đồng và 
-	lãi quá hạn từ ngày 19/7/2007 đến ngày 15/03/2011 là 621.193.500 đồng 
từ ALCII.', N'Yêu cầu khởi kiện:
Nguyên đơn khởi kiện bị đơn ra trước tòa án hôm nay yêu cầu Tòa án giải quyết buộc bị đơn là công ty ALCII trả lại cho nguyên đơn số tiền sau:
-	tiền cọc là 1.513.000.000 đồng, 
-	tiền ký cược là 378.000.000 đồng và 
Cơ sở nguyên đơn yêu cầu là:
-	Hợp đồng thuê tài chính số 096/07/ALCII-HĐ ngày 19/7/2007', N'', N'', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (6, N'TLST2014002', 1, 1, CAST(0xC4370B00 AS Date), N'Đã thụ lý', N'Kiện đòi tài sản', N'Nợ gốc 352.500.000 đ + lãi 141.705.000 đ', N'Đòi tài sản', N'', N'', N'', N'1.	Đình chỉ giải quyết yêu cầu khởi kiện của nguyên đơn
2.	Chấp nhận yêu cầu phản tố của bị đơn', N'Nhân chứng vắng?
=> Hoãn phiên tòa', N'1.	Hỏi đại diện của Vĩnh
-	Kiện cá nhân Tình hay kiện công ty Tài Tình do Tình làm giám đốc? 
2.	Hỏi Nhân chứng
-	Ông/Bà cho biết những gì ông Vĩnh và ông Tình trình bày về việc giao nhận tiền giữa ông/bà với ông Tình, ông Vĩnh có đúng không?', N'', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1006, N'DSST2014002', 3, 2, CAST(0x6A380B00 AS Date), N'Đang thụ lý', N'Tranh chấp đất đai và mâu thuẫn dân sự', N'-	Đất: 911,7m2 thuộc thửa số 609 tờ bản đồ số 45 tọa lạc tại xã Khánh Bình, huyện Tân Uyên, tỉnh Bình Dương.
-	Bồi thường giá trị phần tường rào đã đập phá (25m) với số tiền là 13.725.600 đồng', N'Tranh chấp quyền sử dụng đất.', N'Tranh chấp về quyền sử dụng đất theo quy định của pháp luật về đất đai thì không áp dụng thời hiệu khởi kiện. ( Điều 159 BLTTDS) => còn', N'Hành vi bất hợp pháp của ông Mái: đập phá tường rào ranh đất do ông Lâm xây dựng', N'Ông Kia có lỗi cố ý gây ra thiệt hại về tài sản của ông Lâm. Ngoài ra tranh chấp ranh đất không có cơ sở. 
Thiệt hại xảy ra là toàn bộ tường rào của ông Lâm bị đập phá (25m).
Tự ý nhổ 32 cọc trồng cây cao su do ông Lâm trồng..', N'-	Đòi quyền sử dụng đất theo kết quả đo đạc thực tế ngày 22/02/2012 là 911,7m2 thuộc thửa số 609 tờ bản đồ số 45 tọa lạc tại xã Khánh Bình, huyện Tân Uyên, tỉnh Bình Dương có tứ cận như sau: Đông giáp đất của hộ ông Nguyễn Hà Lan và đất của bà Hoa, Tây giáp đất của vợ chồng ông Nguyễn Văn Mái, Nam giáp mương, Bắc giáp đường giao thông nông thôn.
-	Yêu cầu bồi thường thiệt hại giá trị phần tường rào đã đập phá (25m) với số tiền là 13.725.600 đồng.', N'Ông Lan là chủ sử dụng hợp pháp 20.561 m2 đất tại ấp Khánh Lộc, xã Khánh Bình, huyện Tân Uyên, tỉnh Bình Dương căn cứ GCN QSDĐ  số N 235177 do UBND huyện Tân Uyên cấp ngày 17/5/1999. Đất có nguồn gốc nhận chuyển nhượng từ ông Nhẫn năm 1993', N'Căn cứ đơn khởi kiện ngày 29/08/2011 và đơn khởi kiện bổ sung ngày và biên bản hòa giải ngày 28/10/2012, nguyên đơn ông Nguyễn Hà Lan và những người kế thừa quyền và nghĩa vụ của ông Nguyễn Hà Lan kiện ông Nguyễn Văn Mái và bà Nguyễn Thị Gòn yêu cầu Tòa án giải quyết:
1.	Buộc ông Kia, bà Gòn trả lại 911,7m2 đất lấn chiếm thuộc thửa số 609 tờ bản đồ số 45 theo kết quả đo đạc thực tế ngày 22/2/2012. 
	Đất có vị trí tứ cận Đông giáp đất ông Nguyễn Hà Lan ranh đất là hàng rào xây không tô và hàng rào kẽm gai, giáp đất của bà Hoa; Tây giáp đất ông Nguyễn Văn Kia; Nam giáp đường mương, Bắc giáp đường giao thông nông thôn.
2.	Buộc ông Kia, bà Gòn bồi thường thiệt hại phần tường rào đã đập là 3.724.000 đồng', N'1.	Tôi có đủ căn cứ khẳng định phần diện tích đất 911,7m2 bị đơn lấn chiếm thuộc thửa số 609 tờ bản đồ số 45 theo kết quả đo đạc thực tế ngày 22/2/2012 là phần đất thuộc quyền sử dụng hợp pháp của nguyên đơn.
- Phân tích các căn cứ để chứng minh chủ quyền hợp pháp thuộc về nguyên đơn, trong đó có điều kiện khách quan là ý kiến của Ủy ban (dẫn chứng đo đạc theo bản đồ không ảnh => sai lệch diện tích nhưng ranh móc được xác định bởi nhân chứng và thừa nhận của bị đơn)
- Tìm trong hồ sơ tờ khai đăng ký đất với diện tích hơn 5.400 m2 nhưng khi cấp sổ đỏ thì chỉ có 4.000 m2 => chỉ sai sót của Ủy ban', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1007, N'TLST2014003', 1, 2, CAST(0x62380B00 AS Date), N'Đang thụ lý', N'Tranh chấp thừa kế', N'Căn nhà 68 đường 15, P.4, Q.8 được cấp quyền sử dụng và quyền sở hữu ngày 19/5/2004 do Phạm Đông là đại diện thừa kế của Phạm Văn Yêu và Nguyễn Thị Hai đứng tên.', N'Thừa kế vàdsds', N'Theo khoản 1, điều 633 BLDS quy định về thời điểm mở thừa kế thì: Thời điểm mở thừa kế của ông Phạm Văn Yêu là ngày 08/02/1997 căn cứ chứng tử ngày 17/02/1997 (BL: 10) và của bà Nguyễn Thị Hai là ngày 13/9/1996 căn cứ chứng tử ngày 26/09/1996 (BL: 9).
Theo điều 645 BLDS quy định thời hiệu khởi kiện về thừa kế thì: Người thừa kế thực hiện quyền khởi kiện còn trong phạm vi thời hiệu khởi kiện, căn cứ đơn khởi kiện của Phạm Giao Thừa, Phạm Thị Thanh, Phạm Đình Lệnh ngày 12/7/2006', N'', N'', N'Chia thừa kế đối với căn nhà 68 đường 15, P.4, Q.8 theo pháp luật cho các đồng thừa kế.
Bán nhà để phân chia. dsd', N'Yêu cầu hoãn phiên tòa và triệu tập người liên quan khác tham gia tố tụng với lý do sau:
Hiện tại ông Đông quản lý, sử dụng tại căn nhà 68 đường 15, P.4, Q.8 cùng với vợ là bà _______________________ , khi giải quyết vụ án sẽ làm ảnh hưởng đến cuộc sống của vợ ông Đông. Do đó, thiết nghĩ cần thiết mời vợ ông Đông tham gia vụ án với tư cách là người có quyền lợi, nghĩa vụ liên quan trong vụ án này để lấy ý kiến vì hồ sơ hiện chưa có lời khai của vợ ông Đông cũng như chưa được mời tham gia hòa giải.', N'Nguyên đơn yêu cầu dựa trên các căn cứ sau đây:
-	Giấy chứng nhận quyền sở hữu nhà ở và quyền sử dụng đất ở số 4619/SXD ngày 19/5/2004 do Ubnd Tp.HCM cấp cho ông Phạm Đông là đại diện thừa kế của ông Phạm Văn Yêu và bà Nguyễn Thị Hai.
-	Điều 634; điểm a, khoản 1, điều 675; điểm a, khoản 1, điều 676; khoản 2, điều 676; khoản 2, điều 685 BLDS.', N'Căn cứ theo quy định tại điều 634 BLDS quy định về di sản thì có đủ cơ sở khẳng định căn nhà số 68 đường 15, P.4, Q.8 là di sản của ông Phạm Văn Yêu và bà Nguyễn Thị Hai, dựa trên cơ sở là giấy chứng nhận quyền sở hữu nhà ở và quyền sử dụng đất ở số 4619/SXD ngày 19/5/2004 do Ubnd Tp.HCM cấp cho ông Phạm Đông là đại diện thừa kế của ông Phạm Văn Yêu và bà Nguyễn Thị Hai', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1008, N'TEST201401', 1, 3, CAST(0x35370B00 AS Date), N'Đã thụ lý', N'Hồ sơ test', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1009, N'TEST201402', 1, 2, CAST(0x66370B00 AS Date), N'Đã thụ lý', N'Hồ sơ test', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1010, N'TEST201403', 1, 3, CAST(0x88340B00 AS Date), N'Đã thụ lý', N'Hồ sơ test', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1011, N'CTS031114', 4, 2, CAST(0x33390B00 AS Date), N'Đang thụ lý', N'Chia tài sản', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1012, N'YADIE2014', 1, 2, CAST(0x37390B00 AS Date), N'Đã thụ lý', N'Tranh chấp tình cảm', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1013, N'TCDN0814', 1, 2, CAST(0x6F370B00 AS Date), N'Đã thụ lý', N'Tranh chấp đánh nhau', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1014, N'HDTN281114', 3, 1, CAST(0x4C390B00 AS Date), N'Đang thụ lý', N'Hợp đồng thuê nhà', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1015, N'HDTMB010914', 1, 1, CAST(0xF4380B00 AS Date), N'Đang thụ lý', N'Sai sót hợp đồng thuê mặt bằng', N'', N'', N'', N'', N'', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1016, N'LCLD091214', 1, 1, CAST(0x57390B00 AS Date), N'Đang thụ lý', N'Lấn chiếm lòng lề đường chung', N'', N'', N'', N'', N'', N'dfdfdfdf', N'dfdf', N'dfd', N'dfdf', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1017, N'BHG041214', 1, 2, CAST(0x5C390B00 AS Date), N'Đã thụ lý', N'Kiện bán hàng giả', N'', N'', N'', N'', N'', N'dfd', N'fdfdf', N'fdfdf', N'dfd', 1)
INSERT [dbo].[Cases] ([CaseId], [CaseCode], [CreatorId], [OfficeId], [ReceiptDate], [Status], [CaseContent], [DisputeSubject], [DisputeRelation], [LimitationStatute], [LegalEvent], [ErrorFactor], [ProtectiveGoal], [OpeningProcedure], [InquiryProcedure], [ArgumentProcedure], [Active]) VALUES (1018, N'ACTS121514', 1, 2, CAST(0x5D390B00 AS Date), N'Đã thụ lý', N'Kiện ăn cắp tài sản', N'', N'', N'', N'', N'', N'efdf', N'df', N'dfd', N'df', 1)
SET IDENTITY_INSERT [dbo].[Cases] OFF
/****** Object:  Table [dbo].[Customers]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerGroupId] [int] NOT NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[Representative] [nvarchar](50) NOT NULL,
	[TaxCode] [nvarchar](14) NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[Telephone] [nvarchar](20) NULL,
	[BankAccount] [nvarchar](20) NULL,
	[BankBranch] [nvarchar](50) NULL,
	[Sex] [nvarchar](20) NULL,
	[DateOfBirth] [date] NULL,
	[IdentityNumber] [nvarchar](12) NULL,
	[IdentityDate] [date] NULL,
	[IdentityPlace] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Customers_1] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (1, 1, N'Nguyễn Văn Lượng', N'Nguyễn Văn Lượng', N'8288664590', N'23 Tô Ký, Tân Chánh Hiệp, Q12, HCM', N'fin@gmail.com', N'0977889999', N'08383836250', N'00725621001', N'TPB', N'Nam', CAST(0x87190B00 AS Date), N'136721202', CAST(0xA1350B00 AS Date), N'VP', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (2, 1, N'Maria Luisa', N'Maria Luisa', N'', N'05 Cộng Hòa, P.2, Q.Tân Bình', N'maria@gmail.com', N'0912332487', N'08103836250', N'25725621333', N'ACB', N'Nữ', CAST(0x7E070B00 AS Date), N'256727504', CAST(0x6F170B00 AS Date), N'HCM', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (3, 2, N'Công ty cổ phần tập đoàn SeaFlower', N'Trịnh Khắc Toàn', N'', N'Số 04 Nguyễn Đình Chiểu, Quận 1, TP.HCM.', N'thuhuyen@yahoo.com', N'01234567890', N'08234567890', N'01425621333', N'VCB', N'Nữ', CAST(0x8F0D0B00 AS Date), N'236773904', CAST(0x471F0B00 AS Date), N'HCM', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (4, 1, N'Nguyễn Thị Hoa', N'Nguyễn Thị Huệ', N'', N'81 Đinh Bộ Lĩnh, P.26, Q.BThạnh, HCM', N'nguyenhue@gmail.com', N'0982885334', N'', N'', N'', N'Nữ', CAST(0xF10A0B00 AS Date), N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (5, 1, N'Vương Thùy Dương', N'Nguyễn Thị Hòa Thảo', N'', N'30A ấp Thống Nhất I, Tân Thới Nhì, Hóc Môn', N'saphie@yahoo.com', N'0903346932', N'', N'', N'', N'Nữ', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (6, 1, N'Nguyễn Duy Hoàng', N'Nguyễn Thị Hòa Thảo', N'', N'30A ấp Thống Nhất I, Tân Thới Nhì, Hóc Môn', N'saphie@yahoo.com', N'0903346932', N'', N'', N'', N'Nữ', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (8, 2, N'CTY TNHH DV TM PHÁT ĐẠT', N'Nguyễn Thị Thu Hương', N'', N'339 Trần Xuân Soạn, Kp4, P.Tân Hưng, Q.7', N'phatdat@gmail.com', N'0903965833', N'', N'', N'', N'', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (9, 1, N'Nguyễn Trọng Tình', N'Nguyễn Trọng Tình', N'', N'156 xã Hội Nghĩa, huyện Tân Uyên, tỉnh Bình Dương', N'tinh1980@yahoo.com', N'0907080222', N'', N'', N'', N'', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (40, 3, N'Trần Văn Nam', N'Trần Văn Nam', N'', N'Phạm Hùng - Phường 9 - TP. Vĩnh Long - tỉnh Vĩnh Long', N'namtv99@gmail.com', N'09761222442', N'0703824564', N'92389898932', N'VCB', N'Nam', CAST(0x8D0E0B00 AS Date), N'829839828', CAST(0xE51C0B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (50, 2, N'Lê Thị Thu', N'Lê Thị Thu', N'', N'Số 33/1, đường 3/2, Phường 1, Thành phố Vĩnh Long, tỉnh Vĩnh Long', N'ntthu234@gmail.com', N'09923723732', N'0720632673', N'87349823492', N'AGB', N'Nữ', CAST(0xD8190B00 AS Date), N'249234883', CAST(0x09240B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (60, 1, N'Phạm Thành Nam', N'Phạm Thành Nam', N'', N'Số 24, Đường 3 tháng 2, Phường 1 - TPVL - Vĩnh Long', N'ares3451@gmail.com', N'0973223737', N'0702387823', N'78345878737', N'TPB', N'Nam', CAST(0x3C160B00 AS Date), N'982389239', CAST(0x51210B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (80, 2, N'Hoàng Văn Vương', N'Hoàng Văn Vương', N'', N'79 nguyễn huệ p2 tpvl', N'kakababa@gmail.com', N'0937328273', N'0701272348', N'73278734823', N'AGB', N'Nam', CAST(0x060C0B00 AS Date), N'839823988', CAST(0x00200B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (90, 1, N'Tạ Thị Loan', N'Tạ Thị Loan', N'', N'26 Đinh Tiên Hoàng, P 8, Thị Xã Vĩnh Long.', N'lona341@gmail.com', N'0828218238', N'0720237383', N'02398234732', N'ACB', N'Nữ', CAST(0x53170B00 AS Date), N'329823489', CAST(0xD8220B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (100, 3, N'Lê Thị Thu Huyền', N'Lê Thị Thu Huyền', N'', N'3A Hưng Đạo Vương, P.1, TP. Vĩnh Long', N'hongha123@gmail.com', N'0938323777', N'0702378237', N'98439239823', N'AGB', N'Nữ', CAST(0x45130B00 AS Date), N'234732887', CAST(0xBD220B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (120, 2, N'Trịnh Hoài Diễm', N'Trịnh Hoài Diễm', N'', N'2A Hùng Vương, Phường 1, Thành phố Vĩnh Long', N'diemth@gmail.com', N'0927278298', N'0702628727', N'23893747837', N'ACB', N'Nữ', CAST(0x2A130B00 AS Date), N'982382398', CAST(0x8B210B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (130, 3, N'Hoàng Văn Quyền', N'Hoàng Văn Quyền', N'', N'166, Phạm Hùng,Phường 9, Thành phố Vĩnh Long', N'quyennn234@gmai.com', N'0932738388', N'0732872398', N'98397783337', N'TPB', N'Nam', CAST(0xD2180B00 AS Date), N'232398398', CAST(0x3C240B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (140, 1, N'Ngô Văn Linh', N'Ngô Văn Linh', N'', N'Số 26 đường 3/2, P. 1, Tx. Vĩnh Long, Tỉnh Vĩnh Long.', N'linhnv123@gmail.com', N'0927472728', N'0723723872', N'9834723737', N'AGB', N'Nam', CAST(0x5D0D0B00 AS Date), N'943898998', CAST(0xF7190B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (150, 1, N'Nguyễn Hoàng Bách', N'Nguyễn Hoàng Bách', N'', N'35 đường 2 tháng 9, phường 1, thành phố Vĩnh Long', N'bachala@gmail.com', N'0927373738', N'0738292398', N'9323739298', N'ACB', N'Nam', CAST(0x42140B00 AS Date), N'982389838', CAST(0xEE190B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (160, 2, N'Phạm Anh Dương', N'Phạm Anh Dương', N'', N' 103 đường 3 tháng. P. 1. Thành phố Vĩnh Long', N'duongpa@gmail.com', N'0926278722', N'0726236236', N'98239839898', N'TPB', N'Nam', CAST(0xF7190B00 AS Date), N'383899898', CAST(0x792C0B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (170, 1, N'Lê Anh Thơ', N'Lê Anh Thơ', N'', N'Số 7B Hưng Đạo Vương - Phường 1. TX. Vĩnh Long - Tỉnh Vĩnh Long', N'alisia999@gmail.com', N'09738727878', N'0732676732', N'87238787287', N'AGB', N'Nữ', CAST(0xB3140B00 AS Date), N'983487783', CAST(0x9F290B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (180, 1, N'Phạm Ngọc Tích', N'Phạm Ngọc Tích', N'', N'13 nguyen hue phuong 2 tp vinh long', N'tichchu123@gmail.com', N'09273783278', N'0736267236', N'32873878738', N'ACB', N'Nam', CAST(0x51170B00 AS Date), N'983988787', CAST(0xD8220B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (190, 3, N'Vũ Ngọc Minh', N'Vũ Ngọc Minh', N'', N'Cù Lao An Bình , Ấp An Thuận - Xã An Bình - Huyện Long Hồ - Tĩnh Vĩnh Long', N'minhvn992@gmail.com', N'09272727278', N'0748738227', N'73487378732', N'ACB', N'Nam', CAST(0x08130B00 AS Date), N'982787817', CAST(0xBB220B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (200, 3, N'Lê Trọng Hưng', N'Lê Trọng Hưng', N'', N'số 37 Hưng Quới, Thanh Đúc, Long Hồ, Vĩnh Long', N'hungkk@gmail.com', N'09872343277', N'0721766721', N'72823989898', N'TPB', N'Nam', CAST(0xE80B0B00 AS Date), N'389883839', CAST(0xE11F0B00 AS Date), N'VL', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (1007, 1, N'Nguyễn Hà Lan', N'Nguyễn Hà Lan', N'', N'ấp Khánh Lộc, xã Khánh Bình, huyện Tân Uyên, tỉnh Bình Dương', N'', N'0977245887', N'', N'', N'', N'', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (1008, 1, N'Nguyễn Thị Xuống', N'Nguyễn Thị Xuống', N'', N'Tổ 1, ấp 3, xã Bạch Đằng, huyện Tân Uyên, Bình Dương', N'', N'0903446686', N'', N'', N'', N'Nữ', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (1009, 1, N'Phạm Thị Thanh', N'Phạm Thị Thanh', N'', N'111 C C/c Phạm Thế Hiển, P.4, Q.8', N'', N'0937367544', N'', N'', N'', N'Nữ', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (1010, 4, N'DAD', N'ADADA', N'', N'CDFDF', N'', N'12232312131', N'', N'', N'', N'', NULL, N'', NULL, N'', 1)
INSERT [dbo].[Customers] ([CustomerId], [CustomerGroupId], [CustomerName], [Representative], [TaxCode], [Address], [Email], [Mobile], [Telephone], [BankAccount], [BankBranch], [Sex], [DateOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [Active]) VALUES (1011, 1, N'wewe', N'wewe', N'', N'32323', N'', N'232', N'', N'', N'', NULL, NULL, N'343434', NULL, N'3434', 0)
SET IDENTITY_INSERT [dbo].[Customers] OFF
/****** Object:  Table [dbo].[OtherCost]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OtherCost](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfficeId] [int] NOT NULL,
	[Cost] [float] NOT NULL,
	[Date] [date] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_OtherCost] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OtherCost] ON
INSERT [dbo].[OtherCost] ([Id], [OfficeId], [Cost], [Date], [Description]) VALUES (2, 1, 200000, CAST(0x85380B00 AS Date), N'Mua giấy A4')
INSERT [dbo].[OtherCost] ([Id], [OfficeId], [Cost], [Date], [Description]) VALUES (3, 1, 100000, CAST(0x85380B00 AS Date), N'In tài liệu')
INSERT [dbo].[OtherCost] ([Id], [OfficeId], [Cost], [Date], [Description]) VALUES (4, 1, 300000, CAST(0x85380B00 AS Date), N'Mua mực in')
SET IDENTITY_INSERT [dbo].[OtherCost] OFF
/****** Object:  Table [dbo].[Staffs]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staffs](
	[StaffId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[StaffGroupId] [int] NOT NULL,
	[StaffName] [nvarchar](50) NOT NULL,
	[Position] [nvarchar](50) NULL,
	[Avatar] [nvarchar](100) NULL,
	[Sex] [nvarchar](30) NULL,
	[DateOfBirth] [date] NOT NULL,
	[PlaceOfBirth] [nvarchar](100) NULL,
	[IdentityNumber] [nvarchar](12) NOT NULL,
	[IdentityDate] [date] NOT NULL,
	[IdentityPlace] [nvarchar](50) NOT NULL,
	[TaxCode] [nvarchar](14) NULL,
	[BankAccount] [nvarchar](20) NULL,
	[BankBranch] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Mobile] [nvarchar](20) NOT NULL,
	[Telephone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[AppendantPeople] [int] NULL,
 CONSTRAINT [PK_Staffs] PRIMARY KEY CLUSTERED 
(
	[StaffId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Staffs] ON
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (1, 1, 10, N'Nguyễn Hòa Thuận', N'Trưởng văn phòng', N'/Content/avatars/thuannh.jpg', N'Nam', CAST(0x7C0A0B00 AS Date), N'Vĩnh Long', N'245621204', CAST(0x06270B00 AS Date), N'Vĩnh Long', N'8183264675', N'05355621238', N'VCB', N'58 Mậu Thân, khóm 1, phường 3, thành phố Vĩnh Long', N'0988816668', N'', N'info@luatthuannguyen.com', 1, 2)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (2, 1, 4, N'Nguyễn Huỳnh Kim Ngân', N'Nhân viên chăm sóc khách hàng', N'/Content/avatars/nganhtk.jpg', N'Nữ', CAST(0x65130B00 AS Date), N'Vĩnh Long', N'247139204', CAST(0xC4290B00 AS Date), N'Vĩnh Long', N'', N'', N'', N'Số 2/7A đường Mậu Thân, khóm 1, phường 3, thành phố Vĩnh Long', N'0974332467', N'0703823100', N'info@luatthuannguyen.com', 1, 2)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (3, 2, 10, N'Lê Văn Dụng', N'Luật sư thành viên', N'/Content/avatars/dunglv.jpg', N'Nam', CAST(0x9A0A0B00 AS Date), N'Đà Nẵng', N'135624793', CAST(0xA7210B00 AS Date), N'Đà Nẵng', N'', N'', N'', N'Hải Đông, Sơn Trà, Đà Nẵng', N'0903311879', N'', N'lsdung@luatthuannguyen.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (4, 2, 2, N'Nguyễn Thị Hòa Thảo', N'Luật sư', N'/Content/avatars/no-avatar.png', N'Nữ', CAST(0x150D0B00 AS Date), N'Vĩnh Long', N'142445438', CAST(0x56230B00 AS Date), N'Vĩnh Long', N'', N'', N'', N'Số 88, Hoàng Thái Hiếu, Phường 1, TPVL', N'0913889233', N'0703825531', N'thao@luatthuannguyen.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (5, 3, 4, N'Nguyễn Hữu Tiếng', N'Chuyên viên', N'/Content/avatars/tiengnh.jpg', N'Nam', CAST(0xCE140B00 AS Date), N'Gia Lai', N'258741785', CAST(0xBC290B00 AS Date), N'Gia Lai', N'', N'', N'', N'Khóm 16,Thị trấn An Khê, Tỉnh Gia Lai', N'0902345537', N'0596250884', N'tieng@luatthuannguyen.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (6, 3, 4, N'Lương Thị Kim Anh', N'Kế toán tổng hợp', N'/Content/avatars/anhltk.jpg', N'Nữ', CAST(0xCD170B00 AS Date), N'Kiên Giang', N'225995339', CAST(0xBA2C0B00 AS Date), N'Kiên Giang', N'', N'', N'', N'Xã Dương Hòa, huyện Kiên Lương, tỉnh Kiên giang', N'01992448593', N'0773861615', N'anh@luatthuannguyen.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (7, 2, 10, N'Nguyễn Ngọc Tàu', N'Trưởng Văn phòng', N'/Content/avatars/taunn.jpg', N'Nam', CAST(0x7F070B00 AS Date), N'Vĩnh Long', N'146568448', CAST(0x7C220B00 AS Date), N'Vĩnh Long', N'', N'', N'', N'Phạm Hùng - Phường 9 - TP. Vĩnh Long', N'0909115577', N'', N'nguyentau@thuaphatlaivinhlong.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (8, 3, 4, N'Nguyễn Thành Tín', N'Thành viên hợp danh', N'/Content/avatars/tinnt.jpg', N'Nam', CAST(0xC4100B00 AS Date), N'Vĩnh Long', N'245483233', CAST(0x07240B00 AS Date), N'Vĩnh Long', N'', N'', N'', N'Số 24, Đường 3 tháng 2, Phường 1 - TPVL', N'0978224446', N'', N'thanhtin@thuaphatlaivinhlong.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (9, 3, 6, N'Nguyễn Phi Long', N'Thư ký nghiệp vụ', N'/Content/avatars/longnp.jpg', N'Nam', CAST(0x9E160B00 AS Date), N'Cần Thơ', N'157687633', CAST(0x782F0B00 AS Date), N'Cần Thơ', N'', N'', N'', N'59A, Phường Hung Loi, Quận Ninh Kiều, Cần Thơ', N'0912355854', N'', N'longnguyen@thuaphatlaivinhlong.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (10, 3, 6, N'Ngô Văn Tùng', N'Thư ký nghiệp vụ', N'/Content/avatars/tungnv.jpg', N'Nam', CAST(0xEA150B00 AS Date), N'Cần Thơ', N'159653463', CAST(0xD2310B00 AS Date), N'Cần Thơ', N'', N'', N'', N'02 Hai Bà Trưng - Q. Ninh Kiều - TP. Cần Thơ', N'0914463777', N'07103824583', N'tungngo@thuaphatlaivinhlong.com', 1, 1)
INSERT [dbo].[Staffs] ([StaffId], [RoleId], [StaffGroupId], [StaffName], [Position], [Avatar], [Sex], [DateOfBirth], [PlaceOfBirth], [IdentityNumber], [IdentityDate], [IdentityPlace], [TaxCode], [BankAccount], [BankBranch], [Address], [Mobile], [Telephone], [Email], [Active], [AppendantPeople]) VALUES (11, 1, 2, N'sdsdsdsd', N'dsd', N'/Content/avatars/no-avatar.png', N'Nam', CAST(0x59390B00 AS Date), N'wewe', N'234343', CAST(0x59390B00 AS Date), N'23232', N'23232', N'23232343', N'ssdsd', N'dsds', N'2323232', N'232323', N'duypvse60634@fpt.edu.vn', 0, 2)
SET IDENTITY_INSERT [dbo].[Staffs] OFF
/****** Object:  Table [dbo].[Services]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Services] ON
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (1, 8, N'Tư vấn lập di chúc', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (2, 7, N'Tư vấn quyền sử dụng đất ', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (3, 3, N'Kiện đòi tài sản', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (4, 3, N'Giải quyết tranh chấp thừa kế', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (5, 8, N'Tư vấn quyền sử dụng và kiện đòi tài sản', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (6, 8, N'Tư vấn vi phạm quyền sở hữu trí tuệ', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (7, 3, N'Đòi bồi thường tai nạn giao thông', N'Bồi thường tai nạn giao thông')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (8, 3, N'Xung đột đánh nhau', N'')
INSERT [dbo].[Services] ([ServiceId], [ServiceTypeId], [ServiceName], [Description]) VALUES (9, 6, N'Tranh chấp tài sản thừa kế', N'')
SET IDENTITY_INSERT [dbo].[Services] OFF
/****** Object:  Table [dbo].[UsedServices]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsedServices](
	[UsedServiceId] [int] IDENTITY(1,1) NOT NULL,
	[CaseId] [int] NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[ServiceCost] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[RegisteredDate] [date] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[UsedServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UsedServices] ON
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (1, 3, N'Tư vấn quyền sử dụng và kiện đòi tài sản', 400000111, N'Tư vấn lần 1', CAST(0x77380B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (2, 1006, N'Tư vấn quyền sử dụng và kiện đòi tài sản', 7878, N'y78y78', CAST(0x89380B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (3, 6, N'Tư vấn lập di chúc', 121212121, N'1212', CAST(0x8D380B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (4, 3, N'Tư vấn vi phạm quyền sở hữu trí tuệ', 12121, N'121212', CAST(0x8D380B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (5, 3, N'Tư vấn vi phạm quyền sở hữu trí tuệ', 10000000, N'thanh toán tiếp', CAST(0x64380B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (6, 5, N'Tư vấn quyền sử dụng và kiện đòi tài sản', 100000000, N'tranh chấp đòi tài sản', CAST(0xE5370B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (7, 6, N'Giải quyết tranh chấp thừa kế', 500000, N'a', CAST(0x49390B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (8, 6, N'Kiện đòi tài sản', 100000, N'b', CAST(0x49390B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (9, 1007, N'Giải quyết tranh chấp thừa kế', 343434, N'fgfg', CAST(0x4C390B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (10, 1007, N'Giải quyết tranh chấp thừa kế', 232323, N'wew', CAST(0x57390B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (12, 1018, N'Giải quyết tranh chấp thừa kế', 34343434, N'dffdf', CAST(0x5D390B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (13, 2, N'Đòi bồi thường tai nạn giao thông', 2000000, N'Đòi bồi thường nhanh', CAST(0x2D380B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (14, 1013, N'Xung đột đánh nhau', 3000000, N'Đánh nhau giữa hai người', CAST(0x6F370B00 AS Date))
INSERT [dbo].[UsedServices] ([UsedServiceId], [CaseId], [ServiceName], [ServiceCost], [Description], [RegisteredDate]) VALUES (15, 1, N'Tranh chấp tài sản thừa kế', 4000000, N'Tài sản thừa kế của ông bà', CAST(0x19390B00 AS Date))
SET IDENTITY_INSERT [dbo].[UsedServices] OFF
/****** Object:  Table [dbo].[Subjects]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[CaseId] [int] NOT NULL,
	[SubjectName] [nvarchar](50) NOT NULL,
	[LitigationCapacity] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1, 2, N'Trần Cao Cường', N'Bị đơn', N'0862726182', N'', N'41 Đường số 9, Khu Đô thị Him Lam, P.Tân Hưng, Q.7')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (2, 2, N'Đinh Thị Hương', N'Nhân Chứng', N'0906801606', N'', N'18bis Nguyễn Thị Minh Khai, P.Đa Kao, Q.1')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (3, 2, N'Hồ Quốc Đạt', N'Nguyên đơn', N'0903462365', N'quocdat@yahoo.com', N'35/5A, Trần Đình Xu, Q.1, Tp.HCM')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (4, 2, N'Trần Đại Chí', N'Người liên quan', N'0823554467', N'', N'27 Trần Thiện Chánh, P.12, Q.10, Tp.HCM')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (5, 1, N'Hồ Hoài Nam', N'Bị đơn', N'', N'hoainam83@gmail.com', N'12 AB Khu 1 Thanh Đa, P.27, Q.Bình Thạnh, Tp.HCM')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (6, 1, N'Bùi Minh Tuấn', N'Nguyên đơn', N'', N'', N'12AB Thanh Đa, P.27, Q.Bình Thạnh, Tp.HCM')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (7, 3, N'Nguyễn Văn Lượng', N'Bị đơn', N'', N'', N'Cẩm Phả, Xuân Mỹ, Cẩm Mỹ, Đà Nẵng')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (8, 3, N'Nguyễn Công Bằng', N'Người ủy quyền', N'', N'', N'35 QL 13, P.26, Q.BThạnh, HCM')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (9, 4, N'Phạm Thu Hiền', N'Nguyên đơn', N'', N'', N'Ấp Cồn Dầu, Dân Thành, Duyên Hải, Trà Vinh')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (10, 5, N'CTY CHO THUÊ TÀI CHÍNH II', N'Bị đơn', N'', N'', N'115 Trần Hưng Đạo, P.2, Q.5')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (11, 6, N'Ngô Ngọc Vĩnh', N'Nguyên đơn', N'', N'', N'353/63, tổ 8, kp 2, tt.UH, TU, BD')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (12, 6, N'Phạm Thị Hiền', N'Nhân chứng', N'', N'', N'Kp2, TT.UH, H.TU, BD')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (13, 6, N'Phạm Thị Ngọc', N'Nhân chứng', N'', N'', N'Kp2, TT.UH, H.TU, BD')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (14, 6, N'Đỗ Ngọc Tuyết', N'Nhân chứng', N'', N'', N'Kp2, TT.UH, H.TU, BD')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1010, 1006, N'Nguyễn Văn Kia', N'Bị đơn', N'', N'', N'ấp Khánh Lộc, xã Khánh Bình, huyện Tân Uyên, tỉnh Bình Dương')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1011, 1006, N'Nguyễn Thị Gòn', N'Bị đơn', N'', N'', N'ấp Khánh Lộc, xã Khánh Bình, huyện Tân Uyên, tỉnh Bình Dương')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1012, 1006, N'Nguyễn Văn Cư', N'Nhân chứng', N'', N'', N'ấp Khánh Tân, xã Khánh Bình, huyện Tân Uyên')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1013, 1007, N'Phạm Đình Lệnh', N'Nguyên đơn', N'0918137433', N'', N'C222 C/c An Dương Vương, P.16, Q.8')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1014, 1007, N'Phạm Đông', N'Bị đơn', N'', N'', N'69 đường 15, P.4, Q.8')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1015, 1007, N'dsdsd', N'wwew', N'2323232', N'dfdf@yaho.com', N'dfdfdf')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1016, 1018, N'f', N'ff', N'232323232', N'dsd@yahoo.com', N'sewewe')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1017, 1013, N'Hồ Đắc Nghĩa', N'Bên tố cáo', N'01826352617', N'nghia4hd@yahoo.com', N'Đại học FPT KTX')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1018, 1013, N'Đặng Nguyễn Khiêm', N'Bên bị cáo', N'02381726354', N'Khiem@yahoo.com', N'7 Đường Láng, Thanh Xuân Hà hội')
INSERT [dbo].[Subjects] ([SubjectId], [CaseId], [SubjectName], [LitigationCapacity], [PhoneNumber], [Email], [Address]) VALUES (1019, 1013, N'Trần Anh Tuấn', N'Người chứng kiến', N'023948572625', N'Tuantt@yahoo.com', N'9 Đường Làng Thanh Xuân Hà nội')
SET IDENTITY_INSERT [dbo].[Subjects] OFF
/****** Object:  Table [dbo].[Payments]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[CaseId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[PaymentTime] [datetime] NOT NULL,
	[PaymentMoney] [int] NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Payments] ON
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (1, 3, N'Thanh toán tư vấn', CAST(0x0000A31C00000000 AS DateTime), 50000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (2, 5, N'thanh toán tranh chấp đòi tài sản', CAST(0x0000A29100000000 AS DateTime), 100000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (5, 1008, N'demo3', CAST(0x0000A15900000000 AS DateTime), 3000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (6, 1008, N'demo4', CAST(0x0000A21400000000 AS DateTime), 900000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (10, 3, N'demo8', CAST(0x0000A2A600000000 AS DateTime), 9000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (11, 3, N'demo9', CAST(0x0000A23400000000 AS DateTime), 15000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (12, 3, N'demo10', CAST(0x0000A32200000000 AS DateTime), 50000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (13, 1008, N'demo11', CAST(0x0000A32200000000 AS DateTime), 40000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (18, 1008, N'demo15', CAST(0x0000A1F400000000 AS DateTime), 55000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (19, 1008, N'demo16', CAST(0x0000A21400000000 AS DateTime), 65000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (23, 1008, N'demo20', CAST(0x00009CF100000000 AS DateTime), 30000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (24, 1008, N'demo21', CAST(0x00009E5E00000000 AS DateTime), 45000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (25, 1008, N'demo22', CAST(0x00009FCB00000000 AS DateTime), 66000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (26, 6, N'Thanh toán trước', CAST(0x0000A3EE00000000 AS DateTime), 400000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (27, 6, N'thanh toán sau', CAST(0x0000A3EE00000000 AS DateTime), 3000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (28, 1007, N'erer', CAST(0x0000A3F100000000 AS DateTime), 44343)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (29, 1007, N'esew', CAST(0x0000A3FC00000000 AS DateTime), 232)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (30, 1018, N'dfdf', CAST(0x0000A40200000000 AS DateTime), 34343434)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (31, 2, N'Thanh toán đủ', CAST(0x0000A37900000000 AS DateTime), 2000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (32, 1013, N'Đặt cọc trước', CAST(0x0000A21500000000 AS DateTime), 1000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (33, 1013, N'Thanh toán nốt', CAST(0x0000A23700000000 AS DateTime), 2000000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (34, 1, N'Đặt cọc tiền làm hồ sơ', CAST(0x0000A3BE00000000 AS DateTime), 1500000)
INSERT [dbo].[Payments] ([PaymentId], [CaseId], [Description], [PaymentTime], [PaymentMoney]) VALUES (35, 1, N'Thanh toán tất cả còn nợ', CAST(0x0000A3D500000000 AS DateTime), 2500000)
SET IDENTITY_INSERT [dbo].[Payments] OFF
/****** Object:  Table [dbo].[OperationalEvents]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationalEvents](
	[OperationalEventId] [int] IDENTITY(1,1) NOT NULL,
	[CaseId] [int] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[BeginTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_OperationalEvents] PRIMARY KEY CLUSTERED 
(
	[OperationalEventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[OperationalEvents] ON
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1, 3, 1, N'Khởi kiện', CAST(0x00009E1B00000000 AS DateTime), CAST(0x00009E1B00000000 AS DateTime), N'X&aacute;c định lại y&ecirc;u cầu khởi kiện')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (2, 3, 1, N'Phản tố', CAST(0x00009E6000000000 AS DateTime), CAST(0x00009E6000000000 AS DateTime), N'B&uacute;t lục 62
<ol>
	<li>B&agrave; Huệ trả lại 3 sổ đỏ đang chiếm giữ;</li>
	<li>B&agrave; Huệ trả lại 30 triệu đồng đ&atilde; cho mượn;</li>
	<li>Đồng &yacute; trả lại số tiền 358 triệu đồng đ&atilde; mượn của b&agrave; Huệ.</li>
</ol>
Kh&ocirc;ng đồng &yacute; tất cả c&aacute;c y&ecirc;u cầu của nguy&ecirc;n đơn - b&agrave; Huệ')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (3, 3, 1, N'Đối chất', CAST(0x0000A1EA00000000 AS DateTime), CAST(0x0000A1EA00000000 AS DateTime), N'C&aacute;c b&ecirc;n giữ nguy&ecirc;n &yacute; kiến. C&aacute;c con của bị đơn chỉ c&oacute; c&ocirc;ng sức đ&oacute;ng g&oacute;p cho gia đ&igrave;nh trong thời gian chung sống nhưng kh&ocirc;ng phải l&agrave; đ&oacute;ng g&oacute;p để tạo dựng t&agrave;i sản m&agrave; chỉ l&agrave; đ&oacute;ng g&oacute;p phụ gi&uacute;p cha mẹ c&ocirc;ng việc gia đ&igrave;nh n&ecirc;n kh&ocirc;ng y&ecirc;u cầu g&igrave;.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (4, 3, 5, N'Kết quả xác minh', CAST(0x0000A1FD00000000 AS DateTime), CAST(0x0000A1FD00000000 AS DateTime), N'Bi&ecirc;n bản x&aacute;c minh ng&agrave;y 16/7/2008 với &ocirc;ng Trần C&ocirc;ng Đồng &ndash; chuy&ecirc;n vi&ecirc;n Chi cục thuế huyện Cẩm Mỹ:
<ul>
	<li>Diện t&iacute;ch do &ocirc;ng Ng&acirc;n k&ecirc; khai ng&agrave;y 30/10/1992 kh&ocirc;ng được r&otilde; v&igrave; c&oacute; dấu hiệu tẩy x&oacute;a;</li>
	<li>Bi&ecirc;n lai số 053538 lập ng&agrave;y 30/10/1992 của Ubnd x&atilde; Xu&acirc;n Mỹ l&agrave; tiền ph&uacute;c lợi x&atilde; hội, kh&ocirc;ng li&ecirc;n quan tiền thuế đất</li>
</ul>
')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (5, 3, 5, N'Kết quả giám định', CAST(0x0000A21A00000000 AS DateTime), CAST(0x0000A21A00000000 AS DateTime), N'<strong>Định gi&aacute;</strong>:<br />
Bi&ecirc;n bản định gi&aacute; ng&agrave;y 07/10/2010 v&agrave; điều chỉnh kết quả định gi&aacute; tại ấp Cẩm Sơn, x&atilde; Xu&acirc;n Mỹ, huyện Cẩm Mỹ, Đồng Nai:
<ul>
	<li>C&acirc;y trồng: 2.782.000 đ</li>
	<li>Nh&agrave; ở ch&iacute;nh: 140.587.000 đ</li>
	<li>Nh&agrave; ở (con g&aacute;i): 12.916.000 đ</li>
	<li>Nh&agrave; kho: 6.556.000 đ</li>
	<li>H&agrave;ng r&agrave;o: 6.540.000 đ</li>
	<li>M&aacute;i hi&ecirc;n che tạm: 3.350.000 đ</li>
	<li>Nền xi măng dưới m&aacute;i hi&ecirc;n: 1.005.000 đ</li>
	<li>Giếng d&ugrave;ng chung: 984.000 đ</li>
	<li>Đất (2 thửa): 399.020.000 đ</li>
</ul>
Tổng gi&aacute; trị t&agrave;i sản: <strong>573.740.000 đ</strong><br />
&nbsp;<br />
Bi&ecirc;n bản định gi&aacute; ng&agrave;y 07/10/2010 v&agrave; điều chỉnh kết quả định gi&aacute; tại ấp T&acirc;n H&ograve;a, x&atilde; Bảo B&igrave;nh, huyện Cẩm Mỹ, Đồng Nai:
<ul>
	<li>Nh&agrave; ở: 11.382.000 đ</li>
	<li>Ch&aacute;i bếp: 1.875.000 đ</li>
	<li>Tường: 720.000 đ</li>
	<li>C&acirc;y trồng: 221.762.500 đ</li>
	<li>Giếng: 6.060.000 đ</li>
	<li>Đất: 645.000.000 đ</li>
</ul>
Tổng gi&aacute; trị t&agrave;i sản: <strong>886.799.500 </strong>')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (6, 3, 1, N'Hòa giải', CAST(0x0000A26C00000000 AS DateTime), CAST(0x0000A26C00000000 AS DateTime), N'Ho&atilde;n phi&ecirc;n h&ograve;a giải v&igrave; bị đơn, đại diện bị đơn, &ocirc;ng L&yacute; v&agrave; 7 người con bị đơn vắng mặt.<br />
H&ograve;a giải kh&ocirc;ng th&agrave;nh')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (7, 3, 1, N'QĐXX sơ thẩm', CAST(0x0000A28600000000 AS DateTime), CAST(0x0000A28600000000 AS DateTime), N'Quyết định đưa vụ &aacute;n ra x&eacute;t xử số 86/2010/DSST-QĐ mở phi&ecirc;n t&ograve;a l&uacute;c 13 giờ 30 ph&uacute;t ng&agrave;y 29/12/2013')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (8, 3, 1, N'Phiên tòa sơ thẩm', CAST(0x0000A2A300000000 AS DateTime), CAST(0x0000A2A300000000 AS DateTime), N'Ho&atilde;n v&igrave; những người li&ecirc;n quan l&agrave; c&aacute;c con &ocirc;ng Ng&acirc;n, b&agrave; Th&uacute;y vắng mặt')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (9, 5, 1, N'Khởi kiện', CAST(0x0000A26A00000000 AS DateTime), CAST(0x0000A26A00000000 AS DateTime), N'')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (10, 5, 1, N'Hòa giải', CAST(0x0000A2B200000000 AS DateTime), CAST(0x0000A2B300000000 AS DateTime), N'Kh&ocirc;ng th&agrave;nh')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (11, 6, 1, N'Khởi kiện', CAST(0x00009E2000000000 AS DateTime), CAST(0x0000A29E00000000 AS DateTime), N'Kiện đ&ograve;i 317.000.000 đ + l&atilde;i từ 06/12/2005<br />
Kiện đ&ograve;i 302.400.000 đ + l&atilde;i từ 06/12/2005<br />
Bổ sung đ&ograve;i 363.500.000 đồng<br />
Bổ sung đ&ograve;i 494.205.000 đ = gốc 352.500.000 đ + l&atilde;i 141.705.000 đ')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (12, 6, 1, N'Phản tố', CAST(0x00009FF700000000 AS DateTime), CAST(0x0000A07600000000 AS DateTime), N'Đ&ograve;i Vấn trả 987.730.000 đ + l&atilde;i qu&aacute; hạn')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (13, 6, 4, N'Hòa giải', CAST(0x0000A29700000000 AS DateTime), CAST(0x0000A2A500000000 AS DateTime), N'Kh&ocirc;ng th&agrave;nh<br />
Vấn đ&ograve;i 352.500.000 đ + l&atilde;i từ 06/12/2005 đến ng&agrave;y xử sơ thẩm<br />
Căn cứ:
<ol>
	<li>Bi&ecirc;n bản giao nhận 06/12/2005 Vấn giao cho T&agrave;i 425.000.000 đ</li>
	<li>Bi&ecirc;n bản h&ograve;a giải ng&agrave;y 10/01/2007 tại VKS H.PG giữa T&agrave;i v&agrave; Vấn, Vấn trả cho Nguyễn Thị Hoa 30 triệu.</li>
</ol>
(1)+(2) =&gt; T&agrave;i nhận của Vấn 455.000.000 đ<br />
Tổng tiền T&agrave;i đ&atilde; thực hiện nghĩa vụ li&ecirc;n đới đối với 3 bản &aacute;n tr&ecirc;n l&agrave; 102.500.000 đ
<ul>
	<li>T&agrave;i nợ Vấn 352.500.000 đ</li>
</ul>
')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (14, 6, 4, N'Phiên tòa sơ thẩm', CAST(0x0000A2B700000000 AS DateTime), CAST(0x0000A2E200000000 AS DateTime), N'Ho&atilde;n theo y&ecirc;u cầu luật sư Thuận')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1009, 3, 1, N'Thu thập thông tin hồ sơ án', CAST(0x00009DF900000000 AS DateTime), CAST(0x00009DF900000000 AS DateTime), N'Khoảng năm 1992 &ndash; 1997, b&agrave; Hoa bỏ tiền mua thửa đất c&oacute; diện t&iacute;ch 1.211 m<sup>2</sup> nằm dọc quốc lộ 56 tọa lạc tại ấp Cẩm Sơn, x&atilde; Xu&acirc;n Mỹ, huyện Cẩm Mỹ, Đồng Nai, tr&ecirc;n đất c&oacute; một căn nh&agrave; n&aacute;t.<br />
Qu&aacute; tr&igrave;nh t&igrave;m đất do chị g&aacute;i của b&agrave; Hoa l&agrave; b&agrave; Thu&yacute; v&agrave; anh rể của b&agrave; Hoa l&agrave; &ocirc;ng Lượng t&igrave;m, khi chọn được đất th&igrave; b&agrave; Hoa l&agrave; người đứng ra mặc cả gi&aacute; cả với b&ecirc;n b&aacute;n, gi&aacute; mua b&aacute;n được thống nhất l&agrave; 5,3 c&acirc;y v&agrave;ng.<br />
B&agrave; Hoa gửi v&agrave;ng cho mẹ - b&agrave; Nguyễn Thị Ph&aacute;i để đưa cho &ocirc;ng Lượng, b&agrave; Th&uacute;y trả tiền mua đất.<br />
Sau khi mua b&agrave; Hoa để cho &ocirc;ng Lượng, b&agrave; Thu&yacute; đứng t&ecirc;n chủ quyền nh&agrave;, đất v&agrave; được hưởng hoa lợi từ việc thu hoạch hoa m&agrave;u, khi cần lấy lại nh&agrave;, đất th&igrave; &ocirc;ng Lượng, b&agrave; Thu&yacute; giao nh&agrave;, đất. Đất đ&atilde; được Ubnd huyện Long Kh&aacute;nh cấp giấy chứng nhận quyền sử dụng số K 303571 ng&agrave;y 14/4/1997.<br />
V&igrave; l&agrave; chị em ruột trong gia đ&igrave;nh n&ecirc;n thỏa thuận n&ecirc;u tr&ecirc;n kh&ocirc;ng được lập th&agrave;nh văn bản nhưng anh em trong gia đ&igrave;nh đều biết r&otilde; sự việc.<br />
Sau khi mua nh&agrave;, đất xong th&igrave; b&agrave; Hoa mua vật tư về x&acirc;y dựng lại căn nh&agrave;. Vật tư được mua tại cửa h&agrave;ng vật tư Hồng Phước do b&agrave; Nguyễn Thị Thanh Lan &ndash; em g&aacute;i b&agrave; Hoa l&agrave;m chủ với số tiền 100.000.000 đồng tương đương với 21 c&acirc;y v&agrave;ng l&uacute;c bấy giờ.<br />
&nbsp;<br />
Năm 2005 b&agrave; Hoa mua tiếp 02 thửa đất của &ocirc;ng Ng&ocirc; Văn Dũng với tổng diện t&iacute;ch 21.500 m<sup>2</sup> tọa lạc tại ấp T&acirc;n H&ograve;a, x&atilde; Bảo B&igrave;nh, huyện Cẩm Mỹ, tỉnh Đồng Nai.<br />
Gi&aacute; mua b&aacute;n được thỏa thuận l&agrave; 358.000.000 đồng, lệ ph&iacute; sang t&ecirc;n trước bạ l&agrave; 6.000.000 đồng được chia đ&ocirc;i mỗi b&ecirc;n chịu một nữa.<br />
Ng&agrave;y 25/04/2005 b&agrave; Hoa trực tiếp giao 200.000.000 đồng tại nh&agrave; anh Dũng &ndash; b&ecirc;n b&aacute;n, c&oacute; anh Hiển l&agrave; anh của anh Dũng viết giấy nhận tiền v&agrave; vẽ sơ đồ vị tr&iacute; tứ cận của khu đất <em>[hiện do b&agrave; Th&uacute;y giữ]</em>.<br />
Lần giao tiền thứ hai với số tiền 161.000.000 đồng <em>(bao gồm cả lệ ph&iacute; sang t&ecirc;n trước bạ)</em> cũng do b&agrave; Hoa trực tiếp giao cho vợ chồng anh Dũng tại nh&agrave; &ocirc;ng Lượng, b&agrave; Thu&yacute;.<br />
B&agrave; Hoa cũng để cho &ocirc;ng Lượng, b&agrave; Th&uacute;y đứng t&ecirc;n quyền sử dụng đối với 02 thửa đất n&agrave;y v&agrave; cũng được hưởng hoa lợi từ việc thu hoạch c&agrave; ph&ecirc;, điều tr&ecirc;n đất.<br />
Hai thửa đất được UBND huyện Cẩm Mỹ cấp giấy chứng nhận quyền sử dụng số AC 600723 với diện t&iacute;ch 8.770 m<sup>2</sup> v&agrave; AC 600724 với diện t&iacute;ch 12.730 m<sup>2</sup> c&ugrave;ng ng&agrave;y 13/06/2005.<br />
&nbsp;<br />
Th&aacute;ng 11/2006 b&agrave; Hoa biết được &ocirc;ng Lượng, b&agrave; Th&uacute;y đ&atilde; nhận tiền đền b&ugrave; giải tỏa mở rộng quốc lộ 56 với số tiền 76.500.000 đồng nhưng &ocirc;ng Lượng, b&agrave; Th&uacute;y kh&ocirc;ng b&aacute;o cho b&agrave; Hoa biết.<br />
Đến khi b&agrave; Hoa hỏi th&igrave; b&agrave; Th&uacute;y chỉ trả lại cho b&agrave; Hoa 30.000.000 đồng.<br />
Lần 1 trả 20.000.000 đồng v&agrave;o ng&agrave;y 16/11/2006 do b&agrave; Th&uacute;y trả trực tiếp cho b&agrave; Hoa tại nh&agrave; b&agrave; Thu&yacute;, lần 2 trả v&agrave;o ng&agrave;y 20/12/2006 với số tiền 20.000.000 đồng (trong đ&oacute; c&oacute; 10.000.000 đồng tiền b&agrave; Th&uacute;y mượn để mua m&aacute;y bơm nước tưới c&agrave; ph&ecirc;) do con trai b&agrave; Thu&yacute; cầm tiền đến nh&agrave; b&agrave; Hoa ở 52 Đinh Bộ Lĩnh, quận B&igrave;nh Thạnh để trả.<br />
Sau đ&oacute; b&agrave; Hoa y&ecirc;u cầu &ocirc;ng Lượng, b&agrave; Th&uacute;y giao trả nh&agrave;, đất ở v&agrave; 2 thửa đất n&ocirc;ng nghiệp.<br />
Qu&aacute; tr&igrave;nh h&ograve;a giải tại gia đ&igrave;nh v&agrave; tại UBND x&atilde; Xu&acirc;n Mỹ th&igrave; &ocirc;ng Lượng, b&agrave; Th&uacute;y chỉ đồng &yacute; giao trả v&agrave; l&agrave;m thủ tục sang t&ecirc;n đối với 2 thửa đất n&ocirc;ng nghiệp tại x&atilde; Bảo B&igrave;nh.<br />
C&ograve;n nh&agrave; v&agrave; đất ở th&igrave; chưa đồng &yacute; v&igrave; &ocirc;ng Lượng, b&agrave; Th&uacute;y cho rằng số tiền 200.000.000 đồng b&agrave; Hoa hỗ trợ kh&ocirc;ng đủ để mua đất kh&aacute;c ở.<br />
Ng&agrave;y 26/10/2007, b&agrave; Hoa l&agrave;m đơn kiện b&agrave; Th&uacute;y, &ocirc;ng Lượng ra trước Tand huyện Cẩm Mỹ.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1010, 6, 1, N'Thu thập thông tin hồ sơ án', CAST(0x00009E0900000000 AS DateTime), CAST(0x00009E0900000000 AS DateTime), N'<strong>Đối với y&ecirc;u cầu khởi kiện của nguy&ecirc;n đơn:</strong><br />
Vấn căn cứ 4 bi&ecirc;n bản nhận tiền c&ugrave;ng ng&agrave;y 06/12/2005, Vấn giao cho T&igrave;nh 425 <sup>tr</sup> đ gồm:
<ul>
	<li>120 <sup>tr</sup> đ của &ocirc;ng Đ&agrave;m Văn Th&ocirc;ng, b&agrave; Phạm Thị Hiền</li>
	<li>30 <sup>tr</sup> đ của chị Phạm Thị Ngọc</li>
	<li>175 <sup>tr</sup> đ của &ocirc;ng Ng&ocirc; Chung Hiếu</li>
	<li>100 <sup>tr</sup> đ của anh Nguyễn Hữu Thọ, chị Nguyễn Thị Hoa</li>
</ul>
&nbsp;<br />
Vấn lập luận rằng:<br />
100 <sup>tr</sup> đ của Hoa + Thọ m&agrave; Vấn giao T&igrave;nh + Th&agrave;nh đầu tư dự &aacute;n trồng cao su tại tiểu khu 391 thuộc T&acirc;n Lập, Đồng Ph&uacute;, B&igrave;nh Phước nhưng bất th&agrave;nh.<br />
Hoa + Thọ khiếu nại T&igrave;nh + Vấn + Th&agrave;nh tại VKS h.PG<br />
H&ograve;a giải ng&agrave;y 10/01/2007, T&igrave;nh + Vấn li&ecirc;n đới trả Hoa + Thọ 60 <sup>tr</sup> đ. T&igrave;nh thi h&agrave;nh xong 30 <sup>tr</sup> đ ng&agrave;y 21/01/2008.

<ul>
	<li>Vấn cộng dồn 30 tr đ của T&igrave;nh đ&atilde; thi h&agrave;nh cho Hoa + Thọ v&agrave;o 425 <sup>tr</sup> đ Vấn giao cho T&igrave;nh n&ecirc;u tr&ecirc;n để th&agrave;nh 455 <sup>tr</sup> đ.</li>
</ul>
&nbsp;<br />
B&ecirc;n cạnh đ&oacute;, Vấn cộng 3 khoản sau m&agrave; T&igrave;nh đ&atilde; thi h&agrave;nh với tổng tiền l&agrave; 102,5 <sup>tr</sup> đ.

<ol>
	<li>120 <sup>tr</sup> đ của Th&ocirc;ng + Hồng m&agrave; Vấn giao T&igrave;nh đầu tư dự &aacute;n trồng cao su tại tiểu khu 391 thuộc T&acirc;n Lập, Đồng Ph&uacute;, B&igrave;nh Phước nhưng bất th&agrave;nh.</li>
</ol>
Hồng + Th&ocirc;ng kiện T&igrave;nh + Vấn + Th&agrave;nh.<br />
Bản &aacute;n 224/2007/DSPT ng&agrave;y 10/9/2007 Ta t.BD buộc T&igrave;nh + Vấn li&ecirc;n đới trả Hồng 50 <sup>tr</sup> đ =&gt; T&igrave;nh thi h&agrave;nh xong 25 <sup>tr</sup> đ ng&agrave;y 21/01/2008.<br />
Quyết định 22/QĐDSST ng&agrave;y 11/5/2007 Ta h.TU buộc T&igrave;nh + Th&agrave;nh li&ecirc;n đới trả cho Th&ocirc;ng 70 <sup>tr</sup> đ =&gt; T&igrave;nh thi h&agrave;nh xong 70 <sup>tr</sup> đ ng&agrave;y 08/5/2009.

<ol>
	<li>30 <sup>tr</sup> đ của V&acirc;n m&agrave; Vấn giao T&igrave;nh đầu tư dự &aacute;n n&ecirc;u tr&ecirc;n nhưng bất th&agrave;nh.</li>
</ol>
V&acirc;n kiện T&igrave;nh + Vấn.<br />
Bản &aacute;n 221/2007/DSPT ng&agrave;y 10/9/2007 Ta t.BD buộc T&igrave;nh + Vấn li&ecirc;n đới trả V&acirc;n 30 <sup>tr</sup> đ =&gt; T&igrave;nh thi h&agrave;nh xong 15 <sup>tr</sup> đ ng&agrave;y 21/01/2008.

<ol>
	<li>175 tr đ của Hiếu m&agrave; Vấn giao T&igrave;nh đầu tư dự &aacute;n n&ecirc;u tr&ecirc;n nhưng bất th&agrave;nh.</li>
</ol>
Hiếu kiện T&igrave;nh + Vấn.<br />
Bản &aacute;n 222/2007/DSPT ng&agrave;y 10/9/2007 Ta t.BD buộc T&igrave;nh + Vấn li&ecirc;n đới trả Hiếu 125 <sup>tr</sup> đ =&gt; T&igrave;nh thi h&agrave;nh xong 62,5 <sup>tr</sup> đ ng&agrave;y 21/01/2008.<br />
&nbsp;<br />
Cuối c&ugrave;ng Vấn lấy 425 <sup>tr</sup> đ (4 bi&ecirc;n bản giao nhận ng&agrave;y 06/12/2005) (+) 30 <sup>tr</sup> đ T&igrave;nh đ&atilde; thi h&agrave;nh cho Hoa + Thọ (-) 102,5 <sup>tr</sup> đ T&igrave;nh đ&atilde; thi h&agrave;nh cho Th&ocirc;ng + Hồng, V&acirc;n, Hiếu = 352,5 <sup>tr</sup> đ gốc để buộc T&igrave;nh trả gốc 352.500.000 đ + l&atilde;i 141.705.000 đ = 494.205.000 đ l&agrave; thiếu căn cứ v&igrave; c&aacute;c lẽ:

<ol>
	<li>T&igrave;nh kh&ocirc;ng vay mượn tiền của Vấn từ 4 chứng cứ Vấn cung cấp.</li>
</ol>
4 bi&ecirc;n bản nhận tiền c&ugrave;ng ng&agrave;y 06/12/2005 thể hiện rất r&otilde; r&agrave;ng rằng Vấn giao Tiền cho T&igrave;nh để thực hiện dự &aacute;n n&ecirc;u tr&ecirc;n, nguồn tiền được x&aacute;c định l&agrave; của c&aacute;c &ocirc;ng b&agrave; Đ&agrave;m Văn Th&ocirc;ng + Phạm Thị Hiền, Phạm Thị Ngọc, Ng&ocirc; Chung Hiếu, Nguyễn Hữu Thọ + Nguyễn Thị Hoa.<br />
<br />
<strong>Đối với y&ecirc;u cầu phản tố của bị đơn:</strong><br />
Theo đơn phản tố ng&agrave;y 15/01/2008, T&igrave;nh phản tố buộc Vấn trả 900.000.000 đ + l&atilde;i 321.300.000 đ (0,67%/th&aacute;ng từ 25/10/2006 -&gt; nay) = 1.221.300.000 đ<br />
Căn cứ:
<ul>
	<li>Giấy giao tiền mua đất ng&agrave;y 25/10/2006 giữa T&igrave;nh v&agrave; Vấn.</li>
	<li>Giấy x&aacute;c nhận nợ ng&agrave;y 27/02/2007</li>
	<li>Giấy x&aacute;c nhận nợ ng&agrave;y 18/06/2007</li>
	<li>Giấy x&aacute;c nhận nợ ng&agrave;y 12/09/2007</li>
</ul>
')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1011, 1006, 3, N'Thu thập thông tin hồ sơ án', CAST(0x00009F4500000000 AS DateTime), CAST(0x00009F4E00000000 AS DateTime), N'<strong>Năm 1993</strong> hộ &ocirc;ng Lan c&oacute; nhận chuyển nhượng của hộ &ocirc;ng Dinh diện t&iacute;ch đất thuộc thửa 69 tờ bản đồ số 45 tại Kh&aacute;nh B&igrave;nh. Đến năm 1997 &ocirc;ng Lan c&oacute; mời &ocirc;ng Nguyễn Văn Kia đến thống nhất ranh đất cụ thể v&igrave; thửa số 609 của &ocirc;ng Lan c&oacute; hướng T&acirc;y gi&aacute;p ranh đất của &ocirc;ng Kia v&agrave; &ocirc;ng Kia đ&atilde; thống nhất k&yacute; v&agrave;o bi&ecirc;n bản x&aacute;c định ranh ng&agrave;y 26/01/1997 trước sự chứng kiến của ban điều h&agrave;nh ấp l&agrave; &ocirc;ng Thạch v&agrave; &ocirc;ng Dinh. Năm 1999 &ocirc;ng Lan được huyện T&acirc;n uy&ecirc;n cấp GCN QSDĐ số 00556 cấp ng&agrave;y 17/05/1999.<br />
<strong>Đến năm 2006</strong> khi &ocirc;ng Lan tiến h&agrave;nh x&acirc;y nh&agrave; trọ v&agrave; tường r&agrave;o th&igrave; &ocirc;ng Kia <em>lại tranh chấp ranh đất, đập ph&aacute; tường r&agrave;o </em>v&agrave; đ&atilde; được UBND x&atilde; Kh&aacute;nh B&igrave;nh h&ograve;a giải thống nhất ranh đất một lần nữa v&agrave; vợ chồng &ocirc;ng Kia đ&atilde; ra thực địa cấm mốc, chỉ ranh đất để &ocirc;ng Lan đ&agrave;o m&oacute;ng x&acirc;y dựng tường r&agrave;o sử dụng ổn định.<br />
Tuy nhi&ecirc;n, đến ng&agrave;y <strong>04/07/2011</strong> khi &ocirc;ng Lan đang tiến h&agrave;nh trồng cao su tr&ecirc;n đất th&igrave; vợ chồng &ocirc;ng Kia đ&atilde; đập ph&aacute; tường r&agrave;o bao quanh đất với chiều d&agrave;i tường bị đập l&agrave; 7m, cao 2m nằm ở đoạn tiếp gi&aacute;p với ranh đất của &ocirc;ng Kia, b&agrave; G&ograve;n. H&agrave;nh vi n&agrave;y được vợ chồng &ocirc;ng Kia thừa nhận thể hiện <strong>trong bi&ecirc;n bản x&aacute;c minh ng&agrave;y 04/7/2011.</strong><br />
Tiếp theo đ&oacute;, <strong>ng&agrave;y 05/7/2011</strong>, &ocirc;ng Kia v&agrave; b&agrave; G&ograve;n tiếp tục đập ph&aacute; th&ecirc;m 10m h&agrave;ng r&agrave;o của &ocirc;ng Lan. Ngay sau đ&oacute; &ocirc;ng Kia, b&agrave; G&ograve;n nhổ 32 cọc c&acirc;y trồng cao su (cọc bằng tre) v&agrave; tự &yacute; trồng v&agrave;o đ&oacute; 34 c&acirc;y tr&agrave;m v&agrave;o l&uacute;c 10 giờ 30 ph&uacute;t c&ugrave;ng ng&agrave;y thể hiện tại <strong>bi&ecirc;n bản x&aacute;c minh ng&agrave;y 05/7/2011.</strong><br />
<strong>Ng&agrave;y 26/9/2011</strong>, &ocirc;ng Kia tiếp tục đập x&acirc;m phạm đến t&agrave;i sản của &ocirc;ng Lan, v&agrave; tự &yacute; trồng th&ecirc;m 80 c&acirc;y tr&agrave;m tr&ecirc;n đất của &ocirc;ng Lan thể hiện tại <strong>bi&ecirc;n bản x&aacute;c minh ng&agrave;y 27/9/2011</strong>.<br />
<strong>Tiếp theo đ&oacute; ng&agrave;y 08/11/2011</strong> th&igrave; phần tường r&agrave;o c&ograve;n lại của gia đ&igrave;nh &ocirc;ng Lan x&acirc;y bằng gạch chưa t&ocirc; tường 10 ph&acirc;n, d&agrave;i 6 m&eacute;t rưỡi, cao 2m50 bị đổ sập xuống <strong>theo bi&ecirc;n bản x&aacute;c minh ng&agrave;y 08/11/2011.</strong><br />
Như vậy, tường r&agrave;o tiếp gi&aacute;p đất &ocirc;ng Kia, b&agrave; G&ograve;n c&oacute; chiều d&agrave;i 25m đ&atilde; bị &ocirc;ng Kia, b&agrave; G&ograve;n đập ph&aacute; to&agrave;n bộ. Ngo&agrave;i ra, &ocirc;ng Kia, b&agrave; G&ograve;n c&ograve;n tự &yacute; trồng 114 c&acirc;y tr&agrave;m tr&ecirc;n đất của gia đ&igrave;nh &ocirc;ng Lan.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1012, 1006, 3, N'Khởi kiện', CAST(0x00009F5600000000 AS DateTime), CAST(0x00009F5700000000 AS DateTime), N'&Ocirc;ng Lan l&agrave; chủ sử dụng hợp ph&aacute;p 20.561 m<sup>2</sup> đất tại ấp Kh&aacute;nh Lộc, x&atilde; Kh&aacute;nh B&igrave;nh, huyện T&acirc;n Uy&ecirc;n, tỉnh B&igrave;nh Dương căn cứ GCN QSDĐ &nbsp;số N 235177 do UBND huyện T&acirc;n Uy&ecirc;n cấp ng&agrave;y 17/5/1999. <strong>Đất c&oacute; nguồn gốc nhận chuyển nhượng từ &ocirc;ng Nhẫn năm 1993</strong>.<br />
Trong diện t&iacute;ch n&ecirc;u tr&ecirc;n c&oacute; 4.863 m<sup>2 </sup>thuộc thửa 609 tờ bản đồ số 45 tiếp gi&aacute;p đất &ocirc;ng Nguyễn Văn Kia v&agrave; b&agrave; Nguyễn Thị G&ograve;n. Phần tiếp gi&aacute;p n&agrave;y được x&aacute;c định bằng tường r&agrave;o bao quanh đất của &ocirc;ng Lan x&acirc;y năm 2006 với sự đồng &yacute; <strong>hai lần chỉ ranh đất</strong> của &ocirc;ng Kia, b&agrave; G&ograve;n theo bi&ecirc;n bản x&aacute;c định ranh ng&agrave;y <strong>26/10/1997</strong> v&agrave; bi&ecirc;n bản h&ograve;a giải th&agrave;nh <strong>ng&agrave;y 20/3/2006</strong> tại UBND x&atilde; Kh&aacute;nh B&igrave;nh.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1013, 1006, 3, N'Hòa giải', CAST(0x00009F6700000000 AS DateTime), CAST(0x00009F6C00000000 AS DateTime), N'<strong>Bi&ecirc;n bản h&ograve;a giải tại UBND x&atilde; Kh&aacute;nh B&igrave;nh&nbsp; </strong>c&aacute;c b&ecirc;n chưa thống nhất được &yacute; kiến.<br />
<u>&Yacute; kiến của &ocirc;ng Kia</u>: kh&ocirc;ng đồng &yacute; c&aacute;ch đo của UBND x&atilde; Kh&aacute;nh B&igrave;nh, đo kh&ocirc;ng đ&uacute;ng theo QSDĐ của &ocirc;ng.<br />
<u>&Yacute; kiến &ocirc;ng Lan</u>: y&ecirc;u cầu Hội đồng H&ograve;a giải củng cố hồ sơ chuyển T&ograve;a &aacute;n giải quyết')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1014, 1007, 1, N'Thu thập thông tin hồ sơ án', CAST(0x0000A2A600000000 AS DateTime), CAST(0x0000A2AC00000000 AS DateTime), N'&Ocirc;ng Phạm Văn Y&ecirc;u (1932-08/02/1997) (Chứng tử ng&agrave;y 17/02/1997, BL: 10) v&agrave; b&agrave; Nguyễn Thị Hai (1933-13/9/1996) (Chứng tử ng&agrave;y 26/09/1996, BL: 9), trước l&uacute;c chết c&oacute; tạo lập 2 căn nh&agrave;:
<ol>
	<li>69 đường 15, P.4, Q.8</li>
</ol>
(GCN số 4619/SXD ng&agrave;y 19/5/2004 do Ub TpHCM cấp)

<ol>
	<li>72/1 đường 15, P.4, Q.8</li>
</ol>
Năm 1986, &ocirc;ng b&agrave; tặng cho vợ chồng anh Phạm Bạo (1955-16/5/2005) căn nh&agrave; 72/1 đường 15, P.4, Q.8.<br />
Căn nh&agrave; 68 đường 15, P.4, Q.8 hiện do Phạm Đ&ocirc;ng quản l&yacute; sử dụng l&agrave; t&agrave;i sản của &ocirc;ng Y&ecirc;u, b&agrave; Hai chưa định đoạt bằng di ch&uacute;c trước khi chết.<br />
&Ocirc;ng Y&ecirc;u v&agrave; b&agrave; Hai c&oacute; 7 con chung:
<ol>
	<li>Phạm Bạo (1955-16/5/2005) (Hộ khẩu số 802052, BL: 11 - Chứng tử ng&agrave;y 17/5/2005, BL: 8)</li>
	<li>Phạm Thị Phi(1957-1987) (Hộ khẩu số 802052, BL: 11 - Chứng tử ng&agrave;y 17/7/2007, BL: 20)</li>
	<li>Phạm Giao (06/01/1961, Tr&iacute;ch lục &aacute;n ph&ograve;ng lục sự t&ograve;a ng&agrave;y 16/9/1964, BL: 34)</li>
	<li>Phạm Thị Thanh (29/03/1962, Tr&iacute;ch lục &aacute;n ph&ograve;ng lục sự t&ograve;a ng&agrave;y 16/9/1964, BL: 35)</li>
	<li>Phạm Đ&igrave;nh Lệnh (21/07/1964, Tr&iacute;ch lục bộ khai sanh ng&agrave;y 24/7/1964, BL: 36)</li>
	<li>Phạm Thị Thanh (13/12/1966, Tr&iacute;ch lục bộ khai sanh ng&agrave;y 16/12/1966, BL: 37)</li>
	<li>Phạm Đ&ocirc;ng (Hộ khẩu số 802052, BL: 11)</li>
</ol>
')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1015, 4, 3, N'Gặp trao đổi thông tin khách hàng', CAST(0x0000A324009450C0 AS DateTime), CAST(0x0000A32500A4CB80 AS DateTime), N'')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1016, 6, 3, N'Phiên tòa phúc thẩm', CAST(0x0000A324007B98A0 AS DateTime), CAST(0x0000A324009C8E20 AS DateTime), N'')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1017, 2, 3, N'Thu xếp hòa giải mâu thuẫn', CAST(0x0000A3260083D600 AS DateTime), CAST(0x0000A32600B54640 AS DateTime), N'')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1020, 3, 1, N'Mua vật chứng', CAST(0x0000A3E900000000 AS DateTime), CAST(0x0000A3EE00000000 AS DateTime), N'5.000dfdf.000 đ')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1024, 1013, 1, N'Hòa giải đôi bên', CAST(0x0000A22A00000000 AS DateTime), CAST(0x0000A22F00000000 AS DateTime), N'Hẹn hai b&ecirc;n đ&uacute;ng 8h30 s&aacute;ng tại văn ph&ograve;ng.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1025, 1015, 1, N'dsd', CAST(0x0000A3F000000000 AS DateTime), CAST(0x0000A3F200000000 AS DateTime), N'sdsds')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1026, 1013, 1, N'Bồi thường thiệt hại', CAST(0x0000A3EB00000000 AS DateTime), CAST(0x0000A3F700000000 AS DateTime), N'Như vấn đề trong h&ocirc;m trước đ&atilde; b&agrave;n, b&ecirc;n A sẽ ồi thường b&ecirc;n B ti&ecirc;n thuốc men l&agrave; 3.000.000đ')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1031, 2, 1, N'Bàn bạc lại vấn đề', CAST(0x0000A34600000000 AS DateTime), CAST(0x0000A34700000000 AS DateTime), N'Giải quyết dứt kho&aacute;t c&aacute;c vấn đề c&ograve;n lại.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1033, 2, 1, N'Thanh toán tiền bồi thường giữa hai bên', CAST(0x0000A37100000000 AS DateTime), CAST(0x0000A37700000000 AS DateTime), N'Mỗi b&ecirc;n sẽ phải bồi thường to&agrave;n bộ h&oacute;a đơn lần trước đ&atilde; đưa.')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1034, 1014, 3, N'Trình bày chi tiết vụ việc', CAST(0x0000A3F100000000 AS DateTime), CAST(0x0000A40500000000 AS DateTime), N'Hẹn hai b&ecirc;n l&ecirc;n văn ph&ograve;ng tr&igrave;nh b&agrave;y va đưa ra hợp đồng gốc')
INSERT [dbo].[OperationalEvents] ([OperationalEventId], [CaseId], [CreatorId], [Title], [BeginTime], [EndTime], [Description]) VALUES (1035, 1, 5, N'Hẹn gặp các thanh viên gia đình', CAST(0x0000A2B900000000 AS DateTime), CAST(0x0000A2C300000000 AS DateTime), N'Hẹn gặp c&aacute;c thanh vi&ecirc;n gia đ&igrave;nh để n&oacute;i cụ thể hơn về vụ việc')
SET IDENTITY_INSERT [dbo].[OperationalEvents] OFF
/****** Object:  Table [dbo].[Office_Staff]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Office_Staff](
	[OfficeStaffId] [int] IDENTITY(1,1) NOT NULL,
	[OfficeId] [int] NOT NULL,
	[StaffId] [int] NOT NULL,
 CONSTRAINT [PK_Office_Staff] PRIMARY KEY CLUSTERED 
(
	[OfficeStaffId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Office_Staff] ON
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (19, 1, 5)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (20, 3, 8)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (24, 1, 4)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (25, 2, 4)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (26, 1, 6)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (27, 2, 6)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (28, 3, 6)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (29, 3, 10)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (30, 3, 9)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (1026, 3, 7)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (1027, 1, 3)
INSERT [dbo].[Office_Staff] ([OfficeStaffId], [OfficeId], [StaffId]) VALUES (1028, 2, 3)
SET IDENTITY_INSERT [dbo].[Office_Staff] OFF
/****** Object:  Table [dbo].[Case_Staff]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Case_Staff](
	[CaseStaffId] [int] IDENTITY(1,1) NOT NULL,
	[CaseId] [int] NOT NULL,
	[StaffId] [int] NOT NULL,
 CONSTRAINT [PK_Case_Staff] PRIMARY KEY CLUSTERED 
(
	[CaseStaffId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Case_Staff] ON
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1, 1, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (2, 1, 5)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (3, 2, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (4, 2, 3)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (5, 3, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (6, 3, 5)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (7, 4, 3)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (8, 5, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (9, 6, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1009, 1006, 3)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1011, 1007, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1012, 1008, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1013, 1009, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1014, 1010, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1017, 6, 3)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1019, 6, 5)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1020, 3, 3)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1021, 1011, 4)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1022, 1012, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1023, 1013, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1024, 1014, 3)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1025, 1015, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1026, 1007, 2)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1027, 1016, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1028, 1017, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1029, 1006, 2)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1030, 1018, 1)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1031, 1018, 2)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1032, 1013, 6)
INSERT [dbo].[Case_Staff] ([CaseStaffId], [CaseId], [StaffId]) VALUES (1033, 1015, 5)
SET IDENTITY_INSERT [dbo].[Case_Staff] OFF
/****** Object:  Table [dbo].[Case_Customer]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Case_Customer](
	[CaseCustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CaseId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_Case_Customer] PRIMARY KEY CLUSTERED 
(
	[CaseCustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Case_Customer] ON
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1, 1, 1)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (2, 1, 2)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (3, 2, 2)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (4, 2, 3)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (5, 3, 4)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (6, 4, 5)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (7, 4, 6)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (8, 5, 8)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (9, 6, 9)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1008, 1006, 1007)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1009, 1006, 1008)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1010, 1007, 1009)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1012, 1007, 1)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1013, 1007, 4)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1015, 1018, 9)
INSERT [dbo].[Case_Customer] ([CaseCustomerId], [CaseId], [CustomerId]) VALUES (1016, 1013, 60)
SET IDENTITY_INSERT [dbo].[Case_Customer] OFF
/****** Object:  Table [dbo].[CalendarEvents]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarEvents](
	[CalendarEventId] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Priority] [nvarchar](50) NOT NULL,
	[BeginTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Calendar] PRIMARY KEY CLUSTERED 
(
	[CalendarEventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CalendarEvents] ON
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (1, 1, N'abc', N'label-purple', CAST(0x0000A30400000000 AS DateTime), CAST(0x0000A30400000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (2, 3, N'Tìm kiếm thông tin vụ án TTDD2014001', N'label-success', CAST(0x0000A32100000000 AS DateTime), CAST(0x0000A32300000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (3, 3, N'Gặp khách hàng Vân', N'label-purple', CAST(0x0000A32600000000 AS DateTime), CAST(0x0000A32600000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (5, 3, N'Trực bù nghỉ lễ', N'label-danger', CAST(0x0000A32200000000 AS DateTime), CAST(0x0000A32200000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (6, 3, N'def', N'label-success', CAST(0x0000A3DA00000000 AS DateTime), CAST(0x0000A3DA00000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (7, 1, N'Đi khách', N'label-success', CAST(0x0000A3F000000000 AS DateTime), CAST(0x0000A3F000000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (11, 1, N'Gặp a Hải cty A', N'label-success', CAST(0x0000A40600000000 AS DateTime), CAST(0x0000A40600000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (12, 1, N'Kí hợp đồng mới với cty B', N'label-info', CAST(0x0000A40B00000000 AS DateTime), CAST(0x0000A40B00000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (13, 5, N'Tư vấn các điều luật cho a A', N'label-pink', CAST(0x0000A41200000000 AS DateTime), CAST(0x0000A41200000000 AS DateTime))
INSERT [dbo].[CalendarEvents] ([CalendarEventId], [StaffId], [Title], [Priority], [BeginTime], [EndTime]) VALUES (14, 5, N'Thu thập bằng chứng cho Hồ sơ YADE0001', N'label-yellow', CAST(0x0000A40900000000 AS DateTime), CAST(0x0000A40900000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[CalendarEvents] OFF
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/18/2014 00:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (1, 1, N'thuannh', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (2, 2, N'ngannhk', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (3, 3, N'dunglv', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (4, 4, N'thaonth', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (5, 5, N'tiengnh', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (6, 6, N'anhltk', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (7, 7, N'taunn', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (8, 8, N'tinnt', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (9, 9, N'longnp', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (10, 10, N'tungnv', N'e10adc3949ba59abbe56e057f20f883e')
INSERT [dbo].[Accounts] ([AccountId], [StaffId], [Username], [Password]) VALUES (11, 11, N'sdsdsd', N'e10adc3949ba59abbe56e057f20f883e')
SET IDENTITY_INSERT [dbo].[Accounts] OFF
/****** Object:  ForeignKey [FK_Accounts_Staffs]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Staffs] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staffs] ([StaffId])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Staffs]
GO
/****** Object:  ForeignKey [FK_CalendarEvents_Staffs]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[CalendarEvents]  WITH CHECK ADD  CONSTRAINT [FK_CalendarEvents_Staffs] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staffs] ([StaffId])
GO
ALTER TABLE [dbo].[CalendarEvents] CHECK CONSTRAINT [FK_CalendarEvents_Staffs]
GO
/****** Object:  ForeignKey [FK_OD_Customer_Customers]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Case_Customer]  WITH CHECK ADD  CONSTRAINT [FK_OD_Customer_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Case_Customer] CHECK CONSTRAINT [FK_OD_Customer_Customers]
GO
/****** Object:  ForeignKey [FK_OD_Customer_OperationDocuments]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Case_Customer]  WITH CHECK ADD  CONSTRAINT [FK_OD_Customer_OperationDocuments] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Cases] ([CaseId])
GO
ALTER TABLE [dbo].[Case_Customer] CHECK CONSTRAINT [FK_OD_Customer_OperationDocuments]
GO
/****** Object:  ForeignKey [FK_OD_Staff_OperationDocuments]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Case_Staff]  WITH CHECK ADD  CONSTRAINT [FK_OD_Staff_OperationDocuments] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Cases] ([CaseId])
GO
ALTER TABLE [dbo].[Case_Staff] CHECK CONSTRAINT [FK_OD_Staff_OperationDocuments]
GO
/****** Object:  ForeignKey [FK_OD_Staff_Staffs]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Case_Staff]  WITH CHECK ADD  CONSTRAINT [FK_OD_Staff_Staffs] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staffs] ([StaffId])
GO
ALTER TABLE [dbo].[Case_Staff] CHECK CONSTRAINT [FK_OD_Staff_Staffs]
GO
/****** Object:  ForeignKey [FK_Cases_Offices]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Cases]  WITH CHECK ADD  CONSTRAINT [FK_Cases_Offices] FOREIGN KEY([OfficeId])
REFERENCES [dbo].[Offices] ([OfficeId])
GO
ALTER TABLE [dbo].[Cases] CHECK CONSTRAINT [FK_Cases_Offices]
GO
/****** Object:  ForeignKey [FK_Customers_CustomerGroups]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_CustomerGroups] FOREIGN KEY([CustomerGroupId])
REFERENCES [dbo].[CustomerGroups] ([CustomerGroupId])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_CustomerGroups]
GO
/****** Object:  ForeignKey [FK_Office_Staff_Offices]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Office_Staff]  WITH CHECK ADD  CONSTRAINT [FK_Office_Staff_Offices] FOREIGN KEY([OfficeId])
REFERENCES [dbo].[Offices] ([OfficeId])
GO
ALTER TABLE [dbo].[Office_Staff] CHECK CONSTRAINT [FK_Office_Staff_Offices]
GO
/****** Object:  ForeignKey [FK_Office_Staff_Staffs]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Office_Staff]  WITH CHECK ADD  CONSTRAINT [FK_Office_Staff_Staffs] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Staffs] ([StaffId])
GO
ALTER TABLE [dbo].[Office_Staff] CHECK CONSTRAINT [FK_Office_Staff_Staffs]
GO
/****** Object:  ForeignKey [FK_OperationalEvents_OperationDocuments]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[OperationalEvents]  WITH CHECK ADD  CONSTRAINT [FK_OperationalEvents_OperationDocuments] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Cases] ([CaseId])
GO
ALTER TABLE [dbo].[OperationalEvents] CHECK CONSTRAINT [FK_OperationalEvents_OperationDocuments]
GO
/****** Object:  ForeignKey [FK_OtherCost_Offices]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[OtherCost]  WITH CHECK ADD  CONSTRAINT [FK_OtherCost_Offices] FOREIGN KEY([OfficeId])
REFERENCES [dbo].[Offices] ([OfficeId])
GO
ALTER TABLE [dbo].[OtherCost] CHECK CONSTRAINT [FK_OtherCost_Offices]
GO
/****** Object:  ForeignKey [FK_Payments_OperationDocuments]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_OperationDocuments] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Cases] ([CaseId])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_OperationDocuments]
GO
/****** Object:  ForeignKey [FK_Services_ServiceTypes]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_ServiceTypes] FOREIGN KEY([ServiceTypeId])
REFERENCES [dbo].[ServiceTypes] ([ServiceTypeId])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_ServiceTypes]
GO
/****** Object:  ForeignKey [FK_Staffs_Roles]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Staffs]  WITH CHECK ADD  CONSTRAINT [FK_Staffs_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Staffs] CHECK CONSTRAINT [FK_Staffs_Roles]
GO
/****** Object:  ForeignKey [FK_Staffs_StaffGroups]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Staffs]  WITH CHECK ADD  CONSTRAINT [FK_Staffs_StaffGroups] FOREIGN KEY([StaffGroupId])
REFERENCES [dbo].[StaffGroups] ([StaffGroupId])
GO
ALTER TABLE [dbo].[Staffs] CHECK CONSTRAINT [FK_Staffs_StaffGroups]
GO
/****** Object:  ForeignKey [FK_Subjects_OperationDocuments]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_OperationDocuments] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Cases] ([CaseId])
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [FK_Subjects_OperationDocuments]
GO
/****** Object:  ForeignKey [FK_UsedServices_OperationDocuments]    Script Date: 12/18/2014 00:54:58 ******/
ALTER TABLE [dbo].[UsedServices]  WITH CHECK ADD  CONSTRAINT [FK_UsedServices_OperationDocuments] FOREIGN KEY([CaseId])
REFERENCES [dbo].[Cases] ([CaseId])
GO
ALTER TABLE [dbo].[UsedServices] CHECK CONSTRAINT [FK_UsedServices_OperationDocuments]
GO
