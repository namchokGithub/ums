-- =============================================
-- Author:		Wannapa Srijermtong
-- Create date: 2020-09-02
-- Description: Updating name and password
-- =============================================
CREATE PROCEDURE ums_Update_all
   @param_id nvarchar(256) ,
   @param_fname nvarchar(256),
   @param_lname nvarchar(256),
   @param_newpw nvarchar(256)
AS
BEGIN
   UPDATE [dbo].[Account]    
   SET [dbo].[Account].acc_Firstname = @param_fname,     
		[dbo].[Account].acc_Lastname = @param_lname,
		[dbo].[Account].acc_PasswordHash = @param_newpw
   WHERE [dbo].[Account].acc_Id = @param_id
END