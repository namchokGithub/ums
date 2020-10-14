-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-03
-- Description:	Check user if exist return 1
-- =============================================
CREATE PROCEDURE ums_Check_user
    @param_user nvarchar(max), @param_status char(1)
AS
BEGIN
    IF EXISTS (
				SELECT [dbo].[Account].acc_Id
                FROM [dbo].[Account]
                WHERE [dbo].[Account].acc_User = @param_user AND [dbo].[Account].acc_IsActive = @param_status)
    BEGIN
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END