create or alter proc LoadApartments
as
	select * from Apartment where Apartment.DeletedAt is null
go
create or alter proc LoadApartmentOwners
as
	select * from ApartmentOwner 
go
create or alter proc LoadApartmentPictures
as
	select * 
	from ApartmentPicture 
	where ApartmentPicture.DeletedAt is null

go
create or alter proc LoadApartmentReservations
as
	select *
	from ApartmentReservation
go
create or alter proc LoadApartmentReviews
as
	select *
	from ApartmentReview
go
create or alter proc LoadApartmentReviewsByApartmentId
@ApartmentId int
as
	select *
	from ApartmentReview
	where ApartmentId = @ApartmentId
go
create or alter proc LoadApartmentStatus
as
	select *
	from ApartmentStatus
go
create or alter proc LoadCities
as
	select *
	from City
go
create or alter proc LoadTags
as
	select *
	from Tag
go
create or alter proc UpdateApartmentAsReserved
@ApartmentId int
as
	update Apartment
	set StatusId = 2
	where Id = @ApartmentId
go
select * from ApartmentStatus

go
create or alter proc LoadTaggedApartments
as
	select *
	from TaggedApartment
go
create or alter proc LoadTagTypes
as
	select *
	from TagType
go
create or alter proc LoadUsers
as
	select *
	from AspNetUsers
	where AspNetUsers.DeletedAt is null
go
create or alter proc LoadApartmentById
@Id int 
as
	select * from Apartment where Apartment.Id = @Id
go
create or alter proc LoadApartmentOwnerById
@Id int
as
	select * from ApartmentOwner where ApartmentOwner.Id = @Id
go
create or alter proc LoadApartmentPictureById
@Id int 
as
	select * from ApartmentPicture where ApartmentPicture.Id = @Id
go
create or alter proc LoadApartmentReservationById
@Id int
as 
	select * from ApartmentReservation where ApartmentReservation.Id = @Id
go
create or alter proc LoadApartmentReviewById
@Id int
as
	select * from ApartmentReview where ApartmentReview.Id = @Id
go
create or alter proc LoadApartmentStatusById
@Id int
as
	select * from ApartmentStatus where ApartmentStatus.Id = @Id
go
create or alter proc LoadCitiyById
@Id int
as
	select * from City where City.Id = @Id
go
create or alter proc LoadTagById
@Id	int
as
	select * from Tag where Tag.Id = @Id
go
create or alter proc LoadTaggedApartmentById
@Id int
as
	select * from TaggedApartment where TaggedApartment.Id = @Id
go
create or alter proc LoadTagTypeById
@Id int
as
	select * from TagType where TagType.Id = @Id
go
create or alter proc LoadUserById
@Id int
as
	select * from AspNetUsers where AspNetUsers.Id = @Id
go
create or alter proc LoadTagsByApartmentId
@Id int
as
	select t.Id, t.Guid, t.CreatedAt, t.Name, t.NameEng, t.TypeId
	from Tag as t
	inner join TaggedApartment as ta on ta.TagId = t.Id
	where ta.ApartmentId = @Id
go
create or alter proc LoadApartmentPicturesByApartmentId
@Id int
as
	select * 
	from ApartmentPicture 
	where ApartmentPicture.ApartmentId = @Id
go
create or alter proc LoadApartmentsByOwnerId
@Id int
as
	select *
	from Apartment
	where Apartment.OwnerId = @Id
go
create or alter proc DeleteApartment
@Id int
as
	update Apartment
	set Apartment.DeletedAt = GETDATE()
	where Apartment.Id = @Id
go
create or alter proc DeleteTag
@Id int
as 
	delete Tag
	where Tag.Id = @Id
go
create or alter proc DeleteTaggedApartment
@ApartmentId int,
@TagId int
as 
	delete TaggedApartment
	where ApartmentId = @ApartmentId
	and TagId = @TagId
go
create or alter proc DeleteUser
@Id int
as 
	update AspNetUsers
	set AspNetUsers.DeletedAt = GETDATE()
	where AspNetUsers.Id = @Id
go
/*create procedures*/
create or alter proc InsertTag
@Guid UNIQUEIDENTIFIER,
@CreatedAt datetime,
@TypeId int,
@Name nvarchar(250),
@NameEng nvarchar(250)
as
	insert into Tag (Guid, CreatedAt, TypeId, Name, NameEng)
	values (@Guid, @CreatedAt, @TypeId, @Name, @NameEng)
go

create or alter proc InsertApartment
@Guid UNIQUEIDENTIFIER,
@CreatedAt datetime,
@OwnerId int,
@StatusId int,
@CityId int,
@Address nvarchar(250),
@Name nvarchar(250),
@NameEng nvarchar(250),
@Price money,
@MaxAdults int,
@MaxChildren int,
@TotalRooms int,
@BeachDistance int
as
	insert into Apartment (Guid, CreatedAt, OwnerId, StatusId, CityId, Address, Name, NameEng, Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance, TypeId)
	values(@Guid, @CreatedAt, @OwnerId, @StatusId, @CityId, @Address, @Name, @NameEng, @Price, @MaxAdults, @MaxChildren, @TotalRooms, @BeachDistance, 999)

go

create or alter proc InsertTaggedApartment
@Guid UNIQUEIDENTIFIER,
@ApartmentId int,
@TagId int
as
	insert into TaggedApartment (Guid, ApartmentId, TagId)
	values(@Guid, @ApartmentId, @TagId)

go

create or alter proc InsertApartmentPicture
@Guid UNIQUEIDENTIFIER,
@CreatedAt datetime,
@ApartmentId int,
@Path nvarchar(250),
@Base64Content varchar(MAX),
@Name nvarchar(250),
@IsRepresentative bit
as
	insert into ApartmentPicture (Guid, CreatedAt, ApartmentId, Path, Base64Content, Name, IsRepresentative)
	values (@Guid, @CreatedAt, @ApartmentId, @Path, @Base64Content, @Name, @IsRepresentative)
go

create or alter proc InsertApartmentReview
@Guid UNIQUEIDENTIFIER,
@CreatedAt datetime,
@ApartmentId int,
@UserId int,
@Details nvarchar(1000),
@Stars int
as
	insert into ApartmentReview ( Guid, CreatedAt, ApartmentId, UserId, Details, Stars)
	values (@Guid, @CreatedAt, @ApartmentId, @UserId, @Details, @Stars)

go
create or alter proc LoadApartmentIdByGuid
@Guid UNIQUEIDENTIFIER
as
	select Apartment.Id
	from Apartment 
	where Guid = @Guid
go
create or alter proc UpdateApartment
@Id int,
@OwnerId int,
@StatusId int,
@CityId int,
@Address nvarchar(250),
@Name nvarchar(250),
@NameEng nvarchar(250),
@Price money,
@MaxAdults int,
@MaxChildren int,
@TotalRooms int,
@BeachDistance int
as
	update Apartment
	set OwnerId = @OwnerId, StatusId = @StatusId, CityId = @CityId, Address = @Address, Name =  @Name, NameEng = @NameEng, Price = @Price, MaxAdults = @MaxAdults, MaxChildren = @MaxChildren, TotalRooms = @TotalRooms, BeachDistance = @BeachDistance
	where Id = @Id
go
create or alter proc DeleteTaggedApartmentByApartmentId
@ApartmentId int
as
	delete TaggedApartment where ApartmentId = @ApartmentId

go
create or alter proc InsertApartmentReservation
@Guid UNIQUEIDENTIFIER,
@CreatedAt datetime,
@ApartmentId int,
@Details nvarchar(1000),
@UserId int,
@UserName nvarchar(250),
@UserEmail nvarchar(250),
@UserPhone NCHAR(20),
@UserAddress nvarchar(1000)
as
	insert into ApartmentReservation (Guid, CreatedAt, ApartmentId, Details, UserId, UserName, UserEmail, UserPhone, UserAddress)
	values (@Guid, @CreatedAt, @ApartmentId, @Details, @UserId, @UserName, @UserEmail, @UserPhone, @UserAddress)
go
create or alter proc DeleteApartmentPicture
@Guid UNIQUEIDENTIFIER
as
	update ApartmentPicture
	set DeletedAt = GETDATE()
	where Guid = @Guid

go
create or alter proc LoadApartmentNames
as
	select Apartment.NameEng from Apartment

go
create or alter proc UpdateApartmentPicture
@Guid UNIQUEIDENTIFIER,
@Name nvarchar(250),
@IsRepresentative bit
as
	update ApartmentPicture
	set Name =  @Name, IsRepresentative = @IsRepresentative
	where Guid = @Guid
go
create or alter proc LoadUserRolesByUserId
@UserId int
as
	select * from AspNetUserRoles where UserId = @UserId
go
create or alter proc LoadUserRoleByRoleId
@RoleId int
as
	select * from AspNetRoles where Id = @RoleId
go
create or alter proc InsertUser
@Guid UNIQUEIDENTIFIER,
@CreatedAt datetime,
@UserName nvarchar(256),
@Email nvarchar(256),
@PhoneNumber nvarchar(MAX),
@Address nvarchar(1000),
@PasswordHash nvarchar(MAX)
as
	insert into AspNetUsers(Guid, CreatedAt, Email, PasswordHash, PhoneNumber, UserName, Address, EmailConfirmed, PhoneNumberConfirmed, LockoutEnabled, AccessFailedCount)
	values(@Guid, @CreatedAt, @Email, @PasswordHash, @PhoneNumber, @UserName, @Address, 1, 1,0,0)
	insert into AspNetUserRoles (UserId, RoleId)
	values (SCOPE_IDENTITY(), 1)
go
update AspNetUsers
set PasswordHash = 'D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB'
go
insert into AspNetRoles (Name) values ('user')
go
insert into AspNetRoles (Name) values ('administrator')
go
insert into AspNetUserRoles (UserId, RoleId)
select Id, 1
from AspNetUsers
go
insert into AspNetUserRoles (UserId, RoleId) values (1,2)