-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-03
-- Description:	Check user if exist return 1
-- =============================================
ALTER PROCEDURE [dbo].[ums_Check_User]
	@param_user nvarchar(max)
AS
BEGIN
    IF EXISTS (
				SELECT [dbo].[Account].acc_Id 
				FROM [dbo].[Account] 
				WHERE [dbo].[Account].acc_User = @param_user)
    BEGIN
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END
