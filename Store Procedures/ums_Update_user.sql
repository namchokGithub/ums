-- =============================================
-- Author:		Wannapa
-- Create date: 2020-09-02
-- Description:	For update name and last name
-- =============================================
ALTER procedure ums_Update_user
	@param_Id nvarchar(256),   
	@param_fname nvarchar(256),    
	@param_lname nvarchar(256)   
AS     
BEGIN     
   UPDATE [dbo].[Account]    
   SET [dbo].[Account].acc_Firstname = @param_fname, [dbo].[Account].acc_Lastname = @param_lname  
   WHERE [dbo].[Account].acc_Id = @param_Id;
END