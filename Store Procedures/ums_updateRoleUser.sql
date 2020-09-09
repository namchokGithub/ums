-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-08-28
-- Description:	Update role user
-- =============================================
CREATE PROCEDURE ums_updateRoleUser 
	@param_Id nvarchar(256), @param_Role nchar(10)
AS
	IF (@param_Role <> '' AND @param_Role <> '0')
		UPDATE [dbo].[UserRoles] SET [dbo].[UserRoles].RoleId = @param_Role WHERE [dbo].[UserRoles].UserId = @param_Id;
GO