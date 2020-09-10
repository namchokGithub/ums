-- =============================================
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
