IF TYPE_ID(N'KeyList') IS NULL
	CREATE TYPE dbo.KeyList AS TABLE (
		[Key] int NOT NULL
		PRIMARY KEY ([Key])
	)
GO

IF TYPE_ID(N'PictureList') IS NULL
	CREATE TYPE dbo.PictureList AS TABLE (
		Id int NULL, 
		[Path] nvarchar(250) NULL, 
		[Name] nvarchar(250) NULL, 
		IsRepresentative bit NOT NULL, 
		DoDelete bit NOT NULL
	)
GO

CREATE OR ALTER PROCEDURE dbo.GetApartment
	@id int = null
AS

	SELECT 
		  a.Id,
		  a.[Guid],
		  a.CreatedAt,
		  DeletedAt,
		  OwnerId,
		  OwnerName = ao.[Name],
		  TypeId,
		  StatusId,
		  StatusName = ast.[Name],
		  CityId,
		  CityName = c.[Name],
		  [Address],
		  a.[Name],
		  a.NameEng,
		  Price,
		  MaxAdults,
		  MaxChildren,
		  TotalRooms,
		  BeachDistance
	FROM 
		dbo.Apartment a
		JOIN dbo.ApartmentOwner ao ON ao.Id = a.OwnerId
		JOIN dbo.ApartmentStatus ast ON ast.Id = a.StatusId
		JOIN dbo.City c ON c.Id = a.CityId
	WHERE a.Id = @id
GO

CREATE OR ALTER PROCEDURE dbo.GetApartmentTags
	@apartmentId int
AS
	SELECT 
		t.Id,
		t.[Name]
	FROM 
		dbo.TaggedApartment ta
		JOIN dbo.Tag t ON t.Id = ta.TagId
	WHERE ta.ApartmentId = @apartmentId
	ORDER BY t.[Name]
GO

CREATE OR ALTER PROCEDURE dbo.GetApartmentPictures
	@apartmentId int
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name],
		  [Path],
		  IsRepresentative
	FROM dbo.ApartmentPicture
	WHERE ApartmentId = @apartmentId
	ORDER BY IsRepresentative DESC, [Name] ASC
GO

CREATE OR ALTER PROCEDURE dbo.CreateApartment
	@guid uniqueidentifier,
	@ownerId int,
	@typeId int,
	@statusId int,
	@cityId int,
	@address nvarchar(250),
	@name nvarchar(250),
	@price money,
	@maxAdults int,
	@maxChildren int,
	@totalRooms int,
	@beachDistance int,
	@tags KeyList READONLY,
	@pictures PictureList READONLY
AS

	
	DECLARE @Output KeyList
	DECLARE @ApartmentId int

	INSERT INTO dbo.Apartment(
		[Guid],
		CreatedAt,
		OwnerId,
		TypeId,
		StatusId,
		CityId,
		[Address],
		[Name],
		NameEng,
		Price,
		MaxAdults,
		MaxChildren,
		TotalRooms,
		BeachDistance)
	OUTPUT INSERTED.ID INTO @Output([Key])
    SELECT
		@guid,
		SYSUTCDATETIME(),
		@ownerId,
		@typeId,
		@statusId,
		@cityId,
		@address,
		@name,
		'',
		@price,
		@maxAdults,
		@maxChildren,
		@totalRooms,
		@beachDistance

	SELECT @ApartmentId = [Key]
	FROM @Output

	INSERT INTO dbo.TaggedApartment(
		ApartmentId,
		TagId)
	SELECT
		@ApartmentId,
		[Key]
	FROM @tags

	INSERT INTO dbo.ApartmentPicture(
		ApartmentId,
		[Path],
		[Name],
		IsRepresentative)
	SELECT
		@ApartmentId,
		[Path],
		[Name],
		IsRepresentative
	FROM @pictures

GO

CREATE OR ALTER PROCEDURE dbo.UpdateApartment
	@id int,
	@guid uniqueidentifier,
	@ownerId int,
	@typeId int,
	@statusId int,
	@cityId int,
	@address nvarchar(250),
	@name nvarchar(250),
	@price money,
	@maxAdults int,
	@maxChildren int,
	@totalRooms int,
	@beachDistance int,
	@tags KeyList READONLY,
	@pictures PictureList READONLY
AS

	
	DECLARE @Output KeyList

	UPDATE dbo.Apartment
	SET 
		[Guid] = @guid,
		OwnerId = @ownerId,
		TypeId = @typeId,
		StatusId = @statusId,
		CityId = @cityId,
		[Address] = @address,
		[Name] = @name,
		Price = @price,
		MaxAdults = @maxAdults,
		MaxChildren = @maxChildren,
		TotalRooms = @totalRooms,
		BeachDistance = @beachDistance
	WHERE id = @id

	MERGE dbo.TaggedApartment AS tgt
	USING @tags AS src
	ON (tgt.Id = src.[Key]) 
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (ApartmentId, TagId)
		VALUES (@id, [Key])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	MERGE dbo.ApartmentPicture AS tgt
	USING @pictures AS src
	ON (tgt.Id = src.Id) 
	WHEN MATCHED THEN
		UPDATE SET 
			[Name] = src.[Name],
			IsRepresentative = src.IsRepresentative
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (ApartmentId, [Path], [Name], IsRepresentative)
		VALUES (@id, [Path], [Name], IsRepresentative)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

GO

CREATE OR ALTER PROCEDURE dbo.DeleteApartment
	@id int
AS
	UPDATE dbo.Apartment
	SET DeletedAt = SYSUTCDATETIME()
	WHERE Id = @id
GO

CREATE OR ALTER PROCEDURE dbo.GetApartments
	@statusId int = null,
	@cityId int = null,
	@order int = null
AS
	SELECT 
		  a.Id,
		  a.[Guid],
		  a.CreatedAt,
		  DeletedAt,
		  OwnerId,
		  OwnerName = ao.[Name],
		  TypeId,
		  StatusId,
		  StatusName = ast.[Name],
		  CityId,
		  CityName = c.[Name],
		  [Address],
		  a.[Name],
		  a.NameEng,
		  Price,
		  MaxAdults,
		  MaxChildren,
		  TotalRooms,
		  BeachDistance
	FROM 
		dbo.Apartment a
		JOIN dbo.ApartmentOwner ao ON ao.Id = a.OwnerId
		JOIN dbo.ApartmentStatus ast ON ast.Id = a.StatusId
		JOIN dbo.City c ON c.Id = a.CityId
	WHERE
		(@statusId IS NULL OR @statusId IS NOT NULL AND StatusId = @statusId)
		AND
		(@cityId IS NULL OR @cityId IS NOT NULL AND CityId = @cityId)
		AND
		DeletedAt IS NULL
	ORDER BY 
        CASE
            WHEN @order is null THEN a.Id
            WHEN @order = 1 THEN TotalRooms
            WHEN @order = 2 THEN MaxAdults
            WHEN @order = 3 THEN MaxChildren
            WHEN @order = 4 THEN Price
        END
GO

CREATE OR ALTER PROCEDURE dbo.GetCities
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name]
	FROM dbo.City
	ORDER BY [Name]
GO

CREATE OR ALTER PROCEDURE dbo.GetTags
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name]
	FROM dbo.Tag
	ORDER BY [Name]
GO

CREATE OR ALTER PROCEDURE dbo.GetApartmentOwners
AS
	SELECT 
		  Id,
		  [Guid],
		  [Name]
	FROM dbo.ApartmentOwner
	ORDER BY [Name]
GO

CREATE OR ALTER PROCEDURE SearchApartments
	@rooms int = null,
	@adults int = null,
	@children int = null,
	@destination int = null,
	@order int = null
AS

	SELECT
		a.Id,
		a.[Name],
		StarRating = (
			SELECT avg(Stars) 
			FROM ApartmentReview ar 
			WHERE ar.ApartmentId = a.Id),
		CityName = c.[Name],
		BeachDistance,
		TotalRooms,
		MaxAdults,
		MaxChildren,
		Price,
		RepresentativePicturePath = (
			SELECT TOP 1 ap.[Path]
			FROM dbo.ApartmentPicture ap
			WHERE ap.ApartmentId = a.Id
			ORDER BY ap.IsRepresentative DESC, Id)			
		--TagId = t.Id,
		--TagName = t.[Name]
	FROM 
		dbo.Apartment a
		LEFT JOIN City c ON c.Id = a.CityId
		LEFT JOIN dbo.ApartmentPicture ap ON ap.ApartmentId = a.Id
		--JOIN TaggedApartment ta ON ta.ApartmentId = a.Id
		--JOIN Tag t ON t.Id = ta.TagId
	WHERE 
		(@rooms IS NULL OR @rooms IS NOT NULL AND a.TotalRooms >= @rooms)
		AND 
		(@adults IS NULL OR @adults IS NOT NULL AND a.MaxAdults >= @adults)
		AND 
		(@children IS NULL OR @children IS NOT NULL AND a.MaxChildren >= @children)
		AND 
		(@destination IS NULL OR @destination IS NOT NULL AND a.CityId = @destination)
		AND
		ap.DeletedAt IS NULL
	ORDER BY 
        CASE
            WHEN @order is null THEN a.Id
            WHEN @order = 1 THEN TotalRooms
            WHEN @order = 2 THEN MaxAdults
            WHEN @order = 3 THEN MaxChildren
            WHEN @order = 4 THEN Price
        END
GO

CREATE OR ALTER PROCEDURE dbo.GetPublicApartment
	@id int = null
AS

	SELECT 
		a.Id,
		a.[Name],
		StarRating = (
			SELECT avg(Stars) 
			FROM ApartmentReview ar 
			WHERE ar.ApartmentId = a.Id),
		CityName = c.[Name],
		BeachDistance,
		TotalRooms,
		MaxAdults,
		MaxChildren,
		OwnerName = ao.[Name]
	FROM 
		dbo.Apartment a
		LEFT JOIN dbo.ApartmentOwner ao ON ao.Id = a.OwnerId
		LEFT JOIN dbo.City c ON c.Id = a.CityId
	WHERE a.Id = @id
GO

CREATE OR ALTER PROCEDURE dbo.GetPublicApartmentTags
	@apartmentId int = null
AS

	SELECT 
		t.[Name]
	FROM 
		dbo.Apartment a
		JOIN dbo.TaggedApartment ta ON ta.ApartmentId = a.Id
		JOIN dbo.Tag t ON t.Id = ta.ApartmentId
	WHERE a.Id = @apartmentId
GO

CREATE OR ALTER PROCEDURE dbo.GetPublicApartmentPictures
	@apartmentId int = null
AS

	SELECT 
		ap.Id,
		ap.[Name],
		ap.[Path],
		ap.IsRepresentative
	FROM 
		dbo.Apartment a
		JOIN dbo.ApartmentPicture ap ON ap.ApartmentId = a.Id
	WHERE a.Id = @apartmentId
GO

USE RwaApartmani
GO
/*Bulk select procedures*/
CREATE OR ALTER PROCEDURE LoadApartments
AS
	SELECT * FROM Apartment WHERE Apartment.DeletedAt IS NULL
GO
CREATE OR ALTER PROCEDURE LoadApartmentOwners
AS
	SELECT * FROM ApartmentOwner 
GO
CREATE OR ALTER PROCEDURE LoadApartmentPictures
AS
	SELECT * 
	FROM ApartmentPicture 
	WHERE ApartmentPicture.DeletedAt IS NULL

GO
CREATE OR ALTER PROCEDURE LoadApartmentReservations
AS
	SELECT *
	FROM ApartmentReservation
GO
CREATE OR ALTER PROCEDURE LoadApartmentReviews
AS
	SELECT *
	FROM ApartmentReview
GO
CREATE OR ALTER PROCEDURE LoadApartmentReviewsByApartmentId
@ApartmentId INT
AS
	SELECT *
	FROM ApartmentReview
	WHERE ApartmentId = @ApartmentId
GO
CREATE OR ALTER PROCEDURE LoadApartmentStatus
AS
	SELECT *
	FROM ApartmentStatus
GO
CREATE OR ALTER PROCEDURE LoadCities
AS
	SELECT *
	FROM City
GO
CREATE OR ALTER PROCEDURE LoadTags
AS
	SELECT *
	FROM Tag
GO
CREATE OR ALTER PROCEDURE UpdateApartmentAsReserved
@ApartmentId INT
AS
	UPDATE Apartment
	SET StatusId = 2
	WHERE Id = @ApartmentId
GO
SELECT * FROM ApartmentStatus

GO
CREATE OR ALTER PROCEDURE LoadTaggedApartments
AS
	SELECT *
	FROM TaggedApartment
GO
CREATE OR ALTER PROCEDURE LoadTagTypes
AS
	SELECT *
	FROM TagType
GO
CREATE OR ALTER PROCEDURE LoadUsers
AS
	SELECT *
	FROM AspNetUsers
	WHERE AspNetUsers.DeletedAt IS NULL
GO
/*Id select Procedures*/
CREATE OR ALTER PROCEDURE LoadApartmentById
@Id INT 
AS
	SELECT * FROM Apartment WHERE Apartment.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadApartmentOwnerById
@Id INT
AS
	SELECT * FROM ApartmentOwner WHERE ApartmentOwner.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadApartmentPictureById
@Id INT 
AS
	SELECT * FROM ApartmentPicture WHERE ApartmentPicture.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadApartmentReservationById
@Id INT
AS 
	SELECT * FROM ApartmentReservation WHERE ApartmentReservation.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadApartmentReviewById
@Id INT
AS
	SELECT * FROM ApartmentReview WHERE ApartmentReview.Id = @Id
GO
ALTER OR ALTER PROCEDURE LoadApartmentStatusById
@Id INT
AS
	SELECT * FROM ApartmentStatus WHERE ApartmentStatus.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadCitiyById
@Id INT
AS
	SELECT * FROM City WHERE City.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadTagById
@Id	INT
AS
	SELECT * FROM Tag WHERE Tag.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadTaggedApartmentById
@Id INT
AS
	SELECT * FROM TaggedApartment WHERE TaggedApartment.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadTagTypeById
@Id INT
AS
	SELECT * FROM TagType WHERE TagType.Id = @Id
GO
CREATE OR ALTER PROCEDURE LoadUserById
@Id INT
AS
	SELECT * FROM AspNetUsers WHERE AspNetUsers.Id = @Id
/*Special select procedures*/
GO
CREATE OR ALTER PROCEDURE LoadTagsByApartmentId
@Id INT
AS
	SELECT t.Id, t.Guid, t.CreatedAt, t.Name, t.NameEng, t.TypeId
	FROM Tag as t
	INNER JOIN TaggedApartment as ta ON ta.TagId = t.Id
	WHERE ta.ApartmentId = @Id
GO
CREATE OR ALTER PROCEDURE LoadApartmentPicturesByApartmentId
@Id INT
AS
	SELECT * 
	FROM ApartmentPicture 
	WHERE ApartmentPicture.ApartmentId = @Id
GO
CREATE OR ALTER PROCEDURE LoadApartmentsByOwnerId
@Id INT
AS
	SELECT *
	FROM Apartment
	WHERE Apartment.OwnerId = @Id
/*Delete procedures*/
GO
CREATE OR ALTER PROCEDURE DeleteTag
@Id INT
AS 
	DELETE Tag
	WHERE Tag.Id = @Id
GO
CREATE OR ALTER PROCEDURE DeleteTaggedApartment
@ApartmentId INT,
@TagId INT
AS 
	DELETE TaggedApartment
	WHERE ApartmentId = @ApartmentId
	AND TagId = @TagId
GO
CREATE OR ALTER PROCEDURE DeleteUser
@Id INT
AS 
	UPDATE AspNetUsers
	SET AspNetUsers.DeletedAt = GETDATE()
	WHERE AspNetUsers.Id = @Id
GO
/*Create procedures*/
CREATE OR ALTER PROCEDURE InsertTag
@Guid UNIQUEIDENTIFIER,
@CreatedAt DATETIME,
@TypeId INT,
@Name NVARCHAR(250),
@NameEng NVARCHAR(250)
AS
	INSERT INTO Tag (Guid, CreatedAt, TypeId, Name, NameEng)
	VALUES (@Guid, @CreatedAt, @TypeId, @Name, @NameEng)
GO

CREATE OR ALTER PROCEDURE InsertApartment
@Guid UNIQUEIDENTIFIER,
@CreatedAt DATETIME,
@OwnerId INT,
@StatusId INT,
@CityId INT,
@Address NVARCHAR(250),
@Name NVARCHAR(250),
@NameEng NVARCHAR(250),
@Price MONEY,
@MaxAdults INT,
@MaxChildren INT,
@TotalRooms INT,
@BeachDistance INT
AS
	INSERT INTO Apartment (Guid, CreatedAt, OwnerId, StatusId, CityId, Address, Name, NameEng, Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance, TypeId)
	VALUES(@Guid, @CreatedAt, @OwnerId, @StatusId, @CityId, @Address, @Name, @NameEng, @Price, @MaxAdults, @MaxChildren, @TotalRooms, @BeachDistance, 999)

GO

CREATE OR ALTER PROCEDURE InsertTaggedApartment
@Guid UNIQUEIDENTIFIER,
@ApartmentId INT,
@TagId INT
AS
	INSERT INTO TaggedApartment (Guid, ApartmentId, TagId)
	VALUES(@Guid, @ApartmentId, @TagId)

GO

CREATE OR ALTER PROCEDURE InsertApartmentPicture
@Guid UNIQUEIDENTIFIER,
@CreatedAt DATETIME,
@ApartmentId INT,
@Path NVARCHAR(250),
@Base64Content VARCHAR(MAX),
@Name NVARCHAR(250),
@IsRepresentative BIT
AS
	INSERT INTO ApartmentPicture (Guid, CreatedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
	VALUES (@Guid, @CreatedAt, @ApartmentId, @Path, @Base64Content, @Name, @IsRepresentative)
GO

CREATE OR ALTER PROCEDURE InsertApartmentReview
@Guid UNIQUEIDENTIFIER,
@CreatedAt DATETIME,
@ApartmentId INT,
@UserId INT,
@Details NVARCHAR(1000),
@Stars INT
AS
	INSERT INTO ApartmentReview ( Guid, CreatedAt, ApartmentId, UserId, Details, Stars)
	VALUES (@Guid, @CreatedAt, @ApartmentId, @UserId, @Details, @Stars)

GO
CREATE OR ALTER PROCEDURE LoadApartmentIdByGuid
@Guid UNIQUEIDENTIFIER
AS
	SELECT Apartment.Id
	FROM Apartment 
	WHERE Guid = @Guid
GO
CREATE OR ALTER PROCEDURE DeleteTaggedApartmentByApartmentId
@ApartmentId INT
AS
	DELETE TaggedApartment WHERE ApartmentId = @ApartmentId

GO
CREATE OR ALTER PROCEDURE InsertApartmentReservation
@Guid UNIQUEIDENTIFIER,
@CreatedAt DATETIME,
@ApartmentId INT,
@Details NVARCHAR(1000),
@UserId INT,
@UserName NVARCHAR(250),
@UserEmail NVARCHAR(250),
@UserPhone NCHAR(20),
@UserAddress NVARCHAR(1000)
AS
	INSERT INTO ApartmentReservation (Guid, CreatedAt, ApartmentId, Details, UserId, UserName, UserEmail, UserPhone, UserAddress)
	VALUES (@Guid, @CreatedAt, @ApartmentId, @Details, @UserId, @UserName, @UserEmail, @UserPhone, @UserAddress)
GO
CREATE OR ALTER PROCEDURE DeleteApartmentPicture
@Guid UNIQUEIDENTIFIER
AS
	UPDATE ApartmentPicture
	SET DeletedAt = GETDATE()
	WHERE Guid = @Guid

GO
CREATE OR ALTER PROCEDURE LoadApartmentNames
AS
	SELECT Apartment.NameEng FROM Apartment

GO
CREATE OR ALTER PROCEDURE UpdateApartmentPicture
@Guid UNIQUEIDENTIFIER,
@Name NVARCHAR(250),
@IsRepresentative BIT
AS
	UPDATE ApartmentPicture
	SET Name =  @Name, IsRepresentative = @IsRepresentative
	WHERE Guid = @Guid
GO
CREATE OR ALTER PROCEDURE LoadUserRolesByUserId
@UserId INT
AS
	SELECT * FROM AspNetUserRoles WHERE UserId = @UserId
GO
CREATE OR ALTER PROCEDURE LoadUserRoleByRoleId
@RoleId INT
AS
	SELECT * FROM AspNetRoles WHERE Id = @RoleId
GO
CREATE OR ALTER PROCEDURE InsertUser
@Guid UNIQUEIDENTIFIER,
@CreatedAt DATETIME,
@UserName NVARCHAR(256),
@Email NVARCHAR(256),
@PhoneNumber NVARCHAR(MAX),
@Address NVARCHAR(1000),
@PasswordHash NVARCHAR(MAX)
AS
	INSERT INTO AspNetUsers(Guid, CreatedAt, Email, PasswordHash, PhoneNumber, UserName, Address, EmailConfirmed, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount)
	VALUES(@Guid, @CreatedAt, @Email, @PasswordHash, @PhoneNumber, @UserName, @Address, 1, 1,0,0)
	INSERT INTO AspNetUserRoles (UserId, RoleId)
	VALUES (SCOPE_IDENTITY(), 1)
GO
UPDATE AspNetUsers
SET PasswordHash = 'D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB'
GO
INSERT INTO AspNetRoles (Name) VALUES ('user')
GO
INSERT INTO AspNetRoles (Name) VALUES ('administrator')
GO
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT Id, 1
FROM AspNetUsers
GO
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES (1,2)