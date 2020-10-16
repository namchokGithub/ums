-- =============================================
-- Author:		Namchok Singhachai
-- Create date: 2020-09-14
-- Description:	Searching log by text or date.
-- =============================================
CREATE PROCEDURE ums_Search_log
    @param_dateFirst Date,
    @param_dateEnd Date,
    @param_text NVARCHAR(MAX)
AS
BEGIN
    IF @param_text = '' OR @param_text = null 
		SELECT
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
    WHERE 
			CONVERT(date, [dbo].[Logs].[log_datetime], 121) BETWEEN CONVERT(date, @param_dateFirst, 121) AND CONVERT(date, @param_dateEnd, 121)
    ORDER BY [dbo].[Logs].[log_Id] DESC
	ELSE IF @param_dateFirst = '' OR @param_dateEnd = '' OR @param_dateFirst = null OR @param_dateEnd = null
		SELECT
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
    WHERE 
			[log_message] LIKE '%'+@param_text+'%'
    ORDER BY [dbo].[Logs].[log_Id] DESC
	ELSE 
		SELECT
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
    WHERE 
			[log_message] LIKE '%'+@param_text+'%'
        AND CONVERT(date, [dbo].[Logs].[log_datetime], 121) BETWEEN CONVERT(date, @param_dateFirst, 121) AND CONVERT(date, @param_dateEnd, 121)
    ORDER BY [dbo].[Logs].[log_Id] DESC
END
GO