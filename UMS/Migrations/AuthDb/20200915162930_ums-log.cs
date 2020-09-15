﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UMS.Migrations.AuthDb
{
    public partial class umslog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    acc_Id = table.Column<string>(nullable: false, comment: "User ID"),
                    acc_User = table.Column<string>(maxLength: 256, nullable: true, comment: "Username"),
                    acc_NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true, comment: "Normalized UserName"),
                    acc_Email = table.Column<string>(maxLength: 256, nullable: true, comment: "User email"),
                    acc_NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true, comment: "Normalized user email"),
                    acc_PasswordHash = table.Column<string>(nullable: true, comment: "Password hash"),
                    acc_SecurityStamp = table.Column<string>(nullable: true, comment: "Security Stamp"),
                    acc_ConcurrencyStamp = table.Column<string>(nullable: true, comment: "Concurrency Stamp"),
                    acc_Firstname = table.Column<string>(type: "nvarchar(256)", nullable: true, comment: "Firstname"),
                    acc_Lastname = table.Column<string>(type: "nvarchar(256)", nullable: true, comment: "Lastname"),
                    acc_IsActive = table.Column<string>(type: "char(10)", nullable: false, comment: "Status of account")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.acc_Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    log_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_datetime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date time"),
                    log_level = table.Column<string>(type: "nvarchar(256)", nullable: true, comment: "Level of log"),
                    log_logger = table.Column<string>(type: "nvarchar(256)", nullable: true, comment: "A computer program to keep track of events."),
                    log_message = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    log_exception = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "Exception"),
                    log_user_identity = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    log_mvc_action = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    log_filename = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    log_linenumber = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.log_Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "acc_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "acc_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "acc_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "acc_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Account",
                column: "acc_NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Account",
                column: "acc_NormalizedUserName",
                unique: true,
                filter: "[acc_NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            var ums_Check_User = @"-- =============================================
                                    -- Author: Namchok Singhachai
                                    --Create date: 2020 - 09 - 03
                                    -- Description: Check user if exist return 1
                                    -- =============================================
                                    CREATE PROCEDURE[dbo].[ums_Check_User]

                                        @param_user nvarchar(max)
                                    AS
                                    BEGIN
                                        IF EXISTS(
                                                    SELECT[dbo].[Account].acc_Id
                                                    FROM [dbo].[Account]
                                                    WHERE [dbo].[Account].acc_User = @param_user AND[dbo].[Account].acc_IsActive = 'Y')
                                        BEGIN
                                            RETURN 1;
                                                END
                                                ELSE
                                        BEGIN
                                            RETURN 0;
                                                END
                                            END
                                    ";
            var ums_deleteUser = @"-- =============================================
                        -- Author: Namchok Singhachai
                        --Create date: 2020 - 08 - 31
                        -- Description: Inactive user
                        -- =============================================
                        CREATE PROCEDURE ums_deleteUser

                            @param_Id nvarchar(max)
                        AS
                        BEGIN

                            UPDATE[dbo].[Account] SET[dbo].[Account].acc_IsActive =
                                            CASE WHEN[dbo].[Account].acc_IsActive = 'Y' THEN 'N'

                                                    WHEN[dbo].[Account].acc_IsActive = 'N' THEN 'Y' END
                                                WHERE[dbo].[Account].acc_Id = @param_Id;
                                    END
                                    GO
                        ";
            var ums_get_active_user = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-08-29
                            -- Description:	Get all user for management
                            -- =============================================
                            CREATE PROCEDURE ums_get_active_user
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
                            END
                            ";
            var ums_get_all_active_user = @"-- =============================================
                                -- Author:		Namchok Singhachai
                                -- Create date: 2020-08-29
                                -- Description:	Get all active user for management
                                -- =============================================
                                CREATE PROCEDURE ums_get_all_active_user
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
                                END
                                GO
                                ";
            var ums_get_all_user = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-08-29
                        -- Description:	Get all user for management
                        -- =============================================
                        CREATE PROCEDURE ums_get_all_user
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
                        END
                        GO
                        ";
            var ums_Get_user = @"-- =============================================
                    -- Author:		Wannapa
                    -- Create date: 2020-09-02
                    -- Description:	Get user for edit profile
                    -- =============================================
                    CREATE procedure ums_Get_user
                        @param_Id nvarchar(256)
                    AS
                    BEGIN
                        SELECT [dbo].[Account].acc_Id, [dbo].[Account].acc_Firstname, [dbo].[Account].acc_Lastname , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                        FROM [dbo].[Account]
                        LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                        WHERE [dbo].[Account].acc_Id = @param_Id AND [dbo].[Account].acc_IsActive = 'Y' ;
                    END";
            var ums_getUserById = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-08-29
                        -- Description: Get Active User by ID
                        -- =============================================
                        CREATE PROCEDURE ums_getUserById
                            @param_Id nvarchar(max)
                        AS
                        BEGIN
                            SELECT [dbo].[Account].[acc_Id]
                                , [dbo].[Account].[acc_User]
                                , [dbo].[Account].[acc_Email]
                                , [dbo].[Account].[acc_Firstname]
                                , [dbo].[Account].[acc_Lastname]
                                , [dbo].[Account].[acc_IsActive]
                                , [dbo].[Roles].[Name] as [acc_Rolename]
                                , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
                            FROM [dbo].[Account]
                                LEFT JOIN [dbo].[UserRoles] ON [dbo].[UserRoles].UserId = [dbo].[Account].acc_Id
                                LEFT JOIN [dbo].[Roles] ON [dbo].[Roles].Id = [dbo].[UserRoles].RoleId
                                LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
                            WHERE [dbo].[Account].[acc_IsActive] = 'Y' AND [dbo].[Account].[acc_Id] = @param_Id;
                        END
                        GO
                        ";
            var ums_Update_all = @"-- =============================================
                        -- Author:		Wannapa
                        -- Create date: 2020-09-02
                        -- Description:	Update name
                        -- =============================================
                        CREATE procedure ums_Update_all
                            @id nvarchar(256) ,   
                            @fname nvarchar(256),    
                            @lname nvarchar(256),
                            @newpw nvarchar(256)  
                        AS     
                        BEGIN     
                            UPDATE [dbo].[Account]    
                            SET [dbo].[Account].acc_Firstname = @fname,     
                                [dbo].[Account].acc_Lastname = @lname,
                                [dbo].[Account].acc_PasswordHash = @newpw
                            WHERE [dbo].[Account].acc_Id = @id
                        END";
            var ums_Update_user = @"-- =============================================
                        -- Author:		Wannapa
                        -- Create date: 2020-09-02
                        -- Description:	For update name and last name
                        -- =============================================
                        CREATE procedure ums_Update_user
                            @param_Id nvarchar(256),   
                            @param_fname nvarchar(256),    
                            @param_lname nvarchar(256)   
                        AS     
                        BEGIN     
                            UPDATE [dbo].[Account]    
                            SET [dbo].[Account].acc_Firstname = @param_fname, [dbo].[Account].acc_Lastname = @param_lname  
                            WHERE [dbo].[Account].acc_Id = @param_Id;
                        END";
            var ums_updateRoleUser = @"-- =============================================
                            -- Author:		Namchok Singhachai
                            -- Create date: 2020-08-28
                            -- Description:	Update role user
                            -- =============================================
                            CREATE PROCEDURE ums_updateRoleUser
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
            var ums_updateUser = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-08-28
                        -- Description:	Update firstname and lastname
                        -- =============================================
                        CREATE PROCEDURE ums_updateUser
                            @param_Id nvarchar(256),
                            @param_fname nvarchar(256),
                            @param_lname nvarchar(256)
                        AS
                        IF (SELECT [dbo].[Account].acc_Id
                        FROM [dbo].[Account]
                        WHERE [dbo].[Account].acc_Id = @param_Id) != ''
                                BEGIN
                            UPDATE [dbo].[Account] SET 
                                            [dbo].[Account].acc_Firstname = @param_fname,
                                            [dbo].[Account].acc_Lastname = @param_lname
                                    WHERE [dbo].[Account].acc_Id = @param_Id
                        END
                        GO
                        ";
            var ums_Get_all_log = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-09-11
                        -- Description:	Get all log top ?
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
                        GO
                        ";

            var ums_Search_log = @"-- =============================================
                        -- Author:		Namchok Singhachai
                        -- Create date: 2020-09-14
                        -- Description:	Search log by text or date
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
                            ELSE IF @param_dateFirst = '' OR @param_dateEnd = ''
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
                                    [log_message] LIKE '%'+@param_text+'%' OR [log_message] LIKE @param_text+'%' OR [log_message] LIKE '%'+@param_text
                                    AND CONVERT(date, [dbo].[Logs].[log_datetime], 121) BETWEEN CONVERT(date, @param_dateFirst, 121) AND CONVERT(date, @param_dateEnd, 121)
                                ORDER BY [dbo].[Logs].[log_Id] DESC
                        END
                        GO";

            migrationBuilder.Sql(ums_Check_User);
            migrationBuilder.Sql(ums_deleteUser);
            migrationBuilder.Sql(ums_get_active_user);
            migrationBuilder.Sql(ums_get_all_active_user);
            migrationBuilder.Sql(ums_get_all_user);
            migrationBuilder.Sql(ums_Get_user);
            migrationBuilder.Sql(ums_getUserById);
            migrationBuilder.Sql(ums_Update_all);
            migrationBuilder.Sql(ums_Update_user);
            migrationBuilder.Sql(ums_updateRoleUser);
            migrationBuilder.Sql(ums_updateUser);
            migrationBuilder.Sql(ums_Get_all_log);
            migrationBuilder.Sql(ums_Search_log);
            // End create stored procedure

            var insertRole = @"INSERT INTO [dbo].[Roles] ([Id], [Name], [NormalizedName])
                VALUES (1,'Admin','ADMIN'), (2,'User','USER')";
            var insertAdmin = @"INSERT INTO [dbo].[Account] ([acc_Id] ,[acc_User] ,[acc_NormalizedUserName] ,[acc_Email] ,[acc_NormalizedEmail] ,[acc_PasswordHash] ,[acc_SecurityStamp] ,[acc_ConcurrencyStamp], [acc_Firstname], [acc_Lastname] ,[acc_IsActive]) VALUES ('adminidtempfortestsystem2020' , 'usermanagement2020@gmail.com', 'USERMANAGEMENT2020@GMAIL.com' , 'usermanagement2020@gmail.com', 'USERMANAGEMENT2020@GMAIL.com' , 'AQAAAAEAACcQAAAAEKuboZS9XFQZJ2w6n+Iv/h9lNb/SKUH1AtUwrzPleRfBClrIUQsfNYQCChpGx6MoDQ==' ,'OWWI2VTYV564YBQIUK3PI75QL7DA6NPJ' , 'dc294a4b-8d8a-49b0-8966-e23ff87778b0' , 'Namchok' , 'Singhachai' , 'Y')";
            var insertRoleAdmin = @"INSERT INTO [dbo].[UserRoles] ([UserId] ,[RoleId])
                VALUES ('adminidtempfortestsystem2020', 1)";
            var insertAdminProviderKey = @"INSERT INTO [dbo].[UserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId])
                VALUES ('Email' , 'DHrmO6VFIqxU9hzoUBc3uJFKVE3FxmyadminproviderKey', 'Email', 'adminidtempfortestsystem2020')";
            migrationBuilder.Sql(insertAdmin);
            migrationBuilder.Sql(insertRole);
            migrationBuilder.Sql(insertAdminProviderKey);
            migrationBuilder.Sql(insertRoleAdmin);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
