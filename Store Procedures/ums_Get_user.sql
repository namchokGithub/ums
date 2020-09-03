-- =============================================
-- Author:		Wannapa
-- Create date: 2020-09-02
-- Description:	Get user for edit profile
-- =============================================
ALTER procedure ums_Get_user
	@param_Id nvarchar(256)
AS
BEGIN
	SELECT [dbo].[Account].acc_Id, [dbo].[Account].acc_Firstname, [dbo].[Account].acc_Lastname
	FROM [dbo].[Account]
	WHERE [dbo].[Account].acc_Id = @param_Id AND [dbo].[Account].acc_IsActive = 'Y' ;
END