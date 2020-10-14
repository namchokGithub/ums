-- =============================================
-- Author:		Wannapa Srijermtong
-- Create date: 2020-09-02
-- Description:	Get user for edit profile
-- =============================================
CREATE PROCEDURE ums_Get_user
	@param_Id nvarchar(256)
AS
BEGIN
	SELECT [dbo].[Account].acc_Id, [dbo].[Account].acc_Firstname, [dbo].[Account].acc_Lastname , [dbo].[UserLogins].[ProviderDisplayName] AS acc_TypeAccoutname
	FROM [dbo].[Account]
		LEFT JOIN [dbo].[UserLogins] ON [dbo].[UserLogins].UserId = [dbo].[Account].acc_Id
	WHERE [dbo].[Account].acc_Id = @param_Id AND [dbo].[Account].acc_IsActive = 'Y';
END