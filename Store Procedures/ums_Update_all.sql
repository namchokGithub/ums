-- =============================================
-- Author:		Wannapa
-- Create date: 2020-09-02
-- Description:	Update name
-- =============================================
ALTER procedure ums_Update_all
   @id nvarchar(256) ,   
   @fname nvarchar(256),    
   @lname nvarchar(256),
   @newpw nvarchar(256)  
AS     
BEGIN     
   UPDATE [dbo].[Account]    
   SET [dbo].[Account].acc_Firstname = @fname,     
		[dbo].[Account].acc_Lastname = @lname,
		[dbo].[Account].acc_PasswordHash = @newpw
   WHERE [dbo].[Account].acc_Id = @id
END