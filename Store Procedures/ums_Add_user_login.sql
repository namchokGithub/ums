-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-03
-- Description:	Add log in
-- =============================================
CREATE PROCEDURE ums_Add_user_login
    @param_LoginProvider nvarchar(max), @param_ProviderDisplayName nvarchar(max), @param_ProviderKey nvarchar(max), @param_userId nvarchar(max)
AS
BEGIN
    INSERT INTO [dbo].[UserLogins]
           ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId])
     VALUES
           (@param_LoginProvider, @param_ProviderKey, @param_ProviderDisplayName, @param_userId)
END