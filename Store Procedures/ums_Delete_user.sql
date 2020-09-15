-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-08-31
-- Description:	Inactive user 
-- =============================================
CREATE PROCEDURE ums_Delete_user
	@param_Id nvarchar(max)
AS
BEGIN
	UPDATE [dbo].[Account] SET [dbo].[Account].acc_IsActive = CASE WHEN [dbo].[Account].acc_IsActive = 'Y' THEN 'N' 
																	WHEN [dbo].[Account].acc_IsActive = 'N' THEN 'Y' END
	WHERE [dbo].[Account].acc_Id = @param_Id;
END
GO