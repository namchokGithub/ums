var ums_Add_user_login = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-09-03
                            -- Description:	Adding log in.
                            -- =============================================
                            CREATE PROCEDURE ums_Add_user_login
                                @param_LoginProvider nvarchar(max), @param_ProviderDisplayName nvarchar(max), @param_ProviderKey nvarchar(max), @param_userId nvarchar(max)
                            AS
                            BEGIN
                                INSERT INTO [dbo].[UserLogins]
                                    ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId])
                                VALUES
                                    (@param_LoginProvider, @param_ProviderKey, @param_ProviderDisplayName, @param_userId)
                            END";
var ums_Check_user = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-09-03
                        -- Description:	Checking user if exist return 1.
                        -- =============================================
                        CREATE PROCEDURE ums_Check_user
                            @param_user nvarchar(max), @param_status char(1)
                        AS
                        BEGIN
                            IF EXISTS (
                                        SELECT [dbo].[Account].acc_Id
                                        FROM [dbo].[Account]
                                        WHERE [dbo].[Account].acc_User = @param_user AND [dbo].[Account].acc_IsActive = @param_status)
                            BEGIN
                                RETURN 1;
                            END
                            ELSE
                            BEGIN
                                RETURN 0;
                            END
                        END";
var ums_Delete_user = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-08-31
                        -- Description:	User deactivation.
                        -- =============================================
                        CREATE PROCEDURE ums_Delete_user
                            @param_Id nvarchar(max)
                        AS
                        BEGIN
                            UPDATE [dbo].[Account] SET [dbo].[Account].acc_IsActive = CASE WHEN [dbo].[Account].acc_IsActive = 'Y' THEN 'N' 
                                                                                            WHEN [dbo].[Account].acc_IsActive = 'N' THEN 'Y' END
                            WHERE [dbo].[Account].acc_Id = @param_Id;
                        END
                        GO";
var ums_Get_active_user = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-08-29
                            -- Description:	Getting user for management.
                            -- =============================================
                            CREATE PROCEDURE ums_Get_active_user
                            AS
                            BEGIN
                                SELECT [dbo].[Account].[acc_Id]
                                    , [dbo].[Account].[acc_User]
                                    , [dbo].[Account].[acc_NormalizedUserName]
                                    , [dbo].[Account].[acc_Email]
                                    , [dbo].[Account].[acc_NormalizedEmail]
                                    , [dbo].[Account].[acc_PasswordHash]
                                    , [dbo].[Account].[acc_SecurityStamp]
                                    , [dbo].[Account].[acc_ConcurrencyStamp]
                                    , [dbo].[Account].[acc_Firstname]
                                    , [dbo].[Account].[acc_Lastname]
                                    , [dbo].[Account].[acc_IsActive]
                                    , [dbo].[Roles].[Name] AS acc_Rolename
                                    , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                                FROM [dbo].[Account]
                                    LEFT JOIN [dbo].[UserRoles] ON [dbo].[UserRoles].UserId = [dbo].[Account].acc_Id
                                    LEFT JOIN [dbo].[Roles] ON [dbo].[Roles].Id = [dbo].[UserRoles].RoleId
                                    LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                                WHERE [dbo].[Account].[acc_IsActive] = 'Y';
                            END";
var ums_Get_all_active_user = @"-- =============================================
                                -- Author:		Namchok Singhachai
                                -- Create date: 2020-08-29
                                -- Description:	Getting all active user for management.
                                -- =============================================
                                CREATE PROCEDURE ums_Get_all_active_user
                                AS
                                BEGIN
                                    SELECT [dbo].[Account].[acc_Id]
                                        , [dbo].[Account].[acc_User]
                                        , [dbo].[Account].[acc_NormalizedUserName]
                                        , [dbo].[Account].[acc_Email]
                                        , [dbo].[Account].[acc_NormalizedEmail]
                                        , [dbo].[Account].[acc_PasswordHash]
                                        , [dbo].[Account].[acc_SecurityStamp]
                                        , [dbo].[Account].[acc_ConcurrencyStamp]
                                        , [dbo].[Account].[acc_Firstname]
                                        , [dbo].[Account].[acc_Lastname]
                                        , [dbo].[Account].[acc_IsActive]
                                        , [dbo].[Roles].[Name] AS acc_Rolename
                                        , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                                    FROM [dbo].[Account]
                                        LEFT JOIN [dbo].[UserRoles] ON [dbo].[UserRoles].UserId = [dbo].[Account].acc_Id
                                        LEFT JOIN [dbo].[Roles] ON [dbo].[Roles].Id = [dbo].[UserRoles].RoleId
                                        LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                                    WHERE [dbo].[Account].[acc_IsActive] = 'Y'
                                    ORDER BY [dbo].[Account].[acc_Firstname];
                                END
                                GO";
var ums_Get_all_log = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-09-11
                        -- Description:	Getting all log top ?(parameter).
                        -- =============================================
                        CREATE PROCEDURE ums_Get_all_log
                            @param_num int
                        AS
                        BEGIN
                            SELECT TOP (@param_num)
                                [dbo].[Logs].[log_Id]
                                , [dbo].[Logs].[log_datetime]
                                , CONVERT(VARCHAR(10), [dbo].[Logs].[log_datetime], 111) AS [log_date]
                                , CONVERT(VARCHAR(10), CAST([dbo].[Logs].[log_datetime] AS TIME	), 0) AS [log_time]
                                , [dbo].[Logs].[log_level]
                                , [dbo].[Logs].[log_logger]
                                , CONCAT([dbo].[Logs].[log_message], ' ', [dbo].[Logs].[log_exception]) AS [log_message]
                                , [dbo].[Logs].[log_exception]
                                , [dbo].[Logs].[log_user_identity]
                                , [dbo].[Logs].[log_mvc_action]
                                , [dbo].[Logs].[log_filename]
                                , [dbo].[Logs].[log_linenumber]
                            FROM [dbo].[Logs]
                            ORDER BY [dbo].[Logs].[log_Id] DESC
                        END
                        GO";
var ums_Get_all_user = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-08-29
                        -- Description:	Getting all user for management.
                        -- =============================================
                        CREATE PROCEDURE ums_Get_all_user
                        AS
                        BEGIN
                            SELECT [dbo].[Account].[acc_Id]
                                , [dbo].[Account].[acc_User]
                                , [dbo].[Account].[acc_NormalizedUserName]
                                , [dbo].[Account].[acc_Email]
                                , [dbo].[Account].[acc_NormalizedEmail]
                                , [dbo].[Account].[acc_PasswordHash]
                                , [dbo].[Account].[acc_SecurityStamp]
                                , [dbo].[Account].[acc_ConcurrencyStamp]
                                , [dbo].[Account].[acc_Firstname]
                                , [dbo].[Account].[acc_Lastname]
                                , [dbo].[Account].[acc_IsActive]
                                , [dbo].[Roles].[Name] AS acc_Rolename
                                , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                            FROM [dbo].[Account]
                                LEFT JOIN [dbo].[UserRoles] ON [dbo].[UserRoles].UserId = [dbo].[Account].acc_Id
                                LEFT JOIN [dbo].[Roles] ON [dbo].[Roles].Id = [dbo].[UserRoles].RoleId
                                LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                            ORDER BY [dbo].[Account].[acc_Firstname]
                        END
                        GO";
var ums_Get_status_user = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-09-17
                            -- Description:	 Getting status of user and check if exist.
                            -- =============================================
                            CREATE PROCEDURE ums_Get_status_user
                                @param_user nvarchar(max)
                            AS
                            BEGIN
                                IF EXISTS (
                                            SELECT [dbo].[Account].acc_Id
                                            FROM [dbo].[Account]
                                            WHERE [dbo].[Account].acc_User = @param_user)
                                    BEGIN
                                        IF (SELECT [dbo].[Account].acc_IsActive
                                                FROM [dbo].[Account]
                                                WHERE [dbo].[Account].acc_User = @param_user) = 'Y'
                                            BEGIN
                                                RETURN 1;
                                            END
                                        ELSE IF (SELECT [dbo].[Account].acc_IsActive
                                                FROM [dbo].[Account]
                                                WHERE [dbo].[Account].acc_User = @param_user) = 'N'
                                            BEGIN
                                                RETURN 0;
                                            END
                                        ELSE
                                            BEGIN
                                                RETURN 9;
                                            END	
                                    END
                                ELSE
                                    BEGIN
                                        RETURN 9;
                                    END	
                            END";
var ums_Get_user_by_Id = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-08-29
                            -- Description: Getting Active User by ID.
                            -- =============================================
                            CREATE PROCEDURE ums_Get_user_by_Id
                                @param_Id nvarchar(max)
                            AS
                            BEGIN
                                SELECT [dbo].[Account].[acc_Id]
                                    , [dbo].[Account].[acc_User]
                                    , [dbo].[Account].[acc_NormalizedUserName]
                                    , [dbo].[Account].[acc_Email]
                                    , [dbo].[Account].[acc_NormalizedEmail]
                                    , [dbo].[Account].[acc_PasswordHash]
                                    , [dbo].[Account].[acc_SecurityStamp]
                                    , [dbo].[Account].[acc_ConcurrencyStamp]
                                    , [dbo].[Account].[acc_Firstname]
                                    , [dbo].[Account].[acc_Lastname]
                                    , [dbo].[Account].[acc_IsActive]
                                    , [dbo].[Roles].[Name] AS acc_Rolename
                                    , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                                FROM [dbo].[Account]
                                    LEFT JOIN [dbo].[UserRoles] ON [dbo].[UserRoles].UserId = [dbo].[Account].acc_Id
                                    LEFT JOIN [dbo].[Roles] ON [dbo].[Roles].Id = [dbo].[UserRoles].RoleId
                                    LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                                WHERE [dbo].[Account].[acc_IsActive] = 'Y' AND [dbo].[Account].[acc_Id] = @param_Id;
                            END
                            GO";
var ums_Get_user = @"-- =============================================
                    -- Author:		Wannapa Srijermtong
                    -- Create date: 2020-09-02
                    -- Description:	Getting user for edit profile.
                    -- =============================================
                    CREATE PROCEDURE ums_Get_user
                        @param_Id nvarchar(256)
                    AS
                    BEGIN
                        SELECT [dbo].[Account].acc_Id, [dbo].[Account].acc_Firstname, [dbo].[Account].acc_Lastname , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                        FROM [dbo].[Account]
                            LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                        WHERE [dbo].[Account].acc_Id = @param_Id AND [dbo].[Account].acc_IsActive = 'Y';
                    END";
var ums_Search_log = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-09-14
                        -- Description:	Searching log by text or date.
                        -- =============================================
                        CREATE PROCEDURE ums_Search_log
                            @param_dateFirst Date,
                            @param_dateEnd Date,
                            @param_text NVARCHAR(MAX)
                        AS
                        BEGIN
                            IF @param_text = '' OR @param_text = null 
                                SELECT
                                [dbo].[Logs].[log_Id]
                                    , [dbo].[Logs].[log_datetime]
                                    , CONVERT(VARCHAR(10), [dbo].[Logs].[log_datetime], 111) AS [log_date]
                                    , CONVERT(VARCHAR(10), CAST([dbo].[Logs].[log_datetime] AS TIME	), 0) AS [log_time]
                                    , [dbo].[Logs].[log_level]
                                    , [dbo].[Logs].[log_logger]
                                    , CONCAT([dbo].[Logs].[log_message], ' ', [dbo].[Logs].[log_exception]) AS [log_message]
                                    , [dbo].[Logs].[log_exception]
                                    , [dbo].[Logs].[log_user_identity]
                                    , [dbo].[Logs].[log_mvc_action]
                                    , [dbo].[Logs].[log_filename]
                                    , [dbo].[Logs].[log_linenumber]
                            FROM [dbo].[Logs]
                            WHERE 
                                    CONVERT(date, [dbo].[Logs].[log_datetime], 121) BETWEEN CONVERT(date, @param_dateFirst, 121) AND CONVERT(date, @param_dateEnd, 121)
                            ORDER BY [dbo].[Logs].[log_Id] DESC
                            ELSE IF @param_dateFirst = '' OR @param_dateEnd = '' OR @param_dateFirst = null OR @param_dateEnd = null
                                SELECT
                                [dbo].[Logs].[log_Id]
                                    , [dbo].[Logs].[log_datetime]
                                    , CONVERT(VARCHAR(10), [dbo].[Logs].[log_datetime], 111) AS [log_date]
                                    , CONVERT(VARCHAR(10), CAST([dbo].[Logs].[log_datetime] AS TIME	), 0) AS [log_time]
                                    , [dbo].[Logs].[log_level]
                                    , [dbo].[Logs].[log_logger]
                                    , CONCAT([dbo].[Logs].[log_message], ' ', [dbo].[Logs].[log_exception]) AS [log_message]
                                    , [dbo].[Logs].[log_exception]
                                    , [dbo].[Logs].[log_user_identity]
                                    , [dbo].[Logs].[log_mvc_action]
                                    , [dbo].[Logs].[log_filename]
                                    , [dbo].[Logs].[log_linenumber]
                            FROM [dbo].[Logs]
                            WHERE 
                                    [log_message] LIKE '%'+@param_text+'%'
                            ORDER BY [dbo].[Logs].[log_Id] DESC
                            ELSE 
                                SELECT
                                [dbo].[Logs].[log_Id]
                                    , [dbo].[Logs].[log_datetime]
                                    , CONVERT(VARCHAR(10), [dbo].[Logs].[log_datetime], 111) AS [log_date]
                                    , CONVERT(VARCHAR(10), CAST([dbo].[Logs].[log_datetime] AS TIME	), 0) AS [log_time]
                                    , [dbo].[Logs].[log_level]
                                    , [dbo].[Logs].[log_logger]
                                    , CONCAT([dbo].[Logs].[log_message], ' ', [dbo].[Logs].[log_exception]) AS [log_message]
                                    , [dbo].[Logs].[log_exception]
                                    , [dbo].[Logs].[log_user_identity]
                                    , [dbo].[Logs].[log_mvc_action]
                                    , [dbo].[Logs].[log_filename]
                                    , [dbo].[Logs].[log_linenumber]
                            FROM [dbo].[Logs]
                            WHERE 
                                    [log_message] LIKE '%'+@param_text+'%'
                                AND CONVERT(date, [dbo].[Logs].[log_datetime], 121) BETWEEN CONVERT(date, @param_dateFirst, 121) AND CONVERT(date, @param_dateEnd, 121)
                            ORDER BY [dbo].[Logs].[log_Id] DESC
                        END
                        GO";
var ums_Update_all = @"-- =============================================
                        -- Author:		Wannapa Srijermtong
                        -- Create date: 2020-09-02
                        -- Description: Updating name and password
                        -- =============================================
                        CREATE PROCEDURE ums_Update_all
                        @param_id nvarchar(256) ,
                        @param_fname nvarchar(256),
                        @param_lname nvarchar(256),
                        @param_newpw nvarchar(256)
                        AS
                        BEGIN
                        UPDATE [dbo].[Account]    
                        SET [dbo].[Account].acc_Firstname = @param_fname,     
                                [dbo].[Account].acc_Lastname = @param_lname,
                                [dbo].[Account].acc_PasswordHash = @param_newpw
                        WHERE [dbo].[Account].acc_Id = @param_id
                        END";
var ums_Update_name_user = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-08-28
                            -- Description:	Updating firstname and lastname
                            -- =============================================
                            CREATE PROCEDURE ums_Update_name_user
                                @param_Id nvarchar(256),
                                @param_fname nvarchar(256),
                                @param_lname nvarchar(256)
                            AS
                            IF (SELECT [dbo].[Account].acc_Id FROM [dbo].[Account] WHERE [dbo].[Account].acc_Id = @param_Id) != ''
                                    BEGIN
                                UPDATE [dbo].[Account] SET 
                                        [dbo].[Account].acc_Firstname = @param_fname,
                                        [dbo].[Account].acc_Lastname = @param_lname
                                WHERE [dbo].[Account].acc_Id = @param_Id
                            END
                            GO";
var ums_Update_role_user = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-08-28
                            -- Description:	Updating role user
                            -- =============================================
                            CREATE PROCEDURE ums_Update_role_user
                                @param_Id nvarchar(256),
                                @param_Role nchar(10)
                            AS
                            IF (@param_Role <> '' AND @param_Role <> '0')
                                    BEGIN
                                IF (SELECT [dbo].[UserRoles].UserId
                                FROM [dbo].[UserRoles]
                                WHERE [dbo].[UserRoles].UserId = @param_Id) != ''
                                        BEGIN
                                    UPDATE [dbo].[UserRoles] SET 
                                                    [dbo].[UserRoles].RoleId = @param_Role
                                            WHERE [dbo].[UserRoles].UserId = @param_Id;
                                END
                                        ELSE
                                        BEGIN
                                    INSERT INTO [dbo].[UserRoles]
                                        ([dbo].[UserRoles].UserId, [dbo].[UserRoles].RoleId)
                                    VALUES
                                        (@param_Id, @param_Role);
                                END
                            END
                            GO";
var ums_Update_user = @"-- =============================================
                        -- Author:		Wannapa Srijermtong
                        -- Create date: 2020-09-02
                        -- Description:	For updating name and last name
                        -- =============================================
                        CREATE PROCEDURE ums_Update_user
                        @param_Id nvarchar(256),
                        @param_fname nvarchar(256),
                        @param_lname nvarchar(256)
                        AS
                        BEGIN
                        UPDATE [dbo].[Account]    
                        SET [dbo].[Account].acc_Firstname = @param_fname, [dbo].[Account].acc_Lastname = @param_lname  
                        WHERE [dbo].[Account].acc_Id = @param_Id;
                        END";

migrationBuilder.Sql(ums_Add_user_login);
migrationBuilder.Sql(ums_Check_user);
migrationBuilder.Sql(ums_Delete_user);
migrationBuilder.Sql(ums_Get_active_user);
migrationBuilder.Sql(ums_Get_all_active_user);
migrationBuilder.Sql(ums_Get_all_log);
migrationBuilder.Sql(ums_Get_all_user);
migrationBuilder.Sql(ums_Get_status_user);
migrationBuilder.Sql(ums_Get_user_by_Id);
migrationBuilder.Sql(ums_Get_user);
migrationBuilder.Sql(ums_Search_log);
migrationBuilder.Sql(ums_Update_all);
migrationBuilder.Sql(ums_Update_name_user);
migrationBuilder.Sql(ums_Update_role_user);
migrationBuilder.Sql(ums_Update_user);
// End create stored procedure

var insertRole = @"INSERT INTO [dbo].[Roles] ([Id], [Name], [NormalizedName])
                VALUES (1,'Admin','ADMIN'), (2,'User','USER'), (3,'Manager','MANAGER'), (4,'Support','SUPPORT')";
var insertAdmin = @"INSERT INTO [dbo].[Account] ([acc_Id] ,[acc_User] ,[acc_NormalizedUserName] ,[acc_Email] ,[acc_NormalizedEmail] ,[acc_PasswordHash] ,[acc_SecurityStamp] ,[acc_ConcurrencyStamp], [acc_Firstname], [acc_Lastname] ,[acc_IsActive]) VALUES ('adminidtempfortestsystem2020' , 'usermanagement2020@gmail.com', 'USERMANAGEMENT2020@GMAIL.com' , 'usermanagement2020@gmail.com', 'USERMANAGEMENT2020@GMAIL.com' , 'AQAAAAEAACcQAAAAEKuboZS9XFQZJ2w6n+Iv/h9lNb/SKUH1AtUwrzPleRfBClrIUQsfNYQCChpGx6MoDQ==' ,'OWWI2VTYV564YBQIUK3PI75QL7DA6NPJ' , 'dc294a4b-8d8a-49b0-8966-e23ff87778b0' , 'Namchok' , 'Singhachai' , 'Y')";
var insertRoleAdmin = @"INSERT INTO [dbo].[UserRoles] ([UserId] ,[RoleId])
                VALUES ('adminidtempfortestsystem2020', 1)";
var insertAdminProviderKey = @"INSERT INTO [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId])
                VALUES ('Email' , 'DHrmO6VFIqxU9hzoUBc3uJFKVE3FxmyadminproviderKey', 'Email', 'adminidtempfortestsystem2020')";

migrationBuilder.Sql(insertAdmin);
migrationBuilder.Sql(insertRole);
migrationBuilder.Sql(insertAdminProviderKey);
migrationBuilder.Sql(insertRoleAdmin);

var insertStatus = @"USE [UMS]
                    GO
                    SET ANSI_NULLS ON
                    GO
                    SET QUOTED_IDENTIFIER ON
                    GO
                    CREATE TABLE [dbo].[Status](
                        [stat_Id] [int] IDENTITY(1,1) NOT NULL,
                        [stat_name] [varchar](50) NULL,
                    CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
                    (
                        [stat_Id] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                    ) ON [PRIMARY]
                    GO
                    SET IDENTITY_INSERT [dbo].[Status] ON 

                    INSERT [dbo].[Status] ([stat_Id], [stat_name]) VALUES (1, N'Success')
                    INSERT [dbo].[Status] ([stat_Id], [stat_name]) VALUES (2, N'Error')
                    INSERT [dbo].[Status] ([stat_Id], [stat_name]) VALUES (3, N'Fail')
                    SET IDENTITY_INSERT [dbo].[Status] OFF
                    GO
                    ";
migrationBuilder.Sql(insertStatus);