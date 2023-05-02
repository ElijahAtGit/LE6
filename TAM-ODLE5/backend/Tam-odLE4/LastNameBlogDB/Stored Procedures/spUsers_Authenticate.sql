CREATE PROCEDURE [dbo].[spUsers_Authenticate]
	@username NVARCHAR(16),
	@password NVARCHAR(16)
AS
begin
	set nocount on;

	SELECT [Id], [UserName], [FirstName], [LastName], [Password]
	FROM dbo.Users
	WHERE UserName = @username
	AND Password = @password;

end
