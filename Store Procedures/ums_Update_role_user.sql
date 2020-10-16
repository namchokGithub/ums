-- =============================================
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
GO