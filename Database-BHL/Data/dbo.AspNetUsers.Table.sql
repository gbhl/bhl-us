SET IDENTITY_INSERT dbo.AspNetUsers ON

INSERT dbo.AspNetUsers (Id, FirstName, LastName, [Disabled], Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName) VALUES (1, 'System', 'User', 1, 'system@bhl.org', 0, NULL, NULL, NULL, 0, 0, NULL, 0, 0, 'system')

SET IDENTITY_INSERT dbo.AspNetUsers OFF
