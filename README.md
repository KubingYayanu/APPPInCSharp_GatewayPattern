# APPPInCSharp_GatewayPattern

無瑕的程式碼 : 物件導向原則、設計模式與C#實踐 Agile Principles, Patterns, and Practices in C#

## #A.k.a. DAO(Data Access Object)

## DB Schema

```sql
USE [QuickMart]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 2019/4/12 下午 10:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[orderId] [int] NULL,
	[quantity] [int] NULL,
	[sku] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2019/4/12 下午 10:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[cusId] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2019/4/12 下午 10:03:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[sku] [nvarchar](50) NULL,
	[price] [int] NULL,
	[name] [nvarchar](50) NULL
) ON [PRIMARY]

GO
```