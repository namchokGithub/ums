-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-03
-- Description:	Check user if exist return 1
-- =============================================
CREATE PROCEDURE [dbo].[ums_Check_User]
	@param_user nvarchar(max)
AS
BEGIN
    IF EXISTS (
				SELECT [dbo].[Account].acc_Id 
				FROM [dbo].[Account] 
				WHERE [dbo].[Account].acc_User = @param_user AND [dbo].[Account].acc_IsActive = 'Y')
    BEGIN
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END
