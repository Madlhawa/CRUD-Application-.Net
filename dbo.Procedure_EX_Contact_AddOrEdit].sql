CREATE PROCEDURE [dbo].[EX_Contact_AddOrEdit]
	@mode nvarchar(10),
	@ContactID int,
	@Name nvarchar(50),
	@MobileNumber nvarchar(50),
	@Address nvarchar(250)
AS
	IF @mode = 'add'
	BEGIN
		INSERT INTO EX_Contact(
			Name, MobileNumber, Address
		)
		VALUES(
			@Name,
			@MobileNumber,
			@Address
		)
	END
