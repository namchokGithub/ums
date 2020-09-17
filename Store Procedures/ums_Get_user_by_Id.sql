-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-08-29
-- Description: Get Active User by ID
-- =============================================
CREATE PROCEDURE ums_Get_user_by_Id
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