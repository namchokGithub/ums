var ums_Check_User =  @"-- =============================================
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
// End create stored procedure

var insertRole = @" INSERT INTO [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
                            VALUES (1,'Admin','ADMIN',null), (2,'User','USER',null)";
migrationBuilder.Sql(insertRole);
// End Insert role