USE [master]
GO
/****** Object:  Database [TodoDb3]    Script Date: 26/12/2024 11:14:39 am ******/
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 26/12/2024 11:14:39 am ******/
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
/****** Object:  Table [dbo].[Todos]    Script Date: 26/12/2024 11:14:39 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Status] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Users]    Script Date: 26/12/2024 11:14:39 am ******/
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
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Todos_Title]    Script Date: 26/12/2024 11:14:39 am ******/
CREATE NONCLUSTERED INDEX [IX_Todos_Title] ON [dbo].[Todos]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Todos_UserId]    Script Date: 26/12/2024 11:14:39 am ******/
CREATE NONCLUSTERED INDEX [IX_Todos_UserId] ON [dbo].[Todos]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 26/12/2024 11:14:39 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 26/12/2024 11:14:39 am ******/
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
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 26/12/2024 11:14:39 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AuthenticateUser]
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
/****** Object:  StoredProcedure [dbo].[CreateTodo]    Script Date: 26/12/2024 11:14:39 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE   PROCEDURE [dbo].[CreateTodo]
    @Title NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL,
    @Status INT,
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
/****** Object:  StoredProcedure [dbo].[DeleteTodo]    Script Date: 26/12/2024 11:14:39 am ******/
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
/****** Object:  StoredProcedure [dbo].[GetTodoById]    Script Date: 26/12/2024 11:14:39 am ******/
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
/****** Object:  StoredProcedure [dbo].[GetTodosByUserIdWithPaging]    Script Date: 26/12/2024 11:14:39 am ******/
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
    @Status INT = NULL,
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
/****** Object:  StoredProcedure [dbo].[GetTodosWithPaging]    Script Date: 26/12/2024 11:14:39 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the stored procedure
create PROCEDURE [dbo].[GetTodosWithPaging]
    @PageNumber INT,
    @PageSize INT,
    @SearchTerm NVARCHAR(100) = NULL,
    @Priority NVARCHAR(50) = NULL,
    @Status INT = NULL,
    @Star BIT = NULL,
    @IsActive BIT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @CreatedDate DATETIME = NULL,
    @TotalItem INT OUTPUT
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
-- Get the total count of all items for the user
SELECT @TotalItem = COUNT(*)
    FROM Todos t
END

--EXEC GetTodosWithPaging 1, 10, 'edited'
GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 26/12/2024 11:14:39 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[RegisterUser]
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
/****** Object:  StoredProcedure [dbo].[ToggleTodoStar]    Script Date: 26/12/2024 11:14:39 am ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateTodo]    Script Date: 26/12/2024 11:14:39 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[UpdateTodo]
    @Id INT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL,
    @Status INT,
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
