-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-08-28
-- Description:	Update firstname and lastname
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
GO