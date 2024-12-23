USE [master]
GO
/****** Object:  Database [TodoDb3]    Script Date: 21/12/2024 3:01:38 pm ******/
CREATE DATABASE [TodoDb3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TodoDb3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATA\TodoDb3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TodoDb3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATA\TodoDb3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TodoDb3] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TodoDb3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TodoDb3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TodoDb3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TodoDb3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TodoDb3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TodoDb3] SET ARITHABORT OFF 
GO
ALTER DATABASE [TodoDb3] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TodoDb3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TodoDb3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TodoDb3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TodoDb3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TodoDb3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TodoDb3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TodoDb3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TodoDb3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TodoDb3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TodoDb3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TodoDb3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TodoDb3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TodoDb3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TodoDb3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TodoDb3] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TodoDb3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TodoDb3] SET RECOVERY FULL 
GO
ALTER DATABASE [TodoDb3] SET  MULTI_USER 
GO
ALTER DATABASE [TodoDb3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TodoDb3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TodoDb3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TodoDb3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TodoDb3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TodoDb3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TodoDb3', N'ON'
GO
ALTER DATABASE [TodoDb3] SET QUERY_STORE = ON
GO
ALTER DATABASE [TodoDb3] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TodoDb3]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Todos]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Priority] [int] NOT NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Star] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Todos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241218125334_newMigration', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241218131243_newDb', N'8.0.1')
GO
SET IDENTITY_INSERT [dbo].[Todos] ON 

INSERT [dbo].[Todos] ([Id], [Title], [Description], [Status], [Priority], [StartDate], [EndDate], [CreatedDate], [Star], [IsActive], [UserId]) VALUES (1082, N'1', N'', N'Draft', 0, CAST(N'2024-12-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-20T09:29:24.6066667' AS DateTime2), 0, 1, N'efbc854d-71ce-4081-8201-8dc4b35e4030')
INSERT [dbo].[Todos] ([Id], [Title], [Description], [Status], [Priority], [StartDate], [EndDate], [CreatedDate], [Star], [IsActive], [UserId]) VALUES (1083, N'2', N'', N'Draft', 0, CAST(N'2024-12-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-21T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-20T09:29:30.1533333' AS DateTime2), 0, 1, N'efbc854d-71ce-4081-8201-8dc4b35e4030')
INSERT [dbo].[Todos] ([Id], [Title], [Description], [Status], [Priority], [StartDate], [EndDate], [CreatedDate], [Star], [IsActive], [UserId]) VALUES (1084, N'3', N'', N'Draft', 0, CAST(N'2024-12-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-22T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-20T09:29:36.6366667' AS DateTime2), 0, 1, N'efbc854d-71ce-4081-8201-8dc4b35e4030')
INSERT [dbo].[Todos] ([Id], [Title], [Description], [Status], [Priority], [StartDate], [EndDate], [CreatedDate], [Star], [IsActive], [UserId]) VALUES (1085, N'4', N'', N'Draft', 0, CAST(N'2024-12-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-23T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-20T09:29:43.0100000' AS DateTime2), 0, 1, N'efbc854d-71ce-4081-8201-8dc4b35e4030')
SET IDENTITY_INSERT [dbo].[Todos] OFF
GO
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'3a7ad86e-4084-4a4e-8970-03ab0e6a68d2', N'cardi', N'carde@gmail.com', N'AQAAAAIAAYagAAAAEDUCZYpWTDUwxGxSgYj59Sm5ULFutCXIXhZkpf4A2g/20duRyWWfilFkUVbMHlsLbw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'240a853b-af73-4c22-a756-0e2c508eb9aa', N'aaa', N'a@agmail.com', N'AQAAAAIAAYagAAAAEKga8/q8J+e5rb48a/CI3UP06JlWEdbtqUvz6OCmPB23vwf5jXf8y0Bg/8cW6Sj8aw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'e7d5bae8-3da7-4a28-b1b3-2f8c80c3a9d0', N'tha', N't@gmail.com', N'AQAAAAIAAYagAAAAEC7x+YVtsXA4FCkZbUnq77svDaCKYirtKAOauLpYHYAhYHC4lklkLb0sDSiQITulQw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'0f5b6097-49bd-4b46-ae96-3ae25331dace', N'test', N'test@gmail.com', N'AQAAAAIAAYagAAAAEA4IaatRnd9dFYYb9zFvk0+/GEaMnHWMnCW38hjhMdC1XuG9B1G9ljlt51IpDjim+g==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'6492a5b1-2dc5-45ad-b1e8-3f3b89b57f87', N'thanhne', N'thanhne@gmail.com', N'AQAAAAIAAYagAAAAENMFmSjuVHyZsRlSi6I6F25n3U5UhQrErXJhjSmz11Y8hyQqvVBO170r77t0GK1DVA==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'638c598e-21dc-430b-b6f6-411c4964bdf9', N'st1ring', N'use1r@example.com', N'AQAAAAIAAYagAAAAEJs7fZCcPseb6WsFTyL8q/taBxVzz8XsC8nc77vsQB1iesjaewtiuFyl2/czdxPgRw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'44106404-35a2-4598-bc7f-4db07603e781', N'ethan', N'ethan@gmail.com', N'AQAAAAIAAYagAAAAEI0UpKcMeDmWO07tAPfuoaZgKv+eBZ2/QVi5+03Db4gznaQYNDuHyJ6TDTyYSVe1fw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'cb168ee8-e14f-4e6c-9f15-5b07ed92619e', N'lananhh', N'mla@gmail.com', N'AQAAAAIAAYagAAAAEAMotJThgDpj/y7XfEC6VQQtQfwAReHWa1/WRWJ8kfiqtVoGX2YHdjjoeH+WsWCVRQ==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'3ff0e048-d34a-4b76-98f5-5c0211e18e90', N'ccc', N'ccc@gmail.com', N'AQAAAAIAAYagAAAAEFR3A2/J9WLoGo//DbcojDjpsuIUJlzEZOrmId438zhxsQ8KThB4rVUdOL3Rqkxp5w==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'07a4d565-82bb-4b88-8ba1-80ec55f1cb9c', N'q1qqq', N'aa@gmail.com', N'AQAAAAIAAYagAAAAENMPisbD9Cnzdq03SXdOFcLx9+WvsHT5Gr9n0mfxJAcvBvPrAAag9DQtpk0CzLyAUg==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'735c492a-ab9d-4f37-b293-8c1adf3d571f', N'e33', N'e@gmail.com', N'AQAAAAIAAYagAAAAEAjtZyfs76nVYskvLhvgHxwdWHtTCaNRwYj+Pt8iJE+2vbJpVCe9bhXeikcOEm7s7w==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'efbc854d-71ce-4081-8201-8dc4b35e4030', N'string', N'user@example.com', N'AQAAAAIAAYagAAAAECb9STh6aLCDx20iT157Xhr4zmCVF0C0Hc1pmCNZ+A5UFRV8a79wTh2yKsQRoruF0A==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'fa3d6434-84c4-403a-bc0d-a301ef2675d4', N'dany2', N'dany2@gmail.com', N'AQAAAAIAAYagAAAAEEJGaZdFk0tbTuDl/4AxVzsB5wXuB3GKGCTKGXyoa44IS4tlUyYl+ZaZqUjhaPptRw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'47e42400-9c5e-4c89-a6fa-a6271168c79f', N'3333', N'hoang@gmail.com', N'AQAAAAIAAYagAAAAEPOTKNh53w2QSphBm73ikDiiTBRy1AFV2Nojr1di76ATIw4+2ZddduE2mwrNgwFuRg==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'faae7e69-b3ef-4ccc-94f1-be0bb571816f', N'string2', N'user2@example.com', N'AQAAAAIAAYagAAAAENvyX5oSlS87+TQoFqTig4ZKoj+SLxjyz7CwfjUPugmDG8VvGidrwnvcZX8WR4BbXQ==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'aa686b48-de8b-4890-937a-c4769fd1c03c', N'awdawd', N'rrr@gmail.com', N'AQAAAAIAAYagAAAAEEDarG1iX3Elqu0Tl70KO8rorsRPpGoM6/22TCx9dV1xwD4mtzgZrO8YwJLTNOOYYA==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'7577fe12-b5ed-4a07-ad5a-c84208f65b50', N'adn', N'Dan@gmail.com', N'AQAAAAIAAYagAAAAECNc1/PRA91cdH+dqcAHazpX4N569XOeVkdi2BAHek//3IESFz+urDzWkaZbDmvXJw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'd758da1a-8447-4ed6-85cf-ca1dd16e3a05', N'thanh', N'thanh@gmail.com', N'AQAAAAIAAYagAAAAEKKp8r5IAzkbD2n9DpYPvYhBPNu2VQLwx9sSqjGTKFL0cLYKPJcC//HNoEH8RuwJdw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'a16d35ac-6eb5-46ee-b3c2-d25a2cf90072', N'gnoob', N'gnoob@gmail.com', N'AQAAAAIAAYagAAAAEEiBGItL1VndyQ0zg+1HmpNbkGt7ISx9HgrgaQqCl4lgNLnK+QMQqEHIVPM6GOLIMQ==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'd0a2ed89-ff44-4c37-9a1c-e1294f8b0d9b', N'ccmk', N'ccmk@gmail.com', N'AQAAAAIAAYagAAAAEB3EhJk1TeAcw4naQJBoe2lECukrWzadeHltC+b/fUQa6Z3cg0MPE4xmxV9qavOZDw==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'701270b0-154e-4ac1-8c63-e1a5d3d9a4ab', N'admin', N'admin@admin.com', N'AQAAAAIAAYagAAAAENrlIz4IGjoDX2gUdR/lhYVTmoHGjkbZGqYGC8Zzx6YVEqWaojv5w1ezzMB9PUCZ/A==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'6b381b23-7dfe-4cf9-84c8-f267bd61b4e9', N'awdawdad', N'mmm@gmail.com', N'AQAAAAIAAYagAAAAEINKK8sgZ1SILUQYudYnfl2NXl6nefXSTNsvIAa4l4ChYIssAWooTRY+9XzD/N3ipA==')
INSERT [dbo].[Users] ([Id], [UserName], [Email], [Password]) VALUES (N'28bd4ae7-3f6c-4658-884d-f35bbf4ecd72', N'dany', N'dany@gmail.com', N'AQAAAAIAAYagAAAAEEcW+dltAB1+y2ftsB7Kb/J7+FTh36oOOeNqGHLrjhNWAva5ODME18fc8C+8e9sjbg==')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Todos_Title]    Script Date: 21/12/2024 3:01:38 pm ******/
CREATE NONCLUSTERED INDEX [IX_Todos_Title] ON [dbo].[Todos]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Todos_UserId]    Script Date: 21/12/2024 3:01:38 pm ******/
CREATE NONCLUSTERED INDEX [IX_Todos_UserId] ON [dbo].[Todos]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 21/12/2024 3:01:38 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 21/12/2024 3:01:38 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_UserName] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Todos]  WITH CHECK ADD  CONSTRAINT [FK_Todos_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Todos] CHECK CONSTRAINT [FK_Todos_Users_UserId]
GO
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AuthenticateUser]
    @Email NVARCHAR(256),
    @Password NVARCHAR(MAX),
    @UserId UNIQUEIDENTIFIER OUTPUT,
    @UserName NVARCHAR(256) OUTPUT,
    @EmailOut NVARCHAR(256) OUTPUT,
    @HashedPassword NVARCHAR(MAX) OUTPUT, -- Return the hashed password
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Retrieve user information and hashed password
    SELECT 
        @HashedPassword = Password,
        @UserId = Id,
        @UserName = UserName,
        @EmailOut = Email
    FROM Users
    WHERE Email = @Email;

    -- Check if user exists
    IF @HashedPassword IS NULL
    BEGIN
        SET @Result = -1; -- User not found
        RETURN;
    END

    -- User found
    SET @Result = 1; -- Success
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTodo]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[CreateTodo]
    @Title NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL,
    @Status NVARCHAR(20),
    @Priority INT,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @CreatedDate DATETIME2(7),
    @Star BIT,
    @IsActive BIT,
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Todos ([Title], [Description], [Status], [Priority], [StartDate], [EndDate], [CreatedDate], [Star], [IsActive], [UserId])
    VALUES (@Title, @Description, @Status, @Priority, @StartDate, @EndDate, @CreatedDate, @Star, @IsActive, @UserId);
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteTodo]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteTodo]
    @Id INT
AS
BEGIN
   DELETE FROM Todos
   WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[GetTodoById]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[GetTodoById]
	@Id INT
AS
BEGIN
	SELECT * FROM Todos
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetTodosByUserIdWithPaging]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[GetTodosByUserIdWithPaging]
    @UserId UNIQUEIDENTIFIER,
    @PageNumber INT,
    @PageSize INT,
    @SearchTerm NVARCHAR(100) = NULL,
    @Priority NVARCHAR(50) = NULL,
    @Status NVARCHAR(50) = NULL,
    @Star BIT = NULL,
    @IsActive BIT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @CreatedDate DATETIME = NULL,
    @TotalItem INT OUTPUT -- Output parameter for total items
AS
BEGIN
    SET NOCOUNT ON;
    -- Calculate the number of records to skip
    DECLARE @Skip INT = (@PageNumber - 1) * @PageSize;
    -- Get the paginated results filtered by UserId
    SELECT 
        t.Id,
        t.Title,
        t.Description,
        t.Status,
        t.Priority,
        t.CreatedDate,
        t.StartDate,
        t.EndDate,
        t.Star,
        t.IsActive,
        t.UserId
    FROM Todos t
    WHERE 
        t.UserId = @UserId AND
        (@SearchTerm IS NULL OR t.Title LIKE '%' + @SearchTerm + '%') AND
        (@Priority IS NULL OR t.Priority = @Priority) AND
        (@Status IS NULL OR t.Status = @Status) AND 
        (@Star IS NULL OR t.Star = @Star) AND
        (@IsActive IS NULL OR t.IsActive = @IsActive) AND
        (@CreatedDate IS NULL OR t.CreatedDate >= @CreatedDate) AND
        (@StartDate IS NULL OR t.StartDate >= @StartDate) AND
        (@EndDate IS NULL OR t.EndDate <= @EndDate)
    ORDER BY t.CreatedDate
    OFFSET @Skip ROWS
    FETCH NEXT @PageSize ROWS ONLY;
    -- Get the total count for pagination filtered by UserId
    SELECT COUNT(*) AS TotalCount 
    FROM Todos t
    WHERE 
        t.UserId = @UserId AND
        (@SearchTerm IS NULL OR t.Title LIKE '%' + @SearchTerm + '%') AND
        (@Priority IS NULL OR t.Priority = @Priority) AND
        (@Status IS NULL OR t.Status = @Status) AND
        (@Star IS NULL OR t.Star = @Star) AND
        (@IsActive IS NULL OR t.IsActive = @IsActive) AND
        (@CreatedDate IS NULL OR t.CreatedDate >= @CreatedDate) AND
        (@StartDate IS NULL OR t.StartDate >= @StartDate) AND
        (@EndDate IS NULL OR t.EndDate <= @EndDate);
    -- Get the total count of all items for the user
    SELECT @TotalItem = COUNT(*)
    FROM Todos t
    WHERE t.UserId = @UserId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTodosWithPaging]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the stored procedure
CREATE PROCEDURE [dbo].[GetTodosWithPaging]
    @PageNumber INT,
    @PageSize INT,
    @SearchTerm NVARCHAR(100) = NULL,
    @Priority NVARCHAR(50) = NULL,
    @Status NVARCHAR(50) = NULL,
    @Star BIT = NULL,
    @IsActive BIT = NULL,
	@StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
	@CreatedDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate the number of records to skip
    DECLARE @Skip INT = (@PageNumber - 1) * @PageSize;

    -- Get the paginated results
    SELECT 
        t.Id,
        t.Title,
        t.Description,
        t.Status,
        t.Priority,
        t.CreatedDate,
		t.StartDate,
        t.EndDate,
        t.Star,
        t.IsActive,
		t.UserId
    FROM Todos t
    WHERE 
        (@SearchTerm IS NULL OR t.Title LIKE '%' + @SearchTerm + '%') AND
        (@Priority IS NULL OR t.Priority = @Priority) AND
        (@Status IS NULL OR t.Status = @Status) AND 
        (@Star IS NULL OR t.Star = @Star) AND
        (@IsActive IS NULL OR t.IsActive = @IsActive) AND
        (@CreatedDate IS NULL OR t.CreatedDate >= @CreatedDate) AND
        (@EndDate IS NULL OR t.EndDate <= @EndDate) AND
		(@StartDate IS NULL OR t.StartDate <= @StartDate)
    ORDER BY t.CreatedDate
    OFFSET @Skip ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Get the total count for pagination
    SELECT COUNT(*) AS TotalCount 
    FROM Todos t
    WHERE 
        (@SearchTerm IS NULL OR t.Title LIKE '%' + @SearchTerm + '%') AND
        (@Priority IS NULL OR t.Priority = @Priority) AND
        (@Status IS NULL OR t.Status = @Status) AND
        (@Star IS NULL OR t.Star = @Star) AND
        (@IsActive IS NULL OR t.IsActive = @IsActive) AND
        (@CreatedDate IS NULL OR t.CreatedDate >= @CreatedDate) AND
        (@EndDate IS NULL OR t.EndDate <= @EndDate) AND
		(@StartDate IS NULL OR t.StartDate <= @StartDate);
END

--EXEC GetTodosWithPaging 1, 10, 'edited'
GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterUser]
    @UserName NVARCHAR(256),
    @Email NVARCHAR(256),
    @Password NVARCHAR(MAX),
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if email already exists
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        SET @Result = -1; -- Email already exists
        RETURN;
    END

    -- Check if username already exists
    IF EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName)
    BEGIN
        SET @Result = -2; -- Username already exists
        RETURN;
    END

    -- Insert the new user with the hashed password (Id will be auto-generated)
   INSERT INTO Users (Id, UserName, Email, Password)
    VALUES (NEWID(), @UserName, @Email, @Password); 

    SET @Result = 1; -- Success
END
GO
/****** Object:  StoredProcedure [dbo].[ToggleTodoStar]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ToggleTodoStar]
    @Id INT,
    @Result BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the Todo exists
    IF NOT EXISTS (SELECT 1 FROM Todos WHERE Id = @Id)
    BEGIN
        THROW 50000, 'Todo not found.', 1;
    END

    -- Declare a table variable to capture the output
    DECLARE @TempResult TABLE (Star BIT);

    -- Toggle the Star column and capture the new value
    UPDATE Todos
    SET Star = CASE WHEN Star = 1 THEN 0 ELSE 1 END
    OUTPUT INSERTED.Star INTO @TempResult
    WHERE Id = @Id;

    -- Assign the new value to the output parameter
    SELECT @Result = Star FROM @TempResult;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTodo]    Script Date: 21/12/2024 3:01:38 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateTodo]
    @Id INT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL,
    @Status NVARCHAR(20),
    @Priority INT,
    @StartDate DATETIME2(7) = NULL,
    @EndDate DATETIME2(7) = NULL,
    @Star BIT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Todos
    SET Title = @Title,
        [Description] = @Description,
        [Status] = @Status,
        [Priority] = @Priority,
        [StartDate] = @StartDate,
        [EndDate] = @EndDate,
        [Star] = @Star,
        [IsActive] = @IsActive
    WHERE Id = @Id;
END;
GO
USE [master]
GO
ALTER DATABASE [TodoDb3] SET  READ_WRITE 
GO
