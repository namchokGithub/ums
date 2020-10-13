-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-17
-- Description:	 Getting status of user and check if exist.
-- =============================================
CREATE PROCEDURE ums_Get_status_user
    @param_user nvarchar(max)
AS
BEGIN
    IF EXISTS (
				SELECT [dbo].[Account].acc_Id
                FROM [dbo].[Account]
                WHERE [dbo].[Account].acc_User = @param_user)
		BEGIN
			IF (SELECT [dbo].[Account].acc_IsActive
					FROM [dbo].[Account]
					WHERE [dbo].[Account].acc_User = @param_user) = 'Y'
				BEGIN
					RETURN 1;
				END
			ELSE IF (SELECT [dbo].[Account].acc_IsActive
					FROM [dbo].[Account]
					WHERE [dbo].[Account].acc_User = @param_user) = 'N'
				BEGIN
					RETURN 0;
				END
			ELSE
				BEGIN
					RETURN 9;
				END	
		END
	ELSE
		BEGIN
			RETURN 9;
		END	
END