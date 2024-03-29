USE [GGGETS]
GO
/****** Object:  StoredProcedure [dbo].[UspOutputData]    Script Date: 04/28/2011 12:54:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UspOutputData]
@tablename sysname

AS

declare @column varchar(1000)

declare @columndata varchar(1000)

declare @sql varchar(4000)

declare @xtype tinyint

declare @name sysname

declare @objectId int

declare @objectname sysname

declare @ident int

set nocount on

set @objectId=object_id(@tablename)

if @objectId is null -- 判断对象是否存在

begin

print 'The object not exists'

return

end

set @objectname=rtrim(object_name(@objectId))

if @objectname is null or charindex(@objectname,@tablename)=0 --此判断不严密

begin

print 'object not in current database'

return

end

if OBJECTPROPERTY(@objectId,'IsTable') < > 1 -- 判断对象是否是table

begin

print 'The object is not table'

return

end

select @ident=status&0x80 from syscolumns where id=@objectid and status&0x80=0x80

if @ident is not null

print 'SET IDENTITY_INSERT '+@TableName+' ON'

declare syscolumns_cursor cursor

for select c.name,c.xtype from syscolumns c where c.id=@objectid order by c.colid

open syscolumns_cursor

set @column=''

set @columndata=''

fetch next from syscolumns_cursor into @name,@xtype

while @@fetch_status < >-1

begin

if @@fetch_status < >-2

begin

if @xtype not in(189,34,35,99,98) --timestamp不需处理，image,text,ntext,sql_variant 暂时不处理

begin

set @column=@column+case when len(@column)=0 then'' else ','end+@name

set @columndata=@columndata+case when len(@columndata)=0 then '' else ','','','

end

+case when @xtype in(167,175) then '''''''''+'+@name+'+''''''''' --varchar,char

when @xtype in(231,239) then '''N''''''+'+@name+'+''''''''' --nvarchar,nchar

when @xtype=61 then '''''''''+convert(char(23),'+@name+',121)+''''''''' --datetime

when @xtype=58 then '''''''''+convert(char(16),'+@name+',120)+''''''''' --smalldatetime

when @xtype=36 then '''''''''+convert(char(36),'+@name+')+''''''''' --uniqueidentifier

else @name end

end

end

fetch next from syscolumns_cursor into @name,@xtype

end

close syscolumns_cursor

deallocate syscolumns_cursor

set @sql='set nocount on select ''insert '+@tablename+'('+@column+') values(''as ''--'','+@columndata+','')'' from '+@tablename

print '--'+@sql

exec(@sql)

if @ident is not null

print 'SET IDENTITY_INSERT '+@TableName+' OFF'
GO
/****** Object:  Table [dbo].[Template]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Template](
	[TID] [uniqueidentifier] NOT NULL,
	[TemplateCode] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Desc] [nvarchar](400) NULL,
	[PrintDirection] [int] NULL,
	[PagerWidth] [int] NULL,
	[PagerHeight] [int] NULL,
	[PaperType] [nvarchar](15) NULL,
	[BatchHeight] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[Operator] [nvarchar](20) NOT NULL,
	[CorrespondingTable] [nvarchar](400) NULL,
	[CorrespondingCN] [nvarchar](400) NULL,
	[IdentifyKey] [nvarchar](20) NULL,
 CONSTRAINT [PK_TEMPLATE] PRIMARY KEY CLUSTERED 
(
	[TID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'GUID序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'TID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'TemplateCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印方向' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'PrintDirection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'PagerWidth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'PagerHeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'纸张类型(如A4)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'PaperType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批量套打高度（只用于批量处理时使用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'BatchHeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'ModifyDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'CorrespondingTable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应中文描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'CorrespondingCN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'唯一标示对象标示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template', @level2type=N'COLUMN',@level2name=N'IdentifyKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单证模板' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Template'
GO
INSERT [dbo].[Template] ([TID], [TemplateCode], [Name], [Desc], [PrintDirection], [PagerWidth], [PagerHeight], [PaperType], [BatchHeight], [CreateDate], [ModifyDate], [Operator], [CorrespondingTable], [CorrespondingCN], [IdentifyKey]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe32', N'A2', N'运单模板连打', N'管理多个运单模板，套打个数不限', 3, 3000, 0, NULL, 500, CAST(0x00009E9901024767 AS DateTime), CAST(0x00009E9901024767 AS DateTime), N'admin', N'hawb,hawbbox,hawbitem', N'运单,运单盒子,运单物品', N'HID')
INSERT [dbo].[Template] ([TID], [TemplateCode], [Name], [Desc], [PrintDirection], [PagerWidth], [PagerHeight], [PaperType], [BatchHeight], [CreateDate], [ModifyDate], [Operator], [CorrespondingTable], [CorrespondingCN], [IdentifyKey]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', N'A1', N'运单模板单打', N'管理单个运单模板', 3, 3000, 0, NULL, 0, CAST(0x00009E9901024767 AS DateTime), CAST(0x00009E9901024767 AS DateTime), N'admin', N'hawb,hawbbox,hawbitem', N'运单,运单盒子,运单物品', N'HID')
/****** Object:  View [dbo].[TableDesc]    Script Date: 04/28/2011 12:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TableDesc]
AS
SELECT     CONVERT(varchar(100), O.name) AS name, ISNULL(CONVERT(varchar(100), F.value), CONVERT(varchar(100), O.name)) AS NameCN
FROM         (SELECT     name, id
                       FROM          sys.sysobjects AS S
                       WHERE      (xtype = 'U') AND (name <> 'dtproperties')) AS O LEFT OUTER JOIN
                      sys.extended_properties AS F ON O.id = F.major_id AND F.minor_id = 0
GO
/****** Object:  Table [dbo].[OrganizationChart]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationChart](
	[DID] [uniqueidentifier] NOT NULL,
	[OrganizationCode] [nvarchar](50) NULL,
	[OrganizationName] [nvarchar](200) NULL,
	[ParentDID] [uniqueidentifier] NULL,
	[CreateTime] [datetime] NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_OrganizationChart] PRIMARY KEY CLUSTERED 
(
	[DID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织架构' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrganizationChart'
GO
/****** Object:  Table [dbo].[MAWB]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MAWB](
	[MID] [uniqueidentifier] NOT NULL,
	[BarCode] [nvarchar](50) NOT NULL,
	[From] [char](3) NOT NULL,
	[To] [char](3) NOT NULL,
	[FlightNo] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LockedTime] [datetime] NULL,
	[Operator] [nvarchar](20) NOT NULL,
	[TotalWeight] [decimal](10, 2) NOT NULL,
	[TotalVolume] [decimal](10, 2) NOT NULL,
	[Status] [int] NOT NULL,
	[ImportStatus] [int] NULL,
 CONSTRAINT [PK_MAWB] PRIMARY KEY CLUSTERED 
(
	[MID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'BarCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起飞地机场三字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'From'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'着陆地机场三字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'To'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'航班号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'FlightNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'LockedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'TotalWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总体积' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'TotalVolume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导入状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB', @level2type=N'COLUMN',@level2name=N'ImportStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总运单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MAWB'
GO
INSERT [dbo].[MAWB] ([MID], [BarCode], [From], [To], [FlightNo], [CreateTime], [LockedTime], [Operator], [TotalWeight], [TotalVolume], [Status], [ImportStatus]) VALUES (N'b99f909f-06ab-432f-94a4-c4689e850980', N'M1', N'TIA', N'KBL', N'K494', CAST(0x00009CF100000000 AS DateTime), CAST(0x00009CF200000000 AS DateTime), N'沈志伟', CAST(1.00 AS Decimal(10, 2)), CAST(1.00 AS Decimal(10, 2)), 1, NULL)
INSERT [dbo].[MAWB] ([MID], [BarCode], [From], [To], [FlightNo], [CreateTime], [LockedTime], [Operator], [TotalWeight], [TotalVolume], [Status], [ImportStatus]) VALUES (N'b99f909f-06ab-432f-94a4-c4689e850987', N'M2', N'SHA', N'TYO', N'K495', CAST(0x00009CF100000000 AS DateTime), CAST(0x00009CF200000000 AS DateTime), N'沈志伟', CAST(14.00 AS Decimal(10, 2)), CAST(0.08 AS Decimal(10, 2)), 1, NULL)
/****** Object:  Table [dbo].[CountryCode]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CountryCode](
	[ID] [int] NOT NULL,
	[CountryCode] [char](2) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_COUNTRYCODE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家二字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CountryCode'
GO
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (1, N'AF', N'AFGHANISTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (2, N'AL', N'ALBANIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (3, N'DZ', N'ALGERIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (4, N'AS', N'AMERICAN SAMOA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (5, N'AD', N'ANDORRA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (6, N'AO', N'ANGOLA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (7, N'AI', N'ANGUILLA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (8, N'AG', N'ANTIGUA AND BARBUDA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (9, N'AR', N'ARGENTINA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (10, N'AM', N'ARMENIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (11, N'AW', N'ARUBA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (12, N'AU', N'AUSTRALIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (13, N'AT', N'AUSTRIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (14, N'AZ', N'AZERBAIJAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (15, N'BS', N'BAHAMAS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (16, N'BH', N'BAHRAIN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (17, N'BD', N'BANGLADESH')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (18, N'BB', N'BARBADOS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (19, N'BY', N'BELARUS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (20, N'BE', N'BELGIUM')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (21, N'BZ', N'BELIZE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (22, N'BJ', N'BENIN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (23, N'BM', N'BERMUDA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (24, N'BT', N'BHUTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (25, N'BO', N'BOLIVIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (26, N'BW', N'BOTSWANA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (27, N'BR', N'BRAZIL')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (28, N'BN', N'BRUNEI DARUSSALAM')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (29, N'BG', N'BULGARIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (30, N'BF', N'BURKINA FASO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (31, N'BI', N'BURUNDI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (32, N'KH', N'CAMBODIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (33, N'CM', N'CAMEROON')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (34, N'CA', N'CANADA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (35, N'CV', N'CAPE VERDE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (36, N'KY', N'CAYMAN ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (37, N'CF', N'CEN AFRICA REP')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (38, N'TD', N'CHAD')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (39, N'CL', N'CHILE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (40, N'CN', N'CHINA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (41, N'CO', N'COLOMBIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (42, N'KM', N'COMOROS ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (43, N'CG', N'CONGO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (44, N'CK', N'COOK ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (45, N'CR', N'COSTA RICA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (46, N'CI', N'COTE D''LVOIRE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (47, N'HR', N'CROATIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (48, N'CU', N'CUBA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (49, N'CY', N'CYPRUS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (50, N'CZ', N'CZECH REPUBLIC')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (51, N'DK', N'DENMARK')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (52, N'DJ', N'DJIBOUTI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (53, N'DM', N'DOMINICA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (54, N'DO', N'DOMINICAN REPUBLIC')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (55, N'EC', N'ECUADOR')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (56, N'EG', N'EGYPT')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (57, N'SV', N'EL SALVADOR')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (58, N'GQ', N'EQUATORIAL GUINEA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (59, N'ER', N'ERITREA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (60, N'EE', N'ESTONIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (61, N'ET', N'ETHIOPIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (62, N'FO', N'FAROE ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (63, N'FJ', N'FIJI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (64, N'FI', N'FINLANG')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (65, N'FR', N'FRANCE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (66, N'GF', N'FRENCH GUIANA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (67, N'PF', N'FRENCH POLYNESIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (68, N'GA', N'GABON')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (69, N'GM', N'GAMBIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (70, N'GE', N'GEORGIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (71, N'DE', N'GERMANY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (72, N'GH', N'GHANA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (73, N'GI', N'GIBRALTAR')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (74, N'GR', N'GREECE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (75, N'GL', N'GREENLAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (76, N'GD', N'GRENADA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (77, N'GP', N'GUADELOUPE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (78, N'GU', N'GUAM')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (79, N'GT', N'GUATEMALA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (80, N'GN', N'GUINEA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (81, N'GW', N'GUINEA BISSAU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (82, N'GY', N'GUYANA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (83, N'HT', N'HAITI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (84, N'HN', N'HONDURAS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (85, N'HK', N'HONG KONG')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (86, N'HU', N'HUNGARY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (87, N'IS', N'ICELAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (88, N'IN', N'INDIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (89, N'ID', N'INDONESIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (90, N'IR', N'IRAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (91, N'IQ', N'IRAQ')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (92, N'IE', N'IRELAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (93, N'IL', N'ISRAEL')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (94, N'IT', N'ITALY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (95, N'JM', N'JAMAICA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (96, N'JP', N'JAPAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (97, N'JO', N'JORDAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (98, N'KZ', N'KAZAKHSTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (99, N'KE', N'KENYA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (100, N'KI', N'KIRIBATI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (101, N'KP', N'KOREA NORTH')
GO
print 'Processed 100 total records'
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (102, N'KR', N'KOREA SOUTH（REPUBLIC OF）')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (103, N'KW', N'KUWAIT')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (104, N'KG', N'KYRGYZSTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (105, N'LA', N'LAO PEOPLE S DEM REPUBLIC')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (106, N'LV', N'LATVIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (107, N'LB', N'LEBANON')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (108, N'LS', N'LESOTHO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (109, N'LR', N'LIBERIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (110, N'LY', N'LIBYA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (111, N'LT', N'LITHUANIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (112, N'LU', N'LUXEMBOURG')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (113, N'MO', N'MACAU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (114, N'MK', N'MACEDONIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (115, N'MG', N'MADAGASCAR')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (116, N'MW', N'MALAWI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (117, N'MY', N'MALAYSIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (118, N'MV', N'MALDIVES')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (119, N'ML', N'MALI')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (120, N'MT', N'MALTA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (121, N'MA', N'MOROCCO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (122, N'MH', N'MARSHALL ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (123, N'MQ', N'MARTINIQUE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (124, N'MR', N'MAURITANIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (125, N'MU', N'MAURITIUS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (126, N'YT', N'MAYOTTE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (127, N'MX', N'MEXICO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (128, N'FM', N'MICRONESIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (129, N'MD', N'MOLDOVA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (130, N'MS', N'MONTSERRAT')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (131, N'MZ', N'MOZAMBIQUE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (132, N'MM', N'MYANMAR')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (133, N'NA', N'NAMIBIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (134, N'NR', N'NAURU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (135, N'NP', N'NEPAL')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (136, N'NL', N'NETHERLANDS（HOLLAND）')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (137, N'AN', N'NERTHERLANDS ANTILLES')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (138, N'NC', N'NEW CALEDONIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (139, N'NZ', N'NEW ZEALAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (140, N'NI', N'NICARAGUA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (141, N'NE', N'NIGER')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (142, N'NG', N'NIGERIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (143, N'NU', N'NIUE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (144, N'NF', N'NORFOLK ISLAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (145, N'MP', N'NORTHERN MARIANA ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (146, N'NO', N'NORWAY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (147, N'OM', N'OMAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (148, N'PK', N'PAKISTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (149, N'PW', N'PALAU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (150, N'PA', N'PANAMA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (151, N'PG', N'PAPUA NEW GUINEA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (152, N'PY', N'PARAGUAY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (153, N'PE', N'PERU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (154, N'PH', N'PHILIPPINES')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (155, N'PL', N'POLAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (156, N'PT', N'PORTUGAL')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (157, N'PR', N'PUERTO RICO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (158, N'QA', N'QATAR')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (159, N'RE', N'REUNION')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (160, N'RO', N'ROMANIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (161, N'RU', N'RUSSIAN FEDERATION')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (162, N'RW', N'RWANDA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (163, N'WS', N'SAMOA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (164, N'SM', N'SAN MARINO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (165, N'LC', N'SANTA LUCIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (166, N'ST', N'SAO TOME AND PRINCIPE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (167, N'SA', N'SAUDI ARABIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (168, N'SN', N'SENEGAL')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (169, N'SC', N'SEYCHELLES')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (170, N'SL', N'SIERRA LEONE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (171, N'SG', N'SINGAPORE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (172, N'SK', N'SLOVAKIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (173, N'SI', N'SLOVENIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (174, N'SB', N'SOLOMON ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (175, N'SO', N'SOMALIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (176, N'ZA', N'SOUTH AFRICA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (177, N'ES', N'SPAIN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (178, N'LK', N'SRI LANKA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (179, N'KN', N'ST KITTS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (180, N'VC', N'ST VINCENT')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (181, N'SD', N'SUDAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (182, N'SR', N'SURINAME')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (183, N'SZ', N'SWAZILAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (184, N'SE', N'SWEDEN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (185, N'CH', N'SWITZERLAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (186, N'SY', N'SYRIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (187, N'TW', N'TAIWAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (188, N'TJ', N'TAJIKISTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (189, N'TZ', N'TANZANIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (190, N'TH', N'THAILAND')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (191, N'TG', N'TOGO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (192, N'TO', N'TONGA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (193, N'TT', N'TRINIDAD AND TOBAGO')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (194, N'TN', N'TUNISIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (195, N'TR', N'TURKEY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (196, N'TM', N'TURKMENISTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (197, N'TC', N'TURKS AND CAICOS ISLANDS')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (198, N'TV', N'TUVALU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (199, N'UG', N'UGANDA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (200, N'UA', N'UKRAINE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (201, N'AE', N'UNITED ARAB EMIRATES')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (202, N'GB', N'UNITED KINGDOM')
GO
print 'Processed 200 total records'
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (203, N'US', N'UNITED STATES')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (204, N'UY', N'URUGUAY')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (205, N'UZ', N'UZBRKISTAN')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (206, N'VU', N'VANUATU')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (207, N'VE', N'VENEZUELA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (208, N'VN', N'VIETNAM')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (209, N'VG', N'VIRGIN ISLANDS BRITISH')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (210, N'VI', N'VIRGIN ISLANDS U.S')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (211, N'YU', N'YUGOSLAVIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (212, N'ZR', N'ZAIRE')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (213, N'ZM', N'ZAMBIA')
INSERT [dbo].[CountryCode] ([ID], [CountryCode], [CountryName]) VALUES (214, N'ZW', N'ZIMBABWE')
/****** Object:  Table [dbo].[Company]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CID] [uniqueidentifier] NOT NULL,
	[CompanyCode] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[ShortName] [nvarchar](80) NOT NULL,
	[PostCode] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Contactor] [nvarchar](50) NOT NULL,
	[ContactorPhone] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[OrganizationCode] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_COMPANY] PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司帐号编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'CompanyCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'ShortName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'PostCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Contactor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'ContactorPhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司传真' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Fax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'OrganizationCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=可用,1=不可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签约公司' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Company'
GO
INSERT [dbo].[Company] ([CID], [CompanyCode], [FullName], [ShortName], [PostCode], [Address], [Contactor], [ContactorPhone], [Phone], [Fax], [OrganizationCode], [Status], [Remark]) VALUES (N'd2e7031c-db43-4b20-8ebe-becec8ca3f34', N'M18', N'麦考林', N'麦考林', N'200435', N'上海市普陀区长寿路1118号', N'沈志伟', N'13817011234', N'56881234', N'64950500-8999', N'SH000001', 0, NULL)
/****** Object:  View [dbo].[FindInfo]    Script Date: 04/28/2011 12:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FindInfo]
AS
SELECT     TOP (100) PERCENT a.name AS fieldname, COLUMNPROPERTY(a.id, a.name, 'IsIdentity') AS [identity], (CASE WHEN
                          (SELECT     COUNT(*)
                            FROM          sysobjects
                            WHERE      (name IN
                                                       (SELECT     name
                                                         FROM          sysindexes
                                                         WHERE      (id = a.id) AND (indid IN
                                                                                    (SELECT     indid
                                                                                      FROM          sysindexkeys
                                                                                      WHERE      (id = a.id) AND (colid IN
                                                                                                                 (SELECT     colid
                                                                                                                   FROM          syscolumns
                                                                                                                   WHERE      (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK')) > 0 THEN '1' ELSE '0' END) 
                      AS primarykey, b.name AS type, COLUMNPROPERTY(a.id, a.name, 'PRECISION') AS length, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) 
                      AS decimal, (CASE WHEN a.isnullable = 1 THEN '1' ELSE '0' END) AS isnull, CONVERT(varchar(50), ISNULL(g.value, '')) AS fielddesc, d.name
FROM         sys.syscolumns AS a LEFT OUTER JOIN
                      sys.systypes AS b ON a.xtype = b.xusertype INNER JOIN
                      sys.sysobjects AS d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT OUTER JOIN
                      sys.syscomments AS e ON a.cdefault = e.id LEFT OUTER JOIN
                      sys.extended_properties AS g ON a.colid = g.minor_id AND g.major_id = d.id
ORDER BY a.id, a.colorder
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[23] 4[18] 2[44] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 173
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 211
               Bottom = 121
               Right = 342
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 380
               Bottom = 121
               Right = 549
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 6
               Left = 587
               Bottom = 121
               Right = 725
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 6
               Left = 763
               Bottom = 121
               Right = 895
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3750
         Alias = 900
         Table = 1170
         Output = 72' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'FindInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'0
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'FindInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'FindInfo'
GO
/****** Object:  Table [dbo].[HSProduct]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HSProduct](
	[HSID] [uniqueidentifier] NOT NULL,
	[HSCode] [nvarchar](20) NOT NULL,
	[HSName] [nvarchar](50) NOT NULL,
	[DiscountTax] [decimal](10, 2) NOT NULL,
	[GeneralTax] [decimal](10, 2) NOT NULL,
	[ExportTax] [decimal](10, 2) NULL,
	[ConsumeTax] [decimal](10, 2) NULL,
	[RiseTax] [decimal](10, 2) NOT NULL,
	[CertificateSign] [nvarchar](30) NULL,
	[PricingSign] [nvarchar](30) NULL,
	[TaxDemandSign] [nvarchar](30) NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_HSPRODUCT] PRIMARY KEY CLUSTERED 
(
	[HSID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'HSID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'HS编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'HSCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'HSName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠税率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'DiscountTax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'普通税率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'GeneralTax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出口税率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'ExportTax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消费税率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'ConsumeTax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'增值税率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'RiseTax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所需证件标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'CertificateSign'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重点审价标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'PricingSign'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'征税要求标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'TaxDemandSign'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'海关商品编码详细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProduct'
GO
/****** Object:  Table [dbo].[AppModule]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppModule](
	[ModuleID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[Remark] [nvarchar](200) NULL,
	[IsLeft] [bit] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[PrivilegeDesc] [int] NULL,
	[URL] [nvarchar](200) NULL,
 CONSTRAINT [PK_APPMODULE] PRIMARY KEY CLUSTERED 
(
	[ModuleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppModule', @level2type=N'COLUMN',@level2name=N'ModuleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppModule', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppModule', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppModule', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AppModule'
GO
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'c0cf9aaf-c438-49e5-a5af-028c33dc37a3', N'新建运单', NULL, N'新建运单', 1, N'f0ca5b38-9fc5-4dd1-8e08-657c14b5e670', 192, N'http://localhost:2149/HAWBManage/HAWBAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'0e3923e1-97fc-4d00-a5d8-053d4a097975', N'角色管理', CAST(0x00009EB800DCF733 AS DateTime), N'角色管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'82be44f6-8522-497a-baab-22adf8a48940', N'菜单查询', CAST(0x00009EB800DC8A15 AS DateTime), N'菜单查询', 1, N'11f0013e-f638-41f4-80bf-a59c9eb2a9b7', 384, N'http://localhost:2149/SysManager/AppModuleManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'ab06f3d8-87ae-4231-a004-34cc07566b2c', N'地址簿查询', NULL, N'地址簿查询', 1, N'c27b0ee0-8818-41c7-a758-73e8cad96333', 864, N'http://localhost:2149/AddressBookManage/AddressBookManagemnet.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'a6ea3649-ec9a-4541-934b-3556a0d6d3bf', N'总运单管理', CAST(0x00009EB700BC02EE AS DateTime), N'总运单管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'0cac30a9-3adc-4622-80ef-39cc81b6853d', N'登录帐号查询', CAST(0x00009EB800DBB950 AS DateTime), N'登录帐号查询', 1, N'538affe4-4bc7-4935-aeb1-3eb6dbebf0ba', 320, N'http://localhost:2149/Account/UserManager.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'538affe4-4bc7-4935-aeb1-3eb6dbebf0ba', N'登录帐号管理', CAST(0x00009EB800DADDFB AS DateTime), N'登录帐号管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'816686ea-64e3-4665-bef5-3ec972ed3a26', N'路单生成', CAST(0x00009ECC0173B876 AS DateTime), N'路单生成', 1, N'6f2593c5-4c5c-4f90-b19f-e7927d03f15f', 1020, N'http://localhost:2149/CustomsClearance/WayBillGenerate.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'b50d0de6-9cbd-45b7-8d6f-4bf512aad37b', N'新增登录用户', CAST(0x00009EB800DBF120 AS DateTime), N'新增登录用户', 1, N'538affe4-4bc7-4935-aeb1-3eb6dbebf0ba', 128, N'http://localhost:2149/Account/AddUser.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'3a4ebee1-73fc-4ddd-8b06-4ca6900e2014', N'公司帐号管理', CAST(0x00009EB700BC39D3 AS DateTime), N'公司帐号管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'cf20fbf2-7679-48fc-88d4-4ff9f3f6c7e4', N'新建总运单', CAST(0x00009EB700BDD696 AS DateTime), N'新建总运单', 1, N'a6ea3649-ec9a-4541-934b-3556a0d6d3bf', 128, N'http://localhost:2149/MawbManage/MawbAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'fdeb4cbc-6f44-4981-b258-5189f60c135c', N'航班查询', CAST(0x00009EB700BE43AA AS DateTime), N'航班查询', 1, N'c729fe1d-ac09-4bbb-9567-532a358fe21b', 256, N'http://localhost:2149/FlightManage/FlightManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'c729fe1d-ac09-4bbb-9567-532a358fe21b', N'航班管理', CAST(0x00009EB700BC1597 AS DateTime), N'航班管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'8dc626cb-dab1-43fe-adb0-53e031082142', N'添加角色', CAST(0x00009EB800DD3458 AS DateTime), N'添加角色', 1, N'0e3923e1-97fc-4d00-a5d8-053d4a097975', 128, N'http://localhost:2149/SysManager/AddAppModule.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'3fe9b416-9150-4d4a-9741-561a2bd312bc', N'海关编码查询', CAST(0x00009EB700C4A0BD AS DateTime), N'海关编码查询', 1, N'a27b449b-20af-4a65-b6c7-df21a11481ff', 352, N'http://localhost:2149/ProductManage/ProductManagemnet.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'ee700d30-2394-48ef-be3f-577d57fd0a51', N'总运单查询', CAST(0x00009EB700BDF270 AS DateTime), N'总运单查询', 1, N'a6ea3649-ec9a-4541-934b-3556a0d6d3bf', 372, N'http://localhost:2149/MawbManage/MawbManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'e5565367-ddb2-4b8a-9c3b-5c71be164705', N'个人帐号查询', CAST(0x00009EB700C08C71 AS DateTime), N'个人帐号查询', 1, N'9c3f2f05-3f05-49f0-9442-6e5a56d7d8de', 352, N'http://localhost:2149/PersonnelManage/UserManage/UserManagemnet.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'd46e025f-7199-4c4a-91d7-62f237a2fdca', N'地区三字码查询', CAST(0x00009EB700C41A77 AS DateTime), N'地区三字码查询', 1, N'52a1e8f3-4a5b-46a7-8072-a4f3136e590f', 352, N'http://localhost:2149/RegionZiMaManage/regionManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'7b5c1465-255d-40b2-b742-652684e3e611', N'角色查询', NULL, N'角色查询', 1, N'0e3923e1-97fc-4d00-a5d8-053d4a097975', 480, N'http://localhost:2149/SysManager/RoleManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'4956ba7d-9db8-44d0-bf02-6568c83ac41f', N'新增地区三字码', CAST(0x00009EB700C3C538 AS DateTime), N'新增地区三字码', 1, N'52a1e8f3-4a5b-46a7-8072-a4f3136e590f', 128, N'http://localhost:2149/RegionZiMaManage/regionAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'f0ca5b38-9fc5-4dd1-8e08-657c14b5e670', N'运单管理', CAST(0x00009EB700BBECC6 AS DateTime), N'运单管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'9c3f2f05-3f05-49f0-9442-6e5a56d7d8de', N'个人帐号管理', CAST(0x00009EB700BC4F2F AS DateTime), N'个人帐号管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'276a8bc2-73b4-4b83-95f3-6efe944a094f', N'拆包操作', CAST(0x00009ECC0173E853 AS DateTime), N'拆包操作', 1, N'6f2593c5-4c5c-4f90-b19f-e7927d03f15f', 1020, N'http://localhost:2149/PackageGetOffManage/PackageSplit.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'c27b0ee0-8818-41c7-a758-73e8cad96333', N'地址薄管理', CAST(0x00009EB700BC5E5F AS DateTime), N'地址簿管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'84fbd0e8-481d-4f10-909f-79b29c940a16', N'新建部门', CAST(0x00009EB700BF28CD AS DateTime), N'新建部门', 1, N'ca40eff1-84ef-4c58-9b4d-c56b799eb651', 128, N'http://localhost:2149/PersonnelManage/DepartmentManage/DepartmentAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'332e4393-4506-44cb-843a-805a0fca3882', N'公司帐号查询', CAST(0x00009EB700BEC429 AS DateTime), N'公司帐号查询', 1, N'3a4ebee1-73fc-4ddd-8b06-4ca6900e2014', 352, N'http://localhost:2149/PersonnelManage/CompanyManage/CompanyManagemnet.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'15d3b5d2-2e6d-49d8-aca6-85f6e57c0567', N'包管理', CAST(0x00009EB700BBF6E6 AS DateTime), N'包管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'e905bf16-4202-4f33-8f8e-87da4afe7ba8', N'报关维护', CAST(0x00009ECC017379AD AS DateTime), N'报关维护', 1, N'6f2593c5-4c5c-4f90-b19f-e7927d03f15f', 1020, N'http://localhost:2149/CustomsClearance/ClearanceManage.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'009c8cbe-dcbd-491f-8fb0-8de4beb7dfe8', N'添加菜单', CAST(0x00009EB800DC56A4 AS DateTime), N'添加菜单', 1, N'11f0013e-f638-41f4-80bf-a59c9eb2a9b7', 128, N'http://localhost:2149/SysManager/AddAppModule.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'cdef43fd-5a8f-43b0-a2ce-8e273747895b', N'打包操作', CAST(0x00009ECC0173D70A AS DateTime), N'打包操作', 1, N'6f2593c5-4c5c-4f90-b19f-e7927d03f15f', 1020, N'http://localhost:2149/PackageGetOffManage/PackageAttribute.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'8c077d67-1df0-4507-960e-8ec0ff5d4a2f', N'新建地址簿', CAST(0x00009EB700C29842 AS DateTime), N'新建地址簿', 1, N'c27b0ee0-8818-41c7-a758-73e8cad96333', 128, N'http://localhost:2149/AddressBookManage/AddressBookAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'e9a68782-55bc-4eab-b0ce-9e0955843b89', N'国家二字码管理', CAST(0x00009EB700BC6F87 AS DateTime), N'国家二字码管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'48c70d58-69b3-4ebd-baba-a1eb259fcc8e', N'新建公司帐号', CAST(0x00009EB700BEA064 AS DateTime), N'新建公司帐号', 1, N'3a4ebee1-73fc-4ddd-8b06-4ca6900e2014', 128, N'http://localhost:2149/PersonnelManage/CompanyManage/CompanyAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'43330e27-c58a-46fc-948f-a3a492459e40', N'国家二子码查询', CAST(0x00009EB700C2F1BE AS DateTime), N'国家二字码查询', 1, N'e9a68782-55bc-4eab-b0ce-9e0955843b89', 352, N'http://localhost:2149/CountryZiMaManage/TwoZiMaManage.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'52a1e8f3-4a5b-46a7-8072-a4f3136e590f', N'地区三字码管理', CAST(0x00009EB700BC7FC4 AS DateTime), N'地区三字码管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'11f0013e-f638-41f4-80bf-a59c9eb2a9b7', N'菜单管理', CAST(0x00009EB800DC2EC2 AS DateTime), N'菜单管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'87e49856-18fc-46a7-8b10-acec6197a094', N'清关税率', CAST(0x00009ECC0173A392 AS DateTime), N'清关税率', 1, N'6f2593c5-4c5c-4f90-b19f-e7927d03f15f', 1020, N'http://localhost:2149/CustomsClearance/TaxClearanceClear.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'16109c7c-3f03-419e-9a9d-bbca0d372512', N'新增海关编码', CAST(0x00009EB700C466D3 AS DateTime), N'新增海关编码', 1, N'a27b449b-20af-4a65-b6c7-df21a11481ff', 128, N'http://localhost:2149/ProductManage/ProductAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'317d7f79-394c-44c4-9fee-bc5f558d1162', N'新建包', CAST(0x00009EB700BD702F AS DateTime), N'新建包', 1, N'15d3b5d2-2e6d-49d8-aca6-85f6e57c0567', 128, N'http://localhost:2149/PackageManage/packageAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'ca40eff1-84ef-4c58-9b4d-c56b799eb651', N'部门帐号管理', CAST(0x00009EB700BC4599 AS DateTime), N'部门帐号管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'292c7388-943e-4017-ac54-ca78ccb9cbbd', N'部门查询', CAST(0x00009EB700BF76F6 AS DateTime), N'部门查询', 1, N'ca40eff1-84ef-4c58-9b4d-c56b799eb651', 352, N'http://localhost:2149/PersonnelManage/DepartmentManage/DepartmentManagemnet.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'0250b1b8-1047-49ae-9e54-d16e50bc032f', N'包详细信息', CAST(0x00009EB900ECEC95 AS DateTime), N'包详细信息', 1, N'15d3b5d2-2e6d-49d8-aca6-85f6e57c0567', 68, N'http://localhost:2149/PackageManage/PackageDetails.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'26c9e960-7572-40e6-9c4d-d3b55ec51135', N'新增国家二子码', CAST(0x00009EB700C33088 AS DateTime), N'新增国家二字码', 1, N'e9a68782-55bc-4eab-b0ce-9e0955843b89', 128, N'http://localhost:2149/CountryZiMaManage/TwoZiMaAdd.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'492959af-3bb9-43f4-b709-dec067ced397', N'包查询', CAST(0x00009EB700BDA0B4 AS DateTime), N'包查询', 1, N'15d3b5d2-2e6d-49d8-aca6-85f6e57c0567', 356, N'http://localhost:2149/PackageManage/packageManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'9d24604c-68d0-443f-ae60-def0cd0e3c4c', N'运单查询', CAST(0x00009EB700BD2718 AS DateTime), N'运单查询', 1, N'f0ca5b38-9fc5-4dd1-8e08-657c14b5e670', 368, N'http://localhost:2149/HAWBManage/HAWBManagement.aspx')
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'a27b449b-20af-4a65-b6c7-df21a11481ff', N'海关码管理', CAST(0x00009EB700BCA3E3 AS DateTime), N'海关码管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'6f2593c5-4c5c-4f90-b19f-e7927d03f15f', N'报关管理', CAST(0x00009ECC017343E1 AS DateTime), N'报关管理', 0, NULL, NULL, NULL)
INSERT [dbo].[AppModule] ([ModuleID], [Description], [CreateTime], [Remark], [IsLeft], [ParentId], [PrivilegeDesc], [URL]) VALUES (N'f8167443-bc5e-41a6-a0dc-ef8401261174', N'新建个人帐号', CAST(0x00009EB700C04384 AS DateTime), N'新建个人帐号', 1, N'9c3f2f05-3f05-49f0-9442-6e5a56d7d8de', 128, N'http://localhost:2149/PersonnelManage/UserManage/UserAdd.aspx')
/****** Object:  Table [dbo].[Role]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Status] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[LastUpdateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Role] ([RoleID], [Name], [Status], [Description], [CreateTime], [LastUpdateTime], [UpdateId], [Remark]) VALUES (N'c75ba0a0-ced4-48de-9e1d-ef25393968e8', N'管理员', 1, N'管理员', CAST(0x00009EB700BB1A71 AS DateTime), CAST(0x00009EB700D372D9 AS DateTime), NULL, N'管理员')
/****** Object:  Table [dbo].[RegionCode]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegionCode](
	[ID] [int] NOT NULL,
	[CountryCode] [char](2) NOT NULL,
	[RegionCode] [char](3) NOT NULL,
	[RegionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_REGIONCODE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地区三字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RegionCode'
GO
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (2, N'46', N'ABX', N'ABIDJAN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (3, N'21', N'BUQ', N'BULAWAYO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (4, N'21', N'HRE', N'HARARE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (5, N'1 ', N'KBL', N'KABUL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (6, N'2 ', N'TIO', N'ALBANIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (7, N'2 ', N'TIA', N'TIRANA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (8, N'3 ', N'ALO', N'ALGERIA OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (9, N'3 ', N'ALG', N'ALGIERS ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (10, N'4 ', N'PPG', N'PAGO PAGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (11, N'5 ', N'AND', N'ANDORRA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (12, N'6 ', N'ANO', N'ANGOLA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (13, N'6 ', N'LAD', N'LUANDA ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (14, N'7 ', N'AXA', N'ANGUILLA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (15, N'8 ', N'ANU', N'ANTIGUA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (16, N'9 ', N'BUE', N'BUENOS AIRES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (17, N'9 ', N'AEP', N'BUENOS AIRES AEROPARQUE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (18, N'9 ', N'ARO', N'BUENOS AIRES OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (19, N'9 ', N'ARX', N'BUENOS AIRES UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (20, N'9 ', N'EZE', N'EZEIZA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (21, N'10', N'AMA', N'ARMENIA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (22, N'10', N'EVN', N'YEREVAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (23, N'11', N'AUA', N'ARUBA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (24, N'12', N'ADL', N'ADELAIDE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (25, N'12', N'AUO', N'AUSTRALIA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (26, N'12', N'AUX', N'AUSTRALIA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (27, N'12', N'BNE', N'BRISBANE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (28, N'12', N'CNS', N'CAIRNS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (29, N'12', N'CBR', N'CANBERRA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (30, N'12', N'DRW', N'DARWIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (31, N'12', N'MEL', N'MELBOURNE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (32, N'12', N'NTL', N'NEWCASTLE AUSTRALIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (33, N'12', N'PER', N'PERTH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (34, N'12', N'SYD', N'SYDNEY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (35, N'12', N'SYT', N'SYDNEY MAIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (36, N'12', N'TSV', N'TOWNSVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (37, N'13', N'GRZ', N'GRAZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (38, N'13', N'INN', N'INNSBRUCH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (39, N'13', N'LTA', N'LAUTERACH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (40, N'13', N'LNZ', N'LINZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (41, N'13', N'LXZ', N'LINZ AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (42, N'13', N'SZG', N'SALZBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (43, N'13', N'VIE', N'VIENNA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (44, N'13', N'VZE', N'VIENNA AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (45, N'13', N'AXO', N'VIENNA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (46, N'13', N'AXX', N'VIENNA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (47, N'14', N'AZE', N'AZERBAIJAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (48, N'14', N'BAK', N'BAKU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (49, N'40', N'SHA', N'SHANGHAI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (51, N'15', N'BHO', N'BAHAMAS OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (52, N'15', N'FPO', N'FREEPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (53, N'15', N'NAS', N'NASSAU ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (54, N'16', N'BAH', N'BAHRAIN ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (55, N'17', N'CGP', N'CHITTAGONG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (56, N'17', N'DAC', N'DHAKA ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (57, N'17', N'BGO', N'DHAKA OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (58, N'17', N'BGX', N'DHAKA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (59, N'18', N'BGI', N'BRIDGETOWN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (60, N'19', N'BYO', N'BELARUS OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (61, N'19', N'MSQ', N'MINSK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (62, N'20', N'ANR', N'ANTWERPEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (63, N'20', N'BZH', N'BORNEM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (64, N'20', N'BRU', N'BRUSSELS ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (65, N'20', N'BGU', N'BRUSSELS AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (66, N'20', N'BEO', N'BRUSSELS OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (67, N'20', N'BVN', N'BRUSSELS ROW GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (68, N'20', N'BRT', N'BRUSSELS TANAT ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (69, N'20', N'BEX', N'BRUSSELS UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (70, N'20', N'CRL', N'CHARLEROI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (71, N'20', N'BEC', N'EC PARCELS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (72, N'20', N'GNT', N'GENT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (73, N'20', N'KOR', N'KORTRIJK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (74, N'20', N'LGG', N'LIEGE ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (77, N'21', N'BZE', N'BELIZE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (78, N'21', N'BZO', N'BELIZE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (79, N'22', N'BNO', N'BENIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (80, N'22', N'COO', N'COTONOU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (81, N'23', N'BDA', N'BERMUDA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (82, N'24', N'TMU', N'THIMBU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (83, N'25', N'LPB', N'LA PAZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (84, N'25', N'LPO', N'LA PAZ OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (85, N'25', N'LPX', N'LA PAZ UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (86, N'25', N'SRZ', N'SANTA CRUZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (87, N'26', N'GBE', N'GABORONE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (88, N'26', N'GBO', N'GABORONE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (89, N'27', N'BHZ', N'BELO HORIZONTE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (90, N'27', N'BNU', N'BLUMENAU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (91, N'27', N'CPQ', N'CAMPINAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (92, N'27', N'CWB', N'CURITIBA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (93, N'27', N'FOR', N'FORTALEZA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (94, N'27', N'JOI', N'JOINVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (95, N'27', N'POA', N'PORTO ALEGRE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (96, N'27', N'REC', N'RECIFE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (97, N'27', N'RIO', N'RIO DE JANEIRO ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (98, N'27', N'BRO', N'RIO DE JANEIRO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (99, N'27', N'BRX', N'RIO DE JANEIRO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (100, N'27', N'SSA', N'SALVADOR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (101, N'27', N'SSZ', N'SANTOS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (102, N'27', N'SAO', N'SAO PAULO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (103, N'28', N'BWN', N'BRUNEI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (104, N'28', N'BWO', N'BRUNEI OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (105, N'29', N'SOF', N'SOFIYA')
GO
print 'Processed 100 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (106, N'29', N'BUO', N'SOFIYA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (107, N'30', N'BOY', N'BOBO-DIOUL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (108, N'30', N'OUA', N'OUAGADOUGOU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (109, N'30', N'OUO', N'OUAGADOUGOU OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (110, N'31', N'BJM', N'BUJUMBURA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (111, N'31', N'BJO', N'BUJUMBURA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (112, N'32', N'PNH', N'PHNOM PENH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (113, N'33', N'DLA', N'DOUALA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (114, N'33', N'DLO', N'DOUALA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (115, N'33', N'YAO', N'YAOUNDE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (116, N'34', N'YYC', N'CALGARY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (117, N'34', N'YYG', N'CHARLOTTETOWN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (118, N'34', N'YEG', N'EDMONTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (119, N'34', N'YFC', N'FREDERICTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (120, N'34', N'YHZ', N'HALIFAX')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (121, N'34', N'YHM', N'HAMILTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (122, N'34', N'YQM', N'MONCTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (123, N'34', N'YUL', N'MONTREAL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (124, N'34', N'YOW', N'OTTAWA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (125, N'34', N'YQB', N'QUEBEC CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (126, N'34', N'YQR', N'REGINA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (127, N'34', N'YSJ', N'SAINT JOHN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (128, N'34', N'YXE', N'SASKATOON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (129, N'34', N'YYT', N'ST. JOHNS NF')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (130, N'34', N'YQT', N'THUNDER BAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (131, N'34', N'YYZ', N'TORONTO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (132, N'34', N'YYP', N'TRONTO EMS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (133, N'34', N'YYO', N'TORONTO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (134, N'34', N'YYX', N'TORONTO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (135, N'34', N'YVR', N'VANCOUVER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (136, N'34', N'YYJ', N'VICTORIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (137, N'34', N'YQG', N'WINDSOR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (138, N'34', N'YWG', N'WINNIPEG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (139, N'35', N'RAI', N'CAPE VERDE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (140, N'36', N'GCM', N'GRAND CAYMAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (141, N'37', N'BGF', N'BANGUI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (142, N'37', N'XGO', N'CENTRAL AFRICAN REPUBLIC')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (143, N'38', N'NDJ', N'NDJAMENA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (144, N'39', N'ANF', N'ANTOFAGASTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (145, N'39', N'ARI', N'ARICA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (146, N'39', N'CCP', N'CONCEPCION')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (147, N'39', N'IQQ', N'IQUIQUE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (148, N'39', N'PUQ', N'PUNTA ARENAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (149, N'39', N'SCL', N'SANTIAGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (150, N'39', N'CHO', N'SANTIAGO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (151, N'39', N'CHX', N'SANTIAGO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (152, N'39', N'VAP', N'VALPARAISO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (153, N'40', N'HKG', N'HONG KONG ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (154, N'40', N'HTT', N'HONG KONG TANAT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (158, N'41', N'BAQ', N'BARRANQUILLA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (159, N'41', N'BOG', N'BOGOTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (160, N'41', N'CBO', N'BOGOTA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (161, N'41', N'CBX', N'BOGOTA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (162, N'42', N'YVA', N'COMOROS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (163, N'43', N'BZV', N'BRAZZAVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (164, N'43', N'BXV', N'BRAZZAVILLE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (165, N'43', N'PNR', N'POINTE-NOIRE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (166, N'44', N'RAR', N'COOK ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (167, N'45', N'SJO', N'SAN JOSE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (168, N'45', N'CRO', N'SAN JOSE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (169, N'46', N'ABJ', N'ABIDJAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (170, N'46', N'ABO', N'ABIDJAN OHTER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (171, N'47', N'HVO', N'CROATIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (172, N'47', N'ZAG', N'ZAGREB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (173, N'48', N'HAV', N'HAVANA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (174, N'49', N'CYO', N'CYPRUS OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (175, N'49', N'LCA', N'LARNACA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (176, N'49', N'CYX', N'LARNACA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (177, N'49', N'LMS', N'LIMASSOL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (178, N'49', N'NIC', N'NICOSIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (179, N'50', N'BRQ', N'BRNO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (180, N'50', N'OSR', N'OSTRAVA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (181, N'50', N'PRG', N'PRAGUE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (182, N'50', N'CZO', N'PRAGUE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (183, N'51', N'AAL', N'AALBORG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (184, N'51', N'BLL', N'BILLUND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (185, N'51', N'BXL', N'BILLUND AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (186, N'51', N'CPH', N'COPENHAGEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (187, N'51', N'CPG', N'COPENHAGEN AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (188, N'51', N'CZH', N'COPENHAGEN AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (189, N'51', N'DXO', N'COPENHAGEN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (190, N'51', N'DXX', N'COPENHAGEN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (191, N'52', N'JIB', N'DJIBOUTI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (192, N'53', N'DIM', N'DIMINICA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (193, N'54', N'STI', N'SANTIAGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (194, N'54', N'SDQ', N'SANTO DOMINGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (195, N'55', N'GYE', N'GUAYAQUIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (196, N'55', N'UIO', N'QUITO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (197, N'55', N'ECO', N'QUITO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (198, N'55', N'ECX', N'QUITO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (199, N'56', N'ALX', N'ALEXANDRIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (200, N'56', N'CAI', N'CAIRO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (201, N'56', N'EGO', N'CAIRO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (202, N'56', N'EGX', N'CAIRO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (203, N'56', N'PSD', N'PORT SAID')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (204, N'57', N'SAL', N'SAN SALVADOR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (205, N'57', N'ESO', N'SAN SALVADOR OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (206, N'57', N'ESX', N'SAN SALVADOR UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (207, N'58', N'BSG', N'BATA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (208, N'59', N'ASM', N'ASMERA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (209, N'60', N'EEO', N'ESTONIA')
GO
print 'Processed 200 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (210, N'60', N'TLL', N'TALLINN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (211, N'61', N'ADD', N'ADDIS ABABA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (212, N'62', N'FAE', N'FAROE ISLANDS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (213, N'63', N'NAN', N'NADI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (214, N'63', N'SUV', N'SUVA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (215, N'64', N'HEL', N'HELSINKI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (216, N'64', N'HXL', N'HELSINKI AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (217, N'64', N'FNO', N'HELSINKI OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (218, N'64', N'FNX', N'HELSINKI UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (219, N'64', N'LTI', N'LAHTI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (220, N'64', N'TRE', N'TAMPERE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (221, N'64', N'TKU', N'TURKU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (222, N'65', N'NCY', N'ANNECY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (223, N'65', N'AVN', N'AVIGNON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (224, N'65', N'BSA', N'BESANCON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (225, N'65', N'BOD', N'BORDEAUX')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (226, N'65', N'CPY', N'CDG CHRONOPOST ORIGIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (227, N'65', N'CRC', N'CERGY-PONTOISE NANTERRE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (228, N'65', N'CXG', N'CHARLES DE GAULLE AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (229, N'65', N'FXO', N'CHARLES DE GAULLE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (230, N'65', N'FXX', N'CHARLES DE GAULLE UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (231, N'65', N'ATS', N'CHRONO AFRICAN TRANSHIPMENT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (232, N'65', N'FXP', N'CHRONO POST FRANCE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (233, N'65', N'GNB', N'GRENOBLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (234, N'65', N'HEF', N'HAUT DE SEINE CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (235, N'65', N'ISF', N'ISSY LES MOULINEAUX CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (236, N'65', N'IVF', N'IVRY SUR SEINE CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (237, N'65', N'LEH', N'LE HAVRE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (238, N'65', N'LEM', N'LE MANS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (239, N'65', N'LIL', N'LILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (240, N'65', N'LLF', N'LILLE CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (241, N'65', N'LYS', N'LYON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (242, N'65', N'LXS', N'LYON AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (243, N'65', N'LZS', N'LYON AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (244, N'65', N'LYY', N'LYON FALLAVIER CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (245, N'65', N'LYF', N'LYON VILLEURBANNE CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (246, N'65', N'MRS', N'MARSEILLES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (247, N'65', N'MZM', N'METZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (248, N'65', N'MCM', N'MONACO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (249, N'65', N'MLH', N'MULHOUSE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (250, N'65', N'NTE', N'NANTES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (251, N'65', N'NZE', N'NANTES AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (252, N'65', N'NTF', N'NANTES CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (253, N'65', N'NCE', N'NICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (254, N'65', N'PAL', N'PALAISEAU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (255, N'65', N'CDG', N'PARIS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (256, N'65', N'CTS', N'PARIS AFRICAN GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (257, N'65', N'CDX', N'PARIS XP')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (258, N'65', N'QZV', N'ROISSY CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (259, N'65', N'URO', N'ROUEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (260, N'65', N'SXB', N'STRASBOURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (261, N'65', N'SYB', N'STRASBOURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (262, N'65', N'SXY', N'STRASBOURG CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (263, N'65', N'TLS', N'TOULOUSE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (264, N'65', N'TXS', N'TOULOUSE AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (265, N'65', N'TLY', N'TOULOUSE CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (266, N'65', N'VDF', N'VAL DE FRANCE CHRONOPOST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (267, N'66', N'CAY', N'CAYENNE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (268, N'66', N'FGO', N'FRENCH GUYANA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (269, N'67', N'PPT', N'PAPEETE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (270, N'67', N'THI', N'TAHITI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (271, N'68', N'LBO', N'GABON OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (272, N'68', N'LBV', N'LIBREVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (273, N'68', N'LBX', N'LIBREVILLE UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (274, N'68', N'POG', N'PORT GENTIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (275, N'69', N'BJL', N'BANJUL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (276, N'70', N'GXO', N'GEORGIA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (277, N'70', N'TBS', N'TBILISI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (278, N'71', N'AGB', N'AUGSBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (279, N'71', N'BER', N'BERLIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (280, N'71', N'SXF', N'BERLIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (281, N'71', N'BIE', N'BIELEFELD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (282, N'71', N'BRE', N'BREMEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (283, N'71', N'YYK', N'COLOGNE CANADA GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (284, N'71', N'CGW', N'COLOGNE FAR EAST GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (285, N'71', N'MXI', N'COLOGNE MIAMI GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (286, N'71', N'DXC', N'COLOGNE MIDDLE EAST GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (287, N'71', N'CGX', N'COLOGNE SMART')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (288, N'71', N'JFX', N'COLOGNE USA GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (289, N'71', N'DRT', N'DORTMUND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (290, N'71', N'DTM', N'DORTMUND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (291, N'71', N'DRS', N'DRESDEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (292, N'71', N'DUS', N'DUESSELDORF')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (293, N'71', N'EXO', N'EAST GERMANY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (294, N'71', N'CGT', N'EC PARCELS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (295, N'71', N'ETL', N'ELTEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (296, N'71', N'EMM', N'EMMERICH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (297, N'71', N'ERF', N'ERFURT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (298, N'71', N'FGW', N'FRANKFURT AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (299, N'71', N'FRA', N'FRANKFURT AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (300, N'71', N'FXA', N'FRANKFURT AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (301, N'71', N'FRT', N'FRANKFURT DEPOT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (302, N'71', N'FTS', N'FRANKFURT EAST BLOCK GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (303, N'71', N'FBG', N'FREIBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (304, N'71', N'GRA', N'GERA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (305, N'71', N'WGO', N'GERMANY OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (306, N'71', N'WGX', N'GERMANY UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (307, N'71', N'HAM', N'HAMBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (308, N'71', N'HMK', N'HAMMINKELN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (309, N'71', N'HAJ', N'HANNOVER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (310, N'71', N'HFD', N'HERFORD')
GO
print 'Processed 300 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (311, N'71', N'KLE', N'KASSEL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (312, N'71', N'KBZ', N'KOBLENZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (313, N'71', N'KOZ', N'KOBLENZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (314, N'71', N'COL', N'KOELN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (315, N'71', N'CGN', N'KOELN AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (316, N'71', N'HUB', N'KOELN HUB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (317, N'71', N'CXN', N'KOELN SKYPAK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (318, N'71', N'LPZ', N'LEIPZIG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (319, N'71', N'LEJ', N'LEIPZIG AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (320, N'71', N'MDG', N'MAGDEBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (321, N'71', N'MHZ', N'MALCHIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (322, N'71', N'MHN', N'MANNHEIM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (323, N'71', N'MUC', N'MUENCHEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (324, N'71', N'NUE', N'NUERNBERG AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (325, N'71', N'NXE', N'NUERNBERG AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (326, N'71', N'NBE', N'NUERNBERG DEPOT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (327, N'71', N'SCN', N'SAARBRUECKEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (328, N'71', N'SWN', N'SCHWERIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (329, N'71', N'SIE', N'SIEGEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (330, N'71', N'SBG', N'STRAUBING')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (331, N'71', N'STR', N'STUTTGART')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (332, N'71', N'TRS', N'TROISDORF HEAD OFFICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (333, N'71', N'ULM', N'ULM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (334, N'71', N'VGN', N'VILLINGEN-SCHWENNINGEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (335, N'71', N'WZB', N'WUERZBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (336, N'72', N'ACC', N'ACCRA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (337, N'72', N'ACO', N'ACCRA OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (338, N'72', N'ACX', N'ACCRA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (339, N'73', N'GIB', N'GIBRALTAR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (340, N'74', N'ATH', N'ATHENS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (341, N'74', N'GRO', N'ATHENS OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (342, N'74', N'GXX', N'ATHENS UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (343, N'74', N'ATX', N'ATHENS XP')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (344, N'74', N'PRE', N'PIRAEUS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (345, N'74', N'SKG', N'THESSALONIKI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (346, N'75', N'GOH', N'NUUK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (347, N'76', N'GND', N'GRENADA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (348, N'77', N'GDY', N'GUADELOUPE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (349, N'77', N'PTP', N'POINTE-A-PITRE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (350, N'77', N'SBH', N'SAINT BARTHELEMY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (351, N'77', N'SFG', N'SAINT MARTIN FR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (352, N'78', N'GUM', N'GUAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (353, N'78', N'GMO', N'GUAM OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (354, N'79', N'GUA', N'GUATEMALA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (355, N'79', N'GUO', N'GUATEMALA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (356, N'80', N'CKY', N'CONAKRY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (357, N'81', N'BXO', N'BISSAU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (358, N'82', N'GEO', N'GEORGETOWN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (359, N'83', N'PAP', N'PORT-AU-PRINCE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (360, N'84', N'SAP', N'SAN PEDRO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (361, N'84', N'TGU', N'TEGUCIGALPA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (362, N'84', N'HXO', N'TEGUCIGALPA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (363, N'86', N'BUD', N'BUDAPEST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (364, N'86', N'HUO', N'BUDAPEST OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (365, N'87', N'REO', N'ICELAND OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (366, N'87', N'REK', N'REYKJAVIK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (367, N'88', N'AMD', N'AHMEDABAD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (368, N'88', N'BLR', N'BANGALORE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (369, N'88', N'BOM', N'BOMBAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (370, N'88', N'BOO', N'BOMBAY OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (371, N'88', N'BOX', N'BOMBAY UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (372, N'88', N'CCU', N'CALCUTTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (373, N'88', N'COK', N'COCHIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (374, N'88', N'DEL', N'DELHI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (375, N'88', N'HYD', N'HYDERABAD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (376, N'88', N'MAA', N'MADRAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (377, N'88', N'IXE', N'MANGALORE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (378, N'89', N'BPN', N'BALIKPAPAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (379, N'89', N'BDO', N'BANDUNG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (380, N'89', N'BDJ', N'BANJARMASIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (381, N'89', N'DPS', N'DENPASER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (382, N'89', N'JKT', N'JAKARTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (383, N'89', N'INO', N'JAKARTA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (384, N'89', N'INX', N'JAKARTA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (385, N'89', N'MES', N'MEDAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (386, N'89', N'PLM', N'PALEMBANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (387, N'89', N'SRI', N'SAMARINDA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (388, N'89', N'SGQ', N'SANGGATA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (389, N'89', N'SRG', N'SEMARANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (390, N'89', N'SOC', N'SOLO CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (391, N'89', N'SUB', N'SURABAYA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (392, N'89', N'UPG', N'UJUNG PANDANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (393, N'90', N'THR', N'TEHRAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (394, N'90', N'IAO', N'TEHRAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (395, N'91', N'BGW', N'BAGHDAD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (396, N'91', N'IRQ', N'IRAQ OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (397, N'92', N'ORK', N'CORK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (398, N'92', N'DUB', N'DUBLIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (399, N'92', N'DZB', N'DUBLIN AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (400, N'92', N'IRO', N'DUBLIN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (401, N'92', N'IRX', N'DUBLIN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (402, N'92', N'SNN', N'LIMERICK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (403, N'92', N'SZN', N'LIMERICK AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (404, N'92', N'SXL', N'SLIGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (405, N'92', N'WAT', N'WATERFORD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (406, N'93', N'TVO', N'ISRAEL OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (407, N'93', N'TLV', N'TEL AVIV')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (408, N'94', N'AOI', N'ANCONA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (409, N'94', N'BRI', N'BARI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (410, N'94', N'BRG', N'BERGAMO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (411, N'94', N'BGY', N'BERGAMO AIRPORT')
GO
print 'Processed 400 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (412, N'94', N'BEA', N'BIELLA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (413, N'94', N'BLQ', N'BOLOGNA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (414, N'94', N'ITR', N'BRESCIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (415, N'94', N'CVR', N'COMO VARES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (416, N'94', N'FLR', N'FLORENCE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (417, N'94', N'FRL', N'FORLI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (418, N'94', N'FXL', N'FORLI AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (419, N'94', N'GOA', N'GENOA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (420, N'94', N'GXA', N'GENOA AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (421, N'94', N'IXO', N'ITALY OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (422, N'94', N'ITN', N'ITALY OTHER NORTH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (423, N'94', N'ITS', N'ITALY OTHER SOUTH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (424, N'94', N'MIL', N'MILAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (425, N'94', N'MIG', N'MILAN AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (426, N'94', N'MHO', N'MILAN HEAD OFFICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (427, N'94', N'MIX', N'MILAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (428, N'94', N'IXX', N'MILAN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (429, N'94', N'MDA', N'MODENA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (430, N'94', N'NAP', N'NAPLES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (431, N'94', N'PDV', N'PADOVA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (432, N'94', N'PSR', N'PESCARA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (433, N'94', N'PSA', N'PISA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (434, N'94', N'PON', N'PORDENONE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (435, N'94', N'ROM', N'ROME')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (436, N'94', N'CIA', N'ROME AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (437, N'94', N'TRN', N'TURIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (438, N'94', N'VCE', N'VENICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (439, N'94', N'VRN', N'VERONA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (440, N'94', N'VNZ', N'VICENZA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (441, N'95', N'JAO', N'JAMAICA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (442, N'95', N'KIN', N'KINGSTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (443, N'95', N'MBJ', N'MONTEGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (444, N'96', N'HIJ', N'HIROSHIMA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (445, N'96', N'KOB', N'KOBE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (446, N'96', N'KYO', N'KYOTO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (447, N'96', N'NGS', N'NAGASAKI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (448, N'96', N'NGO', N'NAGOYA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (449, N'96', N'NRT', N'NARITA AIRPORT TOKYO EAST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (450, N'96', N'OSA', N'OSAKA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (451, N'96', N'TYO', N'TOKYO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (452, N'96', N'JXO', N'TOKYO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (453, N'96', N'JXX', N'TOKYO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (454, N'96', N'YOH', N'YOKOHAMA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (455, N'97', N'AMM', N'AMMAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (456, N'97', N'JDO', N'AMMAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (457, N'97', N'JDX', N'AMMAN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (458, N'98', N'ALA', N'ALMATY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (459, N'98', N'KZO', N'KAZAKHSTAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (460, N'99', N'MBA', N'MOMBASA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (461, N'99', N'NBO', N'NAIROBI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (462, N'99', N'KEO', N'NAIROBI OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (463, N'99', N'KEX', N'NAIROBI UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (464, N'10', N'TRW', N'TARAWA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (465, N'10', N'NKO', N'NORTH KOREA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (466, N'10', N'FNJ', N'PYONGYANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (467, N'10', N'INB', N'INCHON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (468, N'10', N'PUS', N'PUSAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (469, N'10', N'SEL', N'SEOUL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (470, N'10', N'SEX', N'SEOUL ISB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (471, N'10', N'KXO', N'SEOUL OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (472, N'10', N'KXX', N'SEOUL UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (473, N'10', N'KWI', N'KUWAIT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (474, N'10', N'FRU', N'BISHKEK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (475, N'10', N'KYR', N'KYRGYZSTAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (476, N'10', N'VTE', N'VIENTIANE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (477, N'10', N'LVO', N'LATVIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (478, N'10', N'RIX', N'RIGA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (479, N'10', N'BEY', N'BEYROUTH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (480, N'10', N'LEO', N'BEYROUTH OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (481, N'10', N'LEX', N'BEYROUTH UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (482, N'10', N'MSU', N'MASERU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (483, N'10', N'MLW', N'MONROVIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (484, N'11', N'BEN', N'BENGHAZI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (485, N'11', N'TIP', N'TRIPOLI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (486, N'11', N'LTO', N'LITHUANIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (487, N'11', N'VNO', N'VILNIUS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (488, N'11', N'LUX', N'LUXEMBOURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (490, N'40', N'MCA', N'MACAU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (491, N'11', N'MJO', N'MACEDONIA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (492, N'11', N'SKP', N'SKOPJE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (493, N'11', N'TNR', N'ANTANARIVO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (494, N'11', N'BLZ', N'BLANTYRE ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (495, N'11', N'BLO', N'BLANTYRE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (496, N'11', N'LLW', N'LILONGWE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (497, N'11', N'AOR', N'ALOR SETAR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (498, N'11', N'BPT', N'BATU PAHAT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (499, N'11', N'BTU', N'BINTULU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (500, N'11', N'IPH', N'IPOH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (501, N'11', N'JHB', N'JOHOR BAHARU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (502, N'11', N'KTG', N'KETAPANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (503, N'11', N'KBR', N'KOTA BAHARU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (504, N'11', N'BKI', N'KOTA KINABALU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (505, N'11', N'KUL', N'KUALA LUMPUR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (506, N'11', N'MAO', N'KUALA LUMPUR OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (507, N'11', N'KLT', N'KUALA LUMPUR OTHER MAIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (508, N'11', N'MAX', N'KUALA LUMPUR UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (509, N'11', N'TGG', N'KUALA TERENGGANU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (510, N'11', N'KUA', N'KUANTAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (511, N'11', N'KCH', N'KUCHING')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (512, N'11', N'LBU', N'LABUAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (513, N'11', N'LDU', N'LAHAD DATU')
GO
print 'Processed 500 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (514, N'11', N'MKZ', N'MELAKA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (515, N'11', N'MYY', N'MIRI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (516, N'11', N'PEN', N'PENANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (517, N'11', N'PKG', N'PORT KELANG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (518, N'11', N'SDK', N'SANDAKAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (519, N'11', N'SBN', N'SEREMBAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (520, N'11', N'SBW', N'SIBU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (521, N'11', N'TWU', N'TAWAU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (522, N'11', N'TEM', N'TEMERLOH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (523, N'11', N'MLE', N'MALDIVES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (524, N'11', N'BKO', N'BAMAKO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (525, N'11', N'BKX', N'BAMAKO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (526, N'12', N'MLA', N'MALTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (527, N'12', N'MRO', N'CAS OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (528, N'12', N'CAS', N'CASABLANCA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (529, N'12', N'RBA', N'RABAT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (530, N'12', N'QEE', N'EBEYE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (531, N'12', N'KWA', N'KWAJALEIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (532, N'12', N'MAJ', N'MAJURO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (533, N'12', N'FDF', N'FORT-DE-FRANCE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (534, N'12', N'MQO', N'MARTINIQUE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (535, N'12', N'NDB', N'NOUADHIBOU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (536, N'12', N'NVB', N'NOUADHIBOU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (537, N'12', N'NKC', N'NOUAKCHOTT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (538, N'12', N'MRU', N'PORT LOUIS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (539, N'12', N'DZA', N'DZAOUDZI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (540, N'12', N'MYT', N'MAYOTTE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (541, N'12', N'GDL', N'GUADALAJARA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (542, N'12', N'MEX', N'MEXICO CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (543, N'12', N'MXO', N'MEXICO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (544, N'12', N'MXX', N'MEXICO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (545, N'12', N'MTY', N'MONTERREY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (546, N'12', N'KSA', N'KOSRAE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (547, N'12', N'RNI', N'POHNPEI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (548, N'12', N'TKK', N'TRUK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (549, N'12', N'YAP', N'YAP')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (550, N'12', N'KIV', N'KISHINEV')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (551, N'12', N'MDO', N'MOLDOVA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (552, N'13', N'MNI', N'MONSERRAT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (553, N'13', N'MPM', N'MAPUTO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (554, N'13', N'MPO', N'MAPUTO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (555, N'13', N'RGO', N'MYANMAR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (556, N'13', N'RGN', N'YANGON ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (557, N'13', N'NMO', N'NAMIBIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (558, N'13', N'WDH', N'WINDHOEK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (559, N'13', N'INU', N'NAURU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (560, N'13', N'KTM', N'KATHMANDU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (561, N'13', N'NPO', N'KATHMANDU OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (562, N'13', N'AMS', N'AMSTERDAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (563, N'13', N'AMG', N'AMSTERDAM AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (564, N'13', N'NEO', N'AMSTERDAM OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (565, N'13', N'NEX', N'AMSTERDAM UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (566, N'13', N'ARH', N'ARNHEM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (567, N'13', N'QAR', N'ARNHEM HUB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (568, N'13', N'EIN', N'EINDHOVEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (569, N'13', N'EUR', N'EUROPEAN REGIONAL OFFICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (570, N'13', N'GHO', N'G.H.O. AMSTERDAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (571, N'13', N'RTM', N'ROTTERDAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (572, N'13', N'HAG', N'THE HAGUE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (573, N'13', N'TIL', N'TILBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (574, N'13', N'UTR', N'UTRECHT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (575, N'13', N'BON', N'BONAIRE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (576, N'13', N'CUR', N'CURACAO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (577, N'13', N'SAB', N'SABA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (578, N'13', N'EUX', N'SAINT EUSTATIUS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (579, N'13', N'SXM', N'SAINT MAARTEN NL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (580, N'13', N'NCO', N'NEW CALEDONIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (581, N'13', N'NOU', N'NOUMEA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (582, N'13', N'AKL', N'AUCKLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (583, N'13', N'AKT', N'AUCKLAND MAIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (584, N'13', N'NZX', N'AUCKLAND UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (585, N'13', N'CHC', N'CHRISTCHURCH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (586, N'13', N'DUD', N'DUNEDIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (587, N'13', N'HLZ', N'HAMILTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (588, N'13', N'NPE', N'NAPIER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (589, N'13', N'NPL', N'NEW PLYMOUTH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (590, N'13', N'NZO', N'NEW ZEALAND OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (591, N'13', N'PMR', N'PLAMERSTON NORTH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (592, N'13', N'TRG', N'TAURANGA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (593, N'13', N'WLG', N'WELLINGTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (594, N'14', N'MGA', N'MANAGUA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (595, N'14', N'NIO', N'MANAGUA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (596, N'14', N'NIM', N'NIAMEY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (597, N'14', N'NIX', N'NIAMEY OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (598, N'14', N'LOS', N'LAGOS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (599, N'14', N'NXO', N'LAGOS OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (600, N'14', N'NXX', N'LAGOS UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (601, N'14', N'IUE', N' NIUE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (602, N'14', N'NLK', N'NORFLOK ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (603, N'14', N'ROP', N'ROTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (604, N'14', N'SPN', N'SAIPAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (605, N'14', N'TIQ', N'TINIAN ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (606, N'14', N'BGN', N'BERGEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (607, N'14', N'OSL', N'OSLO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (608, N'14', N'OXL', N'OSLO AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (610, N'14', N'NWO', N'OSLO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (611, N'14', N'NWX', N'OSLO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (612, N'14', N'SVG', N'STAVANGER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (613, N'14', N'TRD', N'TRONDHEIM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (614, N'14', N'MCT', N'MUSCAT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (615, N'14', N'MCO', N'MUSCAT OTHER')
GO
print 'Processed 600 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (616, N'14', N'MCX', N'MUSCAT UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (617, N'14', N'RUW', N'RUWI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (618, N'14', N'KHI', N'KARACHI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (619, N'14', N'PKO', N'KARACHI OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (620, N'14', N'PKX', N'KARACHI UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (621, N'14', N'LHE', N'LAHORE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (622, N'14', N'RWP', N'RAWALPINDI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (623, N'14', N'ROR', N'KOROR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (624, N'15', N'PTY', N'PANAMA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (625, N'15', N'PTO', N'PANAMA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (626, N'15', N'POM', N'PORT MORESBY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (627, N'15', N'PNO', N'PORT MORESBY OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (628, N'15', N'PNX', N'PORT MORESBY UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (629, N'15', N'ASU', N'ASUNCION')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (630, N'15', N'ASO', N'ASUNCION OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (631, N'15', N'ASX', N'ASUNCION UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (632, N'15', N'LIM', N'LIMA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (633, N'15', N'PEO', N'LIMA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (634, N'15', N'PEX', N'LIMA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (635, N'15', N'CEB', N'CEBU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (636, N'15', N'DVO', N'DAVAO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (637, N'15', N'MNL', N'MANILA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (638, N'15', N'PPO', N'MANILA ONFWD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (639, N'15', N'PPX', N'MANILA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (640, N'15', N'QYY', N'BIALYSTOK ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (641, N'15', N'BZG', N'BYDGOSZCZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (642, N'15', N'GDN', N'GDANSK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (643, N'15', N'KTW', N'KATOWNICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (644, N'15', N'KRK', N'KRAKOW')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (645, N'15', N'QLZ', N'LODZ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (646, N'15', N'QLU', N'LUBLIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (647, N'15', N'POZ', N'POZNAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (648, N'15', N'RZE', N'RZESZOW')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (649, N'15', N'SZZ', N'SZCZECIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (650, N'15', N'WAW', N'WARSAW')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (651, N'15', N'WAO', N'WARSAW OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (652, N'15', N'WRO', N'WROCLAW')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (653, N'15', N'LIS', N'LISBON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (654, N'15', N'PXO', N'LISBON OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (655, N'15', N'PXX', N'LISBON UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (656, N'15', N'OPO', N'OPORTO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (657, N'15', N'SJU', N'SAN JUAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (658, N'15', N'PRO', N'SAN JUAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (659, N'15', N'PRX', N'SAN JUAN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (660, N'15', N'DOH', N'DOHA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (661, N'15', N'QTO', N'DOHA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (662, N'15', N'QTX', N'DOHA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (663, N'15', N'RNY', N'REUNION ISLAND OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (664, N'15', N'RUN', N'SAINT DENIS DE LA REUNION')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (665, N'16', N'BUH', N'BUCURESTI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (666, N'16', N'RXO', N'BUCURESTI OTHER ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (667, N'16', N'MOW', N'MOSCOW')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (668, N'16', N'MOO', N'MOSCOW OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (669, N'16', N'RUO', N'RUSSIAN FEDERATION')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (670, N'16', N'LED', N'ST. PETERSBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (671, N'16', N'KGL', N'KIGALI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (672, N'16', N'KGO', N'KIGALI OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (673, N'16', N'APW', N'APIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (674, N'16', N'SAI', N'SAN MARINO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (675, N'16', N'SLU', N'ST. LUCIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (676, N'16', N'TMS', N'SAO TOME')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (677, N'16', N'DMM', N'DAMMAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (678, N'16', N'DHA', N'DHAHRAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (679, N'16', N'SUX', N'DHAHRAN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (680, N'16', N'JED', N'JIDDAH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (681, N'16', N'SUO', N'JIDDAH OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (682, N'16', N'JBL', N'JUBAIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (683, N'16', N'RUH', N'RIYADH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (684, N'16', N'TIF', N'TAIF')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (685, N'16', N'YNB', N'YANBU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (686, N'16', N'DKR', N'DAKAR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (687, N'16', N'SEO', N'SENEGAL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (688, N'16', N'SEZ', N'VICTORIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (689, N'17', N'FNA', N'FREETOWN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (690, N'17', N'SIO', N'SIERRA LEONE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (691, N'17', N'APR', N'ASIA PACIFIC REGIONAL OFFICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (692, N'17', N'SIN', N'SINGAPORE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (693, N'17', N'SXN', N'SINGAPORE HUB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (694, N'17', N'SIQ', N'SINGAPORE ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (695, N'17', N'BTS', N'BRATISLAVA ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (696, N'17', N'BTO', N'BRATISLAVA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (697, N'17', N'LJU', N'LJUBLJANA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (698, N'17', N'SXI', N'SLOVENIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (699, N'17', N'HIR', N'HONIARA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (700, N'17', N'MGQ', N'MOGADISHU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (701, N'17', N'BFN', N'BLOEMFONTEIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (702, N'17', N'CPT', N'CAPETOWN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (703, N'17', N'DUR', N'DURBAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (704, N'17', N'ELS', N'EAST LONDON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (705, N'17', N'GRJ', N'GEORGE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (706, N'17', N'JNB', N'JOHANNESBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (707, N'17', N'JBX', N'JOHANNESBURG ISB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (708, N'17', N'JNX', N'JOHANNESBURG ISB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (709, N'17', N'SXO', N'JOHANNESBURG OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (710, N'17', N'SXX', N'JOHANNESBURG UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (711, N'17', N'KIM', N'KIMBERLEY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (712, N'17', N'PZB', N'PIETERMARITZBURG ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (713, N'17', N'PLZ', N'PORT ELIZABETH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (714, N'17', N'PRY', N'PRETORIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (715, N'17', N'RCB', N'RICHARDS BAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (716, N'17', N'UTT', N'UMTATA')
GO
print 'Processed 700 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (717, N'17', N'ISB', N'BALEARIC ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (718, N'17', N'BCN', N'BARCELONA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (719, N'17', N'BXN', N'BARCELONA AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (720, N'17', N'BIO', N'BILBAO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (721, N'17', N'BGS', N'BURGOS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (722, N'17', N'ISC', N'CANARY ISLANDS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (723, N'17', N'ODB', N'CORDOBA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (724, N'17', N'GRX', N'GRANADA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (725, N'17', N'MAD', N'MADRID')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (726, N'17', N'MAG', N'MADRID AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (727, N'17', N'MXD', N'MADRID AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (728, N'17', N'EHO', N'MADRID HEAD OFFICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (729, N'17', N'SNO', N'MADRID ONFWD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (730, N'17', N'SPO', N'MADRID OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (731, N'17', N'SPX', N'MADRID UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (732, N'17', N'AGP', N'MALAGA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (733, N'17', N'MJV', N'MURCIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (734, N'17', N'OVD', N'OVIEDO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (735, N'17', N'PMI', N'PALMA DE MALLORCA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (736, N'17', N'SCQ', N'SANTIAGO DE COMPOSTELA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (737, N'17', N'SVQ', N'SEVILLA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (738, N'17', N'VLC', N'VALENCIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (739, N'17', N'FOX', N'VALENCIA FORD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (740, N'17', N'VLL', N'VALLADOLID')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (741, N'17', N'ZAZ', N'ZARAGOZA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (742, N'17', N'ZXZ', N'ZARAGOZA AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (744, N'17', N'CMB', N'COLOMBO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (745, N'17', N'SLO', N'COLOMBO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (746, N'17', N'NEV', N'NEVIS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (747, N'17', N'SKB', N'ST. KITTS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (748, N'18', N'SVD', N'ST. VINCENT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (749, N'18', N'KRT', N'KHARTOUM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (750, N'18', N'SDO', N'KHARTOUM OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (751, N'18', N'PBM', N'PARAMARIBO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (752, N'18', N'MTS', N'MANZINI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (753, N'18', N'MTO', N'MANZINI OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (754, N'18', N'ARN', N'ARLANDA AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (755, N'18', N'GOT', N'GOTHENBURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (756, N'18', N'GXT', N'GOTHENBURG AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (757, N'18', N'HLB', N'HELSINGBORG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (758, N'18', N'AGH', N'HELSINGBORG ANGELHOLM AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (759, N'18', N'JKG', N'JOENKOEPING')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (760, N'18', N'KPG', N'KOEPING')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (761, N'18', N'MMA', N'MALMO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (762, N'18', N'NRK', N'MORRKOEPING ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (763, N'18', N'STO', N'STOCKHOLM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (764, N'18', N'SWO', N'STOCKHOLM OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (765, N'18', N'SWX', N'STOCKHOLM UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (766, N'18', N'VST', N'VASTERAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (767, N'18', N'VOL', N'VOLVO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (768, N'18', N'ARA', N'AARAU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (769, N'18', N'BSL', N'BASEL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (770, N'18', N'BRN', N'BERN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (771, N'18', N'FRI', N'FRIBOURG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (772, N'18', N'GVA', N'GENEVA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (773, N'18', N'SZO', N'GENEVA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (774, N'18', N'SZX', N'GENEVA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (775, N'18', N'LNN', N'LAUSANNE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (776, N'18', N'LUG', N'LUGANO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (777, N'18', N'ZUG', N'ZUG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (778, N'18', N'ZRH', N'ZURICH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (779, N'18', N'DAM', N'DAMASCUS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (780, N'18', N'SYO', N'DAMASCUS OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (781, N'18', N'CHW', N'CHANG-HUA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (782, N'18', N'KHH', N'KAOHSIUNG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (783, N'18', N'TXG', N'TAICHUNG')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (784, N'18', N'TNN', N'TAINAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (785, N'18', N'TPE', N'TAIPEI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (786, N'18', N'TPX', N'TAIPEI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (787, N'18', N'TWO', N'TAIPEI OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (788, N'18', N'TWX', N'TAIPEI UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (789, N'18', N'DYU', N'DUSHANBE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (790, N'18', N'TJK', N'TAJIKISTAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (791, N'18', N'ARK', N'ARUCHA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (792, N'18', N'DAR', N'DAR ES SALAAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (793, N'18', N'MSI', N'MOSHI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (794, N'18', N'TZO', N'TANZANIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (795, N'19', N'BKK', N'BANGKOK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (796, N'19', N'THO', N'BANGKOK OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (797, N'19', N'THX', N'BANGKOK UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (798, N'19', N'HKT', N'PHUKET')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (799, N'19', N'LFW', N'LOME')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (800, N'19', N'TGO', N'TOGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (801, N'19', N'TBU', N'NUKUALOFA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (802, N'19', N'POS', N'PORT OF SPAIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (803, N'19', N'TRO', N'PORT OF SPAIN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (804, N'19', N'TRX', N'PORT OF SPAIN UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (805, N'19', N'TUN', N'TUNIS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (806, N'19', N'TNO', N'TUNISIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (807, N'19', N'ADA', N'ADANA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (808, N'19', N'ANK', N'ANKARA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (809, N'19', N'DZL', N'DENIZLI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (810, N'19', N'IST', N'ISTANBUL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (811, N'19', N'IAG', N'ISTANBUL AGENTS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (812, N'19', N'TXA', N'ISTANBUL EASTBLOCK GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (813, N'19', N'TUO', N'ISTANBUL OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (814, N'19', N'TUX', N'ISTANBUL UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (815, N'19', N'ISX', N'ISTANBUL X')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (816, N'19', N'IZM', N'IZMIR')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (817, N'19', N'ASB', N'ASHKHABAD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (818, N'19', N'TKM', N'TURKMENISTAN')
GO
print 'Processed 800 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (819, N'19', N'GDT', N'GRAND TURK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (820, N'19', N'FUN', N'FUNAFUTI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (821, N'19', N'EBB', N'KAMPALA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (822, N'19', N'EBO', N'UGANDA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (823, N'20', N'IEV', N'KIEV')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (824, N'20', N'UOO', N'UKRAINE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (825, N'20', N'AUH', N'ABU DHABI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (826, N'20', N'DXB', N'DUBAI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (827, N'20', N'FUR', N'FUJAIRAH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (828, N'20', N'MER', N'MIDDLE EAST REGIONAL OFFICE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (829, N'20', N'JAL', N'MINA JEBEL ALI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (830, N'20', N'SHJ', N'SHARJAH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (831, N'20', N'UAO', N'UNITED ARAB EMIRATES OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (832, N'20', N'UAX', N'UNITED ARAB EMIRATES UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (833, N'20', N'MVD', N'MONTEVIDEO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (834, N'20', N'MVX', N'MONTEVIDEO UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (835, N'20', N'MVO', N'MONTEVIDEO OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (837, N'20', N'TAS', N'TASHKENT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (838, N'20', N'UZB', N'UZBEKISTAN OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (839, N'20', N'VLI', N'PORT-VILA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (840, N'20', N'CCS', N'CARACAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (841, N'20', N'MAR', N'MARACAIBO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (842, N'20', N'VEX', N'VENEZUELA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (843, N'20', N'HAN', N'HANOI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (844, N'20', N'VIO', N'HANOI OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (845, N'20', N'SGN', N'HO CHI MINH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (846, N'20', N'EIS', N'BEEF ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (847, N'20', N'RTH', N'TORTOLA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (848, N'21', N'STX', N'SAINT CROIX')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (849, N'21', N'SJF', N'SAINT JOHN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (850, N'21', N'STT', N'SAINT THOMAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (851, N'21', N'BEG', N'BEOGRAD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (852, N'21', N'YUO', N'BEOGRAD OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (854, N'21', N'FIH', N'KINSHASA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (855, N'21', N'ZAO', N'KINSHASA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (856, N'21', N'FBM', N'LUBUMBASHI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (859, N'21', N'LUN', N'LUSAKA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (860, N'21', N'ZXO', N'LUSAKA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (861, N'21', N'ZMO', N'HARARE OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (862, N'20', N'ABZ', N'ABERDEEN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (863, N'20', N'AHE', N'ATHERSTONE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (864, N'20', N'BFS', N'BELFAST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (865, N'20', N'BXS', N'BELFAST AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (866, N'20', N'BHX', N'BIRMINGHAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (867, N'20', N'BXX', N'BIRMINGHAM AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (868, N'20', N'BWD', N'BRENTWOOD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (869, N'20', N'BRS', N'BRISTOL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (870, N'20', N'BYF', N'BYFLEET')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (871, N'20', N'CBG', N'CAMBRIDGE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (872, N'20', N'CNK', N'CANNOCK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (873, N'20', N'CAR', N'CARLISLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (874, N'20', N'LGW', N'CROYDON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (875, N'20', N'EMA', N'EAST MIDLANDS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (876, N'20', N'LEC', N'EC PARCELS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (877, N'20', N'EDI', N'EDINBURGH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (878, N'20', N'EXT', N'EXETER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (879, N'20', N'GLA', N'GLASGOW')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (880, N'20', N'GLM', N'GLOBAL MAIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (881, N'20', N'GCI', N'GUERNSEY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (882, N'20', N'HRN', N'HORNSEY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (883, N'20', N'IOM', N'ISLE OF MAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (884, N'20', N'JER', N'JERSEY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (885, N'20', N'LBA', N'LEEDS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (886, N'20', N'LPL', N'LIVERPOOL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (887, N'20', N'LXL', N'LIVERPOOL AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (888, N'20', N'LLA', N'LLANTRISANT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (889, N'20', N'LHG', N'LONDON AIRFREIGHT GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (890, N'20', N'LCY', N'LONDON CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (891, N'20', N'LHR', N'LONDON HEATHROW ')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (892, N'20', N'LTS', N'LONDON HEATHROW TRANSSHIP')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (893, N'20', N'LXH', N'LONDON ROW GATEWAY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (894, N'20', N'LHX', N'LONDON XP')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (895, N'20', N'LTN', N'LUTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (896, N'20', N'LXN', N'LUTON AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (897, N'20', N'MAS', N'MAIDSTONE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (898, N'20', N'MAN', N'MANCHESTER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (899, N'20', N'MTN', N'MILTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (900, N'20', N'NCL', N'NEWCASTLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (901, N'20', N'NXH', N'NORTH ICD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (902, N'20', N'NTH', N'NORTHAMPTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (903, N'20', N'PIK', N'PRESTWICK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (904, N'20', N'RAM', N'RAMSBOTTOM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (905, N'20', N'RED', N'READING')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (906, N'20', N'RHM', N'ROTHERHAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (907, N'20', N'SOU', N'SOUTHAMPTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (908, N'20', N'STN', N'STANSTED')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (909, N'20', N'LSX', N'STANSTED AIRPORT')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (910, N'20', N'STA', N'STANWELL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (911, N'20', N'NME', N'TEESSIDE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (912, N'20', N'THT', N'THETFORD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (913, N'20', N'UKO', N'UNITED KINGDOM ONFWD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (914, N'20', N'UKX', N'UNITED KINGDOM UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (915, N'20', N'WEL', N'WELLINGBOROUGH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (916, N'20', N'WOR', N'WORCESTER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (917, N'20', N'ALB', N'ALBANY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (918, N'20', N'ABQ', N'ALBUQUERQUE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (919, N'20', N'ACY', N'ATLANTIC CITY INTERNATIONAL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (920, N'20', N'ATL', N'ATLANTA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (921, N'20', N'AUS', N'AUSTIN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (922, N'20', N'BWI', N'BALTIMORE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (923, N'20', N'BOS', N'BOSTON')
GO
print 'Processed 900 total records'
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (924, N'20', N'BUF', N'BUFFALO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (925, N'20', N'CAB', N'CARIBBEAN')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (926, N'20', N'CHS', N'CHARLESTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (927, N'20', N'CLT', N'CHARLOTTE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (928, N'20', N'ORD', N'CHICAGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (929, N'20', N'CVG', N'CINCINNATI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (930, N'20', N'CLE', N'CLEVELAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (931, N'20', N'CMH', N'COLUMBUS`')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (932, N'20', N'DFW', N'DALLAS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (933, N'20', N'DAY', N'DAYTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (934, N'20', N'DEN', N'DENVER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (935, N'20', N'DTT', N'DETROIT`')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (936, N'20', N'FRG', N'FARMINGDALE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (937, N'20', N'GCY', N'GARDEN CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (938, N'20', N'GNW', N'GREENWICH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (939, N'20', N'BDL', N'HARTFORD')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (940, N'20', N'HNL', N'HONOLULU')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (941, N'20', N'IAH', N'HOUSTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (942, N'20', N'IND', N'INDIANA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (943, N'20', N'ISP', N'ISLIP LONG ISLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (944, N'20', N'JAX', N'JACKSONVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (945, N'20', N'JFK', N'JFK HUB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (946, N'20', N'MKC', N'KANSAS CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (947, N'20', N'LAX', N'LOS ANGELES')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (948, N'20', N'LXX', N'LOS ANGELES HUB')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (949, N'20', N'SDF', N'LOUISVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (950, N'20', N'ZMV', N'MAILFAST HEAD OFFICE MELVILLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (951, N'20', N'MEM', N'MEMPHIS FEDEX')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (952, N'20', N'MIA', N'MIAMI')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (953, N'20', N'LAO', N'MIAMI MAIL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (954, N'20', N'MKE', N'MILWAUKEE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (955, N'20', N'MSP', N'MINNEAPOLIS ST. PAUL')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (956, N'20', N'MSY', N'NEW ORLEANS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (957, N'20', N'NYC', N'NEW YORK CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (958, N'20', N'EWR', N'NEWARK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (959, N'20', N'ORF', N'NORFOLK')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (960, N'20', N'OKC', N'OKLAHOMA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (961, N'20', N'SNA', N'ORANGE COUNTY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (962, N'20', N'ORL', N'ORLANDO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (963, N'20', N'PAO', N'PACIFIC')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (964, N'20', N'PHL', N'PHILADELPHIA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (965, N'20', N'PHX', N'PHOENIX')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (966, N'20', N'PIT', N'PITTSBURGH')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (967, N'20', N'PDX', N'PORTLAND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (968, N'20', N'PVD', N'PROVIDENCE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (969, N'20', N'RDU', N'RALEIGH DURHAM')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (970, N'20', N'RIC', N'RICHMOND')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (971, N'20', N'ROC', N'ROCHESTER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (972, N'20', N'SLC', N'SALT LAKE CITY')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (973, N'20', N'SAT', N'SAN ANTONIO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (974, N'20', N'SAN', N'SAN DIEGO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (975, N'20', N'SFO', N'SAN FRANCISCO')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (976, N'20', N'SEA', N'SEATTLE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (977, N'20', N'STL', N'ST. LOUIS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (978, N'20', N'SYR', N'SYRACUSE')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (979, N'20', N'TPA', N'TAMPA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (980, N'20', N'TUL', N'TULSA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (981, N'20', N'TUS', N'TUSCON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (982, N'20', N'USO', N'USA OTHER')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (983, N'20', N'USX', N'USA UNLIST')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (984, N'20', N'USP', N'USPS EMS')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (985, N'20', N'DCA', N'WASHINGTON')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (986, N'20', N'ICT', N'WICHITA')
INSERT [dbo].[RegionCode] ([ID], [CountryCode], [RegionCode], [RegionName]) VALUES (987, N'20', N'ORH', N'WORCESTER')
/****** Object:  Table [dbo].[Param]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Param](
	[PID] [uniqueidentifier] NOT NULL,
	[TID] [uniqueidentifier] NULL,
	[Tag] [int] NOT NULL,
	[Key] [nvarchar](20) NOT NULL,
	[Value] [nvarchar](400) NOT NULL,
	[Top] [int] NOT NULL,
	[Left] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[FontName] [nvarchar](10) NULL,
	[FontSize] [int] NULL,
	[Alignment] [int] NULL,
	[Bold] [int] NULL,
	[Italic] [int] NULL,
	[Underline] [int] NULL,
	[ParamType] [nvarchar](10) NOT NULL,
	[DefaultValue] [nvarchar](40) NULL,
 CONSTRAINT [PK_PARAM] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'GUID序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'GUID序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'TID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数标签' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Tag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'键(主要用于维护界面的显示)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值(主要用于绑定界面的显示,保存SQL语句)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上边距' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Top'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'左边距' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Left'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'高' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宽' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字体名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'FontName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字体大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'FontSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'靠齐方式(1-左对齐 2-居中 3-右对齐)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Alignment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否粗体(1-加粗 0-不加粗)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Bold'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否斜体(0-不 1-斜体)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Italic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否下划线(0-无 1-有)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'Underline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数类型(如Text,BarCode)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'ParamType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param', @level2type=N'COLUMN',@level2name=N'DefaultValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单证模板参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Param'
GO
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'd40fed11-83b7-43d0-9f9a-2c5e3305f5aa', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 27, N'到付月结', N'select SettleType from hawb where HID=''#key#''', 224, 616, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'2')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'26f74db3-79fa-4cf2-8bb1-34340ecec936', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 28, N'预付现结', N'select SettleType from hawb where HID=''#key#''', 224, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'1')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'0811dccf-2a76-4c12-bcb5-40e6dff12512', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 27, N'到付月结', N'select SettleType from hawb where HID=''#key#''', 224, 616, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'2')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'5b7d91d8-b494-4d70-9a37-71ca3eebfb44', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 26, N'预付月结', N'select SettleType from hawb where HID=''#key#''', 224, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'0')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'fbdf9a3d-bf72-4f26-a786-7358f219d4aa', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 28, N'预付现结', N'select SettleType from hawb where HID=''#key#''', 224, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'1')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'5f900e38-c576-4ff9-8ce2-7911e68c62e8', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 22, N'文件', N'select ServiceType from hawb where HID=''#key#''', 150, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'0')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f001', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 1, N'发件人账号', N'select shippername from hawb where HID=''#key#''', 63, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f002', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 2, N'发件人姓名', N'select shippercontactor from hawb where HID=''#key#''', 63, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f003', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 3, N'发件人电话', N'select shippertel from hawb where HID=''#key#''', 101, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f004', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 4, N'发件人邮政编码', N'select shipperzipcode from hawb where HID=''#key#''', 101, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f005', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 5, N'发件人公司名称', N'select shippername from hawb where HID=''#key#''', 146, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f006', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 6, N'发件人地址', N'select shipperaddress from hawb where HID=''#key#''', 181, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f007', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 7, N'发件人地区', N'select shipperregion from hawb where HID=''#key#''', 245, 235, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f008', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 8, N'发件人国家', N'select shippercountry from hawb where HID=''#key#''', 245, 420, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f009', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 9, N'收件人账号', N'select consigneename from hawb where HID=''#key#''', 301, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f010', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 10, N'收件人姓名', N'select consigneename from hawb where HID=''#key#''', 301, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f011', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 11, N'收件人电话', N'select consigneetel from hawb where HID=''#key#''', 339, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f012', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 12, N'收件人邮政编码', N'select consigneezipcode from hawb where HID=''#key#''', 339, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f013', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 13, N'收件人公司名称', N'select consigneename from hawb where HID=''#key#''', 386, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f014', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 14, N'收件人地址', N'select consigneeaddress from hawb where HID=''#key#''', 424, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f015', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 15, N'收件人地区', N'select consigneeregion from hawb where HID=''#key#''', 485, 264, 20, 79, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f016', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 16, N'收件人国家', N'select consigneecountry from hawb where HID=''#key#''', 485, 416, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f019', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 17, N'实际重量', N'select totalweight from hawb where HID=''#key#''', 303, 537, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f020', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 18, N'体积重量', N'select volumeweight from hawb where HID=''#key#''', 303, 672, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f021', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 19, N'件数', N'select piece from hawb where HID=''#key#''', 354, 711, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f022', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 20, N'物品名称及数量', N'select name,piece from hawbitem where HID=''#key#''', 403, 537, 56, 264, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3f023', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 21, N'声明价值', N'select sum(totalamount) from hawbitem where HID=''#key#''', 479, 537, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe31', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 1, N'发件人账号', N'select shippername from hawb where HID=''#key#''', 63, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe32', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 2, N'发件人姓名', N'select shippercontactor from hawb where HID=''#key#''', 63, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe33', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 3, N'发件人电话', N'select shippertel from hawb where HID=''#key#''', 101, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe34', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 4, N'发件人邮政编码', N'select shipperzipcode from hawb where HID=''#key#''', 101, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe35', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 5, N'发件人公司名称', N'select shippername from hawb where HID=''#key#''', 146, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe36', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 6, N'发件人地址', N'select shipperaddress from hawb where HID=''#key#''', 181, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe37', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 7, N'发件人地区', N'select shipperregion from hawb where HID=''#key#''', 245, 235, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe38', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 8, N'发件人国家', N'select shippercountry from hawb where HID=''#key#''', 245, 420, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe39', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 9, N'收件人账号', N'select consigneename from hawb where HID=''#key#''', 301, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe40', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 10, N'收件人姓名', N'select consigneecontactor from hawb where HID=''#key#''', 301, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe41', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 11, N'收件人电话', N'select consigneetel from hawb where HID=''#key#''', 339, 163, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe42', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 12, N'收件人邮政编码', N'select consigneezipcode from hawb where HID=''#key#''', 339, 355, 20, 153, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe43', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 13, N'收件人公司名称', N'select consigneename from hawb where HID=''#key#''', 386, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe44', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 14, N'收件人地址', N'select consigneeaddress from hawb where HID=''#key#''', 424, 163, 58, 323, N'WST_Swed', 9, 0, 0, 0, 0, N'Textarea', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe45', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 15, N'收件人地区', N'select consigneeregion from hawb where HID=''#key#''', 485, 264, 20, 79, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe46', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 16, N'收件人国家', N'select consigneecountry from hawb where HID=''#key#''', 485, 416, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe49', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 17, N'实际重量', N'select totalweight from hawb where HID=''#key#''', 301, 541, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe50', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 18, N'体积重量', N'select volumeweight from hawb where HID=''#key#''', 301, 669, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe51', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 19, N'件数', N'select piece from hawb where HID=''#key#''', 347, 711, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe52', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 20, N'物品名称及数量', N'select name,piece from hawbitem where HID=''#key#''', 406, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'3cc500c0-0cac-45ba-8497-989a73a3fe53', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 21, N'声明价值', N'select sum(totalamount) from hawbitem where HID=''#key#''', 475, 541, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'Text', NULL)
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'665aa275-4348-4870-b4f6-a5794d63625b', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 24, N'普通货物', N'select ServiceType from hawb where HID=''#key#''', 150, 689, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'2')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'2aec14c7-5c8a-4033-9540-ad7378cd8254', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 25, N'到付现结', N'select SettleType from hawb where HID=''#key#''', 224, 690, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'3')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'cf2e8533-f7a6-4df3-9e47-c8b218a3655d', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 26, N'预付月结', N'select SettleType from hawb where HID=''#key#''', 224, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'0')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'7a5a5d74-2ce3-4704-afe3-d25f0003c438', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 23, N'小包裹', N'select ServiceType from hawb where HID=''#key#''', 150, 689, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'1')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'f55cfd0f-89ce-4db4-8c78-dd6aa12fe0cb', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 22, N'文件', N'select ServiceType from hawb where HID=''#key#''', 150, 540, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'0')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'923e19a1-1d59-4bd3-99a4-e334d9bc9c2c', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 24, N'普通货物', N'select ServiceType from hawb where HID=''#key#''', 150, 690, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'2')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'f7e1bda8-6e10-4fde-8517-e69efec5e4f7', N'3cc500c0-0cac-45ba-8497-989a73a3fe32', 23, N'小包裹', N'select ServiceType from hawb where HID=''#key#''', 150, 690, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'1')
INSERT [dbo].[Param] ([PID], [TID], [Tag], [Key], [Value], [Top], [Left], [Height], [Width], [FontName], [FontSize], [Alignment], [Bold], [Italic], [Underline], [ParamType], [DefaultValue]) VALUES (N'f164e681-f613-4fd5-a4d8-e842bae34c05', N'3cc500c0-0cac-45ba-8497-989a73a3fe3d', 25, N'到付现结', N'select SettleType from hawb where HID=''#key#''', 224, 690, 20, 100, N'WST_Swed', 9, 0, 0, 0, 0, N'RadioBox', N'3')
/****** Object:  Table [dbo].[Package]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Package](
	[PID] [uniqueidentifier] NOT NULL,
	[MID] [uniqueidentifier] NULL,
	[BarCode] [nvarchar](50) NOT NULL,
	[OriginalRegionCode] [char](3) NOT NULL,
	[DestinationRegionCode] [char](3) NOT NULL,
	[Piece] [int] NOT NULL,
	[TotalWeight] [decimal](10, 2) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Operator] [nvarchar](20) NOT NULL,
	[Status] [int] NOT NULL,
	[IsMixed] [bit] NOT NULL,
 CONSTRAINT [PK_PACKAGE] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起始地三字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package', @level2type=N'COLUMN',@level2name=N'OriginalRegionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目的地三字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package', @level2type=N'COLUMN',@level2name=N'DestinationRegionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package', @level2type=N'COLUMN',@level2name=N'Piece'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是混包,默认false' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package', @level2type=N'COLUMN',@level2name=N'IsMixed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包裹' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Package'
GO
INSERT [dbo].[Package] ([PID], [MID], [BarCode], [OriginalRegionCode], [DestinationRegionCode], [Piece], [TotalWeight], [CreateTime], [UpdateTime], [Operator], [Status], [IsMixed]) VALUES (N'50f483e6-9020-47f7-a33a-57631378f605', NULL, N'PPP4545', N'SHA', N'TYO', 2, CAST(24.70 AS Decimal(10, 2)), CAST(0x00009ED000D0CDE6 AS DateTime), CAST(0x00009ED000D0CDE6 AS DateTime), N'admin', 1, 0)
INSERT [dbo].[Package] ([PID], [MID], [BarCode], [OriginalRegionCode], [DestinationRegionCode], [Piece], [TotalWeight], [CreateTime], [UpdateTime], [Operator], [Status], [IsMixed]) VALUES (N'e755ebc9-5f5f-4f7c-a45a-5a5f60d0e947', NULL, N'UIUIU', N'SHA', N'TYO', 0, CAST(0.00 AS Decimal(10, 2)), CAST(0x00009ED000D0FC55 AS DateTime), CAST(0x00009ED000D0FC55 AS DateTime), N'admin', 1, 0)
INSERT [dbo].[Package] ([PID], [MID], [BarCode], [OriginalRegionCode], [DestinationRegionCode], [Piece], [TotalWeight], [CreateTime], [UpdateTime], [Operator], [Status], [IsMixed]) VALUES (N'b99f909f-06ab-432f-94a4-c4689e85af54', N'b99f909f-06ab-432f-94a4-c4689e850987', N'p3', N'SHA', N'TYO', 15, CAST(15.00 AS Decimal(10, 2)), CAST(0x00009E990099EBBB AS DateTime), CAST(0x00009E990099EBBB AS DateTime), N'沈志伟', 0, 1)
INSERT [dbo].[Package] ([PID], [MID], [BarCode], [OriginalRegionCode], [DestinationRegionCode], [Piece], [TotalWeight], [CreateTime], [UpdateTime], [Operator], [Status], [IsMixed]) VALUES (N'b99f909f-06ab-432f-94a4-c4689e898008', N'b99f909f-06ab-432f-94a4-c4689e850980', N'P5', N'TIA', N'KBL', 16, CAST(18.00 AS Decimal(10, 2)), CAST(0x00009CF100000000 AS DateTime), CAST(0x00009D2D00000000 AS DateTime), N'沈志伟', 0, 1)
INSERT [dbo].[Package] ([PID], [MID], [BarCode], [OriginalRegionCode], [DestinationRegionCode], [Piece], [TotalWeight], [CreateTime], [UpdateTime], [Operator], [Status], [IsMixed]) VALUES (N'b99f909f-06ab-432f-94a4-c4689e898374', N'b99f909f-06ab-432f-94a4-c4689e850987', N'P4', N'SHA', N'TYO', 1, CAST(13.25 AS Decimal(10, 2)), CAST(0x00009ECD01135D9C AS DateTime), CAST(0x00009ECD01135D9C AS DateTime), N'沈志伟', 0, 1)
/****** Object:  UserDefinedFunction [dbo].[getName]    Script Date: 04/28/2011 12:54:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[getName](@SID varchar(200))
returns varchar(2000)
as
begin
declare @str varchar(2000)
set @str=''
select @str=rtrim(NameCN) from TableDesc where name=@SID
return @str
end
GO
/****** Object:  Table [dbo].[Department]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Department](
	[DID] [uniqueidentifier] NOT NULL,
	[CID] [uniqueidentifier] NULL,
	[CompanyCode] [nvarchar](50) NOT NULL,
	[DepCode] [nvarchar](50) NOT NULL,
	[DepName] [varchar](50) NULL,
	[FeeDiscountType] [int] NOT NULL,
	[FeeDiscountRate] [decimal](4, 2) NOT NULL,
	[WeightDiscountType] [int] NOT NULL,
	[WeightDiscountRate] [decimal](4, 2) NOT NULL,
	[SettleType] [int] NOT NULL,
	[WeightCalType] [int] NOT NULL,
 CONSTRAINT [PK_DEPARTMENT] PRIMARY KEY CLUSTERED 
(
	[DID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'DID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'CID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'CompanyCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门帐号编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'DepCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'DepName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用折扣类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'FeeDiscountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用折扣率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'FeeDiscountRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量折扣类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'WeightDiscountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量折扣率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'WeightDiscountRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'SettleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计重方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'WeightCalType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department'
GO
INSERT [dbo].[Department] ([DID], [CID], [CompanyCode], [DepCode], [DepName], [FeeDiscountType], [FeeDiscountRate], [WeightDiscountType], [WeightDiscountRate], [SettleType], [WeightCalType]) VALUES (N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', N'd2e7031c-db43-4b20-8ebe-becec8ca3f34', N'M18', N'00', N'开发部', 0, CAST(1.00 AS Decimal(4, 2)), 0, CAST(1.00 AS Decimal(4, 2)), 0, 1)
INSERT [dbo].[Department] ([DID], [CID], [CompanyCode], [DepCode], [DepName], [FeeDiscountType], [FeeDiscountRate], [WeightDiscountType], [WeightDiscountRate], [SettleType], [WeightCalType]) VALUES (N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'd2e7031c-db43-4b20-8ebe-becec8ca3f34', N'M18', N'01', N'市场部', 0, CAST(1.00 AS Decimal(4, 2)), 0, CAST(1.00 AS Decimal(4, 2)), 1, 2)
/****** Object:  Table [dbo].[HSProperty]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HSProperty](
	[HSPID] [uniqueidentifier] NOT NULL,
	[HSID] [uniqueidentifier] NULL,
	[PropertyName] [nvarchar](20) NOT NULL,
	[ChineseRemark] [nvarchar](100) NULL,
 CONSTRAINT [PK_HSPROPERTY] PRIMARY KEY CLUSTERED 
(
	[HSPID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProperty', @level2type=N'COLUMN',@level2name=N'HSPID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProperty', @level2type=N'COLUMN',@level2name=N'HSID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProperty', @level2type=N'COLUMN',@level2name=N'PropertyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中文备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProperty', @level2type=N'COLUMN',@level2name=N'ChineseRemark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HSProperty'
GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SysUser](
	[UID] [uniqueidentifier] NOT NULL,
	[LoginName] [varchar](20) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[RealName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Operator] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Status] [int] NOT NULL,
	[DepartmentID] [uniqueidentifier] NULL,
	[RoleID] [uniqueidentifier] NULL,
	[CountryCode] [char](2) NULL,
	[RegionCode] [char](3) NULL,
 CONSTRAINT [PK_SysUSER] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'DepartmentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'RoleID'
GO
INSERT [dbo].[SysUser] ([UID], [LoginName], [Password], [RealName], [Email], [Phone], [UpdateTime], [CreateTime], [Operator], [Remark], [Status], [DepartmentID], [RoleID], [CountryCode], [RegionCode]) VALUES (N'11789d48-e880-4335-9192-1ce8586d0538', N'admin', N'admin', N'admin', N'', N'', CAST(0x00009EB700BB9D12 AS DateTime), CAST(0x00009EB700BB931C AS DateTime), N'Admin', N'', 1, NULL, NULL, N'CN', N'SHA')
INSERT [dbo].[SysUser] ([UID], [LoginName], [Password], [RealName], [Email], [Phone], [UpdateTime], [CreateTime], [Operator], [Remark], [Status], [DepartmentID], [RoleID], [CountryCode], [RegionCode]) VALUES (N'cb88243d-137e-4221-8552-a0e776a2b0e6', N'123', N'123', N'123', N'', N'', CAST(0x00009EB800EEA837 AS DateTime), CAST(0x00009EB800EEA837 AS DateTime), N'Admin', N'', 1, NULL, NULL, N'CN', N'SHA')
/****** Object:  Table [dbo].[Role_Privilege]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Privilege](
	[Role_PrivilegeID] [uniqueidentifier] NOT NULL,
	[ModuleID] [uniqueidentifier] NULL,
	[PrivilegeDesc] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[RoleID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PRIVILEGE] PRIMARY KEY CLUSTERED 
(
	[Role_PrivilegeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Privilege', @level2type=N'COLUMN',@level2name=N'Role_PrivilegeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Privilege', @level2type=N'COLUMN',@level2name=N'ModuleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(000000-六位数 第一位是查询 第二位是添加 第三位是修改 第四位删除 第五位第六位扩充位 0-是无 1-是有' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Privilege', @level2type=N'COLUMN',@level2name=N'PrivilegeDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Privilege', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Privilege', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Privilege'
GO
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'01236997-e34a-461f-87ac-041d19c0616a', N'f8167443-bc5e-41a6-a0dc-ef8401261174', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'66c78153-73d4-412a-8d40-1882c4d483b0', N'009c8cbe-dcbd-491f-8fb0-8de4beb7dfe8', 128, CAST(0x00009EB800EC4FE4 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'fd151033-ba24-4a99-a877-1b5017a4e769', N'317d7f79-394c-44c4-9fee-bc5f558d1162', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'd37600a6-5dc8-47d6-a35c-1b96cc6ab849', N'fdeb4cbc-6f44-4981-b258-5189f60c135c', 256, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'ab3f0449-d97e-4807-a02c-44e62ac2e7aa', N'492959af-3bb9-43f4-b709-dec067ced397', 356, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'2ecf5afd-93f0-444c-bc38-48bc75280940', N'ee700d30-2394-48ef-be3f-577d57fd0a51', 372, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'2a5a3c3b-1a28-49bd-b999-4b96cc3533e0', N'ab06f3d8-87ae-4231-a004-34cc07566b2c', 864, CAST(0x00009EB900FC25CE AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'85bd4bcb-0060-48cd-87d4-4f6181833c32', N'0250b1b8-1047-49ae-9e54-d16e50bc032f', 68, CAST(0x00009EB900ED2051 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'07cd443a-5074-4b18-bb5d-60f3253eafef', N'4956ba7d-9db8-44d0-bf02-6568c83ac41f', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'376c51f8-5d02-471a-b6ab-616b2b452e55', N'84fbd0e8-481d-4f10-909f-79b29c940a16', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'f5b29f84-261f-4ba3-8e99-698ff09d63f0', N'48c70d58-69b3-4ebd-baba-a1eb259fcc8e', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'72ce59e3-363c-4fc9-b9b3-6a9e0c65ff02', N'16109c7c-3f03-419e-9a9d-bbca0d372512', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'f3582038-20e6-4602-8bd3-70c21dc8ee00', N'292c7388-943e-4017-ac54-ca78ccb9cbbd', 352, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'83e17753-5fd0-41c0-8e0f-7378d485459b', N'26c9e960-7572-40e6-9c4d-d3b55ec51135', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'af99f26e-ee5d-4081-85b7-88f1a70bf259', N'8c077d67-1df0-4507-960e-8ec0ff5d4a2f', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'5c940882-4438-43cf-97d3-8c90395f258a', N'e5565367-ddb2-4b8a-9c3b-5c71be164705', 352, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'9f3e9758-0f15-4ffb-bcdc-9306dc48e8be', N'e905bf16-4202-4f33-8f8e-87da4afe7ba8', 1020, CAST(0x00009ECC01742B45 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'3fed6303-40de-49b0-8a1f-a05809964b02', N'0cac30a9-3adc-4622-80ef-39cc81b6853d', 320, CAST(0x00009EB800EC4FE4 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'cc42b3ac-9088-469c-8c4a-b6e27851d173', N'cdef43fd-5a8f-43b0-a2ce-8e273747895b', 1020, CAST(0x00009ECC01742B45 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'e8b57793-5f70-4009-86d9-b7aab0b114ec', N'332e4393-4506-44cb-843a-805a0fca3882', 352, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'b20a5668-9080-4027-b683-bc8a1d5ec1c4', N'cf20fbf2-7679-48fc-88d4-4ff9f3f6c7e4', 128, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'1b3d9245-d30b-4289-82a2-c170bd802684', N'b50d0de6-9cbd-45b7-8d6f-4bf512aad37b', 128, CAST(0x00009EB800EC4FE4 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'ec01568d-f426-4c6f-84a1-c1b4c6196f2d', N'43330e27-c58a-46fc-948f-a3a492459e40', 352, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'f6f7c993-bd34-4ea5-a917-c797e663e112', N'816686ea-64e3-4665-bef5-3ec972ed3a26', 1020, CAST(0x00009ECC01742B45 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'34b8cd78-b874-4d99-bade-d128decaeada', N'3fe9b416-9150-4d4a-9741-561a2bd312bc', 352, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'87237845-b503-4c47-be53-d8e8194ff828', N'82be44f6-8522-497a-baab-22adf8a48940', 384, CAST(0x00009EB800EC4FE4 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'ee302e70-38db-4799-90ae-da320fe9ec8c', N'8dc626cb-dab1-43fe-adb0-53e031082142', 128, CAST(0x00009EBD001BB0A1 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'3a73b8ae-2bee-4aec-93d7-db33bd132950', N'9d24604c-68d0-443f-ae60-def0cd0e3c4c', 368, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'cbd3cd29-e740-490d-8fb8-dbc3670a670c', N'c0cf9aaf-c438-49e5-a5af-028c33dc37a3', 192, CAST(0x00009EBD00EC17AF AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'37ad891c-d53a-44fe-936f-e2b68c791ddf', N'7b5c1465-255d-40b2-b742-652684e3e611', 480, CAST(0x00009EB800DC57D0 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'd1706cfe-e911-41d8-8584-e3ece5c997e9', N'87e49856-18fc-46a7-8b10-acec6197a094', 1020, CAST(0x00009ECC01742B45 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'9f600740-eb89-4011-b49e-f954ae7522c4', N'd46e025f-7199-4c4a-91d7-62f237a2fdca', 352, CAST(0x00009EB700F59903 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
INSERT [dbo].[Role_Privilege] ([Role_PrivilegeID], [ModuleID], [PrivilegeDesc], [CreateTime], [Remark], [RoleID]) VALUES (N'ec3d6a94-9f26-495b-836b-ff800c97744c', N'276a8bc2-73b4-4b83-95f3-6efe944a094f', 1020, CAST(0x00009ECC01742B45 AS DateTime), NULL, N'c75ba0a0-ced4-48de-9e1d-ef25393968e8')
/****** Object:  Table [dbo].[User]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UID] [uniqueidentifier] NOT NULL,
	[DID] [uniqueidentifier] NULL,
	[LoginName] [varchar](20) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[RealName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Operator] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[FeeDiscountType] [int] NOT NULL,
	[FeeDiscountRate] [decimal](4, 2) NOT NULL,
	[WeightDiscountType] [int] NOT NULL,
	[WeightDiscountRate] [decimal](4, 2) NOT NULL,
	[SettleType] [int] NOT NULL,
	[WeightCalType] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'DID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'RealName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用折扣类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'FeeDiscountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用折扣率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'FeeDiscountRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量折扣类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'WeightDiscountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量折扣率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'WeightDiscountRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'SettleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计重方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'WeightCalType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签约用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
INSERT [dbo].[User] ([UID], [DID], [LoginName], [Password], [RealName], [Email], [Phone], [UpdateTime], [CreateTime], [Operator], [Remark], [FeeDiscountType], [FeeDiscountRate], [WeightDiscountType], [WeightDiscountRate], [SettleType], [WeightCalType], [Status]) VALUES (N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'U2', N'123456', N'小美', N'szw@126.com', N'110', CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'小美', NULL, 0, CAST(1.00 AS Decimal(4, 2)), 1, CAST(1.00 AS Decimal(4, 2)), 1, 1, 1)
INSERT [dbo].[User] ([UID], [DID], [LoginName], [Password], [RealName], [Email], [Phone], [UpdateTime], [CreateTime], [Operator], [Remark], [FeeDiscountType], [FeeDiscountRate], [WeightDiscountType], [WeightDiscountRate], [SettleType], [WeightCalType], [Status]) VALUES (N'fa0ab206-7894-493e-a7f2-7708f22054bf', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', N'U1', N'123456', N'小伟', N'szw@126.com', N'119', CAST(0x00009E980157C02D AS DateTime), CAST(0x00009E980157C02D AS DateTime), N'小伟', NULL, 0, CAST(1.00 AS Decimal(4, 2)), 1, CAST(1.00 AS Decimal(4, 2)), 1, 1, 1)
/****** Object:  Table [dbo].[SysUser_Role]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser_Role](
	[SysUser_RoleID] [uniqueidentifier] NOT NULL,
	[RoleID] [uniqueidentifier] NULL,
	[UID] [uniqueidentifier] NULL,
	[LastUpdateTime] [datetime] NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_SysUser_Role] PRIMARY KEY CLUSTERED 
(
	[SysUser_RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SysUser_Role] ([SysUser_RoleID], [RoleID], [UID], [LastUpdateTime], [Remark]) VALUES (N'ee2cf99e-7a5c-4d33-a9a8-3bc5d1e103a2', N'c75ba0a0-ced4-48de-9e1d-ef25393968e8', N'11789d48-e880-4335-9192-1ce8586d0538', NULL, NULL)
/****** Object:  View [dbo].[TableInfo]    Script Date: 04/28/2011 12:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TableInfo]
AS
SELECT     OBJECT_NAME(b.rkeyid) AS PrimaryKeyTable, dbo.getName(OBJECT_NAME(b.rkeyid)) AS PrimaryKeyTableName, b.rkey AS PrimaryKeyID,
                          (SELECT     name
                            FROM          sys.syscolumns
                            WHERE      (colid = b.rkey) AND (id = b.rkeyid)) AS PrimaryKeyName, b.fkeyid AS ForeignKeyTableID, OBJECT_NAME(b.fkeyid) AS ForeignKeyTable, 
                      dbo.getName(OBJECT_NAME(b.fkeyid)) AS ForeignKeyTableName, b.fkey AS ForeignKeyID,
                          (SELECT     name
                            FROM          sys.syscolumns AS syscolumns_1
                            WHERE      (colid = b.fkey) AND (id = b.fkeyid)) AS ForeignKeyName
FROM         sys.sysobjects AS a INNER JOIN
                      sys.sysforeignkeys AS b ON a.id = b.constid INNER JOIN
                      sys.sysobjects AS c ON a.parent_obj = c.id
WHERE     (a.xtype = 'F') AND (c.xtype = 'U')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[15] 4[14] 2[45] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 121
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 121
               Right = 376
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 241
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TableInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TableInfo'
GO
/****** Object:  Table [dbo].[HAWB]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HAWB](
	[HID] [uniqueidentifier] NOT NULL,
	[PID] [uniqueidentifier] NULL,
	[UID] [uniqueidentifier] NULL,
	[DID] [uniqueidentifier] NULL,
	[BarCode] [nvarchar](20) NULL,
	[Carrier] [nvarchar](50) NULL,
	[CarrierHAWBID] [uniqueidentifier] NULL,
	[CarrierHAWBBarCode] [nvarchar](50) NULL,
	[SettleType] [int] NOT NULL,
	[ServiceType] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NULL,
	[Status] [int] NOT NULL,
	[Operator] [nvarchar](50) NULL,
	[Remark] [ntext] NULL,
	[ShipperName] [nvarchar](200) NOT NULL,
	[ShipperContactor] [nvarchar](50) NOT NULL,
	[ShipperCountry] [char](2) NOT NULL,
	[ShipperRegion] [char](3) NOT NULL,
	[ShipperAddress] [nvarchar](500) NOT NULL,
	[ShipperZipCode] [varchar](20) NOT NULL,
	[ShipperTel] [varchar](20) NOT NULL,
	[ConsigneeName] [nvarchar](200) NULL,
	[ConsigneeContactor] [nvarchar](50) NOT NULL,
	[ConsigneeCountry] [char](2) NOT NULL,
	[ConsigneeRegion] [char](3) NOT NULL,
	[ConsigneeAddress] [nvarchar](500) NOT NULL,
	[ConsigneeZipCode] [varchar](20) NOT NULL,
	[ConsigneeTel] [varchar](20) NOT NULL,
	[DeliverName] [nvarchar](20) NULL,
	[DeliverContactor] [nvarchar](50) NULL,
	[DeliverCountry] [char](2) NULL,
	[DeliverRegion] [char](3) NULL,
	[DeliverAddress] [nvarchar](500) NULL,
	[DeliverZipCode] [varchar](20) NULL,
	[DeliverTel] [varchar](20) NULL,
	[WeightType] [int] NOT NULL,
	[VolumeWeight] [decimal](8, 2) NULL,
	[TotalVolume] [decimal](8, 2) NOT NULL,
	[TotalWeight] [decimal](8, 2) NOT NULL,
	[Piece] [int] NOT NULL,
	[IsInternational] [bit] NOT NULL,
	[SpecialInstruction] [nvarchar](500) NULL,
	[BillTax] [int] NULL,
	[ProjectResolve] [varbinary](max) NULL,
	[BillWayCode] [nvarchar](20) NULL,
	[CustomsClearanceState] [char](1) NULL,
 CONSTRAINT [PK_HAWB] PRIMARY KEY CLUSTERED 
(
	[HID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'HID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'包编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'BarCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'承运公司' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'Carrier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'承运公司运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'CarrierHAWBID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'承运公司条码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'CarrierHAWBBarCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结算方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'SettleType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ServiceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件公司名称或者发件人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperContactor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件国家码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperCountry'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件地区码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperRegion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperZipCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ShipperTel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件公司名称或者收件个人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeContactor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件国家码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeCountry'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件地区码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeRegion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeZipCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ConsigneeTel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付公司名称或者交付个人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverContactor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付国家码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverCountry'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付地区码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverRegion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付邮编' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverZipCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'交付联系人电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'DeliverTel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计重方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'WeightType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'体积重' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'VolumeWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总体积' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'TotalVolume'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'TotalWeight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'Piece'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是国际运单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'IsInternational'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特别通关指示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'SpecialInstruction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关税支付方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'BillTax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'ProjectResolve'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'BillWayCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通关状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB', @level2type=N'COLUMN',@level2name=N'CustomsClearanceState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWB'
GO
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e1d02', NULL, N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', N'2010', N'中国航空', N'547649c6-63dd-4e5f-a619-4f83419e1d02', N'F2010', 0, 0, CAST(0x00009E980157BACB AS DateTime), CAST(0x00009E980157B9C0 AS DateTime), 0, N'无言', N'周末寄送', N'沈志伟', N'沈志伟', N'CN', N'SHA', N'test address1', N'200435', N'13817011234', N'李宏', N'李宏', N'JP', N'TYO', N'Japan address1', N'201011', N'1201201', N'小汪', N'小汪', N'JP', N'TYO', N'TOKYO ADDRESS1', N'101', N'111', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(12.75 AS Decimal(8, 2)), 10, 1, NULL, 0, NULL, N'B1', N'C')
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'55c90ff4-bb96-4856-8a94-dbea37edd231', NULL, N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'2015', N'吉祥航空', N'55c90ff4-bb96-4856-8a94-dbea37edd231', N'F2015', 0, 0, CAST(0x00009E970157B9C0 AS DateTime), CAST(0x00009E970157B9C0 AS DateTime), 0, N'无言', N'GOOD', N'沈志伟', N'沈志伟', N'CN', N'SHA', N'test address2', N'200436', N'13817011234', N'李宏', N'李宏', N'JP', N'TYO', N'Japan address2', N'201012', N'1201202', N'小汪', N'小汪', N'JP', N'TYO', N'TOKYO ADDRESS2', N'102', N'112', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(13.25 AS Decimal(8, 2)), 10, 1, NULL, 0, 0x3C526F6F743E0D0A20203C50726F6A6563743E0D0A202020203C49643E63613065656335382D656265372D343564332D386334342D3236616262636366303364333C2F49643E0D0A202020203C50726F6A6563744E616D653ECFEEC4BF333C2F50726F6A6563744E616D653E0D0A202020203C43757272656E6379547970653EC3C0D4AA3C2F43757272656E6379547970653E0D0A202020203C5461783E342E3536353C2F5461783E0D0A20203C2F50726F6A6563743E0D0A20203C50726F6A6563743E0D0A202020203C49643E62636231313838642D663261372D343065642D626662612D6433623461626439356538353C2F49643E0D0A202020203C50726F6A6563744E616D653ECFEEC4BF323C2F50726F6A6563744E616D653E0D0A202020203C43757272656E6379547970653E524D423C2F43757272656E6379547970653E0D0A202020203C5461783E3132333132333C2F5461783E0D0A20203C2F50726F6A6563743E0D0A3C2F526F6F743E, N'B1', N'C')
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'55c90ff4-bb96-4856-8a94-dbea37edd332', NULL, N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'2014', N'国际航空', N'55c90ff4-bb96-4856-8a94-dbea37edd332', N'F2014', 0, 0, CAST(0x00009E980157B9C0 AS DateTime), CAST(0x00009E980157B9C0 AS DateTime), 0, N'无言', N'GOOD', N'沈志伟', N'沈志伟', N'CN', N'SHA', N'test address3', N'200437', N'13817011234', N'李宏', N'李宏', N'JP', N'TYO', N'Japan address3', N'201013', N'1201203', N'小汪', N'小汪', N'JP', N'TYO', N'TOKYO ADDRESS3', N'103', N'113', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(13.50 AS Decimal(8, 2)), 10, 1, NULL, 0, NULL, N'B1', N'C')
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'55c90ff4-bb96-4856-8a94-dbea37edd452', N'50f483e6-9020-47f7-a33a-57631378f605', N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'2012', N'春秋航空', N'55c90ff4-bb96-4856-8a94-dbea37edd452', N'F2012', 0, 0, CAST(0x00009E970157B9C0 AS DateTime), CAST(0x00009E970157B9C0 AS DateTime), 1, N'无言', N'节假日寄送', N'沈志伟', N'沈志伟', N'CN', N'SHA', N'test address4', N'200438', N'13817011234', N'李宏', N'李宏', N'JP', N'TYO', N'Japan address4', N'201014', N'1201204', N'小汪', N'小汪', N'JP', N'TYO', N'TOKYO ADDRESS4', N'104', N'114', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(14.70 AS Decimal(8, 2)), 10, 1, NULL, 0, NULL, N'hihi', N'C')
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'55c90ff4-bb96-4856-8a94-dbea37edd675', N'b99f909f-06ab-432f-94a4-c4689e898374', N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'2013', N'上海航空', N'55c90ff4-bb96-4856-8a94-dbea37edd675', N'F2013', 0, 0, CAST(0x00009E980157B9C0 AS DateTime), CAST(0x00009E980157B9C0 AS DateTime), 0, N'无言', N'GOOD', N'沈志伟', N'沈志伟', N'CN', N'SHA', N'test address5', N'200439', N'13817011234', N'李宏', N'李宏', N'JP', N'TYO', N'Japan address5', N'201015', N'1201205', N'小汪', N'小汪', N'JP', N'TYO', N'TOKYO ADDRESS5', N'105', N'115', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(15.20 AS Decimal(8, 2)), 10, 1, NULL, 0, NULL, N'B1', N'C')
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'55c90ff4-bb96-4856-8a94-dbea37edd861', N'b99f909f-06ab-432f-94a4-c4689e898374', N'c361a279-a9e7-4e70-8d02-3d96a4ed5af2', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'2016', N'东方航空', N'55c90ff4-bb96-4856-8a94-dbea37edd861', N'F2016', 0, 0, CAST(0x00009E970157B9C0 AS DateTime), CAST(0x00009E970157B9C0 AS DateTime), 0, N'无言', N'工作日寄送', N'沈志伟', N'沈志伟', N'AL', N'TIA', N'test address6', N'200440', N'13817011234', N'李宏', N'李宏', N'AF', N'KBL', N'Japan address6', N'201016', N'1201206', N'小汪', N'小汪', N'AF', N'KBL', N'TOKYO ADDRESS6', N'106', N'116', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), 10, 1, NULL, 0, NULL, N'gggg', N'C')
INSERT [dbo].[HAWB] ([HID], [PID], [UID], [DID], [BarCode], [Carrier], [CarrierHAWBID], [CarrierHAWBBarCode], [SettleType], [ServiceType], [CreateTime], [UpdateTime], [Status], [Operator], [Remark], [ShipperName], [ShipperContactor], [ShipperCountry], [ShipperRegion], [ShipperAddress], [ShipperZipCode], [ShipperTel], [ConsigneeName], [ConsigneeContactor], [ConsigneeCountry], [ConsigneeRegion], [ConsigneeAddress], [ConsigneeZipCode], [ConsigneeTel], [DeliverName], [DeliverContactor], [DeliverCountry], [DeliverRegion], [DeliverAddress], [DeliverZipCode], [DeliverTel], [WeightType], [VolumeWeight], [TotalVolume], [TotalWeight], [Piece], [IsInternational], [SpecialInstruction], [BillTax], [ProjectResolve], [BillWayCode], [CustomsClearanceState]) VALUES (N'55c90ff4-bb96-4856-8a94-dbea37edde07', N'50f483e6-9020-47f7-a33a-57631378f605', N'fa0ab206-7894-493e-a7f2-7708f22054bf', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', N'2011', N'南方航空', N'55c90ff4-bb96-4856-8a94-dbea37edde07', N'F2011', 0, 0, CAST(0x00009E980157BAD0 AS DateTime), CAST(0x00009E980157B9C0 AS DateTime), 1, N'无言', N'GOOD', N'沈志伟', N'沈志伟', N'CN', N'SHA', N'test address7', N'200441', N'13817011234', N'李宏', N'李宏', N'JP', N'TYO', N'Japan address7', N'201017', N'1201207', N'小汪', N'小汪', N'JP', N'TYO', N'TOKYO ADDRESS7', N'107', N'117', 2, CAST(1.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), CAST(10.00 AS Decimal(8, 2)), 10, 1, NULL, 0, NULL, NULL, N'C')
/****** Object:  UserDefinedFunction [dbo].[getStrvalue]    Script Date: 04/28/2011 12:54:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[getStrvalue](@SID varchar(200))
returns varchar(2000)
as
begin
declare @str varchar(2000)
set @str=''
select @str=@str+','+rtrim(ForeignKeyTable) from TableInfo where PrimaryKeyTable=@SID
select @str=right(@str,len(@str)-1) where @str<>''
return @str
end
GO
/****** Object:  UserDefinedFunction [dbo].[getStrName]    Script Date: 04/28/2011 12:54:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[getStrName](@SID varchar(200))
returns varchar(2000)
as
begin
declare @str varchar(2000)
set @str=''
select @str=@str+','+rtrim(ForeignKeyTableName) from TableInfo where PrimaryKeyTable=@SID
select @str=right(@str,len(@str)-1) where @str<>''
return @str
end
GO
/****** Object:  Table [dbo].[AddressBook]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AddressBook](
	[AID] [uniqueidentifier] NOT NULL,
	[DID] [uniqueidentifier] NULL,
	[UID] [uniqueidentifier] NULL,
	[ReceiveAID] [uniqueidentifier] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ContactorName] [nvarchar](50) NOT NULL,
	[CountryCode] [char](2) NOT NULL,
	[Provience] [nvarchar](50) NOT NULL,
	[RegionCode] [char](3) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Fax] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[PostCode] [nvarchar](50) NOT NULL,
	[AddressType] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[Operator] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ADDRESSBOOK] PRIMARY KEY CLUSTERED 
(
	[AID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'DID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人地址ID(当该地址是交付地址时，应当记录送货地址ID)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'ReceiveAID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址名称(公司名称，或者个人名称)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人真名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'ContactorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家二字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'CountryCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'Provience'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域三字码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'RegionCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=发货地址,1=送货地址,2=交付地址，默认为0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'AddressType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址簿' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AddressBook'
GO
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'9d27ef31-39d5-4f64-aafb-0510e277b800', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, N'421dde99-2bf2-41f3-8ad9-acfb6741bf24', N'KONAMA', N'本多忠胜', N'SH', N'日本', N'OSA', N'日本爱知县中国事务所', NULL, N'123459', N'100445', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'744692c8-f9f6-4d05-ab03-095956708ded', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'索尼', N'三池崇史', N'JP', N'日本', N'JXX', N'日本北海道神宫520番地4', NULL, N'888884', N'200413', 1, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'c8450fd9-4af2-437c-aa85-17ae787c517a', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'MOTO', N'Ken', N'CZ', N'CZECH REPUBLIC', N'OSR', N'Lindley Ave. Juneau, AK', NULL, N'205091', N'02303', 1, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'b4d19721-3ff2-4e46-829f-185793f67599', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, N'ba28e743-cf5b-4628-8508-4009cdcb7c7f', N'Nokia', N'Josn', N'GL', N'GREENLAND', N'GOH', N'82 MARCHINGTON CIRCLE,SCARBOROUGH,ONM1R 3M7', NULL, N'206460', N'02031', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'a3fd0989-aa69-4854-9ff0-1c3a112b46f3', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'Microsoft', N'John', N'AU', N'AUSTRALIA', N'CBR', N'Room 4-201，Yongshengxiaoqu,XX Road, Putuo District,Shanghai,PRC', NULL, N'118823', N'00002', 0, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'cfa5306f-d428-491e-9808-1f07d7ec97e4', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, N'dbe773b6-261e-450b-9199-640db566fa52', N'Nokia', N'Josn', N'GL', N'GREENLAND', N'GOH', N'xinlung education,sino business uk ltd.cedar house, 8 fairfield st.', NULL, N'206469', N'02039', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'13340575-d0c5-41d5-9bbc-2400cefa3f8c', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, N'2baf7ba4-58a5-4092-a711-b9a1a5c8a51b', N'HTC', N'Mary', N'HN', N'HONDURAS', N'SAP', N'900 Broadway SeattIe University SeattIe, WA98122', NULL, N'888469', N'04039', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'6b78d094-c7a9-4b21-a586-2ef5469ac1ed', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, N'cb3235cd-003e-49eb-93af-3c3d54b73ae0', N'HTC', N'Mary', N'HN', N'HONDURAS', N'SAP', N'1919B-4th Street S.W.,Box 227,Calgary, AlbertaCanadaT2S 1W4', NULL, N'888469', N'04039', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'e5c86380-b470-4a05-ad2c-33af10655bdb', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, N'933938d7-1851-4ee6-bb2c-96c66be4010e', N'KONAMA', N'本多忠胜', N'SH', N'日本', N'JXO', N'日本海中公园  24米海底观览隧道', NULL, N'123456', N'100435', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'210f0edf-1f01-403b-b2b7-34097efb97e4', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'麦考林', N'沈杰', N'CN', N'上海', N'SHA', N'上海市杨浦区长阳路2467号铭大创意园内5楼', NULL, N'56886666', N'200435', 0, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'225f3b25-2989-42ca-a04a-34156f40bb7b', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'索尼', N'三池崇史', N'JP', N'日本', N'NGS', N'日本崎玉県入间郡毛吕山町大字毛吕本郷223番地1', NULL, N'888887', N'200412', 1, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'e1387562-0caf-49c1-9e84-345681289cee', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'麦考林', N'沈杰', N'CN', N'上海', N'SHA', N'上海市峨山路91弄98号', NULL, N'56885555', N'200435', 0, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'cbbb2c1d-2414-4050-bd84-372613bfd4a7', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'Google', N'Peter', N'BR', N'BRAZIL', N'REC', N'3900 west century boulevard inglewood', NULL, N'118833', N'00005', 0, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'cb3235cd-003e-49eb-93af-3c3d54b73ae0', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'MOTO', N'Ken', N'CZ', N'CZECH REPUBLIC', N'OSR', N'Sherwood St. Boston, MA', NULL, N'205081', N'02003', 1, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'ba28e743-cf5b-4628-8508-4009cdcb7c7f', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'Baidu', N'Jack', N'CA', N'CANADA', N'YQR', N'SAN FRANCISCO 806 MONTGOMERY STREET', NULL, N'205060', N'02001', 1, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'3591dca1-28d1-4917-9833-4564d9935820', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'Baidu', N'Jack', N'CA', N'CANADA', N'YQR', N'Branson Rd. Kansas City', NULL, N'205062', N'02003', 1, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'46d619ae-4c29-495c-b41f-49d3ebe87bca', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'Google', N'Peter', N'BR', N'BRAZIL', N'REC', N'mONTEREY PARK500 N. GAFIELD AVE', NULL, N'118838', N'00006', 0, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'54283378-6fe9-4553-93ff-50d65d0a6800', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'麦考林', N'沈从文', N'CN', N'上海', N'SHA', N'上海市松江区车墩镇虬长路63号', NULL, N'56882222', N'200123', 0, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'5c107699-e637-48ae-af5b-5477ccba00f4', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'日本株式会社', N'土肥原贤二', N'JP', N'日本', N'KYO', N'日本茨城県那珂市北酒出２００', NULL, N'888880', N'200414', 1, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'd0c1e289-a6ce-4607-9dc2-57bc9647a129', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, N'421dde99-2bf2-41f3-8ad9-acfb6741bf24', N'KONAMA', N'明智光秀', N'SH', N'日本', N'OSA', N'日本白木屋遵义路10号', NULL, N'123450', N'100444', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'b86ed2b0-fd44-4d06-8a30-5e9e62e87f5b', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'Google', N'Peter', N'BR', N'BRAZIL', N'REC', N'BMW Kundenbetreuung.Wenden Sie sich bei Fragen aller Art an unsere Kundenbetreuung.', NULL, N'118829', N'00004', 0, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'dbe773b6-261e-450b-9199-640db566fa52', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'Baidu', N'Jack', N'CA', N'CANADA', N'YQR', N'TRANS SECTION GRIDLINES 1014E,1/F.ALT LOGISTIC CENTRE B,BERTH3 KAWAI', NULL, N'205061', N'02002', 1, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'cc0a1c59-39f3-4aac-b086-64f93a0e490b', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, N'c8450fd9-4af2-437c-aa85-17ae787c517a', N'HTC', N'Mary', N'HN', N'HONDURAS', N'SAP', N'Alberta’Edmonton，Alberta Canada T6G 2E6', NULL, N'888469', N'04039', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'be5c2435-2f8b-4387-97d8-6c0d09d2d3cf', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, N'3591dca1-28d1-4917-9833-4564d9935820', N'Nokia', N'Josn', N'GL', N'GREENLAND', N'GOH', N'xinlung education,122 marylebone high street,london, w1u 5qx', NULL, N'206464', N'02034', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'032894c6-f8a4-4de8-ba33-71aebde08841', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'麦考林', N'沈从文', N'CN', N'上海', N'SHA', N'上海市浦东新区银城路139号(近浦东南路)', NULL, N'56883333', N'200132', 0, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'3bba3511-63a3-4f5d-afc4-71fe257352f7', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'麦考林', N'沈从文', N'CN', N'上海', N'SHA', N'上海市田林路487号宝石园大厦20号楼22层，近莲花路', NULL, N'56881111', N'200435', 0, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'23ff5673-902c-4372-8758-7db8e5fb236b', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, N'421dde99-2bf2-41f3-8ad9-acfb6741bf24', N'KONAMA', N'本多忠胜', N'SH', N'日本', N'NRT', N'日本名古屋', NULL, N'123458', N'100439', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'372ed231-89f6-450b-925a-8dfaec749677', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, N'421dde99-2bf2-41f3-8ad9-acfb6741bf24', N'KONAMA', N'明智光秀', N'SH', N'日本', N'KOB', N'日本九州大学', NULL, N'123452', N'100333', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'927cad33-41ea-4be5-aa88-94948bb26bcf', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, N'421dde99-2bf2-41f3-8ad9-acfb6741bf24', N'KONAMA', N'明智光秀', N'SH', N'日本', N'KOB', N'日本西岗区不老街231号', NULL, N'123451', N'100378', 2, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'933938d7-1851-4ee6-bb2c-96c66be4010e', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'日本株式会社', N'土肥原贤二', N'JP', N'日本', N'HIJ', N'日本爱知县名古屋市西区幅下2－3－2 sunlife(サンライフ)城南5B', NULL, N'888888', N'200412', 1, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'421dde99-2bf2-41f3-8ad9-acfb6741bf24', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'日本株式会社', N'土肥原贤二', N'JP', N'日本', N'KOB', N'日本埼玉市太田窪', NULL, N'888889', N'200413', 1, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'2baf7ba4-58a5-4092-a711-b9a1a5c8a51b', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'MOTO', N'Ken', N'CZ', N'CZECH REPUBLIC', N'OSR', N'leavesden studios po box 3000 leavesden, herts, wd25 7lt', NULL, N'785091', N'02353', 1, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'd0a23d5d-1b32-4358-b69a-ccb317a93aed', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'Microsoft', N'John', N'AU', N'AUSTRALIA', N'CBR', N'46 DADASKAYA STREET RYAZAN CITY RUSSIA', NULL, N'118812', N'00001', 0, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'd615da59-c304-4aef-b6c5-ef304f6a4063', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'麦考林', N'沈杰', N'CN', N'上海', N'SHA', N'上海市银城中路488号', NULL, N'56884444', N'200435', 0, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'1db544fa-715b-4fc1-9017-f47f147a3768', N'93a2ec83-da17-4aa3-b884-8cd5bc7fe4ab', NULL, NULL, N'索尼', N'三池崇史', N'JP', N'日本', N'YOH', N'日本横须贺888番地2', NULL, N'888886', N'200413', 1, CAST(0x00009E980157C032 AS DateTime), CAST(0x00009E980157C032 AS DateTime), N'沈志伟')
INSERT [dbo].[AddressBook] ([AID], [DID], [UID], [ReceiveAID], [Name], [ContactorName], [CountryCode], [Provience], [RegionCode], [Address], [Fax], [Phone], [PostCode], [AddressType], [CreateTime], [UpdateTime], [Operator]) VALUES (N'03f17d23-753f-48d5-8666-f6aa4c1e67da', N'b7c75eac-8549-4dc1-aabe-0471c17f4f9f', NULL, NULL, N'Microsoft', N'John', N'AU', N'AUSTRALIA', N'CBR', N'Room 403,No.37,ShiFan Residential Quarter,FengTai District', NULL, N'118827', N'00003', 0, CAST(0x00009E980157C03B AS DateTime), CAST(0x00009E980157C03B AS DateTime), N'沈志伟')
/****** Object:  View [dbo].[TemplateDesc]    Script Date: 04/28/2011 12:54:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TemplateDesc]
AS
WITH cr AS (SELECT     PrimaryKeyTable, PrimaryKeyTable + ',' + dbo.getStrvalue(PrimaryKeyTable) AS SVALUE, dbo.getName(PrimaryKeyTable) 
                                                 + ',' + dbo.getStrName(PrimaryKeyTable) AS SName, PrimaryKeyName
                           FROM         dbo.TableInfo
                           GROUP BY PrimaryKeyTable, PrimaryKeyName)
    SELECT     cr_1.PrimaryKeyTable, cr_1.SVALUE, t.NameCN, cr_1.SName, cr_1.PrimaryKeyName
     FROM         dbo.TableDesc AS t INNER JOIN
                            cr AS cr_1 ON t.name = cr_1.PrimaryKeyTable
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[4] 2[29] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 91
               Right = 169
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cr_1"
            Begin Extent = 
               Top = 6
               Left = 207
               Bottom = 121
               Right = 369
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TemplateDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TemplateDesc'
GO
/****** Object:  Table [dbo].[HAWBItem]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HAWBItem](
	[ItemID] [uniqueidentifier] NOT NULL,
	[HID] [uniqueidentifier] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Piece] [int] NOT NULL,
	[UnitAmount] [decimal](8, 2) NOT NULL,
	[TotalAmount] [decimal](8, 2) NOT NULL,
	[Remark] [nvarchar](200) NULL,
 CONSTRAINT [PK_HAWBITEM] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货物号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'HID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货物名字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'Piece'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'UnitAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'TotalAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货物描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单货物' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBItem'
GO
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e1876', N'547649c6-63dd-4e5f-a619-4f83419e1d02', N'A', 2, CAST(2.00 AS Decimal(8, 2)), CAST(2.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3241', N'547649c6-63dd-4e5f-a619-4f83419e1d02', N'B', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3242', N'55c90ff4-bb96-4856-8a94-dbea37edde07', N'C', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3243', N'55c90ff4-bb96-4856-8a94-dbea37edd452', N'D', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3244', N'55c90ff4-bb96-4856-8a94-dbea37edd675', N'E', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3245', N'55c90ff4-bb96-4856-8a94-dbea37edd332', N'F', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3246', N'55c90ff4-bb96-4856-8a94-dbea37edd231', N'G', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
INSERT [dbo].[HAWBItem] ([ItemID], [HID], [Name], [Piece], [UnitAmount], [TotalAmount], [Remark]) VALUES (N'547649c6-63dd-4e5f-a619-4f83419e3247', N'55c90ff4-bb96-4856-8a94-dbea37edd861', N'H', 3, CAST(3.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL)
/****** Object:  Table [dbo].[HAWBBox]    Script Date: 04/28/2011 12:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HAWBBox](
	[BoxID] [uniqueidentifier] NOT NULL,
	[HID] [uniqueidentifier] NOT NULL,
	[BoxType] [int] NOT NULL,
	[Weight] [decimal](8, 2) NOT NULL,
	[Length] [decimal](8, 2) NULL,
	[Width] [decimal](8, 2) NULL,
	[Height] [decimal](8, 2) NULL,
	[TransFee] [decimal](8, 2) NULL,
	[TransCurrency] [int] NOT NULL,
	[Piece] [int] NOT NULL,
 CONSTRAINT [PK_HAWBBOX] PRIMARY KEY CLUSTERED 
(
	[BoxID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盒号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'BoxID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'HID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盒子类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'BoxType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'Weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'Length'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分摊运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'TransFee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费货币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'TransCurrency'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox', @level2type=N'COLUMN',@level2name=N'Piece'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运件
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'HAWBBox'
GO
/****** Object:  StoredProcedure [dbo].[BatchUpdateWayBillCode]    Script Date: 04/28/2011 12:54:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[BatchUpdateWayBillCode]
@xmldata varchar(8000),
@waybillcode nvarchar(20)
as
begin
	declare @xmldata_id int
	begin transaction T
	exec sp_xml_preparedocument @xmldata_id output,@xmldata
	if exists (select * from dbo.sysobjects where id=object_id(N'[dbo].[TempTable3]') and OBJECTPROPERTY(id,N'IsUserTable')=1) 
	drop table [dbo].[TempTable3]
	select barcode into tempTable3 from openxml(@xmldata_id,'//Hawb',2) with (barcode nvarchar(20))

	update hawb set BillWayCode=@waybillcode where hawb.BarCode in (select barcode from tempTable3)

	if exists (select * from dbo.sysobjects where id=object_id(N'[dbo].[TempTable3]') and OBJECTPROPERTY(id,N'IsUserTable')=1) 
	drop table [dbo].[TempTable3]
	exec sp_xml_removedocument @xmldata_id

	if @@error <> 0 goto ErrMsg

	set nocount off
	commit transaction T
	select 1 as Result
	return 1

	ErrMsg:
		set nocount off
		rollback transaction T
		select -1 as Result
		return -1
end
GO
/****** Object:  StoredProcedure [dbo].[BatchUpdateHAWBPackageState]    Script Date: 04/28/2011 12:54:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[BatchUpdateHAWBPackageState]
@xmlStr varchar(8000) --封装了运单信息的XML字符串
as
declare @xmldata_id int --句柄
begin
	
	--Transaction Start
	begin transaction T
	--XML Document Prepare
	exec sp_xml_preparedocument @xmldata_id output,@xmlStr
	--Judge Temp Table
	if exists (select * from dbo.sysobjects where id=object_id(N'[dbo].[TempTable2]') and OBJECTPROPERTY(id,N'IsUserTable')=1) 
	drop table [dbo].[TempTable2]
	--Create Temp Table
	select barcode into tempTable2 from openxml(@xmldata_id,'//Hawb',2) with (barcode nvarchar(20))
	--Batch Update
	update hawb set PID=NULL,Status=0 where hawb.BarCode in (select barcode from tempTable2)
	--Delete Temp Table
	if exists (select * from dbo.sysobjects where id=object_id(N'[dbo].[TempTable2]') and OBJECTPROPERTY(id,N'IsUserTable')=1) 
	drop table [dbo].[TempTable2]
	--Delete XML Document
	exec sp_xml_removedocument @xmldata_id
	--Exception Resolve
	if @@error <> 0 goto ErrMsg
	--Transaction Resolve
	set nocount off
	commit transaction T
	select 1 as Result
	return 1

	ErrMsg:
		set nocount off
		rollback transaction T
		select -1 as Result
		return -1
end
GO
/****** Object:  StoredProcedure [dbo].[BatchUpdateCustomsClearanceState]    Script Date: 04/28/2011 12:54:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BatchUpdateCustomsClearanceState]
@xmldata varchar(8000), --IN
@mawbCode nvarchar(50) --总运单编号
as
begin
declare @xmldata_id int --新创建文档的句柄
declare @result int --记录更新成功条数

--其实没有必要用事务，安全起见还是用了
begin transaction T
--参数1：句柄
--参数2：DOM文档的XML源
--参数3：命名空间，直接为空即可
exec sp_xml_preparedocument @xmldata_id output,@xmldata --返回一个句柄，可用于访问 XML 文档的新创建的内部表示形式

--OPENXML调用的第二个参数是一个映射到包含要提取数据的父节点的扩展路径
--第三个参数（2）指明使用以元素为中心的映射
--WITH子句为被解析的数据提供行集合格式
--首先删除临时表
if exists (select * from dbo.sysobjects where id=object_id(N'[dbo].[TempTable]') and OBJECTPROPERTY(id,N'IsUserTable')=1) 
drop table [dbo].[TempTable]

--创建临时表
select barcode,clearstate into tempTable from openxml(@xmldata_id,'//Hawb',2) with (barcode nvarchar(20),clearstate char(1))

--批量更新
update hawb set 
CustomsClearanceState=
(select clearstate from tempTable where hawb.barcode = tempTable.barcode)
where hawb.barcode in (select barcode from openxml(@xmldata_id,'//Hawb',2) with (barcode nvarchar(20)))

--更新总运单导入字段，标志导入成功
update mawb set ImportStatus=1 where mawb.BarCode=@mawbCode
--END

--批量更新结束后将临时表删除
if exists (select * from dbo.sysobjects where id=object_id(N'[dbo].[TempTable]') and OBJECTPROPERTY(id,N'IsUserTable')=1) 
drop table [dbo].[TempTable]

--sp_xml_removedocument调用删除DOM文档资源
exec sp_xml_removedocument @xmldata_id

--异常处理
if @@error <> 0 goto ErrMsg

--事务处理
set nocount off
commit transaction T
select 1 as Result
return 1

ErrMsg:
	set nocount off
	rollback transaction T
	select -1 as Result
	return -1
end
GO
/****** Object:  Default [DF_AppModule_IsLeft]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[AppModule] ADD  CONSTRAINT [DF_AppModule_IsLeft]  DEFAULT ((0)) FOR [IsLeft]
GO
/****** Object:  Default [DF_Role_Status]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  Default [DF_Role_Privilege_PrivilegeDesc]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Role_Privilege] ADD  CONSTRAINT [DF_Role_Privilege_PrivilegeDesc]  DEFAULT ((0)) FOR [PrivilegeDesc]
GO
/****** Object:  ForeignKey [FK_ADDRESSB_REFERENCE_DEPARTME]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[AddressBook]  WITH CHECK ADD  CONSTRAINT [FK_ADDRESSB_REFERENCE_DEPARTME] FOREIGN KEY([DID])
REFERENCES [dbo].[Department] ([DID])
GO
ALTER TABLE [dbo].[AddressBook] CHECK CONSTRAINT [FK_ADDRESSB_REFERENCE_DEPARTME]
GO
/****** Object:  ForeignKey [FK_ADDRESSB_REFERENCE_USER]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[AddressBook]  WITH CHECK ADD  CONSTRAINT [FK_ADDRESSB_REFERENCE_USER] FOREIGN KEY([UID])
REFERENCES [dbo].[User] ([UID])
GO
ALTER TABLE [dbo].[AddressBook] CHECK CONSTRAINT [FK_ADDRESSB_REFERENCE_USER]
GO
/****** Object:  ForeignKey [FK_DEPARTME_REFERENCE_COMPANY]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTME_REFERENCE_COMPANY] FOREIGN KEY([CID])
REFERENCES [dbo].[Company] ([CID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_DEPARTME_REFERENCE_COMPANY]
GO
/****** Object:  ForeignKey [FK_HAWB_REFERENCE_DEPARTME]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[HAWB]  WITH CHECK ADD  CONSTRAINT [FK_HAWB_REFERENCE_DEPARTME] FOREIGN KEY([DID])
REFERENCES [dbo].[Department] ([DID])
GO
ALTER TABLE [dbo].[HAWB] CHECK CONSTRAINT [FK_HAWB_REFERENCE_DEPARTME]
GO
/****** Object:  ForeignKey [FK_HAWB_REFERENCE_PACKAGE]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[HAWB]  WITH CHECK ADD  CONSTRAINT [FK_HAWB_REFERENCE_PACKAGE] FOREIGN KEY([PID])
REFERENCES [dbo].[Package] ([PID])
GO
ALTER TABLE [dbo].[HAWB] CHECK CONSTRAINT [FK_HAWB_REFERENCE_PACKAGE]
GO
/****** Object:  ForeignKey [FK_HAWB_REFERENCE_USER]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[HAWB]  WITH CHECK ADD  CONSTRAINT [FK_HAWB_REFERENCE_USER] FOREIGN KEY([UID])
REFERENCES [dbo].[User] ([UID])
GO
ALTER TABLE [dbo].[HAWB] CHECK CONSTRAINT [FK_HAWB_REFERENCE_USER]
GO
/****** Object:  ForeignKey [FK_HAWBBOX_REFERENCE_HAWB]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[HAWBBox]  WITH CHECK ADD  CONSTRAINT [FK_HAWBBOX_REFERENCE_HAWB] FOREIGN KEY([HID])
REFERENCES [dbo].[HAWB] ([HID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HAWBBox] CHECK CONSTRAINT [FK_HAWBBOX_REFERENCE_HAWB]
GO
/****** Object:  ForeignKey [FK_HAWBITEM_REFERENCE_HAWB]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[HAWBItem]  WITH CHECK ADD  CONSTRAINT [FK_HAWBITEM_REFERENCE_HAWB] FOREIGN KEY([HID])
REFERENCES [dbo].[HAWB] ([HID])
GO
ALTER TABLE [dbo].[HAWBItem] CHECK CONSTRAINT [FK_HAWBITEM_REFERENCE_HAWB]
GO
/****** Object:  ForeignKey [FK_HSPROPER_FK_HSPROP_HSPRODUC]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[HSProperty]  WITH CHECK ADD  CONSTRAINT [FK_HSPROPER_FK_HSPROP_HSPRODUC] FOREIGN KEY([HSID])
REFERENCES [dbo].[HSProduct] ([HSID])
GO
ALTER TABLE [dbo].[HSProperty] CHECK CONSTRAINT [FK_HSPROPER_FK_HSPROP_HSPRODUC]
GO
/****** Object:  ForeignKey [FK_PACKAGE_REFERENCE_MAWB]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Package]  WITH CHECK ADD  CONSTRAINT [FK_PACKAGE_REFERENCE_MAWB] FOREIGN KEY([MID])
REFERENCES [dbo].[MAWB] ([MID])
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [FK_PACKAGE_REFERENCE_MAWB]
GO
/****** Object:  ForeignKey [FK_PARAM_FK_PARAM__TEMPLATE]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Param]  WITH CHECK ADD  CONSTRAINT [FK_PARAM_FK_PARAM__TEMPLATE] FOREIGN KEY([TID])
REFERENCES [dbo].[Template] ([TID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Param] CHECK CONSTRAINT [FK_PARAM_FK_PARAM__TEMPLATE]
GO
/****** Object:  ForeignKey [FK_PRIVILEG_REFERENCE_APPMODUL]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Role_Privilege]  WITH CHECK ADD  CONSTRAINT [FK_PRIVILEG_REFERENCE_APPMODUL] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[AppModule] ([ModuleID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Role_Privilege] CHECK CONSTRAINT [FK_PRIVILEG_REFERENCE_APPMODUL]
GO
/****** Object:  ForeignKey [FK_Role_Privilege_Role]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[Role_Privilege]  WITH CHECK ADD  CONSTRAINT [FK_Role_Privilege_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Role_Privilege] CHECK CONSTRAINT [FK_Role_Privilege_Role]
GO
/****** Object:  ForeignKey [FK_SysUser_Role]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[SysUser]  WITH CHECK ADD  CONSTRAINT [FK_SysUser_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[SysUser] CHECK CONSTRAINT [FK_SysUser_Role]
GO
/****** Object:  ForeignKey [FK_SysUser_Role_Role]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[SysUser_Role]  WITH CHECK ADD  CONSTRAINT [FK_SysUser_Role_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SysUser_Role] CHECK CONSTRAINT [FK_SysUser_Role_Role]
GO
/****** Object:  ForeignKey [FK_SysUser_Role_SysUser]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[SysUser_Role]  WITH CHECK ADD  CONSTRAINT [FK_SysUser_Role_SysUser] FOREIGN KEY([UID])
REFERENCES [dbo].[SysUser] ([UID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SysUser_Role] CHECK CONSTRAINT [FK_SysUser_Role_SysUser]
GO
/****** Object:  ForeignKey [FK_USER_REFERENCE_DEPARTME]    Script Date: 04/28/2011 12:54:30 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_USER_REFERENCE_DEPARTME] FOREIGN KEY([DID])
REFERENCES [dbo].[Department] ([DID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_USER_REFERENCE_DEPARTME]
GO
