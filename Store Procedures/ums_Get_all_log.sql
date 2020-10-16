-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-11
-- Description:	Getting all log top ?(parameter).
-- =============================================
CREATE PROCEDURE ums_Get_all_log
	@param_num int
AS
BEGIN
	SELECT TOP (@param_num)
		[dbo].[Logs].[log_Id]
		, [dbo].[Logs].[log_datetime]
		, CONVERT(VARCHAR(10), [dbo].[Logs].[log_datetime], 111) AS [log_date]
		, CONVERT(VARCHAR(10), CAST([dbo].[Logs].[log_datetime] AS TIME	), 0) AS [log_time]
		, [dbo].[Logs].[log_level]
		, [dbo].[Logs].[log_logger]
		, CONCAT([dbo].[Logs].[log_message], ' ', [dbo].[Logs].[log_exception]) AS [log_message]
		, [dbo].[Logs].[log_exception]
		, [dbo].[Logs].[log_user_identity]
		, [dbo].[Logs].[log_mvc_action]
		, [dbo].[Logs].[log_filename]
		, [dbo].[Logs].[log_linenumber]
	FROM [dbo].[Logs]
	ORDER BY [dbo].[Logs].[log_Id] DESC
END
GO